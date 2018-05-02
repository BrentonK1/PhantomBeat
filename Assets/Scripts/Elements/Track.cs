using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace PhantomBeat {

    public class Track {
        static Dictionary<Direction, Track> protectedInstances;

        private Direction currentDirection;
        private Transform enemySpawnLocation;
        private Vector2 vectorDirection;
        private Vector3 enemySpawnTransform;

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
            enemySpawnTransform = new Vector3(enemySpawnLocation.position.x, enemySpawnLocation.position.y, 0);
        }

        public GameObject SpawnEnemy() {
             return GameObject.Instantiate(Constants.enemyPrefab, enemySpawnTransform, Quaternion.identity);
        }
    }

}
