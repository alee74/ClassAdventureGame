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

}
