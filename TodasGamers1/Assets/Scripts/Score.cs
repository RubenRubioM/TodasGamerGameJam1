using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

#region Variables
    public Text score;
    public Text scoreToWinText;
    public int scoreToWin;
    public Text weight;
    public Slider weightSlider;
    public float timeToLoseWeight;
    public LevelManager levelmanager;
    public Text deaths;
    [Tooltip("1: Comer    2: Morir")]
    public AudioClip[] PUGSounds;

    private int weightToLose = 10;
    private int currentScore;
    private float currentWeight=3f;
    private PugController PC;
    private bool firstCroqueta = false;
    private int currentDeaths;
    private AudioSource audiosource;
    private CroquetaBehaviour croquetaBehaviour;
    #endregion

    private void Awake() {
        
    }

    void Start () {
        scoreToWinText.text = scoreToWin.ToString();
        weight.text = currentWeight.ToString();
        weightSlider.value = currentWeight;
        PC = GetComponent<PugController>();
        deaths.text = PlayerPrefs.GetInt("Deaths",0).ToString();

        audiosource = GetComponent<AudioSource>();
        audiosource.clip = PUGSounds[1];
        audiosource.Play();
    }


    void Update () {

        if (currentScore >= scoreToWin) {
            Win();
        }

        if (currentWeight >= weightToLose || currentWeight<1) {

            Lose();
        }

        if (IsTimeToLoseWeight() && firstCroqueta) {
            ChangeWeigth(-1f);
        }
	}
    

    public void Lose() {
        PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths", 0) + 1);
        levelmanager.LoadLevel(SceneManager.GetActiveScene().name);

    }


    protected void Win() {
        levelmanager.LoadNextLevel();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Croqueta")) {
            audiosource.clip = PUGSounds[0];
            audiosource.Play();

            firstCroqueta = true;
            currentScore += 1;

            Destroy(collision.gameObject);
            score.text = currentScore.ToString();
            ChangeWeigth(1f);
        }

        
    }


    protected void ChangeWeigth(float change) {

        currentWeight += change;
        weight.text = currentWeight.ToString();
        weightSlider.value = currentWeight;

        if (currentWeight < 8) {
            PC.jumpSpeed = 350f;
        } else {
            PC.jumpSpeed = 0f;
        }

        /*
         * A partir del < 3  es cuando va ganando velocidad y perdiendo volumen
         * A partir de > 3 es cuando va perdiendo velocidad y ganando volumen
         */
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


    protected bool IsTimeToLoseWeight() {

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
