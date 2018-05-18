using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {
	public Button PauseButton;

	void Update(){
		PauseButton.onClick.AddListener (PressedPause);
	}

	void PressedPause(){
		if (Time.timeScale == 1){
             Time.timeScale = 0;
			 PauseButton.GetComponentInChildren<Text>().text = ("Play");
			 SoundManager.song.Pause();
		}
         else{
             Time.timeScale = 1;
			 PauseButton.GetComponentInChildren<Text>().text = ("Pause");
			 SoundManager.song.Play();
		 }
	}
}