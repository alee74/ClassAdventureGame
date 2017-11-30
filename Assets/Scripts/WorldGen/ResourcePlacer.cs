using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePlacer : MonoBehaviour {

	public float resourceRate = 0.99f;
	public int resourceRadius = 500;

	public GameObject foodPrefab;
	public GameObject woodPrefab;
	public GameObject waterPrefab;

	private List<GameObject> events; 

	// Use this for initialization
	void Start () {
		//print (EventSystem.GetEventTiles (0, 20, 1));
		//events = new List<GameObject>(EventSystem.GetEventTiles(0,20,1));
		Vector2 pos = new Vector2(-resourceRadius, -resourceRadius);
		for (int i = -resourceRadius; i < resourceRadius; i++) {
			for(int j = -resourceRadius; j < resourceRadius; j++){
					if (Random.Range (0f, 1f) > 0.99f){
						placeResource (pos);
					}
				pos.y = j;
				}
			pos.x = i;
		}
	}


	void placeResource(Vector2 pos){
		var ran = Random.Range (0f, 1f);
		if (ran < 0.33f) {
			Instantiate (foodPrefab, pos, Quaternion.identity);
		} else if (ran > 0.33f && ran < 0.66f) {
			Instantiate (woodPrefab, pos, Quaternion.identity);
		} else {
			Instantiate (waterPrefab, pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
