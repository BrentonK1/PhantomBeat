using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomBeat {
	public class ButtonV2 : MonoBehaviour {

		//public GameObject EnemyPrefab;
        public GameObject Iggy;
        private Animator IggyAnimator;

		enum ButtonState {
				Active,
				Inactive
			}

        void Start() {
            IggyAnimator = Iggy.gameObject.GetComponent<Animator>();
        }

        static float hitboxRadius = 3.17f;
        ButtonState state = ButtonState.Inactive;

        public static Stack<GameObject> enemiesInRange = new Stack<GameObject>();

        void KillEnemies() {
            var selectedEnemies = enemiesInRange.ToArray();
            Debug.Log(selectedEnemies.Length + " enemies");

            if (selectedEnemies.Length == 0){
                ScoreManager.score --;
            }
            else if (selectedEnemies.Length > 0){
                foreach(var enemy in enemiesInRange) {
                    Destroy(enemy);
                    ScoreManager.score ++;
                }
            }
        }

        void OnTriggerEnter2D(Collider2D enemy) {
            enemiesInRange.Push(enemy.gameObject);
        }

        void OnTriggerExit2D(Collider2D enemy) {
            enemiesInRange.Pop();
            Debug.Log("enemy left collider");
        }

        void Update() {

            if (Input.touchCount >0){
                var firstTouch = Input.GetTouch(0);
                firstTouch.phase = TouchPhase.Began;
                var firstTouchPosition = Camera.main.ScreenToWorldPoint(firstTouch.position);
                var touchInRange = Vector2.Distance(this.gameObject.transform.position, firstTouchPosition) < ButtonV2.hitboxRadius;

                if (touchInRange && this.state == ButtonState.Inactive) {
                    if (this.gameObject.tag == "TriggerButtonDown"){
                        IggyAnimator.Play("Attack2");
                    }
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