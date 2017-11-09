
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfo : MonoBehaviour {


    public static int currentCharacter = 0;
    public static List<CharInfo> characters = new List<CharInfo>();
    public string name;
	public int health = 100;
	public int strength = 10;
	public int stamina = 100;

	public int maxHealth = 100;
	public int maxStrength = 10;
	public int maxStamina = 100;

	// Use this for initialization
	void Start () {
        name = "temp temper";
        characters.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setHealth(int newHealth)
    {
        if(newHealth > maxHealth)
        {
            health = maxHealth;
        }
        if(newHealth < 0)
        {
            health = 0;
        }
        health = newHealth;
    }
    public void setStamina(int newStamina)
    {
        if (newStamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if (newStamina < 0)
        {
            stamina = 0;
        }
        stamina = newStamina;
    }
    public void addHealth(int changeHealth)
    {
        if (health+changeHealth > maxHealth)
        {
            health = maxHealth;
        }
        if (health+changeHealth <= 0)
        {
            health = 0;
        }
        health += changeHealth;
    }
    public void addStamina(int changeStamina)
    {
        if (stamina+changeStamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if (stamina+changeStamina <= 0)
        {
            stamina = 0;
        }
        stamina += changeStamina;
    }
	public void addStrength(int changeStrength)
	{
		if (strength + changeStrength > maxStrength)
		{
			strength = maxStrength;
		}
		if (strength + changeStrength <= 0)
		{
			strength = 0;
		}
		strength++;
	}

}
