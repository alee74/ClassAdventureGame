using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour {
	private int water;
	private int food;
	private int wood;
	// Use this for initialization
	void Start () {
		water = 0;
		food = 0;
		wood = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D resource){
		if (resource.gameObject.tag == "Food") {
			Destroy (resource);
			food++;
		} else if (resource.gameObject.tag == "Water") {
			Destroy (resource);
			water++;
		} else if (resource.gameObject.tag == "Wood") {
			Destroy (resource);
			wood++;
		} else if (resource.gameObject.tag == "NPC") {
			//launch interaction with NPC, maybe check whether friendly or hostile instead?
		} else if(resource.gameObject.tag == "Event"){
			//launch event, OR could just tag as water or wood
		}
	}
}
