using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue {

    private string dialogueStr;
    private Queue<char> dialogueQueue;

    public Dialogue(string _dialogueStr)
    {
        dialogueStr = _dialogueStr;
    }

    private void QueueString(string str)
    {
        for (int i = 0; i < dialogueStr.Length; ++i)
        {
            dialogueQueue.Enqueue(str[i]);
        }
    }

    public void DisplayDialogue(Text textUI)
    {

    }

}
