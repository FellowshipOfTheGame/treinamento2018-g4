using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour {
    public AudioSource step1;
    public AudioSource step2;

	
    public void playStep1() {
        this.step1.Play();
    }
    public void playStep2() {
        this.step2.Play();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
