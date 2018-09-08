using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlyingEnemy : MonoBehaviour {
	Renderer rend;

	float speed;
	bool playerCollision = false;
	bool groundCollision = false;
	Vector3 previousPosition;
	float distToDestroy;
	GameObject player;
	Vector3 whereToGo;
	
	//-------------------------------------------------------------------------------------------
	private void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag == "player"){
			playerCollision = true;
			distToDestroy = 0.05f;
		}
		if(collision.gameObject.tag == "ground"){
			groundCollision = true;
		}
	}

	//-------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		player = GameObject.Find("R foot");
		whereToGo = new Vector3(player.transform.position.x + (0.94f/0.02f)*0.2f /* (time/timePerFrame)*playerSpeed*/ , player.transform.position.y, 0f);
		speed = 0.4f;
	}
	
	//-------------------------------------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		previousPosition = transform.position;
		transform.position = Vector3.MoveTowards(transform.position, whereToGo, speed);

		if(playerCollision == true || groundCollision == true || previousPosition == transform.position) 
			Destroy(this.gameObject);

	}
}
