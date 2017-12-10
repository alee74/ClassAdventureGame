using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharDisplayHospital : MonoBehaviour
{
    public Character character;

    private Transform characterMainPanel;
    public Text characterName;
    public Text healthPoints;
    // Use this for initialization
    void Start()
    {
        characterMainPanel = transform.parent.parent.parent.parent.Find("CharacterMainPanel");
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(displayCharacter);
        if (character != null)
        {
            characterName.text = character.name;
            healthPoints.text = "HP: " + character.health + "/" + character.getMaxHealth();
        }
        else
        {
            Debug.Log("Character is null");
        }

    }
    void displayCharacter()
    {
        characterMainPanel.GetComponent<CharDisplayMainPanelHospital>().updateCurrentCharacter(character);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
