using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerrticalMovement : MonoBehaviour {

    public int move;
    public float speed = 0.025f;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed);
        if (Mathf.Abs(transform.position.y) >= move)
            speed *= -1;

    }
}
