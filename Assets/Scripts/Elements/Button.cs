/* Refactored */

using UnityEngine;
using System.Collections.Generic;

namespace PhantomBeat {

    enum ButtonState {
        Active,
        Inactive
    }

    public class Button : MonoBehaviour {

        static float hitboxRadius = 3.17f;
        ButtonState state = ButtonState.Inactive;

        Stack<GameObject> enemiesInRange = new Stack<GameObject>();

        int KillEnemies() {
            var selectedEnemies = enemiesInRange.ToArray();

            foreach(var enemy in selectedEnemies) {
                Destroy(enemy);
            }

            return selectedEnemies.Length;
        }

        void OnCollision2DEnter(Collision2D enemy) {
            enemiesInRange.Push(enemy.gameObject);
        }

        void OnCollision2DExit(Collision2D enemy) {
            enemiesInRange.Pop();
        }

        void Update() {
            var firstTouch = Input.GetTouch(0);
            firstTouch.phase = TouchPhase.Began;

            var firstTouchPosition = Camera.main.ScreenToWorldPoint(firstTouch.position);
            var touchInRange = Vector2.Distance(this.gameObject.transform.position, firstTouchPosition) < Button.hitboxRadius;

            if (touchInRange && this.state == ButtonState.Inactive) {
                this.state = ButtonState.Active;
                StartCoroutine(Touch1Check());

                var enemiesKilled = this.KillEnemies();
                // ScoreManager.add(enemiesKilled * pointModifier);
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