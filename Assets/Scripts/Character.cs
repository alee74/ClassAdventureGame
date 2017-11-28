using System;
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
    private bool _isHostile;
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

    public bool isHostile
    {
        get
        {
            return _isHostile;
        }
        set
        {
            _isHostile = value;
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
            {
                _strength = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
<<<<<<< HEAD

    public Character(string name, bool isHostile, int maxHealth, int maxStamina, int maxStrength)
=======
    public int getMaxHealth()
    {
        return _maxHealth;
    }
    public int getMaxStamina()
    {
        return _maxStamina;
    }
    public int getMaxStrength()
    {
        return _maxStrength;
    }
    public Character(string name, int maxHealth, int maxStamina, int maxStrength)
>>>>>>> master
    {
        _name = name;
        _isHostile = isHostile;
        _maxHealth = maxHealth;
        _maxStamina = maxStamina;
        _maxStrength = maxStrength;
        _health = maxHealth;
        _stamina = maxStamina;
        _strength = maxStrength;
    }

    public static Character GenerateRandom(float random)
    {
        string[] names = System.IO.File.ReadAllLines(@"Assets/Scripts/CharacterNames.txt");
        string name = names[(int)(random * names.Length)];
        bool isHostile = (random > 0.5f) ? true : false;
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
        
        Character character = new Character(name,isHostile,health,stamina,strength);
        return character;
    }

}
