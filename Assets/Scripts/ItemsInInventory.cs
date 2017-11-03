using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsInInventory : MonoBehaviour {

    private Text waterTxt;
    public int num_water = 0;

    private Text foodTxt;
    public int num_food = 0;

    private Text woodTxt;
	public int num_wood = 0;

	// Use this for initialization
	void Start () {
        waterTxt = GameObject.Find("WaterTxt").GetComponent<Text>();
        foodTxt = GameObject.Find("FoodTxt").GetComponent<Text>();
        woodTxt = GameObject.Find("WoodTxt").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        waterTxt.text = "x" + num_water;
        foodTxt.text = "x" + num_food;
        woodTxt.text = "x" + num_wood;
	}
}
