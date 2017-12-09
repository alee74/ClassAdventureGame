using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NPCinteraction {
    public static bool playerInput;
	// Use this for initialization
    static void TextPopup(){
        playerInput = true;
        while (playerInput){
            if(Input.GetKeyDown(KeyCode.Space)){
                playerInput = false;
            }
        }
        
    }

    static void Start(){
        playerInput = false;
    }
}
