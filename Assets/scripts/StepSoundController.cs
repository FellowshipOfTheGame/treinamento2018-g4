using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundController : MonoBehaviour {

	public AudioSource step1;
	public AudioSource step2;
	public AudioSource slurp;

	public void playStep1() {
		this.step1.Play();
	}
	
	public void playStep2() {
		this.step2.Play();
	}

	public void playSlurp() {
		this.slurp.Play();
		Debug.Log("Played slurp");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
