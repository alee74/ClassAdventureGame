
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharInfo {
    public static int currentCharacter = -1;
    public static List<Character> characters = new List<Character>();
    public static Character getCurrentCharacter()
    {
        if(currentCharacter == -1)
        {
            characters.Add(Character.GenerateRandom(Random.value));
            currentCharacter = 0;
        }
        return characters[currentCharacter];
    } 
}
