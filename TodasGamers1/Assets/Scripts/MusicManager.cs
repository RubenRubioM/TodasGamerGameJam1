using UnityEngine;

public class MusicManager : MonoBehaviour {

    private AudioSource audiosource;
    static bool created = false;  //Variable estatica para que no se destruya al volver a cargar un nivel

    private void Awake() {
        //Si no se ha creado aun un music manager lo mete en DontDestroyOnLoad, si no, lo rompe
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }


    void Start () {
        created = true;
        audiosource = GetComponent<AudioSource>();
        audiosource.loop = true;
        audiosource.Play();


    }

}
