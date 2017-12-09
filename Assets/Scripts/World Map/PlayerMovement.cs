using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerMovement : MonoBehaviour {

	Vector3 pos;                             
	static public float speed = 5.0f;   
	float inputX;
	float inputY;
	private Animator anim;


	bool facingRight = true;

	private Rigidbody2D theRigidBody;
	//private bool notHittingObst = true;

	void Start () {
		//pos = transform.position; 
		theRigidBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}

	void Update () {
		inputX = Input.GetAxis ("Horizontal");
		inputY = Input.GetAxis ("Vertical");
		if (inputX > 0 && !facingRight) {
			Flip ();
		}  else if (inputX < 0 && facingRight) {
			Flip ();
		}
		/*
        if (notHittingObst)
        {788
        */
		if (inputX > 0) {
			theRigidBody.velocity = new Vector3 (inputX * speed, 0f, 0f);
			anim.SetBool("movingSide", true);
            anim.SetBool("movingUp", false);
            anim.SetBool("movingDown", false);
        } else if (inputX < 0) {
			theRigidBody.velocity = new Vector3 (inputX * speed, 0f, 0f);
			anim.SetBool("movingSide", true);
            anim.SetBool("movingUp", false);
            anim.SetBool("movingDown", false);
        } else if (inputY < 0) {
			theRigidBody.velocity = new Vector3 (0f, inputY * speed, 0f);
			anim.SetBool("movingDown", true);
            anim.SetBool("movingUp", false);
            anim.SetBool("movingSide", false);
        } else if (inputY > 0) {
			theRigidBody.velocity = new Vector3 (0f, inputY * speed, 0f);
			anim.SetBool("movingUp", true);
            anim.SetBool("movingDown", false);
            anim.SetBool("movingSide", false);
        } else if (inputX == 0 && inputY == 0) {
			anim.SetBool("movingDown", false);
			anim.SetBool("movingUp", false);
			anim.SetBool("movingSide", false);
			theRigidBody.velocity = new Vector3 (0f, 0f, 0f);
		}


		//}


		//transform.position = pos;   // Move there
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
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		Debug.Log("Stopped colliding with: " + collision.gameObject.name);
		//notHittingObst = true;
	}

}
