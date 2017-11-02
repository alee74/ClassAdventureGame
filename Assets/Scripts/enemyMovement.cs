using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum enemyState
{
	Stand,
	Walk,
	Punch
}

public class enemyMovement : MonoBehaviour {

	PlayerScript playerScript;

	State enemyState;

	int maxHealth = 3;
	int curHealth;

	// Use this for initialization
	void Start () {
		curHealth = maxHealth;
		playerScript = GameObject.FindWithTag ("Player").GetComponent<PlayerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (curHealth == 0) {
			Destroy (gameObject);
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
}
