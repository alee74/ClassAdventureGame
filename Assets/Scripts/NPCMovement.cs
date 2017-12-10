using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

	public float moveSpeed;
	private Rigidbody2D rb;

	public bool isWalking;
	bool facingRight = true;
	public Vector2 facing;
	public float walkTime;
	private float walkCounter;
	public float waitTime;
	private float waitCounter;
	private int walkDirection;

	public Collider2D walkingArea;
	private Vector2 minWalkpt;
	private Vector2 maxWalkpt;
	private bool inWalkingArea;

	
	private Animator anim;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		waitCounter = waitTime;
		walkCounter = walkTime;

		ChooseDirection();

		if (walkingArea != null)
		{
			minWalkpt = walkingArea.bounds.min;
			maxWalkpt = walkingArea.bounds.max;
			inWalkingArea = true;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (isWalking == false)
		{
			facing.x = 0;
			facing.y = 0;
		}
		if (isWalking == true)
		{
			walkCounter -= Time.deltaTime;
			switch (walkDirection)
			{
				case 0: //up
					rb.velocity = new Vector2(0, moveSpeed);
					facing.y = 1; facing.x = 0;
					anim.SetBool("movingUp", true);
					anim.SetBool("movingDown", false);
					anim.SetBool("movingSide", false);
					if(inWalkingArea && transform.position.y > maxWalkpt.y)
					{
						isWalking = false;
						waitCounter = waitTime;
						anim.SetBool("movingDown", false);
						anim.SetBool("movingUp", false);
						anim.SetBool("movingSide", false);
					}
					break;
				case 1: //right
					rb.velocity = new Vector2(moveSpeed, 0);
					facing.x = 1; facing.y = 0;
					if (facing.x > 0 && !facingRight) Flip();
					anim.SetBool("movingSide", true);
					anim.SetBool("movingUp", false);
					anim.SetBool("movingDown", false);
					if (inWalkingArea && transform.position.x > maxWalkpt.x)
					{
						isWalking = false;
						waitCounter = waitTime;
						anim.SetBool("movingDown", false);
						anim.SetBool("movingUp", false);
						anim.SetBool("movingSide", false);
					}
					break;
				case 2: //down
					rb.velocity = new Vector2(0, -moveSpeed);
					facing.y = -1; facing.x = 0;
					anim.SetBool("movingDown", true);
					anim.SetBool("movingUp", false);
					anim.SetBool("movingSide", false);
					if (inWalkingArea && transform.position.y < minWalkpt.y)
					{
						isWalking = false;
						waitCounter = waitTime;
						anim.SetBool("movingDown", false);
						anim.SetBool("movingUp", false);
						anim.SetBool("movingSide", false);
					}
					break;
				case 3: //left
					rb.velocity = new Vector2(-moveSpeed, 0);
					facing.y = 0; facing.x = -1;
					if (facing.x < 0 && facingRight) Flip();
					anim.SetBool("movingSide", true);
					anim.SetBool("movingUp", false);
					anim.SetBool("movingDown", false);
					if (inWalkingArea && transform.position.x < minWalkpt.x)
					{
						isWalking = false;
						waitCounter = waitTime;
						anim.SetBool("movingDown", false);
						anim.SetBool("movingUp", false);
						anim.SetBool("movingSide", false);
					}
					break;
			}
			if (walkCounter < 0) //standing
			{
				isWalking = false;
				anim.SetBool("movingDown", false);
				anim.SetBool("movingUp", false);
				anim.SetBool("movingSide", false);
				waitCounter = waitTime;
			}
		}
		else
		{
			rb.velocity = Vector2.zero;
			waitCounter -= Time.deltaTime;
			if (waitCounter < 0)
			{
				ChooseDirection();
			}
		}

	}


	public void ChooseDirection()
	{
		walkDirection = Random.Range(0, 4);
		isWalking = true;
		walkCounter = walkTime;
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}

