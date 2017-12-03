using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hospital : MonoBehaviour {


	public BarStats health;



	private void Awake() {
		health.Intialize();
	
	}

	// Update is called once per frame
	void Update () {

		health.Intialize();


		if (Input.GetKeyDown (KeyCode.H)) {

			health.currentVal -= 10;

		}
		// Resource menu Buy or upgrade health Items

	
		// Inventory



		// Restore Player Health
	




	}

	//add method to view inventory

	public void OnTriggerEnter2D(Collider2D col) {

        //Character character = CharInfo.getCurrentCharacter();
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Hospital");
		
			if ( health.currentVal != health.maxVal) //character.health < character.getMaxHealth()
            {
				health.currentVal = health.maxVal;
				//character.health += character.getMaxHealth() - character.health;
				//FindObjectOfType<HealthBar> ().minBar (); 
				//health.currentVal += FindObjectOfType<HealthBar> ().fillAmnt;
				Debug.Log("Player healed" + FindObjectOfType<HealthBar> ().fillAmnt);
				Debug.Log("Your HP Is Full" + health.currentVal + health.maxVal);
            }
            else
            {
				Debug.Log("Your HP Is Full" + health.currentVal + health.maxVal);
            }
        }
    }

}
