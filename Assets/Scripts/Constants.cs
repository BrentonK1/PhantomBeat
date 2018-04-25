using UnityEngine;
using PhantomBeat;

namespace PhantomBeat {
    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }

    public class Constants: MonoBehaviour {
        public static Transform originTransform {
            get { return Constants.spawner.transform; }
        }
        public GameObject enemyPrefab;
        //public static readonly GameObject enemyPrefab = Resources.Load("Enemy") as GameObject;
        public static readonly Logger globalLogger = new Logger(Debug.unityLogger.logHandler);
        public static readonly GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
    }
}