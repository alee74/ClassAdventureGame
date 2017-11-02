using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float walkSpeed = 3f;
    public float jumpPower = 300f;

    public LayerMask groundMask;
    public float groundRadius = 0.1f;

    private Rigidbody2D theRigidbody;
    private Transform groundCheckLeft;
    private Transform groundCheckRight;


    // Use this for initialization
    void Start()
    {
        theRigidbody = GetComponent<Rigidbody2D>();
        //groundCheckLeft = transform.Find("LeftGround");
        //roundCheckRight = transform.Find("RightGround");
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        theRigidbody.velocity = new Vector2(inputX * walkSpeed, inputY * walkSpeed);

    }

}
