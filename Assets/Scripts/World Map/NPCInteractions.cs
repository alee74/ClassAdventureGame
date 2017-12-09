using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractions : MonoBehaviour {
    //This function should make a dialogue box popup when you touch it

    private Text dialogueTxt;
    private DialogueBoxController dialogueBoxControllerScript;

	// Use this for initialization
	void Start () {
        dialogueTxt = GameObject.Find("DialogueText").GetComponent<Text>();
        dialogueBoxControllerScript = GameObject.Find("DialogueBox").GetComponent<DialogueBoxController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FNPC" || collision.tag == "NPC")
        {
            Debug.Log("Colliding with npc");
            Event npc = collision.GetComponent<EventTile>().tileEvent;
            dialogueTxt.text = npc.description;
            dialogueBoxControllerScript.SetDialogue(true);
        }
    }
}
