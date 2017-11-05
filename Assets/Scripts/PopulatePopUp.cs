using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulatePopUp : MonoBehaviour {

    public Text popUpTxt;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // tells PopUp (thru ItemOption) which ItemButton tag user clicked on...
    public void FillPopUpWithItemOptions()
    {
        ItemOptions.currItemTag = gameObject.tag;
        popUpTxt.text = gameObject.tag + " was selected.";
    }
}
