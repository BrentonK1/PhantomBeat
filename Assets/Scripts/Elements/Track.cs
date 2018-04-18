using System.Collections.Generic;
using UnityEngine;

namespace PhantomBeat {

    public class Track {
        static Dictionary<Direction, Track> protectedInstances;


        public static Dictionary<Direction, Track> instances {
            get {
                if (Track.protectedInstances == null)
                    Track.protectedInstances = DictionaryGenerator.BuildUsingDirection(direction => new Track(direction));
                return Track.protectedInstances;
            }
        }

        public readonly Direction direction;

        public Track(Direction direction) {
            this.direction = direction;
        }

        public GameObject SpawnEnemy() {
            var enemy = GameObject.Instantiate(Constants.enemyPrefab, Constants.originTransform);
        }
    }

}
