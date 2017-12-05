using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharDisplayMainPanel : MonoBehaviour {

    public Text healthText;
    public Text characterText;
    public Text staminaText;
    public Text strengthText;

    private Character currentCharacter;

    void Start ()
    {
        updatePanel();
    }


    public void updateCurrentCharacter(Character c)
    {
        currentCharacter = c;
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
        }
        else
        {
            healthText.text = "";
            characterText.text = "";
            staminaText.text = "";
            strengthText.text = "";
        }

    }

}
