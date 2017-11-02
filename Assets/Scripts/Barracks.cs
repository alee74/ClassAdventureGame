using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour {

    CharInfo character;
    private GameObject player;

	// Use this for initialization
	void Start () {
        character = gameObject.GetComponent<CharInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   void OnTriggerEnter2D(Collider2D col)
   {
        character = col.gameObject.GetComponent<CharInfo>(); 
        //character = GetComponent<CharInfo>();
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered");
        }
        
        if (character.stamina < 100)
        {
            character.stamina += character.maxStamina - character.stamina;

        }
        else
        {

            Debug.Log("Your Stamina Is Full");

        }
    }
}
