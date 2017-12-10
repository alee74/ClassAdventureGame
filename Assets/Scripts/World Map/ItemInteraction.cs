using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemInteraction : MonoBehaviour {
    
    public int max = 5;

    private Character currChara;

    private float rand;
	private AudioSource aud; 
	public AudioClip pickup_item; 

    private void Start()
    {
        currChara = CharInfo.getCurrentCharacter();
        transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
        //ONLY UNCOMMENT IF NEEDED FOR TESTING //transform.position = new Vector3(0, 0, -1);

		aud = GetComponent<AudioSource> (); 
    }
    void OnTriggerEnter2D(Collider2D resource){
        if (resource.gameObject.tag == "NPC") {
            PlayerMovement.speed = 5.0f;
            FightOutcome.wasInFight = true;
            StartCoroutine(WaitForKeyDown(KeyCode.Space,resource));

		} else if(resource.gameObject.tag == "FNPC") {
			PlayerMovement.speed = 5.0f;
            ItemsInInventory.num_food = ItemsInInventory.num_food + Random.Range(1,10);
            ItemsInInventory.num_water= ItemsInInventory.num_water + Random.Range(1, 10);
            ItemsInInventory.num_wood= ItemsInInventory.num_wood + Random.Range(1, 10);
            //Destroy(resource.gameObject);

		} else if (resource.gameObject.tag == "CampLife") {
             PlayerPrefs.SetFloat("X", 0);
             PlayerPrefs.SetFloat("Y", 0);
             PlayerPrefs.SetFloat("Z", -1);
            PlayerMovement.speed = 5.0f;
            ItemsInInventory.AddInventoryItemsToCampResource();
			SceneManager.LoadScene ("T-MinusDays");
		} else if (resource.gameObject.tag == "Event"){
            //placeholder?
            EventInteraction();
        } else
        {
            // Item has been touched!
			if (ItemsInInventory.GetTotalItems() < max && resource.gameObject.tag != "Tree" && resource.gameObject.tag != "Lake")
            {
                // if you can still carry stuff
                Destroy(resource.gameObject);

				aud.clip = pickup_item;
				aud.Play ();

                switch (resource.gameObject.tag)
                {
                    case "Food":
                        ItemsInInventory.num_food++;
                        //currChara.health -= 5;
                        break;
                    case "Water":
                        ItemsInInventory.num_water++;
                        //currChara.stamina -= 5;
                        break;
                    case "Wood":
                        ItemsInInventory.num_wood++;
                        //currChara.strength -= 5;
                        break;
                }
            } else
            {
                Debug.Log("Your bag is heavy!!");
            }
        }
	}

    void NPCInteraction()
    {
        //generate a random number
        //choose between a series of interactions
        //maybe generate whether it was positive or negative here?
        rand = Random.Range(0f, 1f);
        PlayerPrefs.SetFloat("X", transform.position.x);
        PlayerPrefs.SetFloat("Y", transform.position.y);
        PlayerPrefs.SetFloat("Z", transform.position.z);
        if (rand < .33){
            SceneManager.LoadScene("2DFighterStage0");
        }else if (rand > .66){
            SceneManager.LoadScene("2DFighterStage1");
        } else {
            SceneManager.LoadScene("2DFighterStage2");
        }
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

    IEnumerator WaitForKeyDown(KeyCode keyCode,Collider2D resource)
    {
        Debug.Log("Starting IEnum");
        while (!Input.GetKeyDown(keyCode))
        {
            yield return null;
        }
        Destroy(resource.gameObject);
        NPCInteraction();
    }

}
