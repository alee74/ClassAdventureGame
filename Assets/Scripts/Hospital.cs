using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hospital : MonoBehaviour {


	private int health; 
	private int maxhealth = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		// Resource menu Buy or upgrade health Items


		// Inventory



		// Restore Player Health
	




	}

	//add method to view inventory

	public void OnTriggerEnter2D(Collider2D Player) {
	
		if (health <= 100) {
			health += maxhealth - health;
		
		} else {
			
			Debug.Log ("Your Health Is Full");
		
		}
			


	
	}

}
