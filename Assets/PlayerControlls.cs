using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    public Text health;             //Hud representation of player health
    public int intLives = 10;       //player lives
    public int intMaxJump;          //maximum number of jumps the player can preform without landing
    public int intJumpHeight = 500000; //Force applied on player upon jumping
    public int intMoveSpeed = 1000;   //player movement speed
    public Animator FaceOnBall;

    private Vector3 vect3CurrentRespawn; //current respawn location
    private int intJumpsLeft = 2;   //jumps remaining until player has to land
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        health.text = "Lives: " + intLives;     //updates lives count in hud
		if (Input.GetKey(KeyCode.D))            //triggeres if the "d" (right) key is down
        {
            this.GetComponent<Rigidbody>().AddForce(intMoveSpeed, 0, 0);      //adds movement speed to in the x direction on the player
        }
        if (Input.GetKey(KeyCode.A))            //triggers when the "a" (left) key is down
        {
            this.GetComponent<Rigidbody>().AddForce(-intMoveSpeed, 0, 0);       //adds movement speed as a force in the -x direction on the player
        }

        if (Input.GetKeyDown(KeyCode.Space)&& intJumpsLeft >0)      //triggeres as soon as the space key is pressed and as long as there are jumps available
        {
            this.GetComponent<Rigidbody>().AddForce(0,intJumpHeight,0);     //add jumpheight to the force
            intJumpsLeft -= 1;      //suptract a jump from the remaining jumps counter
        }
	}
    private void OnCollisionEnter(Collision collision)      //triggers on any collision
    {
        intJumpsLeft = intMaxJump;      //resets jumps

    }
    private void OnTriggerEnter(Collider other) //triggers on any collision with a trigger object
    {

        if (other.name.Contains("Death"))   //triggers if the collision was with an object with "Death" in its name
        {
            FaceOnBall.SetTrigger("Dead");
            intLives--; //count down lives
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            this.transform.position = vect3CurrentRespawn; //respawn
        }
        if (other.name.Contains("Respawn")) //triggers if the collision with an object with "Respawn" in its name and simply remembers its position
        {
            vect3CurrentRespawn = other.transform.position;
        }
    }
}
