using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

#region Variables Publicas (Se ven en la interfaz)
    public Text score;  //Objeto Texto del marcador de croquetas
    public Text scoreToWinText;  //Objeto Texto con el marcador de croquetas para ganar
    public int scoreToWin;  //Entero con las croquetas comidas
    public Text weight;  //Objeto Texto con el peso
    public Slider weightSlider;  //Objeto Slider para sincronizar el slider con el texto del peso
    public float timeToLoseWeight;  //float para el tiempo que tarda en perder peso, aunque al final es semi-random
    public LevelManager levelmanager;  //Objeto LevelManager para gestionar las escenas
    public Text deaths;  //Objeto Texto para el marcador de muertes
    [Tooltip("1: Comer    2: Morir")]
    public AudioClip[] PUGSounds;  //Array de clips de audio con los sonidos de comer [0] y morir [1]
    #endregion

#region Variables Privadas (No se ven en la interfaz, si no los serializas)
    private int weightToLose = 10;
    private int currentScore;
    private float currentWeight=3f;
    private PugController PC;
    private bool firstCroqueta = false;
    private int currentDeaths;
    private AudioSource audiosource;
    private CroquetaBehaviour croquetaBehaviour;
#endregion


    //Este metodo se llama al iniciar el nivel y se suele utilizar para inicializar las variables
    void Start () {
        scoreToWinText.text = scoreToWin.ToString();
        weight.text = currentWeight.ToString();
        weightSlider.value = currentWeight;
        PC = GetComponent<PugController>();
        deaths.text = PlayerPrefs.GetInt("Deaths",0).ToString(); //PlayerPrefs es un tipo de variables que son persistentes

        //Asignamos el audio de la muerte y lo activamos
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = PUGSounds[1];
        audiosource.Play();
    }


    //Llamada una vez cada frame
    void Update () {

        //Comprobamos si hemos ganado
        if (currentScore >= scoreToWin) {
            Win();
        }

        //Comprobamos si hemos perdido
        if (currentWeight >= weightToLose || currentWeight<1) {

            Lose();
        }

        //Vemos si pierde peso
        if (IsTimeToLoseWeight() && firstCroqueta) {
            ChangeWeigth(-1f);
        }
	}
    

    //Metodo cuando perdemos
    public void Lose() {
        PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths", 0) + 1);
        levelmanager.LoadLevel(SceneManager.GetActiveScene().name);

    }


    //Metodo cuando ganamos
    protected void Win() {
        levelmanager.LoadNextLevel();
    }


    //Cuando colisionamos con otro objeto que este triggered
    private void OnTriggerEnter2D(Collider2D collision) {

        //Comprobamos si hemos colisionado con un objeto con el Tag "Croqueta"
        if (collision.gameObject.CompareTag("Croqueta")) {
            //Asignamos el sonido de comer croquetas y lo activamos
            audiosource.clip = PUGSounds[0];
            audiosource.Play();

            firstCroqueta = true;
            currentScore += 1;

            Destroy(collision.gameObject);
            score.text = currentScore.ToString();
            ChangeWeigth(1f);
        }

        
    }


    //Metodo para cambiar el peso del pug que le enviamos cuanto sube o baja
    protected void ChangeWeigth(float change) {

        currentWeight += change;
        weight.text = currentWeight.ToString();
        weightSlider.value = currentWeight;  //Esto para actualizar el slider

        //Si pesa mas de 8kg no puede saltar
        if (currentWeight < 8) {
            PC.jumpSpeed = 350f;
        } else {
            PC.jumpSpeed = 0f;
        }

        /*
         * A partir del < 3  es cuando va ganando velocidad y perdiendo volumen
         * A partir de > 3 es cuando va perdiendo velocidad y ganando volumen
         */

        //Switch con los casos del 1 al 9 con sus vainas
        switch ((int)currentWeight) {

            case 1:
                transform.localScale = new Vector3(4f, 4f);
                PC.speed = 9f;
                break;
            case 2:
                transform.localScale = new Vector3(7f, 7f);
                PC.speed = 7f;
                break;
            case 3:
                transform.localScale = new Vector3(10f, 10f);
                PC.speed = 5f;
                break;
            case 4:
                transform.localScale = new Vector3(12f, 12f);
                PC.speed = 4.5f;
                break;
            case 5:
                transform.localScale = new Vector3(14f, 14f);
                PC.speed = 4f;
                break;
            case 6:
                transform.localScale = new Vector3(16f, 16f);
                PC.speed = 3.5f;
                break;
            case 7:
                transform.localScale = new Vector3(18f, 18f);
                PC.speed = 3f;
                break;
            case 8:
                transform.localScale = new Vector3(20f, 20f);
                PC.speed = 2.5f;
                break;
            case 9:
                transform.localScale = new Vector3(22f, 22f);
                PC.speed = 2.5f;
                break;

            default:
                break;
        }
        
    }


    //Metodo que nos dice cuando perdemos peso, devolviendonoslo como bool
    protected bool IsTimeToLoseWeight() {
        //Mira esto es un copypaste que flipas pero es el sistema mas facil para hacer un spawner

        float meanLoseDelay = timeToLoseWeight;
        float losePerSecond = 1 / meanLoseDelay;

        if (Time.deltaTime > meanLoseDelay) {
            Debug.LogWarning("Lose Weight rate capped");
        }

        float threshold = losePerSecond * Time.deltaTime / 5;

        if (Random.value < threshold) {
            return true;
        } else {
            return false;
        }
    }
}
