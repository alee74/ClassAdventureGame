using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hospital : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

		// Resource menu Buy or upgrade health Items

	
		// Inventory



		// Restore Player Health
	




	}

	//add method to view inventory

	public void OnTriggerEnter2D(Collider2D col) {

        Character character = CharInfo.getCurrentCharacter();
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Hospital");
            if (character.health < character.getMaxHealth())
            {
                character.health += character.getMaxHealth() - character.health;
                Debug.Log("Player healed");
            }
            else
            {
                Debug.Log("Your HP Is Full");
            }
        }
    }

}
