using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOptions : MonoBehaviour {
    
    public static string currItemTag;

    // prefabs of different items to drop
    public GameObject food;
    public GameObject water;
    public GameObject wood;
    

    // Use this for initialization
    void Start () {
        currItemTag = "null";
	}

    public void OnClick()
    {
        gameObject.SetActive(true);
        //transform.position = Input.mousePosition;
        transform.position = new Vector3(Input.mousePosition.x + 100, Input.mousePosition.y, Input.mousePosition.z);
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
    {
        switch (currItemTag)
        {
            case "Food":
                //Instantiate(food, player.transform.position, Quaternion.identity);
                if (ItemsInInventory.num_food > 0)
                {
                    ItemsInInventory.num_food--;
                    Instantiate(food, new Vector3(0, 0, 0), Quaternion.identity);
                }
                break;
            case "Water":
                //Instantiate(water, player.transform.position, Quaternion.identity);
                if (ItemsInInventory.num_water > 0)
                {
                    ItemsInInventory.num_water--;
                    Instantiate(water, new Vector3(0, 0, 0), Quaternion.identity);
                }
                break;
            case "Wood":
                //Instantiate(wood, player.transform.position, Quaternion.identity);
                if (ItemsInInventory.num_wood > 0)
                {
                    ItemsInInventory.num_wood--;
                    Instantiate(wood, new Vector3(0, 0, 0), Quaternion.identity);
                }
                break;
        }
    }

    public void OnUse()
    {
        Debug.Log(currItemTag + " will be used..");
    }
}
