using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {

	public bool paused;
	public GameObject PausePanel;
	public Player player;

	public void playButton() {
		//Unpauses game
		Time.timeScale = 1;
		//enable player input
		//todo
		//disables pause
		PausePanel.SetActive(false);
		paused = false;
	}

	public void restartButton() {
		//Reloads scene
		//TODO: change scene name
		SceneManager.LoadScene("SampleScene");
		Time.timeScale = 1;
	}

	public void homeButton() {
		//Loads home menu scene
		SceneManager.LoadScene("MainMenu");
		Time.timeScale = 1;
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)){
			//if pause already has been pressed
			if(PausePanel.active){
				playButton();
			} else {
				//pauses game
				paused = true;
				Time.timeScale = 0;
				//disables player input
				//todo
				//sets pause panel to active
				PausePanel.SetActive(true);
			}
		}
	}

}
