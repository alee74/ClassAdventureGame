using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContainer  {

    public string name;
    public UpgradeContainer[] upgradeContainer;
    public Upgrade[] upgrades;

    public UpgradeContainer(string name, UpgradeContainer[] upgradeContainer, Upgrade[] upgrades)
    {
        this.name = name;
        this.upgradeContainer = upgradeContainer;
        this.upgrades = upgrades;
    }
    public UpgradeContainer(string name, UpgradeContainer[] upgradeContainer)
    {
        this.name = name;
        this.upgradeContainer = upgradeContainer;
    }
    public UpgradeContainer(string name, Upgrade[] upgrades)
    {
        this.name = name;
        this.upgrades = upgrades;
    }



    public static UpgradeContainer firstContainer()
    {
        UpgradeContainer bar = new UpgradeContainer("Barracks", new Upgrade[] { new Upgrade("Increase Size", "Increase the size of your barracks allowing more characters to sleep there.", 100) });
        UpgradeContainer kit = new UpgradeContainer("Kitchen", new Upgrade[] { new Upgrade("Improve Storage", "Allows less food to go to waste.", 200), new Upgrade("Improve Water Collection", "Allows less water to go to waste.", 300) });
        return new UpgradeContainer("first", new UpgradeContainer[] { new UpgradeContainer("Buildings", new UpgradeContainer[] { bar, kit }) });
    }
}
