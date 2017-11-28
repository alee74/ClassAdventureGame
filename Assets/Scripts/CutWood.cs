using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutWood : MonoBehaviour {

	private GameObject player;
	private GameObject tree;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		tree = GameObject.Find ("Tree");
		Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
		Vector3 treePos = new Vector3 (tree.transform.position.x, tree.transform.position.y, tree.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		// (player.transform.position - target.transform.position).magnitude
		if (((player.transform.position - tree.transform.position).magnitude < 2f) && Input.GetButtonDown ("GetWood")) {
			var treehealth = tree.GetComponent<TreeHealth> ();
			if (treehealth != null) {
				treehealth.DamageTaken (1); 
			}
		}
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
