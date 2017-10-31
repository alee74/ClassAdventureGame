using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Stand,
    Walk,
    Punch,
    Jump
}

public class PlayerScript : MonoBehaviour {

    Rigidbody2D rgb;
    Animator anim;
    State state;
    float speed = 5f;
    bool isFacingRight;


	// Use this for initialization
	void Start () {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = State.Stand;
	}
	
	// Update is called once per frame
	void Update () {
        DirectionFacing();
        HandleInput();
	}

    void HandleInput()
    {
        switch (state)
        {
            case State.Stand:
                Stand();
                break;
            case State.Walk:
                Walk();
                break;
            case State.Jump:
                break;
            case State.Punch:
                break;
        }
    }

    void ChangeState(State newState)
    {
        state = newState;

               switch (state)
               {
                   case State.Stand:
                       anim.SetInteger("action", 0);
                       break;
                   case State.Walk:
					   anim.SetInteger("action", 1);
                       break;
                   case State.Jump:
                       break;
                   case State.Punch:
                       break;
               }
    }

    void DirectionFacing()
    {
        float inX = Input.GetAxis("Horizontal");
        if (inX > 0)
        {
			//anim.SetBool("isFacingRight", true);
			Debug.Log("right");
            isFacingRight = true;
        }
        else if (inX < 0)
        {
			//anim.SetBool("isFacingRight", false);
			Debug.Log("left");
            isFacingRight = false;
        }

		if(isFacingRight) {
			rgb.transform.localScale = new Vector3 (Mathf.Abs(rgb.transform.localScale.x), rgb.transform.localScale.y, rgb.transform.localScale.z);
		}
		else {
			rgb.transform.localScale = new Vector3 (-Mathf.Abs(rgb.transform.localScale.x), rgb.transform.localScale.y, rgb.transform.localScale.z);
		}
    }
 

    void Stand()
    {
		//rgb.velocity = new Vector2 (0, 0);
        float inX = Input.GetAxis("Horizontal");
        bool inY = Input.GetKeyDown("space");
        if (inX != 0)
        {
            ChangeState(State.Walk);
        }       
    }

    void Walk()
    {
        float inX = Input.GetAxis("Horizontal");
        bool inY = Input.GetKeyDown("space");
		if (inX == 0) {
			ChangeState (State.Stand);

		} else if (inX > 0) {
			rgb.velocity = new Vector2 (inX*speed, 0);
		} else if (inX < 0) {
			rgb.velocity = new Vector2 (inX*speed, 0);
		}

    }
         /*   if (inY)
        {
            rgb.AddForce(new Vector3(0, 350f, 0));
            runner.ChangeState(State.jump);
        }*/



}
