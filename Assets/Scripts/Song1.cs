using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song1 : MonoBehaviour {

	public GameObject EnemyUp, EnemyDown, EnemyLeft, EnemyRight, Spawner;

	void Start () {
		AudioSource song1 = GetComponent<AudioSource> ();
		song1.Play ();
		//SpawnEnemyUp ();
        StartCoroutine(Waitspawn());
	}

	void Update () {
		
	}

	private void SpawnEnemyUp() {
		Instantiate (EnemyUp, Spawner.transform.position, Quaternion.identity);
	}
    private void SpawnEnemyDown() {
        Instantiate(EnemyDown, Spawner.transform.position, Quaternion.identity);
    }
    private void SpawnEnemyLeft() {
        Instantiate(EnemyLeft, Spawner.transform.position, Quaternion.identity);
    }
    private void SpawnEnemyRight() {
        Instantiate(EnemyRight, Spawner.transform.position, Quaternion.identity);
    }

    IEnumerator Waitspawn()
    {
        yield return new WaitForSeconds(5);
            SpawnEnemyUp();
    }

}