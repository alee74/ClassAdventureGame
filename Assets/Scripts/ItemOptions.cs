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

    private GameObject player;

    // Use this for initialization
    void Start () {
        currItemTag = "null";
        player = GameObject.Find("Player");
	}

    public void OnClick()
    {
        gameObject.SetActive(true);
        //transform.position = Input.mousePosition;
        transform.position = new Vector3(Input.mousePosition.x - 25, Input.mousePosition.y, Input.mousePosition.z);
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
    {
        Vector3 newPos = new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z);
        switch (currItemTag)
        {
            case "Food":
                if (ItemsInInventory.num_food > 0)
                {
                    ItemsInInventory.num_food--;
                    Instantiate(food, newPos, Quaternion.identity);

                }
                break;
            case "Water":
                if (ItemsInInventory.num_water > 0)
                {
                    ItemsInInventory.num_water--;
                    Instantiate(water, newPos, Quaternion.identity);
                }
                break;
            case "Wood":
                if (ItemsInInventory.num_wood > 0)
                {
                    ItemsInInventory.num_wood--;
                    Instantiate(wood, newPos, Quaternion.identity);

                }
                break;
        }
    }

    public void OnUse()
    {
        Debug.Log(currItemTag + " will be used..");
    }
}
