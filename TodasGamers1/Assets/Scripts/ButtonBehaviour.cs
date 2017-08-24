using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonBehaviour : MonoBehaviour {

    public AudioClip[] audioClick;
    private AudioSource audiosource;

    private void Start() {
        audiosource = GetComponent<AudioSource>();
    }


    private void OnMouseDown() {
        audiosource.clip = audioClick[0];
        audiosource.Play();
    }

    void OnMouseEnter() {
        audiosource.clip = audioClick[1];
        audiosource.Play();
    }






}