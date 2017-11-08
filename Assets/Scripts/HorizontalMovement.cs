﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour {

    public int move;
    public float speed = 0.05f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        if (Mathf.Abs(transform.position.x) >= move)
            speed *= -1;

	}
}
