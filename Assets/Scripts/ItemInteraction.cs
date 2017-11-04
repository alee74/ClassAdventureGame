using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {

    private ItemsInInventory itemsInInventoryScript;
    private int itemsCarried = 0;

    public int max = 5;
    
	// Use this for initialization
	void Start () {
        itemsInInventoryScript = GameObject.Find("Inventory").GetComponent<ItemsInInventory>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D resource){
		if (resource.gameObject.tag == "Food") {
            if (itemsCarried < max) { 
				Destroy (resource.gameObject);
				itemsInInventoryScript.num_food += 1;
                itemsCarried++;
			}
		} else if (resource.gameObject.tag == "Water") {
            if (itemsCarried < max) {
                Destroy (resource.gameObject);
				itemsInInventoryScript.num_water += 1;
                itemsCarried++;
            }
		} else if (resource.gameObject.tag == "Wood") {
            if (itemsCarried < max) {
                Destroy (resource.gameObject);
				itemsInInventoryScript.num_wood += 1;
                itemsCarried++;
            }
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
		//generate a random number
		//choose between a series of events
	}
}
