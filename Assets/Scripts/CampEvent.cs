using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;

public class CampEvent
{
    public string name;
    public string message;
    public int food;
    public int water;
    public int wood;
}

public static class CampEventSystem
{
    public static XElement campEventXML = XElement.Load(@"CampEvents.xml");

    public static CampEvent GetRandomCampEvent(float rand)
    {
        int eventIdx = (int)(rand * GetNumberOfEvents());
        IEnumerable<XElement> events = campEventXML.DescendantsAndSelf("Event");
        XElement xmlEvent = events.ElementAt(eventIdx);
        CampEvent campEvent = new CampEvent();

    }

    public static int GetNumberOfEvents()
    {
        IEnumerable<XElement> descendants = campEventXML.DescendantsAndSelf("Event");
        return descendants.Count();
    }
}