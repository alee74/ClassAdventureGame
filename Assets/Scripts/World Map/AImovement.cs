using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class AImovement : MonoBehaviour {

	Vector3 pos;                             
	float speed = 20.0f;   
	float inputX;
	float inputY;
	private Animator anim;
	float timer = 3f;
	float chooser;


	bool facingRight = true;

	private Rigidbody2D theRigidBody;
	//private bool notHittingObst = true;

	void Start () {
		//pos = transform.position; 
		theRigidBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}

	void Update () {
		Debug.Log ("timer is going" + timer);
		timer -= Time.deltaTime;
		if (timer < 0) {
			chooser = Random.Range(-1f, 1f);
			
			if (chooser < 0) {
				inputX = Random.Range(-5f,5f);
			}
			if (chooser > 0) {
				inputY = Random.Range(-5f,5f);
			}
		

		if (inputX > 0 && !facingRight) {
			Flip ();
		}  else if (inputX < 0 && facingRight) {
			Flip ();
		}
		
		if (inputX > 0) {
				Debug.Log ("move right");
				theRigidBody.AddForce (Vector2.right * speed);
				//theRigidBody.velocity = transform.position.x * speed;
			//theRigidBody.velocity = new Vector3 (Mathf.Abs(inputX) * speed, 0f, 0f);
			anim.SetBool("WalkingSide", true);
			anim.SetBool("WalkingBack", false);
			anim.SetBool("isSurprised", false);
			
		} else if (inputX < 0) {
				Debug.Log ("move left");
				//theRigidBody.velocity = -transform.position.x * speed;
				theRigidBody.AddForce (Vector2.left * speed);
			//theRigidBody.velocity = new Vector3 (Mathf.Abs(inputX) * speed, 0f, 0f);
			anim.SetBool("WalkingSide", true);
			anim.SetBool("WalkingBack", false);
			anim.SetBool("isSurprised", false);
		} else if (inputY < 0) {
				Debug.Log ("move up");
				theRigidBody.AddForce (Vector2.up * speed);
				//theRigidBody.velocity = transform.position.y * speed;
			//theRigidBody.velocity = new Vector3 (0f, Mathf.Abs(inputY) * speed, 0f);
			anim.SetBool("WalkingBack", true);
			anim.SetBool("isSurprised", false);
			anim.SetBool("WalkingSide", false);
		} else if (inputY > 0) {
				Debug.Log ("move down");
				//theRigidBody.velocity = transform.position.y * speed;
				theRigidBody.AddForce (Vector2.down * speed);
			//theRigidBody.velocity = new Vector3 (0f, Mathf.Abs(inputY) * speed, 0f);
			anim.SetBool("WalkingBack", false);
			anim.SetBool("WalkingSide", false);
			anim.SetBool("isSurprised", false);
		} 

			timer = 3f;
		}



	}


	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        notHittingObst = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Stopped colliding with: " + collision.gameObject.name);
        notHittingObst = true;
    }
    */


	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Collided with: " + collision.gameObject.name);
		//notHittingObst = false;
		theRigidBody.velocity = new Vector2(0f,0f);
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		Debug.Log("Stopped colliding with: " + collision.gameObject.name);
		//notHittingObst = true;
	}

}
