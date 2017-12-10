using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
    
    private EncounterCharacterEvent thisNPC;
    

	// Use this for initialization
	void Start () {
        thisNPC = gameObject.GetComponent<EventTile>().tileEvent;
        
        if (thisNPC.character.isHostile)
        { 
            gameObject.tag = "NPC";
        } 
        else
        {
            gameObject.tag = "FNPC";
        }
        
	}

    private void Update()
    {
        if (thisNPC == null)
        {
            Debug.Log("fuck my life");
        }
        /*
        if (thisNPC.character.isHostile)
        {
            //gameObject.tag = "NPC";
            Debug.Log(thisNPC.character.name + " is hostile");
        }
        else
        {
            //gameObject.tag = "FNPC";
            Debug.Log(thisNPC.character.name + " is a good boy");
        }
        */
    }
}
