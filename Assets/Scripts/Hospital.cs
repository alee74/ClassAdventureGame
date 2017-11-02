using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hospital : MonoBehaviour {

    CharInfo character;

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

	public void OnTriggerEnter2D(Collider2D col) {

        character = col.gameObject.GetComponent<CharInfo>();
        //character = GetComponent<CharInfo>();
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Hospital");
        }

        if (character.health < 100)
        {
            character.health += character.maxHealth - character.health;
            Debug.Log("Player healed");
        }
        else
        {

            Debug.Log("Your HP Is Full");

        }



    }

}
