using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractions : MonoBehaviour {
    //This function should make a dialogue box popup when you touch it

    public Image dialogueBox;
    public Text encounterDesc;
    public Text encounterDialogue;
    
    private DialogueBoxController dialogueBoxControllerScript;

	// Use this for initialization
	void Start () {
        dialogueBoxControllerScript = dialogueBox.GetComponent<DialogueBoxController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EncounterCharacterEvent npc;

        if (collision.tag == "FNPC" || collision.tag == "NPC")
        {
            npc = collision.GetComponent<EventTile>().tileEvent;
            encounterDesc.text = npc.name + " " + npc.description;
            encounterDialogue.text = npc.encounterDialog;
            dialogueBoxControllerScript.SetDialogue(true);
        }
    }
}
