using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum enemyState
{
    Stand,
    //Walk,
    //Punch
    Walk,
    Attack,
    Air
}

public class enemyMovement : MonoBehaviour
{

    Rigidbody2D rgb;
    enemyState myState;
    Animator enemyAnim;
    bool enemyFacingRight;
    private Transform enemyJumpCheck;
    private CapsuleCollider2D enemyCollider;
    public LayerMask groundMask;
    private bool enemyGrounded;
    public float enemyGroundRadius = 0.1f;

    PlayerScript playerScript;
    GameObject player;

    public Slider healthSlider;

    public float speed = 10f;
    public float attackXDistance = 1f;
    public float attackYDistance = 1f;

    float maxHealth = 5;
    float curHealth;

    // Use this for initialization
    void Start()
    {
        curHealth = 5;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        rgb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
        healthSlider = GameObject.Find("EnemyHealth").GetComponent<Slider>();

        myState = enemyState.Walk;
        enemyGrounded = false;
        enemyJumpCheck = transform.Find("enemyJumpCheck");

    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = curHealth / maxHealth;

        if (enemyCollider.IsTouchingLayers(groundMask))
        {
            enemyGrounded = true;
        }
        else
        {
            enemyGrounded = false;
        }

        if (enemyGrounded)
        {
            //Debug.Log("Grounded!!!");
        }

		//WIN-LOSS HANDLING
        if (curHealth <= 0)
        {
			Text winText = GameObject.Find("EndGameText").GetComponent<Text>();
			winText.text = "YOU HAVE WON!";
            SceneManager.LoadScene("WorldMapMainScene");
            Destroy(gameObject);
        }


        DirectionFacing();
        HandleInput();
    }

    void DirectionFacing()
    {
        float inX = rgb.velocity.x;
        if (inX > 0)
        {
            enemyFacingRight = true;
        }
        else if (inX < 0)
        {
            enemyFacingRight = false;
        }

        if (enemyFacingRight)
        {
            rgb.transform.localScale = new Vector3(Mathf.Abs(rgb.transform.localScale.x), rgb.transform.localScale.y, rgb.transform.localScale.z);
        }
        else
        {
            rgb.transform.localScale = new Vector3(-Mathf.Abs(rgb.transform.localScale.x), rgb.transform.localScale.y, rgb.transform.localScale.z);
        }
    }

    void ChangeState(enemyState newState)
    {
        myState = newState;

        switch (myState)
        {
            case enemyState.Walk:
                //rgb.velocity = new Vector2(0f, 0f);
                enemyAnim.SetInteger("enemyAction", 0);
                break;
            case enemyState.Attack:
                enemyAnim.SetInteger("enemyAction", 1);
                break;
            case enemyState.Air:
                //enemyAnim.SetInteger("enemyAction", 3);
                break;
            case enemyState.Stand:
                enemyAnim.SetInteger("enemyAction", 2);
                break;
        }
    }

    void HandleInput()
    {
        switch (myState)
        {
            case enemyState.Walk:
                Walk();
                break;
            case enemyState.Attack:
                Attack();
                break;
            case enemyState.Air:
                //Punch ();
                break;
            case enemyState.Stand:
                Stand();
                break;
        }
    }


    /*void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Touched Player");

		

			Debug.Log ("Lost Health!");
			curHealth = curHealth - 1;
			Debug.Log (curHealth);

		}
	}*/

    void OnTriggerEnter2D(Collider2D other)
    {
        curHealth = curHealth - 1;
    }

    void Walk()
    {
        if (enemyGrounded)
        {
            if (transform.position.x > player.transform.position.x)
            {
                rgb.velocity = new Vector2(-0.1f * speed, 0);
            }
            else
            {
                rgb.velocity = new Vector2(0.1f * speed, 0);
            }

            if (((Mathf.Abs(transform.position.x - player.transform.position.x)) <= attackXDistance) & ((Mathf.Abs(transform.position.y - player.transform.position.y)) <= attackYDistance))
            {
                ChangeState(enemyState.Attack);
            }
        }

    }

    void Stand()
    {
        //Attack
        Debug.Log("Attack");
        if (((Mathf.Abs(transform.position.x - player.transform.position.x)) >= attackXDistance) || ((Mathf.Abs(transform.position.y - player.transform.position.y)) >= attackYDistance))
        {
            ChangeState(enemyState.Walk);
        } else
        {
            ChangeState(enemyState.Attack);
        }
    }

    void Attack()
    {
        //Attack
        Debug.Log("Attack");
        if (((Mathf.Abs(transform.position.x - player.transform.position.x)) >= attackXDistance) || ((Mathf.Abs(transform.position.y - player.transform.position.y)) >= attackYDistance))
        {
            ChangeState(enemyState.Walk);
        } else
        {
            ChangeState(enemyState.Stand);
        }
    }
}