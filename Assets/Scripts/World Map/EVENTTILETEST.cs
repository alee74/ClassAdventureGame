using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVENTTILETEST : MonoBehaviour {

    private GameObject eventTile;
    private GameObject eventTile2;
    private EventTile eventTileScript;
    private EventTile eventTileScript2;
	// Use this for initialization
	void Start () {
        eventTile = GameObject.Find("EventTile");
        eventTileScript = eventTile.GetComponent<EventTile>();
        eventTileScript.tileEvent = EncounterCharacterEvent.GenerateRandom();
        eventTile2 = GameObject.Find("EventTile2");
        eventTileScript2 = eventTile2.GetComponent<EventTile>();
        eventTileScript2.tileEvent = EncounterCharacterEvent.GenerateRandom();
        Debug.Log(eventTileScript2.tileEvent.description);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
