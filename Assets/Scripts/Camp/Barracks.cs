using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour {
    public GameObject ui;

   void OnTriggerEnter2D(Collider2D col)
   {
        Character character = CharInfo.getCurrentCharacter(); 
        //character = GetComponent<CharInfo>();
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Barracks");
            ui.SetActive(true);
        }
        
        if (character.stamina  < character.getMaxStamina())
        {
            character.stamina += character.getMaxStamina() - character.stamina;
            Debug.Log("Stamina restored");
        }
        else
        {

            Debug.Log("Your Stamina Is Full");

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ui.SetActive(false);
        }
    }
}
