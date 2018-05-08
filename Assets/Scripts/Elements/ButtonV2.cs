using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomBeat {
	public class ButtonV2 : MonoBehaviour {

		//public GameObject EnemyPrefab;

		enum ButtonState {
				Active,
				Inactive
			}

        static float hitboxRadius = 3.17f;
        ButtonState state = ButtonState.Inactive;

        Stack<GameObject> enemiesInRange = new Stack<GameObject>();

        void KillEnemies() {
            var selectedEnemies = enemiesInRange.ToArray();
            if (selectedEnemies.Length == 0){
                Debug.Log("No enemies there");
                ScoreManager.score --;
            }
            else if (selectedEnemies.Length > 0){
                Debug.Log("There are " + selectedEnemies.Length + " enemies there");
                foreach(var enemy in enemiesInRange) {
                    Destroy(enemy);
                    ScoreManager.score += (1 * selectedEnemies.Length);
                }
            }
        }

        //WILL NOT REGISTER ENEMIES IN COLLISION
        void OnTriggerEnter2D(Collider2D enemy) {
            Debug.Log("enemy in collision");
            this.enemiesInRange.Push(enemy.gameObject);
        }

        void OnTriggerExit2D(Collider2D enemy) {
            this.enemiesInRange.Pop();
        }

        void Update() {
            if (Input.touchCount >0){
                var firstTouch = Input.GetTouch(0);
                firstTouch.phase = TouchPhase.Began;
                var firstTouchPosition = Camera.main.ScreenToWorldPoint(firstTouch.position);
                var touchInRange = Vector2.Distance(this.gameObject.transform.position, firstTouchPosition) < ButtonV2.hitboxRadius;

                if (touchInRange && this.state == ButtonState.Inactive) {
                    this.state = ButtonState.Active;
                    StartCoroutine(Touch1Check());
                    KillEnemies();
                }
            }
        }

        IEnumerator<UnityEngine.WaitForSeconds> Touch1Check () {
            yield return new WaitForSeconds(0.01f);
            if (Input.GetTouch(0).phase != TouchPhase.Ended)
                StartCoroutine(Touch1Check());
            else {
                this.state = ButtonState.Inactive;
                StopCoroutine(Touch1Check());
            }
        }
	}
}