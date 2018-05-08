/* Refactored */
using UnityEngine;
using PhantomBeat;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour{

    void Start() {
        StartCoroutine(WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine());
        //Track.instances[Direction.Right].SpawnEnemy();
    }

    int directionNumber(){
        var randomNumber = UnityEngine.Random.Range(1,4);
        return randomNumber;
    }

    IEnumerator<WaitForSeconds> WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine() {
        yield return new WaitForSeconds(4);
        var number = directionNumber();
        if(number == 1)
            Track.instances[Direction.Up].SpawnEnemy();
        else if(number == 2)
            Track.instances[Direction.Down].SpawnEnemy();
        else if(number == 3)
            Track.instances[Direction.Left].SpawnEnemy();
        else if(number == 4)
            Track.instances[Direction.Right].SpawnEnemy();
        
        StartCoroutine(WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine());
        //StopAllCoroutines();
    }
}

// https://answers.unity.com/questions/1138633/how-to-sync-gameobject-creation-to-the-beat-of-a-s.html