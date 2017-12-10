using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class defining common behavior for Player and Enemy.
/// superclass for PlayerFighter and EnemyFighter.
/// </summary>
public abstract class Fighter : MonoBehaviour {

    // TODO : talk to other group(s) about attributes and interaction with world

    #region Member declarations

    // states for both player and enemy
    protected enum State { Stand, Walk, Punch, Jump, Roll, Knockback, Falling }

    // members common to enemy and player
    protected Rigidbody2D rgb;
    protected Animator anim;
    protected State state;
    protected bool isFacingRight;
    protected Slider healthSlider;
    protected Slider staminaSlider;
	protected Text healthText;
    protected GameObject punchBox;
    protected Fighter opponent;
    protected Transform opponentTransform;
    protected bool alreadyRolling;

    //Game Values Fetched From Character Class
    protected float health;
    protected float maxHealth;
    protected float stamina;
    protected float maxStamina;
    
    // members we can set from editor.
    public float speed;
    public float jumpPower;
    public float rollPower;
    public float damage;
    public float knockbackDist;
    public float recoverRate;   //rate to recover stamina at
    public float punchCost;     //stamina cost of a punch
    public float jumpCost;      //stamina cost of a jump
    public float rollCost;      //stamina cost of a roll

    // members that will not be overriden
    private bool initialGroundedCheck;
    private float punchBoxOffset = 0.75f;
    private static float groundCheckDelay = 0.05f;
    private static float groundedDist = 0.85f;
    private static float punchBeginDelay = 0.25f;
    private static float punchEndDelay = 0.15f;
    private static float rollTime = 0.25f;
    private static float knockbackTime = 0.15f;
    private Camera cam;
    private bool waitForCooldown;
    private bool knockedBack;
    #endregion


    #region Abstrat methods -- must be overriden by subclasses!

    #region protected abstract void OnTriggerEnter2D(Collider2D other);
    /// <summary>
    /// called when Fighter collides with a collider set to trigger.
    /// </summary>
    /// <param name="other">the collider that triggered the collision</param>
    protected abstract void OnTriggerEnter2D(Collider2D other);
    #endregion

    #region protected abstract void Death();
    /// <summary>
    /// handles death of character.
    /// </summary>
    protected abstract void Death();
    #endregion

    #region protected abstract void Stand();
    /// <summary>
    /// defines behavior while in the Stand state.
    /// </summary>
    protected abstract void Stand();
    #endregion

    #region protected abstract void Walk();
    /// <summary>
    /// defines behavior while in the Walk state.
    /// </summary>
    protected abstract void Walk();
    #endregion

    #region protected abstract void Punch();
    /// <summary>
    /// defines behavior while in the Punch state.
    /// </summary>
    protected abstract void Punch();
    #endregion

    #region protected abstract void Jump();
    /// <summary>
    /// defines Fighter behavior while in the Jump state.
    /// </summary>
    protected abstract void Jump();
    #endregion

    #region protected abstract void SetDirectionFacing();
    /// <summary>
    /// sets the direction the Fighter is facing.
    /// </summary>
    protected abstract void SetDirectionFacing();
    #endregion

    // This method technically virtual, but has no implementation and intended to be overriden by PlayerFighter only
    #region public virtual void InformWorld(bool win);
    /// <summary>
    /// to be overriden by PlayerFighter.
    /// communicates relevant information to rest of game.
    /// </summary>
    /// <param name="win">indicates whether the Player won or lost</param>
    public virtual void InformWorld(bool win) { }
    #endregion
    #endregion

    #region Unity methods

    #region protected virtual void Start();
    /// <summary>
    /// called when character is enabled.
    /// initializes remaining members.
    /// sets starting state to Stand.
    /// ***override for enemy/player specific initialization.
    /// </summary>
    protected virtual void Start() {

        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        punchBox = transform.Find("PunchBox").gameObject;
        cam = GameObject.FindObjectOfType<Camera>();
        state = State.Stand;
        waitForCooldown = false;
        knockedBack = false;

	}
    #endregion

