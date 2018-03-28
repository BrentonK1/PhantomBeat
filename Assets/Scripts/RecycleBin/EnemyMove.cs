using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	private float speed = 2;
	private Rigidbody2D rb2d;


	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (gameObject.tag == "EnemyUp")
			rb2d.velocity = Vector2.up * speed;
		else if (gameObject.tag == "EnemyDown")
			rb2d.velocity = Vector2.down * speed;
		else if (gameObject.tag == "EnemyLeft")
			rb2d.velocity = Vector2.left * speed;
		else if (gameObject.tag == "EnemyRight")
			rb2d.velocity = Vector2.right * speed;
	}

}