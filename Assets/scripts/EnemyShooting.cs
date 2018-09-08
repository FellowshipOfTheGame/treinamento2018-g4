using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyShooting : MonoBehaviour {

	Renderer rend;
	public GameObject player;
	//just for u to undertand, they were bullets, now they are lasers, byt variable name is still the same because i am lazy
	public GameObject bullet;
	public AudioSource laserSound;
	GameObject bulletIns;

	float distanceFromPlayer; //distance emeny has to be from the player
	float distBulletShooter;
	float time;
	float speed;
	public int numberOfShoots;
	int shootCounter;
	public float timeForShooting;

	//---------------------------------------------------------------
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		distanceFromPlayer = 17f; 
		time = 0f;
		speed = 0.1f;

		shootCounter = 0;
		distBulletShooter = -0.5f;
	}
	
	//---------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		
		//if it is in camera field of view:
		if(rend.isVisible && shootCounter < numberOfShoots){
			//walk  with character
			if(transform.position.x - player.transform.position.x <= distanceFromPlayer){
				//shot 3 times, wait, and do it again:
				if(time <= 0.01) {
					bulletIns = Instantiate(bullet, transform.position+(new Vector3(distBulletShooter, 0f, 0f)), Quaternion.identity);	
					laserSound.Play();
					shootCounter++;				
				}

				//walking:
				transform.position = new Vector3(player.transform.position.x + distanceFromPlayer, transform.position.y, 0f);
				time += Time.deltaTime;
				if(time >= timeForShooting) time = 0f;
			}

		}
		else if(shootCounter == numberOfShoots){
			distanceFromPlayer += speed;
			transform.position = new Vector3(player.transform.position.x + distanceFromPlayer, transform.position.y, 0f);
			if(!rend.isVisible){
				Destroy(this.gameObject);
			}
		}

	}
}
