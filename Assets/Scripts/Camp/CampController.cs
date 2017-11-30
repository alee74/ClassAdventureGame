using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampController : MonoBehaviour {
    // Use this for initialization
    public static int day = -1;

    public Text foodNum;
    public Text waterNum;
    public Text woodNum;
    public Text dayNum;
	void Start () {
        if(day == -1)
        {
            firstDay();
        }
        day += 1;
        consumeResources();

    }

    void firstDay()
    {
        ResourceInfo.setFoodStock(1000);
        ResourceInfo.setWaterStock(1000);
        ResourceInfo.setWoodStock(100);
        updateUI();
    }

    void consumeResources()
    {
        ResourceInfo.addFoodStock(-100 * CharInfo.characters.Count);
        ResourceInfo.addWaterStock(-100 * CharInfo.characters.Count);
        updateUI();
    }
    void updateUI()
    {
        dayNum.text = day.ToString();
        foodNum.text = ResourceInfo.getFoodStock().ToString();
        waterNum.text = ResourceInfo.getWaterStock().ToString();
        woodNum.text = ResourceInfo.getWoodStock().ToString();
    }
}
