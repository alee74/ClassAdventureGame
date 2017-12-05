using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharDisplayMainPanelHospital : MonoBehaviour
{

    public Text healthText;
    public Text characterText;

    public Button healCharacterBtn;

    private Character currentCharacter;

    private int characterPosition;

    void Start()
    {
        updatePanel();
        healCharacterBtn.onClick.AddListener(healCharacter);
    }


    public void updateCurrentCharacter(Character c)
    {
        currentCharacter = c;
        characterPosition = CharInfo.characters.IndexOf(currentCharacter);
        updatePanel();
    }

    private void updatePanel()
    {
        if (currentCharacter != null)
        {
            characterText.text = currentCharacter.name;
            healthText.text = "Health: " + currentCharacter.health + "/" + currentCharacter.getMaxHealth();

            if (currentCharacter.health == currentCharacter.getMaxHealth())
            {
                healCharacterBtn.interactable = false;
            }
            else
            {
                healCharacterBtn.interactable = true;
            }
        }
        else
        {
            healthText.text = "";
            characterText.text = "";
        }

    }

    private void healCharacter()
    {
        currentCharacter.health = currentCharacter.getMaxHealth();
        updatePanel();
    }

}

