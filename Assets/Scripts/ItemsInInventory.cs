using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsInInventory : MonoBehaviour {
    private Text dayText;

    private Text waterTxt;
    public static int num_water = 0;

    private Text foodTxt;
    public static int num_food = 0;

    private Text woodTxt;
    public static int num_wood = 0;

	// Use this for initialization
	void Start () {
        dayText = GameObject.Find("DayText").GetComponent<Text>();
        waterTxt = GameObject.Find("WaterTxt").GetComponent<Text>();
        foodTxt = GameObject.Find("FoodTxt").GetComponent<Text>();
        woodTxt = GameObject.Find("WoodTxt").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        dayText.text = "Day " + CampController.day;
        waterTxt.text = num_water.ToString();
        foodTxt.text = num_food.ToString();
        woodTxt.text = num_wood.ToString();
	}

    public static int GetTotalItems()
    {
        return num_water + num_food + num_wood;
    }

    public static void AddInventoryItemsToCampResource()
    {
        ResourceInfo.addFoodStock(num_food);
        ResourceInfo.addWaterStock(num_water);
        ResourceInfo.addWoodStock(num_wood);
        
        num_food = 0; num_water = 0; num_wood = 0;

    }
}
