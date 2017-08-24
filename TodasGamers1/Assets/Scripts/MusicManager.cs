using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private AudioSource audiosource;
    static bool created = false;
    private void Awake() {
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
