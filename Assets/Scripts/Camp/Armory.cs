using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour {


    public GameObject panel;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            panel.SetActive(true);
        }
        
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            panel.SetActive(false);
        }
    }

}
