using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoForWorldMap : MonoBehaviour {
    
    private CharInfo currChara;
    private Text nameText;
    
	// Use this for initialization
	void Start () {
        currChara = CharInfo.characters[0];
        nameText = GameObject.Find("Name").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        //nameText.text = currChara.health.ToString();
        nameText.text = "My name is: " + currChara.name;
        //nameText.text = "hm";
        //Debug.Log(currChara.name);
	}
}
