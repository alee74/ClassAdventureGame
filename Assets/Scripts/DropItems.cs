using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour {
	public GameObject resource;
	private ItemsInInventory itemsInInventoryScript;
	// Use this for initialization
	void Start () {
	//	itemsInInventoryScript = GameObject.Find("Inventory").GetComponent<ItemsInInventory>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			Instantiate (resource, new Vector3 (transform.position.x+1, transform.position.y, transform.position.z), Quaternion.identity);
			if (resource.gameObject.tag == "Food") {
			//	itemsInInventoryScript.food -= 1;
			//	Debug.Log (itemsInInventoryScript.food);
			} else if (resource.gameObject.tag == "Wood") {
			//	itemsInInventoryScript.wood -= 1;
			} else if (resource.gameObject.tag == "Water") {
			//	itemsInInventoryScript.water -= 1;
			}
		}
	}
}
