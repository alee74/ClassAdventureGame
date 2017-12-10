using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
    
	public RuntimeAnimatorController goodGirlControl;
	public RuntimeAnimatorController badGirlControl;

	private Animator npcAnimator;
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

		npcAnimator = gameObject.GetComponent<Animator> ();

	}

    private void Update()
    {
        
        if (thisNPC.character.isHostile)
        {
            gameObject.tag = "NPC";
			npcAnimator.runtimeAnimatorController = badGirlControl;
            //Debug.Log(thisNPC.character.name + " is hostile");
        }
        else
        {
            gameObject.tag = "FNPC";
			npcAnimator.runtimeAnimatorController = goodGirlControl;
            //Debug.Log(thisNPC.character.name + " is a good boy");
        }
    }
}
