using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[SerializeField]
	private float fillAmnt;

	[SerializeField]
	private Image fillcontent; // Image Used to Fill bar


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		FillBar ();  //Updates the bar to fill the image	
	
	}

	private void FillBar() {
	
		fillcontent.fillAmount = CalculatedStats(100, 0, 100, 0, 1);
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

