/* Refactored */
using UnityEngine;
using PhantomBeat;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour{
    private float waitTime = 0.75f;
    public Text LoadingText;

    void Start() {
        StartCoroutine(WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine());
        LoadingText.text = ("Loading...");
    }

    int directionNumber(){
        var randomNumber = UnityEngine.Random.Range(1,5);
        return randomNumber;
    }

    IEnumerator<WaitForSeconds> WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine() {
         LoadingText.text = ("");
        yield return new WaitForSeconds(waitTime);
        var number = directionNumber();
        if(number == 1)
            Track.instances[Direction.Up].SpawnEnemy();
        else if(number == 2)
            Track.instances[Direction.Down].SpawnEnemy();
        else if(number == 3)
            Track.instances[Direction.Left].SpawnEnemy();
        else if(number == 4)
            Track.instances[Direction.Right].SpawnEnemy();
        
        waitTime = 1;
        StartCoroutine(WaitForUnityTogetTheHellUpAndLearnHowToBeAFunctionalEngine());
    }
}