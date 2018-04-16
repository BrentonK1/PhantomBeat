/* <!> DEPRECATED <!> */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomBeat.CoreLibrary;

public class Song1 : MonoBehaviour {

	public GameObject EnemyUp, EnemyDown, EnemyLeft, EnemyRight, Spawner;
    public AudioSource FirstSong;

	void Start () {
        FirstSong = GetComponent<AudioSource> ();
        FirstSong.Play ();
	}

	void Update () {
        float TimeToSpawnUpEnemy = 2;
        var PlaybackTime = (float)FirstSong.time;

        //Debug.Log(PlaybackTime);

        if (PlaybackTime - TimeToSpawnUpEnemy == 0)
            SpawnEnemy(Direction.Up);
	}

    string BuildTagFrom(string prefix, Direction direction){
        return prefix + direction.ToString();
    }

    GameObject Enemy(Direction direction) {
        var enemy = GameObject.FindWithTag(BuildTagFrom("Enemy", direction));
        return enemy;
    }

    private void SpawnEnemy(Direction direction) {
        GameObject enemy = Enemy(direction);
        Instantiate(enemy, Spawner.transform.position, Quaternion.identity);
        enemy.SetActive(true);
	}
}