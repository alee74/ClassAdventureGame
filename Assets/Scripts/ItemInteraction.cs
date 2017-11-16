using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemInteraction : MonoBehaviour {
    
    public int max = 5;
    
	void OnTriggerEnter2D(Collider2D resource){
		if (resource.gameObject.tag == "NPC") {
			NPCInteraction ();
		} else if (resource.gameObject.tag == "CampLife") {
			SceneManager.LoadScene ("TestCamp");
		} else if (resource.gameObject.tag == "Event"){
            EventInteraction();
        } else
        {
            // Item has been touched!
            if (ItemsInInventory.GetTotalItems() < max)
            {
                // if you can still carry stuff
                Destroy(resource.gameObject);
                switch (resource.gameObject.tag)
                {
                    case "Food":
                        ItemsInInventory.num_food++;
                        break;
                    case "Water":
                        ItemsInInventory.num_water++;
                        break;
                    case "Wood":
                        ItemsInInventory.num_wood++;
                        break;
                }
            } else
            {
                Debug.Log("Your bag is heavy!!");
            }
        }
	}

	void NPCInteraction(){
		//generate a random number
		//choose between a series of interactions
		//maybe generate whether it was positive or negative here?
		SceneManager.LoadScene ("2dFighting");
	}

	void EventInteraction(){
		//generate a random number
		//choose between a series of events
	}
}
