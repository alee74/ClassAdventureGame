using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePlacer : MonoBehaviour {



	public GameObject foodPrefab;
	public GameObject woodPrefab;
	public GameObject waterPrefab;

	// Use this for initialization
	void Start () {
		Vector2 pos = new Vector2(-500, -500);
		for (int i = -500; i < 500; i++) {
				for(int j = -500; j < 500; j++){
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
