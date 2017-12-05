using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade {
    public string name;
    public string description;
    public int cost;

    public Upgrade(string name, string description, int cost)
    {
        this.name = name;
        this.description = description;
        this.cost = cost;
    }
}
