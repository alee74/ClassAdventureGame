using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoForWorldMap : MonoBehaviour {
    
    private Character currChara;
    private Text nameText;
    
	// Use this for initialization
	void Start () {
        currChara = CharInfo.getCurrentCharacter();
        nameText = GameObject.Find("Name").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        nameText.text = currChara.name;
	}
}
