using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text score;
    public Text scoreToWinText;
    public int scoreToWin;

    private int currentScore;

    void Start () {
        scoreToWinText.text = scoreToWin.ToString();
	}


    void Update () {

        if (currentScore == scoreToWin) {
            Debug.Log("HAS GANAO");
        }

	}


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Croqueta")) {
            currentScore += 1;
            score.text = currentScore.ToString();
            Destroy(collision.gameObject);
        }
    }
}
