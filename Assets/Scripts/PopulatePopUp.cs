using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulatePopUp : MonoBehaviour {

    public Text popUpTxt;

    // tells PopUp (thru ItemOption) which ItemButton tag user clicked on...
    public void FillPopUpWithItemOptions()
    {
        Debug.Log(gameObject.tag);
        ItemOptions.currItemTag = gameObject.tag;
        popUpTxt.text = gameObject.tag + " was selected.";
    }
}
