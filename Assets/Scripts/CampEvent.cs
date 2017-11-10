using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;

public class CampEvent : Event
{
    public string message;
    public int food;
    public int water;
    public int wood;

    public static CampEvent GenerateRandom()
    {
        CampEvent result = new CampEvent();
        // TODO
        // result.message = TextReader.GetDialog(TODO);
        return result;
    }
}
