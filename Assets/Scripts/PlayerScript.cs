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
    float jumpPower = 300f;
    bool isFacingRight;
    private Transform jumpCheck;
    public LayerMask groundMask;
    private bool grounded;
    public float groundRadius = 0.1f;



    // Use this for initialization
    void Start () {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = State.Stand;
        jumpCheck = transform.Find("JumpCheck");
        grounded = false;
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.OverlapCircle (jumpCheck.position, groundRadius, groundMask);
        //Debug.Log("State: " + state);
        //Debug.Log(grounded);
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
                Jump();
                break;
			case State.Punch:
				Punch ();
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
                       anim.SetInteger("action", 3);
                       break;
                   case State.Punch:
                       anim.SetInteger("action", 2);
                       break;
               }
    }

    void DirectionFacing()
    {
        float inX = Input.GetAxis("Horizontal");
        if (inX > 0)
        {
			//anim.SetBool("isFacingRight", true);
            isFacingRight = true;
        }
        else if (inX < 0)
        {
			//anim.SetBool("isFacingRight", false);
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
		//Debug.Log ("standing");
		//rgb.velocity = new Vector2 (0, 0);
        float inX = Input.GetAxis("Horizontal");
        bool inY = Input.GetKeyDown("space");
		bool inF = Input.GetKey(KeyCode.F);
        if (inX != 0)
        {
            ChangeState(State.Walk);
        } if (inY && grounded)
        {
            ChangeState(State.Jump);
            rgb.velocity = new Vector2(rgb.velocity.x, 0f);
            rgb.AddForce(new Vector2(0, jumpPower));
        }

		if (inF) {
			Debug.Log ("Should punch1");
			ChangeState(State.Punch);
			return;
		}
    }

    void Walk()
    {
		Debug.Log ("walking");
        float inX = Input.GetAxis("Horizontal");
        bool inY = Input.GetKeyDown("space");
		bool inF = Input.GetKey(KeyCode.F);
		if (inX == 0) {
			ChangeState (State.Stand);
		} else if (inX > 0) {
			rgb.velocity = new Vector2 (inX*speed, 0);
		} else if (inX < 0) {
			rgb.velocity = new Vector2 (inX*speed, 0);
		} 
        if (inY && grounded)
        {
            ChangeState(State.Jump);
            rgb.velocity = new Vector2(rgb.velocity.x, 0f);
            rgb.AddForce(new Vector2(0, jumpPower));
        }

		if (inF) {
			ChangeState (State.Punch);
			return;
			Debug.Log("punch while walk");
		}

    }

    void Jump()
    {
		Debug.Log ("jumping");
        float inX = Input.GetAxis("Horizontal");
		rgb.velocity = new Vector2(inX * speed, rgb.velocity.y);
		grounded = Physics2D.OverlapCircle (jumpCheck.position, groundRadius, groundMask);

        if (grounded)
        {
			Debug.Log ("Grounded!");
            ChangeState(State.Stand);
        }

    }


    void Punch()
    {
		//Punch!
		Debug.Log("Punch");

		//Need to change state?
		float inX = Input.GetAxis("Horizontal");
		bool inY = Input.GetKeyDown("space");

		if (inX == 0) {
			ChangeState (State.Stand);
		} else {
			rgb.velocity = new Vector2 (inX * speed, 0);
			ChangeState (State.Walk);
		}
    }

}
