using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemInteraction : MonoBehaviour {
    
    public int max = 5;


    private void Start()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
            //transform.position = new Vector3(0, 0, -1);
    }
    void OnTriggerEnter2D(Collider2D resource){
		if (resource.gameObject.tag == "NPC") {
            Destroy(resource.gameObject);
            NPCInteraction();
		} else if (resource.gameObject.tag == "CampLife") {
            PlayerPrefs.SetFloat("X", 0);
            PlayerPrefs.SetFloat("Y", 0);
            PlayerPrefs.SetFloat("Z", -1);
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
        PlayerPrefs.SetFloat("X", transform.position.x);
		PlayerPrefs.SetFloat("Y", transform.position.y);
		PlayerPrefs.SetFloat("Z", transform.position.z);
		SceneManager.LoadScene ("2dFighting");
	}

	void EventInteraction(){
		//generate a random number
		//choose between a series of events
	}

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("X", 0);
        PlayerPrefs.SetFloat("Y", 0);
        PlayerPrefs.SetFloat("Z", -1);
    }

}
