using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Deaths", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
