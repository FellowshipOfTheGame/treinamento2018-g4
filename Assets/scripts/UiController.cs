using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {

	public bool paused;
	public GameObject PausePanel;
	public GameObject GameOverPanel;
	public GameObject WinPanel;
	public Player player;
	public AudioSource deathSound;

	Button[] options;
	Button[] optionsGameOver; // game over menu options
	int selected;

	public void playButton() {
		//Unpauses game
		Time.timeScale = 1;
		//enable player input
		//todo
		//disables pause
		PausePanel.SetActive(false);
		GameOverPanel.SetActive(false);
		WinPanel.SetActive(false);
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

	public void GameOver() {
		//play gameover song
		optionsGameOver = GameOverPanel.GetComponentsInChildren<Button>();
		selected = 0;
		paused = true;
		Time.timeScale = 0;
		GameOverPanel.SetActive(true);
	}

	public void Win() {
		optionsGameOver = WinPanel.GetComponentsInChildren<Button>();
		selected = 0;
		paused = true;
		Time.timeScale = 0;
		WinPanel.SetActive(true);
	}

	
	private void Start() {
		deathSound = GetComponent<AudioSource>();
		options = PausePanel.GetComponentsInChildren<Button>();
		optionsGameOver = GameOverPanel.GetComponentsInChildren<Button>();
		selected = 0;
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!PausePanel.activeSelf && !GameOverPanel.activeSelf){
				//pauses game
				paused = true;
				Time.timeScale = 0;
				//disables player input
				//todo
				//sets pause panel to active
				PausePanel.SetActive(true);
			} else if (!GameOverPanel.activeSelf){
				playButton();
			}
		}
		if(PausePanel.activeSelf){
			//if game is paused, check for inputs to cycle between options
			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				selected -= 1;
				if(selected <= -1) selected = 2;
			} else if(Input.GetKeyDown(KeyCode.RightArrow)){
				selected += 1;
				if(selected >= 3) selected = 0;
			} else if(Input.GetKeyDown(KeyCode.KeypadEnter)){
				//push button
				options[selected].onClick.Invoke();
			}
			// Selecting current button
			options[selected].Select();
		}
		if(GameOverPanel.activeSelf || WinPanel.activeSelf){
			//if game is paused, check for inputs to cycle between options
			if(Input.GetKeyDown(KeyCode.UpArrow)){
				selected -= 1;
				if(selected <= -1) selected = 1;
			} else if(Input.GetKeyDown(KeyCode.DownArrow)){
				selected += 1;
				if(selected >= 2) selected = 0;
			} else if(Input.GetKeyDown(KeyCode.KeypadEnter)){
				//push button
				optionsGameOver[selected].onClick.Invoke();
			}
			// Selecting current button
			optionsGameOver[selected].Select();
		}
	}

}
