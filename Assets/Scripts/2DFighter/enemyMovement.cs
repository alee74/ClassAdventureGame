using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum enemyState
{
	Stand,
	Walk,
	Punch
}

public class enemyMovement : MonoBehaviour {

	Rigidbody2D rgb;
	enemyState myState;

	PlayerScript playerScript;
	GameObject player;

	public Slider healthSlider;

	public float speed = 10f;

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

	void HandleInput()
	{
		switch (myState)
		{
		case enemyState.Stand:
			//Stand();
			break;
		case enemyState.Walk:
			Walk();
			break;
		case enemyState.Punch:
			//Punch ();
			break;
		}
	}


	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Touched Player");

		
			//THIS WILL CHECK IF THE PLAYER IS IN PUNCH STATE   
			//if (playerScript.currentState == 3) {
				Debug.Log ("Lost Health!");
				curHealth = curHealth - 1;
				Debug.Log (curHealth);
			//}
		}
	}

	void Walk() {
		//bool playerIsLeft = false;
		if (transform.position.x > player.transform.position.x) {
			rgb.velocity = new Vector2 (-0.1f*speed, 0);
		} else {
			rgb.velocity = new Vector2 (0.1f* speed, 0);
		}
	}
}
