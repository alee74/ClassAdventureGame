using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    float jumpPower = 250f;  //will probably move to Dependent Variables
    bool isFacingRight;
    bool firstCheck; //for jumping

    //Child Objects 
   // private Transform jumpCheck;
    private GameObject punchBox;
	private CapsuleCollider2D playerCollider;

    //Layers and Ground
    public LayerMask groundMask;

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
		playerCollider = GetComponent<CapsuleCollider2D>();
        state = State.Stand;
     //   jumpCheck = transform.Find("JumpCheck");
        punchBox = transform.Find("PunchBox").gameObject;

		healthSlider =  GameObject.Find ("PlayerHealth").GetComponent <Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.value = health / maxHealth;

        DirectionFacing();
        HandleInput();

		//WIN-LOSS HANDLING
		if (health <= 0)
		{
			SceneManager.LoadScene("GameOver");
		}

	}

	bool IsOnGround ()
    { 
        Debug.DrawRay(transform.position, new Vector2(0, -0.9f), Color.green);
        RaycastHit2D hit =  Physics2D.Raycast(transform.position, Vector2.down, distance: 0.85f, layerMask: groundMask);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            return true;
        } else
        {
            return false;
        }
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
                Debug.Log("Calling Jump");
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
                        anim.SetInteger("state", 0);
                       break;
                   case State.Walk:
                       anim.SetInteger("state", 1);
                       break;
                   case State.Jump:
                       firstCheck = true;
                       rgb.AddForce(new Vector2(0, jumpPower));
                       anim.SetInteger("state", 3);
                       break;
                   case State.Punch:
                       anim.SetInteger("state", 2);
                       break;
               }
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

		if(isFacingRight) {
			rgb.transform.localScale = new Vector3 (Mathf.Abs(rgb.transform.localScale.x), rgb.transform.localScale.y, rgb.transform.localScale.z);
		}
		else {
			rgb.transform.localScale = new Vector3 (-Mathf.Abs(rgb.transform.localScale.x), rgb.transform.localScale.y, rgb.transform.localScale.z);
		}
    }
 

    void Stand()
    {
        bool inY = Input.GetKeyDown("space");
        float inX = Input.GetAxis("Horizontal");
		bool inF = Input.GetKey(KeyCode.F);


        if (inY && IsOnGround())
        {
            ChangeState(State.Jump);
            return;
        }
        else if (inF)
        {
            Debug.Log("Should punch1");

            ChangeState(State.Punch);
            return;
        }
        else if (inX != 0)
        {
            ChangeState(State.Walk);
        }
        
    }

    void Walk()
    {
		//Debug.Log ("walking");
        float inX = Input.GetAxis("Horizontal");
        bool inY = Input.GetKeyDown("space");
		bool inF = Input.GetKey(KeyCode.F);

        if (inY && IsOnGround()) {
            ChangeState(State.Jump);
            return;
        } else if (inF) {
            ChangeState(State.Punch);
		} else if (inX == 0)
        {
            ChangeState(State.Stand);
        } else {
            rgb.velocity = new Vector2(inX * speed, 0f);
        }


    }

    void Jump()
    {

        Debug.Log("Enter Jump");
		Debug.Log ("jumping");
        float inX = Input.GetAxis("Horizontal");
		rgb.velocity = new Vector2(inX * speed, rgb.velocity.y);

        StartCoroutine(JumpToGround());

    }


    void Punch()
    {
		//Punch!
		Debug.Log("Punch");

        StartCoroutine(PunchFunc());
        //Need to change state?
        float inX = Input.GetAxis("Horizontal");

		if (inX == 0) {
			ChangeState (State.Stand);
		} else {
			rgb.velocity = new Vector2 (inX * speed, 0);
			ChangeState (State.Walk);
		}
    }

    
    IEnumerator JumpToGround()
    {
        Debug.Log("J2G");
        if (firstCheck)
        {
            yield return new WaitForSeconds(.05f);
            if (IsOnGround())
            {
                Debug.Log("First Grounded!");
                ChangeState(State.Stand);
            }
            firstCheck = false;
        }
        else
        {
            if (IsOnGround())
            {
                Debug.Log("Grounded!");
                ChangeState(State.Stand);
            }
        }
    }
    
    IEnumerator PunchFunc()
    {

        yield return new WaitForSeconds(.25f);
        if (isFacingRight)
        {
            punchBox.transform.SetPositionAndRotation(new Vector3(transform.position.x + 0.75f, transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        } else
        {
            punchBox.transform.SetPositionAndRotation(new Vector3(transform.position.x - 0.75f, transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        }
        punchBox.SetActive(true);
        yield return new WaitForSeconds(.15f);
        punchBox.SetActive(false);
        punchBox.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 0));
    }

}
