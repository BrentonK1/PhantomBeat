using UnityEngine;
using PhantomBeat;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour{
    public static float score = 0;
    public Text ScoreText;
    void Update(){
        ScoreText.text = ("Score: " + score);
    }

}