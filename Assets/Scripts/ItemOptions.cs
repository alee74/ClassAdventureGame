using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOptions : MonoBehaviour {
    

	// Use this for initialization
	void Start () {

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
}
