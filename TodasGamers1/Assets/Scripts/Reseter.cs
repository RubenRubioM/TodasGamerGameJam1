using UnityEngine;

public class Reseter : MonoBehaviour {
    
    //Un script que simplemente resetea el numero de muertes cuando entras al menu
	void Start () {
        PlayerPrefs.SetInt("Deaths", 0);
	}
	
}
