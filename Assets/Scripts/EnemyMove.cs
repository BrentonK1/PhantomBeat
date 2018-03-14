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

    private void OnCollisionEnter2D(Collision2D triggerbutton)
    {
        if (triggerbutton.gameObject.tag == "LTriggerButton")
            Playing.CanPressL = true;
        //in playing script, change to false either when this object is destroyed or it hits the player because the points are not collected
        else if (triggerbutton.gameObject.tag == "RTriggerButton")
            Playing.CanPressR = true;
        //ibid
        else if (triggerbutton.gameObject.tag == "UTriggerButton")
            Playing.CanPressU = true;
        //ibid
        else if (triggerbutton.gameObject.tag == "DTriggerButton")
            Playing.CanPressD = true;
        //ibid
    }

}