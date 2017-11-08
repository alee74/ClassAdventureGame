using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather { Sunny, Rainy, Cloudy, Stormy };

static public class EventSystem {

    static public int maxCharacters = 20;
    static public int maxEvents = 20;

    static public List<Character> GetEncounterableCharacters(int day)
    {
        UnityEngine.Random.InitState(day);
        float random = UnityEngine.Random.value;
        int numCharacters = (int)(random * (maxCharacters));
        List<Character> result = new List<Character>(numCharacters);
        for(int i = 0; i < numCharacters; i++)
        {
            result[i] = Character.GenerateRandomCharacter(random);
        }
        return result;
    }

    static public Weather GetWeather(int day)
    {
        UnityEngine.Random.InitState(day);
        int weather = (int)(UnityEngine.Random.value * (Enum.GetNames(typeof(Weather)).Length));
        return (Weather)weather;
    }

    static public List<CampEvent> GetCampEvents(int day)
    {
        UnityEngine.Random.InitState(day);
        int numEvents = (int)(UnityEngine.Random.value * (maxEvents));
        List<CampEvent> events = new List<CampEvent>(numEvents);
        // TODO: generate random camp events
        return events;
    }
	
}
