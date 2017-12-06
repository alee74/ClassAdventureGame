using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoForWorldMap : MonoBehaviour {
    
    private Character currChara;
    private Text nameText;
    private Text strengthText;
    private Text staminaText;
    private Text healthText;
    
	// Use this for initialization
	void Start () {
        currChara = CharInfo.getCurrentCharacter();
        nameText = GameObject.Find("Name").GetComponent<Text>();
        strengthText = GameObject.Find("Strength").GetComponent<Text>();
        staminaText = GameObject.Find("Stamina").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        nameText.text = currChara.name;
        strengthText.text = "Strength: " + currChara.strength.ToString();
        staminaText.text = "Stamina: " + currChara.stamina.ToString();
        healthText.text = currChara.health.ToString();
	}
}
