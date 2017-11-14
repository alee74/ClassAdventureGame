﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



public class Character
{
    private static int baseHealth = 10;
    private static int baseStamina = 10;
    private static int baseStrength = 10;
    private static int limitHealth = 500;
    private static int limitStamina = 500;
    private static int limitStrength = 500;

    private string _name;
    private GameObject _prefab;
    private int _health;
    private int _stamina;
    private int _strength;
    private int _maxHealth;
    private int _maxStamina;
    private int _maxStrength;

    public string name
    {
        get
        {
            return _name;
        }
    }

    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value < _maxHealth)
            {
                _health = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public int stamina
    {
        get
        {
            return _stamina;
        }
        set
        {
            if(value < _maxStamina)
            {
                _stamina = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public int strength
    {
        get
        {
            return _strength;
        }
        set
        {
            if(value < _maxStrength)
            {
                _strength = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    public Character(string name, int maxHealth, int maxStamina, int maxStrength)
    {
        _name = name;
        _maxHealth = maxHealth;
        _maxStamina = maxStamina;
        _maxStrength = maxStrength;
        _health = maxHealth;
        _stamina = maxStamina;
        _strength = maxStrength;
        // TODO: add some way to pick a GameObject for character
    }

    public static Character GenerateRandom(float random)
    {
        string[] names = System.IO.File.ReadAllLines(@"Assets/Scripts/CharacterNames.txt");
        string name = names[(int)(random * names.Length)];
        int health;
        int stamina;
        int strength;
        if (random < 0.75f)
        {
            // basic character, normal strength
            health = (int)(UnityEngine.Random.Range((float)(baseHealth + (baseHealth * random)),limitHealth/2.0f));
            stamina = (int)(UnityEngine.Random.Range((float)(baseStamina + (baseStamina * random)), limitStamina / 2.0f));
            strength = (int)(UnityEngine.Random.Range((float)(baseStrength + (baseStrength * random)), limitStrength / 2.0f));
        }
        else
        {
            // stronger character
            health = (int)(UnityEngine.Random.Range((float)(baseHealth + (2*(baseHealth * random))), limitHealth));
            stamina = (int)(UnityEngine.Random.Range((float)(baseStamina + (2*(baseStamina * random))), limitStamina));
            strength = (int)(UnityEngine.Random.Range((float)(baseStrength + (2*(baseStrength * random))), limitStrength));
        }
        
        Character character = new Character(name,health,stamina,strength);
        return character;
    }

}
