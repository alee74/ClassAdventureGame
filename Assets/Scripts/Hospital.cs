using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hospital : MonoBehaviour {

    CharInfo character;


    private int health; 
	public BarStats bar;
	// Use this for initialization
	private void awake() {
		bar.Intialize();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.A)) {
			bar.currVal -= 10f;
		
		}
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
			bar.currVal += 10f;
		}
        else
        {

            Debug.Log("Your HP Is Full");

        }



    }

}
