using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayNight : MonoBehaviour {
	
	private Image dayNight; 
	public float lerpControl;
	public float duration; 
	private Color day = new Color (0f, 0f, 0f, 0f);
	private Color night = new Color (0f, 0f, 0f, 1f); 

	// Use this for initialization
	void Start () {
		lerpControl = 0f; 
		dayNight = GetComponent<Image> ();  
		dayNight.color = new Color (0f, 0f, 0f, 0f); 
	}
	//TODO: two separate timers - one for day, one for night
	
	// Update is called once per frame
	void Update () {
		lerpControl += Time.deltaTime/duration;
		dayNight.color = Color.Lerp (day, night, lerpControl);
		if (dayNight.color == night) {
            ItemsInInventory.AddInventoryItemsToCampResource();
			SceneManager.LoadScene ("TestCamp");
		}
	}
}
