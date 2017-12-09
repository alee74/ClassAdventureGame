using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVENTTILETEST : MonoBehaviour {

    private GameObject eventTile;
    private EventTile eventTileScript;

	// Use this for initialization
	void Start () {
        eventTile = GameObject.Find("EventTile");
        eventTileScript = eventTile.GetComponent<EventTile>();
        eventTileScript.tileEvent = EncounterCharacterEvent.GenerateRandom();

        Debug.Log(eventTileScript.tileEvent.description);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
