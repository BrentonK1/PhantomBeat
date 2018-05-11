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

        public static readonly GameObject enemyPrefab = Resources.Load<GameObject>("enemy");
        public static readonly Logger globalLogger = new Logger(Debug.unityLogger.logHandler);
        public static readonly GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
    }
}