using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject {

	public static GameObject enemyPrefab;
	public Sprite enemySprite;
	//public static GameObject enemything;

	void Start () {
		enemyPrefab = new GameObject ("EnemyToSpawn");
		enemyPrefab.AddComponent<PhantomBeat.Enemy>();
		enemyPrefab.AddComponent<Rigidbody2D>();
		enemyPrefab.AddComponent<SpriteRenderer>();
		enemyPrefab.GetComponent<SpriteRenderer>().sprite = enemySprite;
	}
}