using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather { Sunny, Rainy, Cloudy, Stormy };

static public class EventSystem {

    static public List<Character> GetEncounterableCharacters(int day)
    {
        float rand = UnityEngine.Random.value;

    }

    static public Weather GetWeather(int day)
    {
        int weather = (int)(UnityEngine.Random.value * (Enum.GetNames(typeof(Weather)).Length));
        return (Weather)weather;
    }

    static public List<CampEvent> GetCampEvents(int day)
    {
        float rand = UnityEngine.Random.value;

    }
	
}
