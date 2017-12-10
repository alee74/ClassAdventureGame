using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
    
	public RuntimeAnimatorController goodGirlControl;
	public RuntimeAnimatorController badGirlControl;

	private Animator npcAnimator;
    private EncounterCharacterEvent thisNPC;

	// Use this for initialization
	void Start () {
        thisNPC = gameObject.GetComponent<EventTile>().tileEvent;
		npcAnimator = gameObject.GetComponent<Animator> ();
	}

    private void Update()
    {
        if (thisNPC.character.isHostile)
        {
            gameObject.tag = "NPC";
			npcAnimator.runtimeAnimatorController = badGirlControl;
        }
        else
        {
            gameObject.tag = "FNPC";
			npcAnimator.runtimeAnimatorController = goodGirlControl;
        }
    }
}
