using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenTest : MonoBehaviour {

    List<CampEvent> campEvents;
    List<GameObject> eventTiles;

	// Use this for initialization
	void Start () {
        ResourceInfo.setFoodStock(100);
        ResourceInfo.setWaterStock(100);
        ResourceInfo.setWoodStock(100);
        campEvents = new List<CampEvent>(EventSystem.GetCampEvents(0, 20, 10));
        eventTiles = new List<GameObject>(EventSystem.GetEventTiles(0, 20, 10));
        Weather weather = EventSystem.GetWeather(0);

        foreach(CampEvent ce in campEvents)
        {
            Debug.Log("Camp Event Message: \"" + ce.message 
                + "\"\nFood Effect: " + ce.food 
                + "\nWater Effect: " + ce.water 
                + "\nWood Effect: " + ce.wood);
        }
        foreach (GameObject go in eventTiles)
        {
            EventTile et = go.GetComponent<EventTile>();
            EncounterCharacterEvent ece = (EncounterCharacterEvent)et.tileEvent;
            Debug.Log("Event Name: \"" 
                + ece.name + "\"\nDescription: " 
                + ece.description + "\nDialog: " 
                + ece.encounterDialog + "\nCharacter Name: " 
                + ece.character.name + "\nHostile?: " 
                + ece.character.isHostile + "\nmaxHealth: " 
                + ece.character.getMaxHealth() + "\nmaxStamina: " 
                + ece.character.getMaxStamina() + "\nmaxStrength: " 
                + ece.character.getMaxStrength());
        }
        Debug.Log(weather.ToString());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
