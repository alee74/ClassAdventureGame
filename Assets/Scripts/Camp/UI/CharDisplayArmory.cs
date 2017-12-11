using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharDisplayArmory : MonoBehaviour {
    public Character character;

    public Transform characterMainPanel;
    public Text characterName;
    public Text healthPoints;
    public Text staminaPoints;
	// Use this for initialization
	void Start () {
        characterMainPanel = transform.parent.parent.parent.parent.Find("CharacterMainPanel");
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(displayCharacter);
        if (character != null)
        {
            characterName.text = character.name;
            healthPoints.text = "HP: " + character.health + "/" + character.getMaxHealth();
            staminaPoints.text = "SP: " + character.stamina + "/" + character.getMaxStamina();
        }
        else
        {
            Debug.Log("Character is null");
        }
        
	}
    void displayCharacter()
    {
        characterMainPanel.GetComponent<CharDisplayMainPanel>().updateCurrentCharacter(character);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
