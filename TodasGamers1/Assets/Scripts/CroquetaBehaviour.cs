using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CroquetaBehaviour : MonoBehaviour {


    private ParticleSystem ps;

	void Start () {


    }
	

	void Update () {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {
            ps = GetComponentInChildren<ParticleSystem>();
            Vector3 psPos = transform.position;
            ps.transform.position = new Vector3(psPos.x, psPos.y, -1f);
            ps.Play();
            Debug.Log(ps.name);
        }

        
    }


}
