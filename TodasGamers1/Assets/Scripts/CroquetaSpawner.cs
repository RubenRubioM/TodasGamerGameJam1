using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroquetaSpawner : MonoBehaviour {

    public float minTimeBetweenCroqueta;
    public float maxTimeBetweenCroqueta;
    public GameObject croqueta;

	void Start () {
		
	}
	

	void Update () {

        if (IsTimeToSpawn()) {
            SpawnCroqueta();
        }
    }

    protected void SpawnCroqueta() {
        Vector3 spawnPoint = new Vector3(Random.Range(-7f, 7f), 10f, 1f);
        GameObject croquetaSpawned = Instantiate(croqueta) as GameObject;
        croquetaSpawned.transform.parent = transform;
        croquetaSpawned.transform.position = spawnPoint;
    }

    protected bool IsTimeToSpawn() {

        float meanSpawnDelay = Random.Range(minTimeBetweenCroqueta, maxTimeBetweenCroqueta);
        float spawnPerSecond = 1 / meanSpawnDelay;

        if (Time.deltaTime > meanSpawnDelay) {
            Debug.LogWarning("Spawn rate capped");
        }

        float threshold = spawnPerSecond * Time.deltaTime / 5;

        if (Random.value < threshold) {
            return true;
        } else {
            return false;
        }

    }
}
