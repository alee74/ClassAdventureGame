using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour {

	// per cycle resource consumption
	private int waterConsumption;
	private int foodConsumption;

	// Use this for initialization
	void Start () {
		getWaterConsumption();
		getFoodConsumption();
	}

	void DailyConsumption () {
		ResourceInfo.addWaterStock(-waterConsumption);
		ResourceInfo.addFoodStock(-foodConsumption);
	}

	public int getWaterConsumption () {
		// code to get total consumption values from list of characters
		return waterConsumption;
	}
	public int getFoodConsumption () {
		// code to get total consumption values from list of characters
		return foodConsumption;
	}
}
