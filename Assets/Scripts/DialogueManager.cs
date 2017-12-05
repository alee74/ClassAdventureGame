using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	public GameObject dBox;
	public GameObject dText;

	public bool active;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (active && Input.GetKeyDown(KeyCode.End)) 
			{
			dBox.SetActive(false);
			active = false;
			}
	}
}
