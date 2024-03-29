﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour {


	public Character charHealth;
	public float fillAmnt;

	public Image fillcontent; // Image Used to Fill bar


	public float Maxvalue { get; set; }

	public float Value { 
		set {
			fillAmnt = CalculatedStats (value, 0, Maxvalue, 1, 0); 
			Debug.Log ("Value" + value);
		} 
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


		FillBar ();  //Updates the bar to fill the image	


	}
	// Fill content and fill amount
	public void FillBar() {

		if (fillAmnt != fillcontent.fillAmount) {
			fillcontent.fillAmount = fillAmnt; 
			Debug.Log ("Bar Filling" + fillcontent.fillAmount);
		}
	}





	// value - Takes current Health 
	// max - Is the max health
	// min = 0
	// retMin = o 
	// retMax = 1  


	private float CalculatedStats(float value, float min, float max, float retMax, float retMin) {  


									
		return (value - min) * (retMax - retMin) / (max - min) + retMin;

	}

}

