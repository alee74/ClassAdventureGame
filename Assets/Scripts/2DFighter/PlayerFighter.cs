using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// class defining Player behavior.
/// inherits from Fighter.
/// </summary>
public class PlayerFighter : Fighter {

    private float staminaTimer;
    private int staminaLossPerMinute;


    #region protected override void Start();
    /// <summary>
    /// override of Start().
    /// performs player specific initialization.
    /// then calls Fighter.Start().
    /// try/catch to prevent breaking when enemy not in scene, for testing purposes.
    /// </summary>
    protected override void Start() {

        maxStamina = 10f;
        character = CharInfo.getCurrentCharacter();
        stamina = (character.stamina / character.getMaxStamina()) * maxStamina;
        healthSlider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
		staminaSlider = GameObject.Find("PlayerStamina").GetComponent<Slider>();
		healthText = GameObject.Find ("PlayerHealthText").GetComponent<Text> ();
        staminaTimer = 0f;
        staminaLossPerMinute = 30;

        try {

            opponent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyFighter>();
            opponentTransform = GameObject.FindGameObjectWithTag("Enemy").transform;

        } catch (NullReferenceException ne) {

            Debug.Log(ne.ToString());

        }

        base.Start();

    }
    #endregion

    #region protected override void Update();
    /// <summary>
    /// called once per frame.
    /// increments staminaTimer by the time since last frame.
    /// calls base implementation of Update().
    /// </summary>
    protected override void Update() {

        staminaTimer += Time.deltaTime;
        base.Update();

    }
    #endregion

    #region protected override void OnTriggerEnter2D(Collider2D other);
    /// <summary>
    /// called when player collides with a collider set to trigger.
    /// if its the enemy's fist, take damage and set state to Knockback.
    /// </summary>
    /// <param name="other">the collider that triggered the collision</param>
    protected override void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "EnemyFist") {

            health -= opponent.damage;
            ChangeState(State.Knockback);

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
        else if (moving < 0)
            isFacingRight = false;

        gameObject.GetComponent<SpriteRenderer>().flipX = !isFacingRight;

    }
    #endregion

    #region protected override void Death();
    /// <summary>
    /// handles death of character.
    /// sets appropriate variables in world.
    /// loads next scene.
    /// </summary>
    protected override void Death() {

        InformWorld(false);
        SceneManager.LoadScene("WorldMapMainScene");

    }
    #endregion

    #region public override void InformWorld(bool win);
    /// <summary>
    /// called from on death in this and EnemyFighter classes.
    /// sets global (game level) health and stamina,
    ///  scaling stamina appropriately.
    /// sets global variable indicating whether we won or lost.
    /// </summary>
    public override void InformWorld(bool win) {

        character.health = (int)health;
        int staminaLoss = (int)(staminaLossPerMinute * (staminaTimer % 60));

        if (character.stamina - staminaLoss >= 0)
			character.stamina -= (int)(staminaLossPerMinute * ((staminaTimer / 60) + (staminaTimer % 60)));
        else
            character.stamina = 0;


      	FightOutcome.wonFight = win;
		FightOutcome.LoseItemsAfterLosingFight();
		FightOutcome.currentlyFighting = null;

    }
    #endregion

    #region State control methods

    #region protected override void Stand();
    /// <summary>
    /// defines Player behavior while in the Stand state.
    /// if we are not grounded, we are Falling.
    /// else if we are trying to jump, we Jump.
    /// else if we are trying to move, we Walk.
    /// else if we are trying to punch, we Punch.
    /// else if we are trying to roll, we Roll.
    /// </summary>
    protected override void Stand() {

        float move = Input.GetAxis("Horizontal");
        bool jumping = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        bool punching = Input.GetKeyDown("space");
        bool rolling = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        if (!Grounded())
            ChangeState(State.Falling);
        else if (jumping)
            ChangeState(State.Jump);
        else if (move != 0)
            ChangeState(State.Walk);
        else if (punching)
            ChangeState(State.Punch);
        else if (rolling)
            ChangeState(State.Roll);

    }
    #endregion

    #region protected override void Walk();
    /// <summary>
    /// defines Player behavior while in the Walk state.
    /// if we aren't grounded, we are Falling.
    /// else if we are trying to jump, we Jump.
    /// else if we are trying to punch, we Punch.
    /// else if we are trying to roll, we Roll.
    /// else if we are not moving, we Stand.
    /// otherwise, we move accordingly.
    /// </summary>
    protected override void Walk() {

        float move = Input.GetAxis("Horizontal");
        bool jumping = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
		bool punching = Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.F);
        bool rolling = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        if (!Grounded())
            ChangeState(State.Falling);
        else if (jumping)
            ChangeState(State.Jump);
        else if (punching)
            ChangeState(State.Punch);
        else if (rolling)
            ChangeState(State.Roll);
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
    /// then, if we are not moving, we stand.
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

    #region protected override void Falling();
    /// <summary>
    /// defines Player behavior while in the Falling state.
    /// sets the velocity to account for horizontal movement while airborne.
    /// calls base implementation (checks for grounded, changes to Stand).
    /// </summary>
    protected override void Falling() {

        rgb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rgb.velocity.y);

        base.Falling();

    }
    #endregion
    #endregion

}
