using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FightOutcome {

    public static Character currentlyFighting;
	public static Character NPCcurrentlyFighting;
    public static bool wasInFight;
    public static bool wonFight;

    // Use this for initialization


    // Update is called once per frame
    static void LoseItemsAfterLosingFight () {
        if(wasInFight && !wonFight){
            ResourceInfo.setFoodStock(0);
            ResourceInfo.setWaterStock(0);
            ResourceInfo.setWoodStock(0);
            wasInFight = false;
        }
	}
}
