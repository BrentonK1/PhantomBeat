/* Refactored */
using UnityEngine;
using PhantomBeat;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour{

    void Start() {
        StartCoroutine(WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine());
        //Track.instances[Direction.Right].SpawnEnemy();
    }
    IEnumerator<WaitForSeconds> WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine() {
        yield return new WaitForSeconds(4);
        Track.instances[Direction.Left].SpawnEnemy();
        StartCoroutine(WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine());
        //StopAllCoroutines();
    }
}

// https://answers.unity.com/questions/1138633/how-to-sync-gameobject-creation-to-the-beat-of-a-s.html