using UnityEngine;
using PhantomBeat;

namespace PhantomBeat {
    public static class VectorGenerator {
        public static Vector2 fromDirection(Direction direction) {
            return direction == Direction.Down ? Vector2.down
                 : direction == Direction.Left ? Vector2.left
                 : direction == Direction.Right ? Vector2.right
                 : Vector2.up;
        }
    }
}