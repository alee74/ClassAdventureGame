using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWater : MonoBehaviour {

	private GameObject player;
	public GameObject lake = null; 
//	private GameObject lakeTile = null;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player"); 
	}
	
	// Update is called once per frame
//	void Update() {
//		if ((player.transform.position - lake.transform.position).magnitude < 2.5f 
////			&& (player.transform.position - lake.transform.position).magnitude < 3f
//			&& Input.GetButtonDown("GetWater")) { /*&&  Input.GetButtonDown ("GetWater")*/ 
//				Instantiate (waterBottle, player.transform.position, Quaternion.identity);
//
//		}
//	}
//}

	void OnTriggerEnter2D(Collider2D col) {
		lake = col.gameObject; 
		var currentWater = lake.GetComponent<MaxWater> (); 
		if (col.tag == "Lake" && currentWater != null) {
			ItemsInInventory.num_water++;
			currentWater.waterTaken (1);
			Debug.Log (currentWater);
			//lakeTile = col.gameObject;
		}
	}
//
//	void OnTriggerExit2D(Collider2D col) {
//		lakeTile = null;
//	}


	void OnTriggerExit2D(Collider2D col) {
		//	touchingTree = false;
		lake = null;
	}
}
		

