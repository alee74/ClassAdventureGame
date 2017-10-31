using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MenuPopup : MonoBehaviour {

	public GameObject menuPopup;
	bool menuIsUp;

	// Use this for initialization
	void Start () {
		menuPopup.SetActive (false); 
		menuIsUp = false; 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			if (menuIsUp == false) {
				menuPopup.SetActive (true); 
				menuIsUp = true; 
			}
		}
		if (Input.GetButtonDown("Inventory")){
			if (menuIsUp == true) {
				menuPopup.SetActive (false); 
				menuIsUp = false; 
			}
		}
	}
}
