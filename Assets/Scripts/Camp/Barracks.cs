using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour {
    private GameObject player;

	
	// Update is called once per frame
	void Update () {
		
	}

   void OnTriggerEnter2D(Collider2D col)
   {
        Character character = CharInfo.getCurrentCharacter(); 
        //character = GetComponent<CharInfo>();
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Barracks");
        }
        
        if (character.stamina < 100)
        {
            character.stamina += 100;
            Debug.Log("Stamina restored");
        }
        else
        {

            Debug.Log("Your Stamina Is Full");

        }
    }
}
