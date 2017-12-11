using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapHealthBar : MonoBehaviour {

    private Character currChara;
    private Image healthBar;

	// Use this for initialization
	void Start () {
        currChara = CharInfo.getCurrentCharacter();
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.rectTransform.localScale = new Vector3(currChara.health / (float)currChara.getMaxHealth(), 1, 1);
	}
}
