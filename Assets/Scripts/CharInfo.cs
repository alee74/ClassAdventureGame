using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfo : MonoBehaviour {

    public static List<CharInfo> characters = new List<CharInfo>();
    private string name;
    private int health = 100;
    private int strength = 10;
    private int stamina = 100;

    private int maxHealth = 100;
    private int maxStrength = 10;
    private int maxStamina = 100;

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
            return;
        }
        if(newHealth < 0)
        {
            health = 0;
            return;
        }
        health = newHealth;
    }
    public void setStamina(int newStamina)
    {
        if (newStamina > maxStamina)
        {
            stamina = maxStamina;
            return;
        }
        if (newStamina < 0)
        {
            stamina = 0;
            return;
        }
        stamina = newStamina;
    }
    public void addHealth(int changeHealth)
    {
        if (health+changeHealth > maxHealth)
        {
            health = maxHealth;
            return;
        }
        if (health+changeHealth <= 0)
        {
            health = 0;
            return;
        }
        health += changeHealth;
    }
    public void addStamina(int changeStamina)
    {
        if (stamina+changeStamina > maxStamina)
        {
            stamina = maxStamina;
            return;
        }
        if (stamina+changeStamina <= 0)
        {
            stamina = 0;
            return;
        }
        stamina += changeStamina;
    }
    public void addStrength(int changeStrength)
    {
        if(changeStrength+strength <= 0)
        {
            strength = 0;
            return;
        }
        strength += changeStrength;
    }
}
