using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRun : MonoBehaviour {

    public Text stamina;
    public Text staminaTxt;
    private Character currChara;
    private Image staminaBar;

    private bool running = false;
    private bool recharging = false;
    private int rechargeLimit;

    // Use this for initialization
    void Start () {
        currChara = CharInfo.getCurrentCharacter();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();
        staminaTxt = GameObject.Find("Stamina").GetComponent<Text>();
        rechargeLimit = currChara.getMaxStamina() / 3;
        Debug.Log(rechargeLimit);
	}

	void Update () {

        if (recharging)
        {
            staminaTxt.text = "Recharging!";
            PlayerMovement.speed = 5.0f;
            RestoreStamina();
            //if (currChara.stamina == currChara.getMaxStamina() )
            if (currChara.stamina > rechargeLimit)
            {
                recharging = false;
            }
        } 
        else
        {
            if (Input.GetKey(KeyCode.LeftShift) && CanRun())
            {
                staminaTxt.text = "Running!!";
                PlayerMovement.speed = 15.0f;
                currChara.stamina-=2;
            }
            else
            {
                recharging = true;
            }

        }

        /*
        if (!recharging)
        {
            if (Input.GetKey(KeyCode.LeftShift) && CanRun())
            {
                // run
                stamina.text = "Player is running!"; 
                PlayerMovement.speed = 10.0f;
                currChara.stamina--;
            }
            else
            {
               
                //stamina.text = "Player has stopped running, should recharge";
                //PlayerMovement.speed = 5.0f;
                //RestoreStamina();
                
                recharging = true;
                
            }
        }
        else
        {
            stamina.text = "Stop move, recharging!";
            PlayerMovement.speed = 5.0f;
            currChara.stamina++;
            if (currChara.stamina == currChara.getMaxStamina())
            {
                recharging = false;
            }
        }
        */
        //stamina.text = "Stamina: " + currChara.stamina.ToString();
        /*
        if (Input.GetKey(KeyCode.LeftShift) && CanRun()) {
            stamina.text = "Player should run";
            currChara.stamina--;
            PlayerMovement.speed = 10.0f;
        }
        else
        {
            stamina.text = "Player has stooped running";
            RestoreStamina();
            PlayerMovement.speed = 5.0f;
        }
        */

        /*
         * Run if left shift is pressed and can run!
         * if run out, wait until it recharges (
         * */
         

        staminaTxt.text = "Stamina: " + currChara.stamina.ToString();
        staminaBar.rectTransform.localScale = new Vector3(currChara.stamina / (float)currChara.getMaxStamina(), 1, 1);
    }

    private void DisplayStaminaBar()
    {
        Debug.Log(currChara.stamina / (float)currChara.getMaxStamina());
        staminaBar.rectTransform.localScale = new Vector3(currChara.stamina / (float)currChara.getMaxStamina(), 1, 1);
    }

    private bool CanRun()
    {
        return currChara.stamina > 0;
    }

    private void RestoreStamina()
    {
        if (currChara.stamina < currChara.getMaxStamina())
        {
            currChara.stamina++;
        }
    }

}
