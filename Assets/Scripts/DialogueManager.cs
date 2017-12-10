using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public GameObject dBox;
	public Text dText;


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
	public void ShowBox(string dialogue)
	{
		active = true;
		dBox.SetActive(true);
		dText.text = dialogue;
	}

}