    #region protected virtual void Update();
    /// <summary>
    /// called once per frame.
    /// adjust health slider.
    /// check for and handle death.
    /// set direction facing.
    /// execute code for current state.
    /// **override for enemy/player specific updating.
    /// </summary>
    protected virtual void Update() {

        healthSlider.value = health / maxHealth;
		float healthPercent = (float)health / (float)maxHealth;
		if (healthPercent < 0.7f) {
			var fill = (healthSlider as UnityEngine.UI.Slider).GetComponentsInChildren<UnityEngine.UI.Image>()
				.FirstOrDefault(t => t.name == "Fill");
			if (fill != null)
			{
				if (healthPercent < 0.3f)
					fill.color = Color.red;
				else if(healthPercent < 0.5f)
					fill.color = new Color(1F, 0.6F, 0F, 1F);
				else
					fill.color = Color.yellow;
			}

		}

		healthText.text = "" + health + "/" + maxHealth;
        staminaSlider.value = stamina / maxStamina;

        if (stamina < maxStamina) {

            stamina += recoverRate * Time.deltaTime;
            if (stamina > maxStamina)
                stamina = maxStamina;

        }

        if (health <= 0) {
            Death();
            return;
        }

        SetDirectionFacing();
        ExecuteCurrentState();

	}
    #endregion
    #endregion

    #region User defined methods

    #region protected bool Grounded();
    /// <summary>
    /// checks to see if character is grounded.
    /// uses a Raycast to determine if the ground is within
    ///  the specified distance from the character.
    /// </summary>
    /// <returns>boolean indicating whether the character is grounded</returns>
    protected bool Grounded() {

        if (Physics2D.Raycast(transform.position, Vector2.down, groundedDist, LayerMask.GetMask("Ground")))
            return true;
        else
            return false;

    }
    #endregion

    #region State control methods

    #region protected void ChangeState(State newState);
    /// <summary>
    /// changes the state of the character to that of the parameter.
    /// ensure we have enough stamina to perform desired action,
    ///  do not allow state transition if not.
    /// if the character is standing, zero out its velocity.
    /// if character is punching, decrement stamina by punchCost.
    /// if the character is jumping, set initialGroundedCheck to true (flag for coroutine), 
    ///  decrement stamina by rollCost, and add an upward force.
    /// if character is rolling, set alreadyRolling to false and decrement stamina by rollCost.
    /// values used for transitions between states in the animator correspond to int values
    ///  of the State enum, so we simply set the animator's state parameter to the int value
    ///  of the character's updated state.
    /// </summary>
    /// <param name="newState">the character's state following execution of this function</param>
    protected void ChangeState(State newState) {

        if (!(newState == State.Jump && stamina < jumpCost) &&
            !(newState == State.Punch && stamina < punchCost) &&
            !(newState == State.Roll && stamina < rollCost) &&
            !(newState == State.Punch && waitForCooldown)) {

            state = newState;

            switch (state) {

                case State.Stand:
                    rgb.velocity = Vector2.zero;
                    break;

                case State.Punch:
                    waitForCooldown = true;
                    stamina -= punchCost;
                    break;

                case State.Jump:
                    stamina -= jumpCost;
                    initialGroundedCheck = true;
                    rgb.AddForce(Vector2.up * jumpPower);
                    break;

                case State.Roll:
                    alreadyRolling = false;
                    stamina -= rollCost;
                    break;

                case State.Knockback:
                    knockedBack = true;
                    break;
                
            }

            anim.SetInteger("state", (int)state);
        }
    }
    #endregion

    #region protected void ExecuteCurrentState();
    /// <summary>
    /// executes the code associated with each state
    ///  by simply calling the state-named function.
    /// </summary>
    protected void ExecuteCurrentState() {

        switch (state) {

            case State.Stand:
                Stand();
                break;
            case State.Walk:
                Walk();
                break;
            case State.Punch:
                Punch();
                break;
            case State.Jump:
                Jump();
                break;
            case State.Roll:
                Roll();
                break;
            case State.Knockback:
                Knockback();
                break;
            case State.Falling:
                Falling();
                break;

        }

    }
    #endregion

