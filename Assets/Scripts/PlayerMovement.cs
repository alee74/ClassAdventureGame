using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Vector3 pos;                             
	float speed = 2.0f;   
	float inputX;
	float inputY;


	void Start () {
		pos = transform.position; 

	}

	void Update () {
		inputX = Input.GetAxis ("Horizontal");
		inputY = Input.GetAxis ("Vertical");

			pos += Vector3.right * inputX * Time.deltaTime * speed;

			pos += Vector3.up * inputY * Time.deltaTime * speed;
		
	
		transform.position = pos;   // Move there
	}

}