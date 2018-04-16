/* Refactored */

using UnityEngine;

namespace PhantomBeat {
    public class Enemy : MonoBehaviour {
        Direction direction;
        Vector2 directionVector;
        Rigidbody2D hitbox;
        float speed = 2;

        void Start () {
            this.hitbox = GetComponent<Rigidbody2D>();
            this.direction = DirectionGenerator.BuildFromRigidbody2D(this.hitbox);
            this.directionVector = this.direction == Direction.Down ? Vector2.down
                                 : this.direction == Direction.Left ? Vector2.left
                                 : this.direction == Direction.Right ? Vector2.right
                                 : Vector2.up;
        }

        void Update() {
            this.hitbox.velocity = this.directionVector * this.speed;
        }
    }
}