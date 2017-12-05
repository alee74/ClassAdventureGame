using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutWood : MonoBehaviour {

	private GameObject player;
	private GameObject tree = null;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	/*
	// Update is called once per frame
	void OnTriggerStay2D(Collider2D col) {
		// (player.transform.position - target.transform.position).magnitude
		Debug.Log("wood");
		touchingTree = false;
		if (Input.GetButtonDown ("GetWood")) {
			var treehealth = col.gameObject.GetComponent<TreeHealth> ();
			if (treehealth != null) {
				treehealth.DamageTaken (1); 
			}
		}
	}
	*/

	void Update() {
		if (tree != null && Input.GetButtonDown ("GetWood")) {
			Debug.Log ("IM TOUCHING IT");
			var treehealth = tree.GetComponent<TreeHealth> ();
			if (treehealth != null) {
				treehealth.DamageTaken (1); 
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
	//	touchingTree = true;
		tree = col.gameObject; 
	}

	void OnTriggerExit2D(Collider2D col) {
	//	touchingTree = false;
		tree = null;
	}
		

//	void OnCollisionEnter2D(Collision2D col) {
//		//if player is within a certain distance of tree and presses "GetWood", decrement treehealth
//
//			var chop = col.gameObject; 
//			var treehealth = chop.GetComponent<TreeHealth> (); 
//			if ((treehealth != null) /*&& Input.GetButtonDown("GetWood")*/) {		
//				treehealth.DamageTaken (1);
//			}
//		}
//	}
}
