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
    static string[] adjectives = new string[] { "old", "green", "squaemous" };
    static string[] nouns = new string[] { "dog", "cat", "bird" };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize () {
        binData = Resources.Load("DialogTest") as TextAsset;
        //Debug.Log(binData);
        //string initialLines = binData.text;
        dialogs = binData.text.Split('\n');
        dialogLines = new string[dialogs.Length][];
        for (int j = 0; j < dialogs.Length; j++) {dialogLines[j] = dialogs[j].Split('\t'); }
        //textBox = GameObject.Find("MainText");
        //postedDialog = textBox.GetComponent<Text>();
    }

    public static string GetDialog(string dialogTitle) {
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
}
