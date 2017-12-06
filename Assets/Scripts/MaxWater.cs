using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxWater : MonoBehaviour {

	public int maxWater = 5; //full amount of water
	public int currentWater;
	private GameObject player; 

	// Use this for initialization
	void Start () {
		currentWater = maxWater; 
		player = GameObject.Find ("Player"); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void waterTaken (int amount) {
		currentWater -= amount; 
		if (currentWater <= 0) {
			currentWater = 0; 
		}
	}
}
