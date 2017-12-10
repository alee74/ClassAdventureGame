using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampPopulator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CharInfo.characters = new List<Character>();
        for(int i = 0; i < 3; i++)
        {
            Character c = Character.GenerateRandom(Random.value);
            CharInfo.characters.Add(c);
        }
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
