using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVENTTILETEST : MonoBehaviour {

    private GameObject eventTile;
    //private GameObject eventTile2;
    private EventTile eventTileScript;
    //private EventTile eventTileScript2;

	// Use this for initialization
	void Start () {
        //after i push this, max carry wont work.... bc of naming of eventtiles
        eventTile = GameObject.Find("Test1");
        eventTileScript = eventTile.GetComponent<EventTile>();
        eventTileScript.tileEvent = EncounterCharacterEvent.GenerateRandom();

        /*
        eventTile2 = GameObject.Find("Test2");
        eventTileScript2 = eventTile2.GetComponent<EventTile>();
        eventTileScript2.tileEvent = EncounterCharacterEvent.GenerateRandom();
        */

        Debug.Log(eventTileScript.tileEvent.character.name );
        Debug.Log("Hostile = " + eventTileScript.tileEvent.character.isHostile);
        Debug.Log(eventTileScript.tileEvent.description);
        //Debug.Log(eventTileScript2.tileEvent.description);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
