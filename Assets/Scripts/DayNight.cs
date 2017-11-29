using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour {
	
	private Image dayNight; 
	public float timer; 
	public float dayTimer;
	private Color yellow = new Color(189f/255f,189f/255f,85f/255f, 0f);
	private Color orange = new Color (189f/255f,143f/255f, 85f/255f, 0f);
	private Color purple = new Color (123f/255f, 85f/255f, 189f/255f, 0f);

	// Use this for initialization
	void Start () {
		timer = 20f;
		dayTimer = 5f;
		dayNight = GetComponent<Image> ();  
		dayNight.color = new Color (0f, 0f, 0f, 0f); 

	}
	//TODO: two separate timers - one for day, one for night
	
	// Update is called once per frame
	void Update () {
		dayTimer -= Time.deltaTime;
		if (dayTimer < 0f) {
			timer -= Time.deltaTime;
			yellow.a = .5f;
			orange.a = .5f;
			purple.a = .5f;

			if (timer < 15f && timer > 10f) {
				dayNight.color = Color.Lerp (yellow,orange,timer-10f);
			}
			if (timer < 10f && timer > 5f) {
				dayNight.color = Color.Lerp (orange, purple, timer - 5f);
			}
			if (timer < 5f) {
				dayNight.color = purple;
			}
			if (timer < 0f) {
				dayTimer = 10f;
				dayNight.color = new Color(0f,0f,0f,0f);
		 		timer = 20f;
			}
		}
		
	}
}
