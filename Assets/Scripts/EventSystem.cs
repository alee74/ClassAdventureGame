using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weather { Sunny, Rainy, Cloudy, Stormy };

static public class EventSystem {

    static public Weather GetWeather(int day)
    {
        UnityEngine.Random.InitState(day);
        int weather = (int)(UnityEngine.Random.value * (Enum.GetNames(typeof(Weather)).Length));
        return (Weather)weather;
    }

    static public List<CampEvent> GetCampEvents(int day, int maxEvents = 20, int minEvents = 0)
    {
        UnityEngine.Random.InitState(day);
        int numEvents = Math.Max(minEvents, (int)(UnityEngine.Random.value * maxEvents));
        List<CampEvent> events = new List<CampEvent>(numEvents);
        for(int i = 0; i < numEvents; ++i)
        {
            events.Add(CampEvent.GenerateRandom());
        }
        return events;
    }

    static public List<GameObject> GetEventTiles(int day, int maxEvents = 20, int minEvents = 0)
    {
        UnityEngine.Random.InitState(day);
        int numEvents = Math.Max(minEvents, (int)(maxEvents * UnityEngine.Random.value));
        GameObject eventTilePrefab = (GameObject)Resources.Load("Prefabs/EventTile");
        List<GameObject> result = new List<GameObject>(numEvents);
        for(int i = 0; i < numEvents; ++i)
        {
            result.Add(UnityEngine.Object.Instantiate(eventTilePrefab));
            result[i].GetComponent<EventTile>().tileEvent = EncounterCharacterEvent.GenerateRandom();
        }
        return result;
    }
	
}
