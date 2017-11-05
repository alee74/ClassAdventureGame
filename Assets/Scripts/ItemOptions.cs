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
	
	// Update is called once per frame
	void Update () {

	}

    public void OnClick()
    {
        gameObject.SetActive(true);
        transform.position = Input.mousePosition;
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
    {
        Debug.Log(currItemTag + " will be dropped");
        switch (currItemTag)
        {
            case "Food":
                //Instantiate(food, player.transform.position, Quaternion.identity);
                Instantiate(food, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case "Water":
                //Instantiate(water, player.transform.position, Quaternion.identity);
                Instantiate(water, new Vector3(0, 0, 0), Quaternion.identity);

                break;
            case "Wood":
                //Instantiate(wood, player.transform.position, Quaternion.identity);
                Instantiate(wood, new Vector3(0,0,0), Quaternion.identity);
                break;
        }
    }

    public void OnUse()
    {
        Debug.Log(currItemTag + " will be used..");
    }
}
