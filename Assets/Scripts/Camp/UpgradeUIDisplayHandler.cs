using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIDisplayHandler : MonoBehaviour {

    UpgradeContainer display;
    public Transform prefab;
    private List<Transform> children = new List<Transform>();
    public bool first = false;
    public Transform nextList;
    public Transform upgradeMainPanel;

    void Start()
    {
        if (first)
        {
            setDisplay(UpgradeContainer.firstContainer());
        }
    }
    private void displayInfo()
    {
        if (display != null)
        {
            if(display.upgradeContainer != null)
            {
                foreach (UpgradeContainer uc in display.upgradeContainer)
                {
                    Transform newItem = Instantiate(prefab, gameObject.transform, false);
                    UpgradeItemDisplay fas = newItem.GetComponent<UpgradeItemDisplay>();
                    fas.setValue(uc);
                    fas.nextPanel = nextList;
                    fas.upgradeMainPanel = upgradeMainPanel;
                    children.Add(newItem);
                }
            }
            if(display.upgrades != null)
            {
                foreach (Upgrade u in display.upgrades)
                {
                    Transform newItem = Instantiate(prefab, gameObject.transform, false);
                    UpgradeItemDisplay fas = newItem.GetComponent<UpgradeItemDisplay>();
                    fas.setValue(u);
                    fas.nextPanel = nextList;
                    fas.upgradeMainPanel = upgradeMainPanel;
                    children.Add(newItem);
                }
            }
        }
    }

    public void setDisplay(UpgradeContainer uc)
    {
        foreach(Transform child in children)
        {
            Destroy(child.gameObject);
        }
        display = uc;
        children = new List<Transform>();
        displayInfo();
    }
}
