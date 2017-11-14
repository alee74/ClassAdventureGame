using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour {


    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            addOneStrength(CharInfo.getCurrentCharacter());
            Debug.Log("Player Entered Armory");
        }
        
    }

    void addOneStrength(Character character)
    {
        character.strength += 1;
        Debug.Log("Added strength to player");
    }

}
