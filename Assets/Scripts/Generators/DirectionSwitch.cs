using System.Collections.Generic;
using PhantomBeat;

namespace PhantomBeat {

    public class DirectionSwitch<T> {
        public readonly Dictionary<Direction, T> values;

        public DirectionSwitch(Dictionary<Direction, T> values) {
            this.values = values;
        }

        public T performSwitch(Direction direction) {
            return this.values[direction];
        }
    }

}