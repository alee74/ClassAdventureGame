using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hospital : MonoBehaviour {

public BarStats health;
	public GameObject Char;



	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)) {

			Debug.Log("Health Restored");
		
		}

		// Resource menu Buy or upgrade health Items


		// Inventory



		// Restore Player Health
		if(health.currentVal > health.maxVal) {

			FindObjectOfType<HealthBar> ().FillBar ();
		}


	}
		public void ReplinishHealth(CharInfo character)
		{
			character.addHealth(1);
		}



	//add method to view inventory


}


