using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
    
    private EncounterCharacterEvent thisNPC;
	private GameObject a;
	private EventTile b;
	private Character c;
	private bool d;

    

	// Use this for initialization
	void Start () {
		a = gameObject;
		b = a.GetComponent<EventTile>();
		thisNPC = gameObject.GetComponent<EventTile>().tileEvent;
		c = thisNPC.character; //lol
		d = c.isHostile;
        
		/*
        if (thisNPC.character.isHostile)
        { 
            gameObject.tag = "NPC";
			Debug.Log ("N P C");

        } 
        else
        {
            gameObject.tag = "FNPC";
			Debug.Log ("F R I E N D");
        }
        */
	}

    private void Update()
    {
        if (thisNPC == null)
        {
            Debug.Log("fuck my life");
        }
        
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
    }
}
