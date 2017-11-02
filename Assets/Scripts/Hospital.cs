using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hospital : MonoBehaviour {

public BarStats health;
	public GameObject Char;


    private int health;
	private int maxhealth = 100;

	// Use this for initialization


	// Update is called once per frame
	void Update () {

			Debug.Log("Health Restored");

		}

		// Resource menu Buy or upgrade health Items


		// Inventory



		// Restore Player Health





	}

	//add method to view inventory

	public void OnTriggerEnter2D(Collider2D col) {

        character = col.gameObject.GetComponent<CharInfo>();
        //character = GetComponent<CharInfo>();
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered Hospital");
        }

        if (character.health < 100)
        {
            character.health += character.maxHealth - character.health;
            Debug.Log("Player healed");
        }
        else
        {

}
