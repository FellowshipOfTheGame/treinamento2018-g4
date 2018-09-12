using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet1 : MonoBehaviour {

	UiController ui;
	Rigidbody2D rb;
	Renderer rend;

	float speed;
	bool playerCollision = false;
	float distToDestroy;

	//-------------------------------------------------------------------------------------------
	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "player"){
			playerCollision = true;
			distToDestroy = 0.05f;
		}
	}

	//-------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () {
		ui = FindObjectsOfType<UiController>()[0];
		rb = GetComponent<Rigidbody2D>();
		rend = GetComponent<Renderer>();
		speed = 0.25f;
	}
	
	//-------------------------------------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		if(!ui.paused)
			transform.position += new Vector3(-speed, 0f, 0f);
		if(playerCollision == true || !rend.isVisible) 
			Destroy(this.gameObject, 0.07f);
	}
}
