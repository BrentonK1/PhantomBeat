using UnityEngine;

namespace PhantomBeat {

    public static class Constants {
        public static readonly Transform originTransform = GameObject.FindGameObjectWithTag("Spawner").transform;
        public static readonly GameObject enemyPrefab = Resources.Load("Enemy") as GameObject;
    }

}