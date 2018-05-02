using UnityEngine;
using PhantomBeat;

namespace PhantomBeat {
    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }

    public static class Constants {
        public static Transform originTransform {
            get { return Constants.spawner.transform; }
        }

        //public static readonly GameObject enemyPrefab = Resources.Load("Enemy") as GameObject;
        public static readonly Logger globalLogger = new Logger(Debug.unityLogger.logHandler);
        public static readonly GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        
        public static GameObject enemyPrefab {
            get { 
                if (Constants.protectedEnemyPrefab == null) {
                    Constants.protectedEnemyPrefab = new GameObject("EnemyToSpawn");
                    Constants.protectedEnemyPrefab.AddComponent<PhantomBeat.Enemy>();
                    Constants.protectedEnemyPrefab.AddComponent<Rigidbody2D>();
                    Constants.protectedEnemyPrefab.AddComponent<SpriteRenderer>();
                    Constants.protectedEnemyPrefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("dingDong");
                }
                return Constants.protectedEnemyPrefab;
            }
        }


        static GameObject protectedEnemyPrefab;
    }
}