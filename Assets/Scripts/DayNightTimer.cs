using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightTimer : MonoBehaviour {

    private Text timerText;

	// Use this for initialization
	void Start () {
        timerText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timerText.text = (System.DateTime.Now.Second % 24).ToString();
	}
}
