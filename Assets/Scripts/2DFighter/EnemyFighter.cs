﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// class defining Enemy behavior.
/// inherits from Figther class.
/// </summary>
public class EnemyFighter : Fighter {

    private float xAttackDist = 1.5f;
    private float yAttackDist = 2f;
    private float jumpHeightDiff = 0.5f;
    public float distanceForJump = 1.5f;


    // FIXME : get health and strength and stamina from ... ???
    #region protected override void Start();
    /// <summary>
    /// override of Start().
    /// performs enemy specific initialization.
    /// then calls Fighter.Start().
    /// </summary>
    protected override void Start() {

		//character = FightOutcome.currentlyFighting;
        maxStamina = 10f;
        stamina = maxStamina;
        healthSlider = GameObject.Find("EnemyHealth").GetComponent<Slider>();
        staminaSlider = GameObject.Find("EnemyStamina").GetComponent<Slider>();
		healthText = GameObject.Find ("EnemyHealthText").GetComponent<Text> ();
        opponent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFighter>();
        opponentTransform = GameObject.FindGameObjectWithTag("Player").transform;

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

            health -= opponent.damage;
            ChangeState(State.Knockback);

        }

    }
    #endregion

    #region protected void OnCollisionEnter2D(Collider2D other);
    /// <summary>
    /// called when enemy collides with a collider..
    /// if an edge, enemy rolls.
    /// </summary>
    /// <param name="other">the collider that triggered the collision</param>
    protected void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "Edge") {

            if (Mathf.Abs(transform.position.x - opponentTransform.position.x) <= xAttackDist)
                ChangeState(State.Roll);

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

        if (transform.position.x < opponentTransform.position.x)
            isFacingRight = true;
        else
            isFacingRight = false;

        gameObject.GetComponent<SpriteRenderer>().flipX = !isFacingRight;

    }
    #endregion

    #region protected override void Death();
    /// <summary>
    /// handles death of enemy.
    /// sets endGameText.
    /// loads world map scene.
    /// destroys this enemy.
    /// </summary>
    protected override void Death() {

        opponent.InformWorld(true);
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

        if (!Grounded())
            ChangeState(State.Falling);

        else {

            if (Mathf.Abs(transform.position.x - opponentTransform.position.x) > xAttackDist)
                ChangeState(State.Walk);
            else if (Mathf.Abs(transform.position.y - opponentTransform.position.y) <= yAttackDist)
                ChangeState(State.Punch);

        }

    }
    #endregion

    #region protected override void Walk();
    /// <summary>
    /// defines Enemy behavior while in the Walk state.
    /// if enemy is grounded, sets velocity to be in the direction
    ///  towards the player and 
    /// changes state to punch if player is within horizontal attack distance.
    /// </summary>
    protected override void Walk() {

        if (!Grounded())
            ChangeState(State.Falling);

        else {

            if ((Mathf.Abs(opponentTransform.position.y - transform.position.y) >= jumpHeightDiff) &&
                (Mathf.Abs(opponentTransform.position.x - transform.position.x) >= distanceForJump))
                ChangeState(State.Jump);

            else {

                rgb.velocity = Vector2.right * speed;

                if (stamina < punchCost) {

                    if (transform.position.x < opponentTransform.position.x)
                        rgb.velocity *= -1;

                } else {

                    if (transform.position.x > opponentTransform.position.x)
                        rgb.velocity *= -1;

                    // don't check y, because it will always be true when they are both standing on the ground
                    if (Mathf.Abs(transform.position.x - opponentTransform.position.x) <= xAttackDist)
                        ChangeState(State.Punch);

                }
            }
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

       /* 
        if (Mathf.Abs(transform.position.x - opponent.position.x) >= xAttackDist)
            ChangeState(State.Walk);
        else
        */
        ChangeState(State.Stand);

    }
    #endregion

    #region protected override void Jump();
    /// <summary>
    /// defines Enemy behavior while in the Jump state.
    /// sets the velocity to account for horizontal movement while airborne.
    /// starts coroutine to delay the first check for grounded and then check
    ///  repeatedly until grounded and then change state to stand.
    /// </summary>
    protected override void Jump() {

        if (opponentTransform.position.x < transform.position.x)
            rgb.velocity = new Vector2(-speed, rgb.velocity.y);
        else
            rgb.velocity = new Vector2(speed, rgb.velocity.y);

        StartCoroutine(DelayGroundedCheck());

    }
    #endregion

    #region protected override void Falling();
    /// <summary>
    /// defines Enemy behavior while in Falling state.
    /// sets the velocity to account for horizontal movement while airborne.
    /// calls base implentation (checks if grounded, changes to Stand).
    /// </summary>
    protected override void Falling() {

        if (opponentTransform.position.x < transform.position.x)
            rgb.velocity = new Vector2(-speed, rgb.velocity.y);
        else
            rgb.velocity = new Vector2(speed, rgb.velocity.y);

        base.Falling();

    }
    #endregion
    #endregion

}
