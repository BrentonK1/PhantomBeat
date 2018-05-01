using System.Collections.Generic;
using UnityEngine;

namespace PhantomBeat {

    public class Track {
        static Dictionary<Direction, Track> protectedInstances;

        private Direction currentDirection;
        private Transform enemySpawnLocation;
        private Vector2 vectorDirection;

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
            this.vectorDirection = VectorGenerator.fromDirection(direction);
            this.enemySpawnLocation = Constants.originTransform;
            this.enemySpawnLocation.position = 0.000001f * this.vectorDirection;
        }

        public GameObject SpawnEnemy() {
             return GameObject.Instantiate(ButtonV2.EnemyPrefab, enemySpawnLocation);
        }
    }

}
