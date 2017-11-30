using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWater : MonoBehaviour {

	private GameObject player;
	public Transform waterBottle; 
	private GameObject lakeTile = null;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player"); 
	}
	
	// Update is called once per frame
	void Update() {
		if (lakeTile != null && Input.GetButtonDown ("GetWater")) {
			Vector3 newpos = new Vector3 (player.transform.position.x, player.transform.position.y - 1f, player.transform.position.z);
			Instantiate (waterBottle, newpos, Quaternion.identity);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Lake") {
			lakeTile = col.gameObject;
			Debug.Log ("touching lake");
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		lakeTile = null;
	}
		
}
