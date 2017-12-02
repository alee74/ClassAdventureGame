﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour {

    public float xStart;
    public float yStart;

    private GameObject enemy;
    private string[] enemyType = new string[] { "Panda" };
    static private System.Random rando = new System.Random();

	// Use this for initialization
	void Start () {

		Character character = CharInfo.getCurrentCharacter();
		Text player = GameObject.Find("PlayerName").GetComponent<Text>();
		player.text = character.name;

        string enemyName = enemyType[rando.Next(enemyType.Length)];
        enemy = Resources.Load<GameObject>("Prefabs/2DFighter/" + enemyName);
        Instantiate(enemy, new Vector3(xStart, yStart, 0f), Quaternion.identity);

        Text enemyNameText = GameObject.Find("EnemyName").GetComponent<Text>();
        enemyNameText.text = enemyName;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
