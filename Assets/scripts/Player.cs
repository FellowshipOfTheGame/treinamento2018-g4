using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour {
    SpriteRenderer sr;
    Rigidbody2D rb;
    public Camera cm;

    public AudioSource backgroundSound;
    public AudioSource jumpSound;
    public AudioSource trashCollisionSound;
    

    public float jump_force;
    public float speed;
    public float climbSpeed;
    public float cameraSpeed;
    public float  gravityValue;
    float distance_cm_player_x;
    float distance_cm_player_y;

    //controllers
	bool grounded = true; 
    bool groundCollision = true;
    public bool wallCollision = false;     //has to be public
    public bool obstacleCollision = false; //has to be public
    public bool sensorCollision = false;         //has to be public
     
    bool climb = false;
    bool cameraAlignment = true;

    //ground collision control:
    float y_previous_frame;
    float y_now;

    //die
    public bool alive = true; //has to be public

    //when collides with an obstacle:
    public float speedBack;
    public float speedBackAux;
    float accelerationBack;

    //enemy that shows up because of the sensor
    public GameObject enemy;
    GameObject enemyIns;

    //---------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision){
        //colliding with the wall:
        if (collision.gameObject.tag == "wall") {
            wallCollision = true;
            Debug.Log("wall in: "+wallCollision);
        }

        if(groundCollision==false && collision.gameObject.tag == "ground"){
            groundCollision = true;
            Debug.Log("Ground collision: "+groundCollision);
        }

        if(collision.gameObject.tag == "bullet"){
            alive = false;
        }

        if(collision.gameObject.tag == "Obstacle"){
            obstacleCollision = true;
            trashCollisionSound.Play();
        }

        if(collision.gameObject.tag == "Sensor"){
            sensorCollision = true;
        }
    }
    //---------------------------------------------------------------------------------
    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.tag == "wall") { 
            wallCollision = false;
            Debug.Log("wall out: "+wallCollision);
        }

        if(groundCollision == true && collision.gameObject.tag == "ground"){
            groundCollision = false;
            Debug.Log("Ground collision: "+groundCollision);
        }
    }
 
    //---------------------------------------------------------------------------------
    // Use this for initialization
    void Start () {
        speed = 0.2f;
        climbSpeed = 0.6f;
        cameraSpeed = 0.3f;
        jump_force = 2200f;
        gravityValue = 12;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = gravityValue;
        distance_cm_player_x = Math.Abs(cm.transform.position.x - transform.position.x);
        distance_cm_player_y = Math.Abs(cm.transform.position.y - transform.position.y) + 0.07f;

        y_previous_frame = y_now = transform.position.y;

        speedBack = 0.2f;
        speedBackAux = speedBack;
        accelerationBack = 0.005f;

    }
	
	//---------------------------------------------------------------------------------
    // Update is called once per frame
    void Update () {

        if(alive == true){


            //CONTROLLING GROUND COLLISION _______________________________________________________
            // do it by comparing y axis position in this frame and the one of the previous frame
            y_now = transform.position.y;
            if((int)(y_now*1000) != (int)(y_previous_frame*1000) ){ //comparing 1 decimal place
                if(grounded == true) {
                    grounded = false;
                }
            }
            else if(grounded == false){
                grounded = true;
            }
            
            //happens in the end of the code:
            //y_previous_frame = y_now;

            //RUNNING ____________________________________________________________________________    
            //forever running if is not in contact with a wall
            if (wallCollision == false && obstacleCollision == false) {
                transform.position += new Vector3(speed, 0, 0);
                sr.flipX = false;
            }

            //JUMP ____________________________________________________________________________
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ) && grounded ==  true && wallCollision == false) { 
                rb.AddRelativeForce(transform.up * jump_force);           

                //garantia:
                grounded = false;  // <<---

                //jumping sound effect:
                jumpSound.Play();

            }

            //CLIMBING __________________________________________________________________________

            //if player is touching a wall and presses  "G" to go up
            if(wallCollision == true && Input.GetKey(KeyCode.G)){
                rb.velocity = new Vector2(0f, 0f);
                climb = true;
                rb.gravityScale = 0;
                cameraAlignment = true;
                cm.transform.position += new Vector3(0, climbSpeed, 0); 
                transform.position += new Vector3(0, climbSpeed, 0);
            }
            //if player ended climbing
            else if(climb == true && wallCollision == false){
                rb.gravityScale = gravityValue;
                climb = false;
            }

            //OBSTACLE COLLISION __________________________________________________________________________
            if(obstacleCollision == true){
                //if player collides with an obstacle, he gives some steps back, then keep running forwards
                if(speedBackAux > 0){
                    transform.position += new Vector3(-speedBackAux, 0f, 0f);
                    speedBackAux -= accelerationBack;
                }
                else{
                    speedBackAux = speedBack;
                    obstacleCollision = false;
                }   
            }

            //SENSOR COLLISION __________________________________________________________________________
            if(sensorCollision == true){
                //new Vector3(15f, 0f, 0f) --> 15f because in KillingWall script thats the initial distance from player (if dist>23 enemy is destroied)
                enemyIns = Instantiate(enemy, transform.position+(new Vector3(-11f, 0f, 0f)), Quaternion.identity);
                sensorCollision = false;
            }

            //CAMERA ____________________________________________________________________________
            //camera follows character in x axis:
            Vector3 auxiliarVector = new Vector3(transform.position.x, cm.transform.position.y, cm.transform.position.z);
            cm.transform.position = auxiliarVector + new Vector3(distance_cm_player_x, 0.0f, 0.0f);

            //camera sometimes follows in the y axis hehe:
            if( Math.Abs(cm.transform.position.y - transform.position.y) > distance_cm_player_y ) {
                cameraAlignment = false;
            }

            if(cameraAlignment == false && cm.transform.position.y > transform.position.y){ //player bellow the camera
                cm.transform.position += new Vector3(0.0f, (y_now - y_previous_frame), 0.0f);
            }
            else if(cameraAlignment == false && cm.transform.position.y < transform.position.y){//player above the camera
                //cm.transform.position += new Vector3(0.0f, cameraSpeed, 0.0f);
                cm.transform.position += new Vector3(0.0f, (y_now - y_previous_frame), 0.0f);
            }

            if(Math.Abs(cm.transform.position.y - transform.position.y) <= distance_cm_player_y){
                cameraAlignment = true;    
            }

            y_previous_frame = y_now;
      
        } //ALL OF IT JUST HAPPENS IF PLAYER IS ALIVE
        else{
            backgroundSound.Stop();
        }
	}
}