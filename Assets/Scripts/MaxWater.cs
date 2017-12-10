using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxWater : MonoBehaviour {

	public int currentWater;

	// Use this for initialization
	void Start () { 
		currentWater = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void waterTaken (int amount) {
		if (currentWater > 0) {
			currentWater -= amount;
		}
	}
}
