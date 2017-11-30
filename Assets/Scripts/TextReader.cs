using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;


public static class TextReader{

    //GameObject textBox;
    //Text postedDialog;
    static TextAsset binData;
    static string[] dialogs; 
    static string[][] dialogLines;
    static string[] adjectives = new string[] { "old", "green", "squaemous","sinister","ancient","ominous","mysterious","eldritch","strange","red","blue", "yellow","flavorful","frightful","pleasant","grand","small","giant","vengeful","ruined","haunted","playful"};
    static string[] nouns = new string[] { "dog", "cat", "bird", "error", "urn", "temple", "ruin", "chapel", "structure", "man", "creature", "chest", "batman", "device", "shelter","game developer"};

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize () {
        binData = Resources.Load("DialogTest") as TextAsset;
        //Debug.Log(binData);
        //string initialLines = binData.text;
        dialogs = binData.text.Split('\n');
        dialogLines = new string[dialogs.Length][];
        for (int j = 0; j < dialogs.Length; j++) {
            dialogLines[j] = dialogs[j].Split('\t');
            Debug.Log("Dialog Line " + j + ": " + dialogLines[j]);
        }
        //textBox = GameObject.Find("MainText");
        //postedDialog = textBox.GetComponent<Text>();
    }

    public static string GetDialog(string dialogTitle) {
        bool found = false;
        //string storedResult = "NotFound2";

        List<String> possibilities = new List<String>(); ;

        for (int i = 0; i < dialogLines.Length; i++) {
            if (dialogLines[i][0].Equals(dialogTitle))
            {
                found = true;
                possibilities.Add(dialogLines[i][1].Replace("ADJECTIVE", adjectives[UnityEngine.Random.Range(0, adjectives.Length)]).Replace("NOUN", nouns[UnityEngine.Random.Range(0, nouns.Length)]));
                return possibilities[UnityEngine.Random.Range(0, possibilities.Count)];
            }
        }
        if (found == false)
        {
            return "NotFound ";
        }
        return "NotFound2";
    }
}
