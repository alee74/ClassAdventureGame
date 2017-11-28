using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour {

	public int treeHealth = 5; //full health
	public Transform logs; 
	private GameObject tree; 
	public float currentHp;  
	private GameObject player; 

	// Use this for initialization
	void Start () {
		tree = GameObject.Find ("Tree");
		currentHp = treeHealth; 
		player = GameObject.Find ("Player");
	}

	public void DamageTaken (int amount) {
		currentHp -= amount; 
		if (currentHp <= 0) {
			currentHp = 0;
			Destroy (tree); 
			Vector3 newpos = new Vector3 (player.transform.position.x, player.transform.position.y - 2f, player.transform.position.z);
			Instantiate (logs, newpos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
