using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EventHandler : MonoBehaviour {

    public Text foodVal;
    public Text woodVal;
    public Text waterVal;
    public Text eventText;
    public Button next;

    private List<CampEvent> events;

    void Start()
    {
        next.onClick.AddListener(handleNext);
    }

    public void displayEvents(List<CampEvent> e)
    {
        events = e;
        if(events.Count > 0)
        {
            gameObject.SetActive(true);
            CampEvent currentEvent = events[0];
            events.Remove(currentEvent);
            displayEvent(currentEvent);
        }
    }

    public void displayEvent(CampEvent e)
    {
        eventText.text = e.description;
        foodVal.text = e.food.ToString();
        woodVal.text = e.wood.ToString();
        waterVal.text = e.water.ToString();
    }

    private void handleNext()
    {
        if (events.Count > 0)
        {
            CampEvent currentEvent = events[0];
            events.Remove(currentEvent);
            displayEvent(currentEvent);
        }
        else
        {
            if(ResourceInfo.getFoodStock() < 0 || ResourceInfo.getWaterStock() < 0 || CharInfo.characters.Count == 0)
            {
                CampController.day = -1;
                SceneManager.LoadScene("GameOver");
            }
            gameObject.SetActive(false);
        }
    }

}
