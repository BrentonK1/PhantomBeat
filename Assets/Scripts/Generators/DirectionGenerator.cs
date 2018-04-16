/* Refactored */

using UnityEngine;

namespace PhantomBeat {

    /// <summary> `DirectionFactory` is used in finding and translating `Direction` values. </summary>
    public static class DirectionGenerator {

        /// <summary> `BuildFromPosition` builds a new `Direction` by using a `Vector2` value. </summary>
        /// <param name="position"> The `Vector2D` used in building this new direction. </param>
        public static Direction BuildFromPosition(Vector2 position) {
            var notOnAxis = position.x != 0 && position.y != 0;
            var isOnOrgin = position.x == 0 && position.y == 0;

            if (isOnOrgin) throw new System.ArgumentOutOfRangeException("position", "Given position can not be at (0, 0).");
            if (notOnAxis) throw new System.ArgumentOutOfRangeException("position", "Given position is not on the X or Y axis.");

            var isUp = position.y > 0;
            var isDown = position.y < 0;
            var isRight = position.x > 0;

            return isUp ? Direction.Up
                : isDown ? Direction.Down
                : isRight ? Direction.Right
                : Direction.Left;
        }

        /// <summary> `BuildFromCollision2D` builds a new `Direction` by using a `Collision2D` value. </summary>
        /// <param name="collision"> The `Collision2D` used in building this new direction. </param>
        public static Direction BuildFromCollision2D(Collision2D collision) {
            return DirectionGenerator.BuildFromRigidbody2D(collision.rigidbody);
        }

        public static Direction BuildFromRigidbody2D(Rigidbody2D rigidbody2D) {
            return DirectionGenerator.BuildFromPosition(rigidbody2D.position);
        }

    }
}