using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public GameObject titlePanel;
	public GameObject optionsPanel;
	public GameObject creditsPanel;
	public GameObject bunny;
	Button[] options;
	public int selected;

	public void PlayClick() {
		SceneManager.LoadScene("SampleScene");
	}

	public void CreditsClick() {
		titlePanel.SetActive(false);
		optionsPanel.SetActive(false);
		bunny.SetActive(false);
		creditsPanel.SetActive(true);
	}

	public void ExitClick() {
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

	// Use this for initialization
	void Start () {
		options = optionsPanel.GetComponentsInChildren<Button>();
	}

	
	// Update is called once per frame
	void Update () {

		// Controlling menu 
		if(creditsPanel.activeSelf){
			if(Input.GetKeyDown(KeyCode.Escape)){
				titlePanel.SetActive(true);
				optionsPanel.SetActive(true);
				bunny.SetActive(true);
				creditsPanel.SetActive(false);
			}
		} else {
			if(Input.GetKeyDown(KeyCode.UpArrow)){
				selected -= 1;
				if(selected <= -1) selected = 2;
			} else if(Input.GetKeyDown(KeyCode.DownArrow)){
				selected += 1;
				if(selected >= 3) selected = 0;
			} else if(Input.GetKeyDown(KeyCode.KeypadEnter)){
				//push button
				options[selected].onClick.Invoke();
			}
			
			// Selecting current button
			options[selected].Select();

		}

	}


}
