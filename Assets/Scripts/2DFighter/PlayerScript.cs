using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum State
{
    Stand,
    Walk,
    Punch,
    Jump
}

public class PlayerScript : MonoBehaviour {

    //Local variables
    Rigidbody2D rgb;
    Animator anim;
    State state;
    float speed = 5f;        //will probably move to Dependent Variables
    float jumpPower = 300f;  //will probably move to Dependent Variables
    bool isFacingRight;

    //Child Objects 
    private Transform jumpCheck;
    private GameObject punchBox;

    //Layers and Ground
    public LayerMask groundMask;
    private bool grounded;
    public float groundRadius = 0.1f;

    //Dependent Variables
    private float health;
	private float maxHealth;
    private Character character;

    //UI variables
    private Slider healthSlider;



    // Use this for initialization
    void Start () {
        character = CharInfo.getCurrentCharacter();
        health = character.health;
		maxHealth = 100;
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = State.Stand;
        jumpCheck = transform.Find("JumpCheck");
        punchBox = transform.Find("PunchBox").gameObject;
        grounded = false;
		//groundMask = 9;

		healthSlider =  GameObject.Find ("PlayerHealth").GetComponent <Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.value = health / maxHealth;
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
                        rgb.velocity = new Vector2(0f, 0f);
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
		//Debug.Log ("walking");
        float inX = Input.GetAxis("Horizontal");
        bool inY = Input.GetKeyDown("space");
		bool inF = Input.GetKey(KeyCode.F);
		if (inX == 0) {
			ChangeState (State.Stand);
		} else {
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
			Debug.Log("punch while walk");
			return;
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

        StartCoroutine(PunchFunc());
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

    IEnumerator PunchFunc()
    {

        yield return new WaitForSeconds(.25f);
        punchBox.transform.SetPositionAndRotation(new Vector3(transform.position.x + 0.75f, transform.position.y, 0), new Quaternion(0,0,0,0));
        punchBox.SetActive(true);
        yield return new WaitForSeconds(.15f);
        punchBox.SetActive(false);
        punchBox.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 0));
    }

    void SaveCharInfo()
    {
       // character.health = health;
    }

}
