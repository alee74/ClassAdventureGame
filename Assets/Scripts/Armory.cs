using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour {

    CharInfo character;

    public void OnTriggerEnter2D(Collider2D col)
    {
        character = col.gameObject.GetComponent<CharInfo>();
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Armory");
        }
        addOneStrength(character);
    }

    void addOneStrength(CharInfo character)
    {
        character.addStrength(1);
        Debug.Log("Added strength to player");
    }

}
