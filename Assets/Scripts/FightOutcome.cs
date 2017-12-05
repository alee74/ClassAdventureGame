using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightOutcome : MonoBehaviour {

    public static bool wasInFight;
    public static bool wonFight;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(wasInFight && !wonFight){
            ResourceInfo.setFoodStock(0);
            ResourceInfo.setWaterStock(0);
            ResourceInfo.setWoodStock(0);
            wasInFight = false;
        }
	}
}
