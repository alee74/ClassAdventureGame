using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class EnemyFighter : Fighter {

    private float xAttackDist = 1f;
    private float yAttackDist = 2f;


    #region protected override void Start();
    /// <summary>
    /// override of Start().
    /// performs enemy specific initialization.
    /// then calls Fighter.Start().
    /// </summary>
    protected override void Start() {

        maxHealth = 15f;
        health = maxHealth;
        healthSlider = GameObject.Find("EnemyHealth").GetComponent<Slider>();
        opponent = GameObject.FindGameObjectWithTag("Player").transform;

        base.Start();

    }
    #endregion

    #region protected override void OnTriggerEnter2D(Collider2D other);
    /// <summary>
    /// called when enemy collides with a collider set to trigger.
    /// if it's the player's fist, take damage and get knocked back.
    /// </summary>
    /// <param name="other">the collider that triggered the collision</param>
    protected override void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "PlayerFist") {
            health--;
            Knockback();
        }

    }
    #endregion

    #region protected override void SetDirectionFacing();
    /// <summary>
    /// sets the direction Enemy is facing.
    /// always sets enemy to face in the direction of the opponent (Player)
    ///  by comparing their position's x values.
    /// sets SpriteRenderer.flipX, which indicates if sprite should be flipped.
    ///  - because of this option, we don't need to do anything else!
    /// </summary>
    protected override void SetDirectionFacing() {

        if (transform.position.x < opponent.position.x)
            isFacingRight = true;
        else
            isFacingRight = false;

        gameObject.GetComponent<SpriteRenderer>().flipX = !isFacingRight;

    }
    #endregion

    // FIXME - interact with rest of game.
    #region protected override void Death();
    /// <summary>
    /// handles death of enemy.
    /// sets endGameText.
    /// loads world map scene.
    /// destroys this enemy.
    /// 
    /// FIXME:
    /// ***Need to set variable in world indicating we won before loading scene.
    /// ***should probably also set players health in world.
    /// </summary>
    protected override void Death() {

        GameObject.Find("EndGameText").GetComponent<Text>().text = "YOU WON!";
        SceneManager.LoadScene("WorldMapMainScene");
        Destroy(gameObject);

    }
    #endregion

    #region State control methods

    #region protected override void Stand();
    /// <summary>
    /// defines Enemy behavior while in the Stand state.
    /// if the player is outside of our horizontal attack range, we walk.
    /// otherwise, ensure player is within vertical attack range and attack (punch).
    /// </summary>
    protected override void Stand() {

        if (Mathf.Abs(transform.position.x - opponent.position.x) > xAttackDist)
            ChangeState(State.Walk);
        else if (Mathf.Abs(transform.position.y - opponent.position.y) <= yAttackDist)
            ChangeState(State.Punch);

    }
    #endregion

    // FIXME - check for grounded unnecessary? shouldn't be in walk state if not grounded.
    #region protected override void Walk();
    /// <summary>
    /// defines Enemy behavior while in the Walk state.
    /// if enemy is grounded, sets velocity to be in the direction
    ///  towards the player and 
    /// changes state to punch if player is within horizontal attack distance.
    /// </summary>
    protected override void Walk() {

        if (Grounded())
        {

            if (transform.position.x > opponent.position.x)
                rgb.velocity = new Vector2(-speed, 0);  // Vector2.left * speed;
            else
                rgb.velocity = new Vector2(speed, 0);   // Vector2.right * speed;

            // don't check y, because it will always be true when they are both standing on the ground
            if (Mathf.Abs(transform.position.x - opponent.position.x) <= xAttackDist)
                ChangeState(State.Punch);

        }

    }
    #endregion

    #region protected override void Punch();
    /// <summary>
    /// defines Enemy behavior while in the Punch state.
    /// starts coroutine to control timing of punch and whether punchBox is active.
    /// if the player is beyond the horizontal attack range, we walk.
    /// otherwise, we stand.
    /// </summary>
    protected override void Punch() {

        StartCoroutine(ControlPunchTiming());

        if (Mathf.Abs(transform.position.x - opponent.position.x) >= xAttackDist)
            ChangeState(State.Walk);
        else
            ChangeState(State.Stand);

    }
    #endregion

    // FIXME - definitely should do more than just start coroutine.
    #region protected override void Jump();
    /// <summary>
    /// defines Enemy behavior while in the Jump state.
    /// 
    /// FIXME:
    /// ***currently only starts coroutine. need to fix.
    /// </summary>
    protected override void Jump()
    {

        StartCoroutine(DelayGroundedCheck());

    }
    #endregion
    #endregion

}