    #region protected void Roll();
    /// <summary>
    /// defines Fighter behavior while in the Roll state.
    /// if not already rolling, add a force to Player's RigidBody2D.
    /// tell Unity to ignore collisions between Player and Enemy.
    /// starts coroutine that delays exit of roll state.
    /// </summary>
    protected void Roll()
    {

        if (!alreadyRolling)
        {

            float rollMultiplier = rollPower;
            if (!isFacingRight)
                rollMultiplier *= -1;

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            rgb.AddForce(Vector2.right * rollMultiplier, ForceMode2D.Impulse);

        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        StartCoroutine(DelayTransitionFromRoll());

    }
    #endregion

    #region protected void Knockback();
    /// <summary>
    /// defines Fighter behavior while in Knockback state.
    /// starts coroutine to delay transition back to Stand state.
    /// moves character by knockbackDist in direction opposite the one they are facing.
    /// if moving by knockbackDist puts Fighter beyond bounds of camera, adjusts distance
    ///  Fighter will be moved to keep them in frame.
    /// </summary>
    protected void Knockback() {

        StartCoroutine(DelayTransitionFromKnockback());

        if (knockedBack) {

            float translateDist = knockbackDist;
            // distance from player's position.x to x-coordinate of edge of collider
            float halfWidth = gameObject.GetComponent<CapsuleCollider2D>().bounds.size.x / 2;

            if (transform.position.x < opponentTransform.position.x)
            {   // Fighter is to left of opponent

                float leftEdge = cam.ViewportToWorldPoint(Vector2.zero).x;

                // if translating by knockbackDist puts Fighter beyond camera's left edge, adjust translateDist
                if (transform.position.x - halfWidth - knockbackDist < leftEdge)
                    translateDist = leftEdge - (transform.position.x - halfWidth);
                else
                    translateDist *= -1;

            }
            else
            {    // Fighter is to right of opponent

                float rightEdge = cam.ViewportToWorldPoint(Vector2.right).x;

                // if translating by knockbackDist puts Fighter beyond camera's right edge, adjust translateDist
                if (transform.position.x + halfWidth + knockbackDist > rightEdge)
                    translateDist = rightEdge - (transform.position.x + halfWidth);

            }

            gameObject.transform.Translate(Vector2.right * translateDist);
            knockedBack = false;

        }

    }
    #endregion

    #region protected virtual void Falling();
    /// <summary>
    /// defines Fighter behavior while in Falling state.
    /// if on the ground, change state to Stand.
    /// </summary>
    protected virtual void Falling() {

        if (Grounded())
            ChangeState(State.Stand);

    }
    #endregion
    #endregion

    #region Coroutines

    #region protected IEnumerator DelayGroundedCheck();
    /// <summary>
    /// coroutine to delay the check to see if character is grounded.
    /// the purpose is to prevent an early check that causes the jump to malfunction.
    /// if this is the first time we are checking for grounded, wait for specified time.
    /// after we've waited, check if character is grounded and change state to Stand if so.
    /// Subtracts punch cost form stamina.
    /// </summary>
    /// <returns>an amount of time to wait over multiple frames</returns>
    protected IEnumerator DelayGroundedCheck() {

        if (initialGroundedCheck) {
            initialGroundedCheck = false;
            yield return new WaitForSeconds(groundCheckDelay);

        }

        if (Grounded())
            ChangeState(State.Stand);

    }
    #endregion

    #region protected IEnumerator ControlPunchTiming();
    /// <summary>
    /// coroutine to control timing of punch action.
    /// intent is to slow down animation and speed of punching.
    /// waits for specified time before activated collider on fist.
    /// then sets position of collider and activates it.
    /// waits for specified time before deactivating collider on fist.
    /// then deactivates fist's collider. 
    /// Subtracts punch cost from stamina.
    /// </summary>
    /// <returns>times to wait before beginning and ending the punch</returns>
    protected IEnumerator ControlPunchTiming() {

        yield return new WaitForSeconds(punchBeginDelay);

        Vector3 punchBoxPosition = gameObject.transform.position;
        punchBoxPosition.x = isFacingRight ? punchBoxPosition.x + punchBoxOffset : punchBoxPosition.x - punchBoxOffset;
        punchBox.transform.SetPositionAndRotation(punchBoxPosition, Quaternion.identity);
        punchBox.SetActive(true);

        yield return new WaitForSeconds(punchEndDelay);
        punchBox.SetActive(false);
        waitForCooldown = false;

    }
    #endregion

    #region protected IEnumerator DelayTransitionFromRoll();
    /// <summary>
    /// coroutine to control timing of state change following a roll.
    /// if not already rolling, say we are and wait for rollTime.
    /// tell Unity to stop ignoring collisions between Player and Enemy.
    /// change state to Stand.
    /// </summary>
    /// <returns>time to wait prior to exiting Roll state</returns>
    protected IEnumerator DelayTransitionFromRoll() {

        if (!alreadyRolling) {

            alreadyRolling = true;
            yield return new WaitForSeconds(rollTime);

        }

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        ChangeState(State.Stand);

    }
    #endregion

    #region protected IEnumerator DelayTransitionFromKnockback();
    /// <summary>
    /// coroutine to control timing of state change following being knocked back.
    /// simply waits for knockbackTime and changes state to Stand.
    /// </summary>
    /// <returns>time to wait prior to exiting Knockback state</returns>
    protected IEnumerator DelayTransitionFromKnockback() {

        yield return new WaitForSeconds(knockbackTime);
        ChangeState(State.Stand);

    }
    #endregion
    #endregion
    #endregion
}
