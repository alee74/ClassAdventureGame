using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemDisplay : MonoBehaviour {

    public Upgrade u;
    public UpgradeContainer uc;

    public Text upgradeName;
    public Text costText;

    public Transform nextPanel;

    public Transform upgradeMainPanel;

    private Button btn;
	// Use this for initialization
	void Start () {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(handleClick);
	}
	
    void handleClick()
    {
        if(uc != null)
        {
            nextPanel.GetComponent<UpgradeUIDisplayHandler>().setDisplay(uc);
        }
        else
        {
            if (u != null)
            {
                upgradeMainPanel.GetComponent<UpgradeUIMainPanel>().setDisplay(u);
            }
        }
    }

    public void setValue(Upgrade u)
    {
        this.u = u;
        this.uc = null;
        upgradeName.text = u.name;
        costText.text = "Cost: " + u.cost;
    }
    public void setValue(UpgradeContainer uc)
    {
        this.u = null;
        this.uc = uc;
        upgradeName.text = uc.name;
        costText.text = "";
    }

}
