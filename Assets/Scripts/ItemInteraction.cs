﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    private ItemsInInventory itemsInInventoryScript;

    /*
	private int water;
	private int food;
	private int wood;
    */

	// Use this for initialization
	void Start () {
        itemsInInventoryScript = GameObject.Find("Inventory").GetComponent<ItemsInInventory>();
        /*
        water = 0;
		food = 0;
		wood = 0;
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D resource){
		if (resource.gameObject.tag == "Food") {
			Destroy (resource.gameObject);
            itemsInInventoryScript.food += 1;
			//food++;
		} else if (resource.gameObject.tag == "Water") {
			Destroy (resource.gameObject);
            //water++;
            itemsInInventoryScript.water += 1;
			Debug.Log ("water");
		} else if (resource.gameObject.tag == "Wood") {
			Destroy (resource.gameObject);
            itemsInInventoryScript.wood += 1;
			//wood++;
		} else if (resource.gameObject.tag == "NPC") {
			//NPCInteraction();
			//launch interaction with NPC, maybe check whether friendly or hostile instead?
		} else if(resource.gameObject.tag == "Event"){
			//launch event, OR could just tag as water or wood
		}
	}

	void NPCInteraction(){
		//generate a random number
		//choose between a series of interactions
		//maybe generate whether it was positive or negative here?
	}

	void EventInteraction(){
		//generate a random nubmer
		//choose between a series of events
	}
}
