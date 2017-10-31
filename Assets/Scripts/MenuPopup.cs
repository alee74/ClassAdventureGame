using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MenuPopup : MonoBehaviour {

	public GameObject menuPopup;
	private bool isShowing;

	// Use this for initialization
	void Start () {
		isShowing = false;
		menuPopup.SetActive (isShowing); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			isShowing = !isShowing;
			menuPopup.SetActive (isShowing); 
		}
	}
}
