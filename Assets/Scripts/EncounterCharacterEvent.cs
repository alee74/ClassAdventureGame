using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EncounterCharacterEvent : Event
{
    public Character character;
    public string encounterDialog;

    public static EncounterCharacterEvent GenerateRandom(int day)
    {
        UnityEngine.Random.InitState(day);
        float random = UnityEngine.Random.value;
        EncounterCharacterEvent result = new EncounterCharacterEvent();
        result.character = Character.GenerateRandom(random);
        result.encounterDialog = (result.character.isHostile) ? TextReader.GetDialog("Hostile") : TextReader.GetDialog("Nonhostile");
        return result;
    }
}
