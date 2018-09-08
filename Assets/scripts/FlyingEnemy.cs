using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {

	public Transform target; //bunny
	Renderer rend;
	bool rendered=false;
	float ratio;
	float ratioControl;
	float upDownSpeed;
	bool direction; // false: up; true: down
	float distBulletShooter;
	
	float time;
	bool firstShot = false;
	bool secShot = false;

	Vector3 offset;
	public GameObject bullet;
	GameObject bulletIns;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		ratio = 2.4f;
		ratioControl = 0f;
		upDownSpeed = 0.03f;
		direction = false; //up and down
		distBulletShooter = -0.5f;
		time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(rend.isVisible){
			rendered = true;
			if(direction == false)	transform.position += new Vector3(0f, upDownSpeed, 0f);  
			else                    transform.position += new Vector3(0f, -upDownSpeed, 0f);

			offset = new Vector3(target.position.x, 0f, 0f) - transform.position;
			transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);
			transform.Rotate(new Vector3(0, 0, -90));
			
			//LEARNING 2: https://docs.unity3d.com/ScriptReference/Transform.Rotate.html 

			ratioControl += upDownSpeed;
			if(ratioControl >= ratio) {
				direction = !direction;
				ratioControl = 0f;
			}
			
			//shooting for the fisrt time:
			if(time >0.2 && firstShot == false){
				bulletIns = Instantiate(bullet, transform.position+(new Vector3(0f, distBulletShooter, 0f)), Quaternion.identity);
				bulletIns.transform.Rotate(0f, 0f, 30f);
				firstShot = true;
			}
			else if(time >= 2.3 && secShot == false){
				bulletIns = Instantiate(bullet, transform.position+(new Vector3(0f, distBulletShooter, 0f)), Quaternion.identity);
				bulletIns.transform.Rotate(0f, 0f, -30f);
				secShot = true;
			}
			time += Time.deltaTime;

		}
		else if(rendered == true){
			Destroy(this.gameObject);
		}

	}
}
