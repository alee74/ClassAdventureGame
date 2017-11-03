using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulatePopUp : MonoBehaviour {

    public Image popUp;
    private Text popUpTxt;

	// Use this for initialization
	void Start () {
        popUpTxt = popUp.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FillPopUpWithItemOptions()
    {
        // here we can check what was selected based on tags......!
        popUpTxt.text = gameObject.tag + " was selected.";
    }
}
