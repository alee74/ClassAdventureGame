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
   // Animator anim;
    State state;
    float speed = 10f;
    bool isFacingRight;


	// Use this for initialization
	void Start () {
        rgb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
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
        Debug.Log(state);
        /*       switch (state)
               {
                   case State.Stand:
                       //anim.SetInteger("action", 0);

                       break;
                   case State.Walk:
                       break;
                   case State.Jump:
                       break;
                   case State.Punch:
                       break;
               }*/
    }

    void DirectionFacing()
    {
        float inX = Input.GetAxis("Horizontal");
        if (inX > 0)
        {
            isFacingRight = true;
        }
        else if (inX < 0)
        {
            isFacingRight = false;
        }
    }
 

    void Stand()
    {
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
        if (inX == 0)
        {
            ChangeState(State.Stand);
        }
        else
        {
            rgb.AddForce(new Vector2(speed * inX, 0));
        }

    }
         /*   if (inY)
        {
            rgb.AddForce(new Vector3(0, 350f, 0));
            runner.ChangeState(State.jump);
        }*/



}
