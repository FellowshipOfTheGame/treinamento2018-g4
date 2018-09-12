﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingWall : MonoBehaviour {

	//has to be the same speed as the player's 
	UiController ui;
	GameObject player;
	Player playerScript;
	float distFromPlayer;
	float distInitial;

	float initialForwardSpeed;  //when enemy is showing up on screen
	float forwardSpeed;         //when player collides with obstacle
	float backwardSpeed;        //when player is running
	bool killPlayer = false;
	bool wallCollision = false;
	bool showingUp = true;

	public float initialSpeed; //13
	public float normalSpeed;  //10
	public float speedWithObstacle; //8


	Rigidbody2D rb;


	//-------------------------------------------------------------------------------------------
	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "player"){
			killPlayer = true;
		}
	}


	// Use this for initialization ------------------------------------------------------------------
	void Start () {
		player = GameObject.FindGameObjectWithTag("player");
		ui = FindObjectsOfType<UiController>()[0];

		distFromPlayer = 7f;
		distInitial = 13f;
		initialForwardSpeed = 0.09f;
		forwardSpeed = 0.7f;
		backwardSpeed = 0.03f;
		playerScript = player.gameObject.GetComponent<Player>();
		transform.position = new Vector3(player.transform.position.x-distInitial, transform.position.y+1.5f, 0f);

		//so that wall only collides with player
        Physics2D.IgnoreLayerCollision(11, 8, true);
        Physics2D.IgnoreLayerCollision(11, 10, true);		        
		Physics2D.IgnoreLayerCollision(11, 12, true);
		Physics2D.IgnoreLayerCollision(11, 14, true);

		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 12;

	}
	
	// Update is called once per frame ----------------------------------------------------------------
	void Update () {
		if(!ui.paused){
			//if(playerScript.wallCollision == true) wallCollision = true;

			if(playerScript.alive == true){

				//showing up on screen:
				if(showingUp == true){
					//transform.position += new Vector3(playerScript.speed+initialForwardSpeed, 0f, 0f);
					 rb.velocity = new Vector2(initialSpeed,rb.velocity.y);
					if(Mathf.Abs(transform.position.x - player.transform.position.x) <= distFromPlayer) showingUp = false;
				}
				else{

					//wall follows player only if it is running (did not collide with an obstacle):
					//obs: wall is a little slower
					if(playerScript.obstacleCollision == false &&  killPlayer == false){
						//..transform.position += new Vector3((playerScript.speed-backwardSpeed) , 0f, 0f);
						rb.velocity = new Vector2(normalSpeed,rb.velocity.y);
					}
				
					//otherwise, it goes a little forward:
					else if(killPlayer == false){
						//so that it looks like wall walks a little forwards:	
						rb.velocity = new Vector2(speedWithObstacle,rb.velocity.y);
						//distFromPlayer = Mathf.Abs(transform.position.x-player.transform.position.x);
					}
				
					//if wall collides with player, he dies...
					else {
						playerScript.alive = false;
						playerScript.obstacleCollision = false;
					}
					distFromPlayer = Mathf.Abs(transform.position.x-player.transform.position.x);

				}//end else

			}

			//if wall stops being redered, ir is destoied
			if(Mathf.Abs(transform.position.x-player.transform.position.x) > 17){
				Destroy(this.gameObject);
			}

		}

	}
}