using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CroquetaBehaviour : MonoBehaviour {

    //Creo que nada de esta clase funciona pero bueno se intento meter particulas pero no se pudo
    private ParticleSystem ps;

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
