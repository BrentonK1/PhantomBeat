using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class Menu : MonoBehaviour {

	public Button StartButton;
	public Button ExitButton;

	void Update () {
		StartButton.onClick.AddListener (PressedStartButton);
		ExitButton.onClick.AddListener (PressedExitButton);
	}

	void PressedStartButton() {
		SceneManager.LoadSceneAsync("Play Scene");
	}
	void PressedExitButton() {
		  Application.Quit();
	}
}