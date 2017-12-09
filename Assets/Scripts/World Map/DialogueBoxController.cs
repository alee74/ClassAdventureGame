using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxController : MonoBehaviour {

    public static bool activeDialogue;

	// Use this for initialization
	void Start () {
        SetDialogue(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (activeDialogue && Input.GetKey(KeyCode.Space))
        {
            SetDialogue(false);
        }
	}

    public void SetDialogue(bool openClose)
    {
        gameObject.SetActive(openClose);
        activeDialogue = openClose;
    }
}
