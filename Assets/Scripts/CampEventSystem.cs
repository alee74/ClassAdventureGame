﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Linq;

public static class CampEventSystem
{
    public static XElement campEventXML = XElement.Load(@"Assets/Scripts/CampEvents.xml");

    public static CampEvent GenerateRandomCampEvent()
    {
        int eventIdx = (int)(UnityEngine.Random.value * GetNumberOfEvents());
        IEnumerable<XElement> events = campEventXML.DescendantsAndSelf("Event");
        XElement xmlEvent = events.ElementAt(eventIdx);
        CampEvent campEvent = new CampEvent();
        //TODO!!!!
        return campEvent;
    }

    public static int GetNumberOfEvents()
    {
        IEnumerable<XElement> descendants = campEventXML.DescendantsAndSelf("Event");
        return descendants.Count();
    }
}
