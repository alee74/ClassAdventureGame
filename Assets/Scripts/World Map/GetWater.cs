using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWater : MonoBehaviour {

	public GameObject lake = null; 
	private AudioSource aud; 
	public AudioClip grabWater; 

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource> (); 
	}

	void OnCollisionEnter2D(Collision2D col) {
		lake = col.gameObject;

		if (lake.tag == "Lake") {
			MaxWater maxWaterScript = lake.GetComponent<MaxWater> ();
			if (maxWaterScript.currentWater > 0) {
				//add to inventory then dec
				ItemsInInventory.num_water++;
				maxWaterScript.waterTaken (1);
				aud.clip = grabWater;
				aud.Play ();
			}
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		lake = null;
	}
}
		

