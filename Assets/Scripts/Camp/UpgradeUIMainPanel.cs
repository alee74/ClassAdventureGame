using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIMainPanel : MonoBehaviour {


    Upgrade display;

    public Text upgradeName;
    public Text descriptionName;
    public Text costText;

    public Button buyBtn;
    void Start()
    {
        refresh();
    }
    public void setDisplay(Upgrade u)
    {
        display = u;
        refresh();
    }

    private void refresh()
    {
        if(display == null)
        {
            upgradeName.text = "";
            descriptionName.text = "";
            costText.text = "";
            buyBtn.interactable = false;
        }
        else
        {
            buyBtn.interactable = true;
            upgradeName.text = display.name;
            descriptionName.text = display.description;
            costText.text = "Cost: " + display.cost + " wood";
        }

    }
}
