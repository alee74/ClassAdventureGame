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

    public GameObject eventPanel;
    private List<CampEvent> campEvents;

	void Start () {
        day += 1;
        if (day == 100)
        {
            //Win Game
        }
        if (day == 0)
        {
            firstDay();
        }
        else
        {
            CampEvent ce = consumeResources();
            campEvents = EventSystem.GetCampEvents(day, 5, 0);
            campEvents.Add(ce);
            foreach (CampEvent camp in campEvents)
            {
                Debug.Log(camp.food);
                ResourceInfo.addFoodStock(camp.food);
                ResourceInfo.addWaterStock(camp.water);
                ResourceInfo.addWoodStock(camp.wood);
            }
            displayCampEvents();
            updateUI();
        }
        

    }

    void displayCampEvents()
    {
        eventPanel.SetActive(true);
        eventPanel.GetComponent<EventHandler>().displayEvents(campEvents);
    }

    void firstDay()
    {
        ResourceInfo.setFoodStock(1000);
        ResourceInfo.setWaterStock(1000);
        ResourceInfo.setWoodStock(100);
        updateUI();
    }

    CampEvent consumeResources()
    {
        CampEvent ce = new CampEvent();
        ce.food = -100 * CharInfo.characters.Count;
        ce.water = -100 * CharInfo.characters.Count;
        ce.message = "Your characters consumed resources.";
        return ce;
    }
    void updateUI()
    {
        dayNum.text = day.ToString();
        foodNum.text = ResourceInfo.getFoodStock().ToString();
        waterNum.text = ResourceInfo.getWaterStock().ToString();
        woodNum.text = ResourceInfo.getWoodStock().ToString();
    }
}
