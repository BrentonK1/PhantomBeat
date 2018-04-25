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
            this.directionVector = VectorGenerator.fromDirection(this.direction);
        }

        void Update() {
            this.hitbox.velocity = this.directionVector * this.speed;
        }
    }
}