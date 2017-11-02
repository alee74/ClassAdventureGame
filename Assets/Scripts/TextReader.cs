using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;


public class TextReader : MonoBehaviour {

    GameObject textBox;
    Text postedDialog;
    TextAsset binData;
    string[] dialogs;
    string[][] dialogLines;
    string[] adjectives = new string[] { "old", "green", "squaemous" };
    string[] nouns = new string[] { "dog", "cat", "bird" };
    // Use this for initialization
    void Start () {
        binData = Resources.Load("DialogTest") as TextAsset;
        //Debug.Log(binData);
        //string initialLines = binData.text;
        dialogs = binData.text.Split('\n');
        dialogLines = new string[dialogs.Length][];
        for (int j = 0; j < dialogs.Length; j++) {dialogLines[j] = dialogs[j].Split('\t'); }
        textBox = GameObject.Find("MainText");
        postedDialog = textBox.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetDialog(string dialogTitle) {

        bool found = false;
        //string storedResult = "NotFound2";

        for (int i = 0; i < dialogLines.Length; i++) {

            if (dialogLines[i][0].Equals(dialogTitle))
            {
                found = true;
                return dialogLines[i][1].Replace("ADJECTIVE", adjectives[UnityEngine.Random.Range(0,adjectives.Length)]).Replace("NOUN", nouns[UnityEngine.Random.Range(0, nouns.Length)]);
            }

        }

        if (found == false)
        {

            return "NotFound ";

        }

        return "NotFound2";

    }

    public void ShowDialog(string dialog){

        postedDialog.text = dialog;

    }

    public void SetSelectedDialog(string targetDialog)
    {

        string suppliedString = GetDialog(targetDialog);

        ShowDialog(suppliedString);

    }

}
