using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {



    public void LoadLevel(string n) {


        SceneManager.LoadScene(n);
    }


    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitApp() {

        Application.Quit();
    }

    public void OpenURL(string s) {
        Application.OpenURL(s);
    }



}