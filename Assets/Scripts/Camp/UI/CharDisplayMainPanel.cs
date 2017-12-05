using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharDisplayMainPanel : MonoBehaviour {

    public Text healthText;
    public Text characterText;
    public Text staminaText;
    public Text strengthText;

    public Button setCharacter;

    private Character currentCharacter;

    private int characterPosition;

    void Start ()
    {
        updatePanel();
        setCharacter.onClick.AddListener(changeCharacter);
    }


    public void updateCurrentCharacter(Character c)
    {
        currentCharacter = c;
        characterPosition = CharInfo.characters.IndexOf(currentCharacter);
        updatePanel();
    }

    private void updatePanel()
    {
        if (currentCharacter  != null)
        {
            characterText.text = currentCharacter.name;
            healthText.text = "Health: " + currentCharacter.health + "/" + currentCharacter.getMaxHealth();
            staminaText.text = "Stamina: " + currentCharacter.stamina + "/" + currentCharacter.getMaxStamina();
            strengthText.text = "Strength: " + currentCharacter.strength + "/" + currentCharacter.getMaxStrength();

            if (CharInfo.currentCharacter == characterPosition)
            {
                setCharacter.interactable = false;
            }
            else
            {
                setCharacter.interactable = true;
            }
        }
        else
        {
            healthText.text = "";
            characterText.text = "";
            staminaText.text = "";
            strengthText.text = "";
        }

    }

    private void changeCharacter()
    {
        CharInfo.currentCharacter = characterPosition;
        updatePanel();
    }

}
