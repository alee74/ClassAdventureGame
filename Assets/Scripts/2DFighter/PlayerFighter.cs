using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerFighter : Fighter {

    private Character character;


    // FIXME - get maxHealth from CharInfo
    #region protected override void Start();
    /// <summary>
    /// override of Start().
    /// performs player specific initialization.
    /// then calls Fighter.Start().
    /// </summary>
    protected override void Start() {

        character = CharInfo.getCurrentCharacter();
        maxHealth = 100;        // should get from character
        healthSlider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        opponent = GameObject.FindGameObjectWithTag("Enemy").transform;

        base.Start();

    }
    #endregion

    #region protected override void OnTriggerEnter2D(Collider2D other);
    /// <summary>
    /// called when player collides with a collider set to trigger.
    /// if its the enemy's fist, take damage and get knocked back.
    /// </summary>
    /// <param name="other">the collider that triggered the collision</param>
    protected override void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "EnemyFist") {
            health--;
            Knockback();
        }

    }
    #endregion

    #region protected override void SetDirectionFacing();
    /// <summary>
    /// sets the direction Player is facing.
    /// only changes direction if user is moving player.
    /// sets SpriteRenderer.flipX, which indicates if sprite should be flipped.
    ///  - because of this option, we don't need to do anything else!
    /// </summary>
    protected override void SetDirectionFacing() {

        float moving = Input.GetAxis("Horizontal");

        if (moving > 0)
            isFacingRight = true;
        else
            isFacingRight = false;

        gameObject.GetComponent<SpriteRenderer>().flipX = !isFacingRight;

    }
    #endregion

    // FIXME - interact with world
    #region protected override void Death();
    /// <summary>
    /// handles death of character.
    /// 
    /// FIXME:
    /// ***currently just loads GameOver scene.
    /// ***need to update to set variable in world indicating we lost and return to it
    /// </summary>
    protected override void Death() {

        SceneManager.LoadScene("GameOver");

    }
    #endregion

    #region State control methods

    #region protected override void Stand();
    /// <summary>
    /// defines Player behavior while in the Stand state.
    /// if we are trying to jump and are grounded, we jump.
    /// else if we are trying to move, we walk.
    /// else if we are trying to punch, we punch.
    /// </summary>
    protected override void Stand() {

        float move = Input.GetAxis("Horizontal");
        bool jumping = Input.GetKeyDown("space");
        bool punching = Input.GetKeyDown(KeyCode.F);

        if (jumping && Grounded())
            ChangeState(State.Jump);
        else if (move != 0)
            ChangeState(State.Walk);
        else if (punching)
            ChangeState(State.Punch);

    }
    #endregion

    #region protected override void Walk();
    /// <summary>
    /// defines Player behavior while in the Walk state.
    /// if we are trying to jump and are grounded, we jump.
    /// else if we are trying to punch, we punch.
    /// else if we are not moving, we stand.
    /// otherwise, we more accordingly.
    /// </summary>
    protected override void Walk() {

        float move = Input.GetAxis("Horizontal");
        bool jumping = Input.GetKeyDown("space");
        bool punching = Input.GetKeyDown(KeyCode.F);

        if (jumping && Grounded())
            ChangeState(State.Jump);
        else if (punching)
            ChangeState(State.Punch);
        else if (move == 0)
            ChangeState(State.Stand);
        else
            rgb.velocity = new Vector2(move * speed, 0f);

    }
    #endregion

    #region protected override void Punch();
    /// <summary>
    /// defines Player behavior while in the Punch state.
    /// starts coroutine to control the timing and speed of the punch.
    /// if we are not moving, we stand.
    /// otherwise, we walk.
    /// </summary>
    protected override void Punch() {

        StartCoroutine(ControlPunchTiming());

        float move = Input.GetAxis("Horizontal");

        if (move == 0)
            ChangeState(State.Stand);
        else {
            rgb.velocity = new Vector2(move * speed, 0f);
            ChangeState(State.Walk);
        }

    }
    #endregion

    #region protected override void Jump();
    /// <summary>
    /// defines Player behavior while in the Jump state.
    /// sets the velocity to account for horizontal movement while airborne.
    /// starts coroutine to delay the first check for grounded and then check
    ///  repeatedly until grounded and then change state to stand.
    /// </summary>
    protected override void Jump() {

        rgb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rgb.velocity.y);
        StartCoroutine(DelayGroundedCheck());

    }
    #endregion
    #endregion

}
