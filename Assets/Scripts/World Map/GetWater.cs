using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWater : MonoBehaviour {

	private GameObject player;
	public GameObject lake = null; 

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	void OnCollisionEnter2D(Collision2D col) {
		lake = col.gameObject;

		if (lake.tag == "Lake") {
			MaxWater maxWaterScript = lake.GetComponent<MaxWater> ();
			if (maxWaterScript.currentWater > 0) {
				//add to inventory then dec
				ItemsInInventory.num_water++;
				maxWaterScript.waterTaken (1);
			}
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		lake = null;
	}
}
		

