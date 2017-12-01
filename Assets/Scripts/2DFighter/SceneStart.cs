using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Character character = CharInfo.getCurrentCharacter();
		Text player = GameObject.Find("PlayerName").GetComponent<Text>();
		player.text = character.name;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
