using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRun : MonoBehaviour {

    public float runSpeed;
    public int decRate;
    public int incRate;

    private Image staminaBar;
    private bool recharging = false;
    private float rechargeLimit;

    // Use this for initialization
    void Start () {
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();
        rechargeLimit = PlayerInfoForWorldMap.stamina / 3;
	}


    //fixedUpdate
	void Update () {

        if (recharging)
        {
            PlayerMovement.speed = 5.0f;
            RestoreStamina();
            //if (currChara.stamina == currChara.getMaxStamina() )
            if (PlayerInfoForWorldMap.stamina > rechargeLimit)
            {
                recharging = false;
            }
        } 
        else
        {
            if (Input.GetKey(KeyCode.LeftShift) && CanRun() && 
                (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ))
            {
                PlayerMovement.speed = runSpeed;
                PlayerInfoForWorldMap.stamina -= (decRate*Time.deltaTime);
            }
            else
            {
                PlayerMovement.speed = 5.0f;
                recharging = true;
            }

        }         
        
        staminaBar.rectTransform.localScale = new Vector3(PlayerInfoForWorldMap.stamina / PlayerInfoForWorldMap.maxStamina, 1, 1);
    }

    private bool CanRun()
    {
        return PlayerInfoForWorldMap.stamina > 0;
    }

    private void RestoreStamina()
    {
        if (PlayerInfoForWorldMap.stamina < PlayerInfoForWorldMap.maxStamina)
        {
            PlayerInfoForWorldMap.stamina += (incRate * Time.deltaTime);
        }
    }

}
