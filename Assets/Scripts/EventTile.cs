using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTile : MonoBehaviour {

    public Event tileEvent;
    private BoxCollider2D triggerCollider;

    void Awake()
    {
        triggerCollider = gameObject.GetComponent<BoxCollider2D>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
