using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour {
	public GameObject resource;
	private ItemsInInventory itemsInInventoryScript;

	// Use this for initialization
	void Start () {
		itemsInInventoryScript = /*GameObject.Find("Main Camera").*/GetComponent<ItemsInInventory>();
		//itemsInInventoryScript
		Debug.Log(GameObject.Find("Player").gameObject.transform.position.x);

	}
	
	// Update is called once per frame
	void Update () {
		
			
	}

	public void dropResource(GameObject resource){
		Instantiate (resource, new Vector3 (transform.position.x+1, transform.position.y, transform.position.z), Quaternion.identity);
		if (resource.gameObject.tag == "Food") {
            //itemsInInventoryScript.num_food -= 1;

            ItemsInInventory.num_food -= 1;

			Debug.Log (GameObject.Find ("food(Clone)").gameObject.transform.position);
			//	Debug.Log (itemsInInventoryScript.food);
		} else if (resource.gameObject.tag == "Wood") {
            //itemsInInventoryScript.num_wood -= 1;
            ItemsInInventory.num_wood -= 1;
		} else if (resource.gameObject.tag == "Water") {
            //itemsInInventoryScript.num_water -= 1;
            ItemsInInventory.num_water -= 1;
		}
	}

}
