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
}
