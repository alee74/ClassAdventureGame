using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum enemyState
{
	//Stand,
	//Walk,
	//Punch
	Walk,
	Attack,
	Air
}

public class enemyMovement : MonoBehaviour {

	Rigidbody2D rgb;
	enemyState myState;

	PlayerScript playerScript;
	GameObject player;

	public Slider healthSlider;

	public float speed = 10f;
	public float attackXDistance = 1f;
	public float attackYDistance = 2f;

	float maxHealth = 5;
	float curHealth;

	// Use this for initialization
	void Start () {
		curHealth = 5;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ();
		rgb = GetComponent<Rigidbody2D> ();
		healthSlider =  GameObject.Find ("EnemyHealth").GetComponent <Slider> ();

		myState = enemyState.Walk;

	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.value = curHealth / maxHealth;

		if (curHealth == 0) {
            SceneManager.LoadScene("WorldMapMainScene");
			Destroy (gameObject);
		}

		HandleInput ();
	}

	void ChangeState(enemyState newState)
	{
		myState = newState;

		switch (myState)
		{
		case enemyState.Walk:
			//rgb.velocity = new Vector2(0f, 0f);
			//anim.SetInteger("action", 0);
			break;
		case enemyState.Attack:
			//anim.SetInteger("action", 1);
			break;
		case enemyState.Air:
			//anim.SetInteger("action", 3);
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
			Attack ();
			break;
		case enemyState.Air:
			//Punch ();
			break;
		}
	}


	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Touched Player");

		

			Debug.Log ("Lost Health!");
			curHealth = curHealth - 1;
			Debug.Log (curHealth);

		}
	}

	void Walk() {
		//bool playerIsLeft = false;
		if (transform.position.x > player.transform.position.x) {
			rgb.velocity = new Vector2 (-0.1f*speed, 0);
		} else {
			rgb.velocity = new Vector2 (0.1f* speed, 0);
		}

		if (((Mathf.Abs(transform.position.x - player.transform.position.x)) <= attackXDistance) & ((Mathf.Abs(transform.position.y - player.transform.position.y)) <= attackYDistance)) {
			ChangeState(enemyState.Attack);
		}
	}

	void Attack() {
		//Attack
		Debug.Log ("Attack");
		if (((Mathf.Abs(transform.position.x - player.transform.position.x)) >= attackXDistance) || ((Mathf.Abs(transform.position.y - player.transform.position.y)) >= attackYDistance)) {
			ChangeState(enemyState.Walk);
		}
	}
}
