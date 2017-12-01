using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;

public class CampEvent : Event
{
    private enum CampEventType { ADD_FOOD, SUB_FOOD, ADD_WATER, SUB_WATER, ADD_WOOD, SUB_WOOD };

    private static float randomModifier = 0.4f;

    public string message;
    public int food;
    public int water;
    public int wood;

    public static CampEvent GenerateRandom()
    {
        CampEvent result = new CampEvent();
        CampEventType type = (CampEventType)(UnityEngine.Random.value * Enum.GetNames(typeof(CampEventType)).Length);
        if(type == CampEventType.ADD_FOOD)
        {
            result.food = (int)((randomModifier * UnityEngine.Random.value) * ResourceInfo.getFoodStock());
            result.message = TextReader.GetDialog("AddFood");
        }
        else if(type == CampEventType.SUB_FOOD)
        {
            result.food = -(int)((randomModifier * UnityEngine.Random.value) * ResourceInfo.getFoodStock());
            result.message = TextReader.GetDialog("SubFood");
        }
        else if(type == CampEventType.ADD_WATER)
        {
            result.water = (int)((randomModifier * UnityEngine.Random.value) * ResourceInfo.getWaterStock());
            result.message = TextReader.GetDialog("AddWater");
        }
        else if(type == CampEventType.SUB_WATER)
        {
            result.water = -(int)((randomModifier * UnityEngine.Random.value) * ResourceInfo.getWaterStock());
            result.message = TextReader.GetDialog("SubWater");
        }
        else if(type == CampEventType.ADD_WOOD)
        {
            result.wood = (int)((randomModifier * UnityEngine.Random.value) * ResourceInfo.getWoodStock());
            result.message = TextReader.GetDialog("AddWood");
        }
        else if(type == CampEventType.SUB_WOOD)
        {
            result.wood = -(int)((randomModifier * UnityEngine.Random.value) * ResourceInfo.getWoodStock());
            result.message = TextReader.GetDialog("SubWood");
        }
        return result;
    }
}

