using System;
using System.Collections.Generic;

namespace PhantomBeat {
    
    /// <summary> `DictionaryFactory` is used in the creation of various dictionaries. </summary>
    public static class DictionaryGenerator {

        /// <summary> 
        /// `ConstructorFunction` represents any method that returns a value of type T
        /// when given a direction.
        /// </summary>
        ///
        /// <typeparam name="V"> The type of value this constructor returns. </typeparam>
        /// <param name="direction"> The direction used in construction. </param>
        public delegate V ConstructorFunction<V>(Direction direction);

        /// <summary> `BuildUsingDirection` builds a new `Dictionary` from an enumeration of directions. </summary>
        /// <typeparam name="V"> The type of value the constructed dictionary is filled with. </typeparam>
        /// <param name="constructor"> The `ConstructionFunction` used in building the new dictionary. </param>
        public static Dictionary<Direction, V> BuildUsingDirection<V>(ConstructorFunction<V> constructor)
        {
            var dictionary = new Dictionary<Direction, V>();
            var keys = Enum.GetValues(typeof(Direction));

            foreach (Direction key in keys)
            {
                V value = constructor(key);
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }

}