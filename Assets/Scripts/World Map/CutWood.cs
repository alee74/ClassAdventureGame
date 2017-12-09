using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutWood : MonoBehaviour {

	private GameObject tree = null;
	private AudioSource aud; 
	public AudioClip cut_wood; 

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource> (); 
	}

	void Update() {
		if (tree != null && Input.GetButtonDown ("GetWood")) {
			Debug.Log ("IM TOUCHING IT");
			var treehealth = tree.GetComponent<TreeHealth> ();
			if (treehealth != null) {
				treehealth.DamageTaken (1); 
				aud.clip = cut_wood;
				aud.Play ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		tree = col.gameObject; 
	}

	void OnTriggerExit2D(Collider2D col) {
		tree = null;
	}

}
