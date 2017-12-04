using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class for enemies, serves as base class for player
public abstract class Fighter : MonoBehaviour {

    // TODO : decrement health by damage of opponent

    #region Member declarations

    // states for both player and enemy
    protected enum State { Stand, Walk, Punch, Jump }

    // members common to enemy and player
    protected Rigidbody2D rgb;
    protected Animator anim;
    protected State state;
    protected bool isFacingRight;
    protected float health;
    protected float maxHealth;
    protected Slider healthSlider;
    protected GameObject punchBox;
    protected Transform opponent;

    // members we can set from editor.
    public float speed;
    public float jumpPower;
    public float damage;
    public float knockbackDist;

    // members that will not be overriden
    private bool initialGroundedCheck;
    private float punchBoxOffset = 0.75f;   // needs to be public if different for different enemies (will be overriden in this case)
    private static float groundCheckDelay = 0.05f;
    private static float groundedDist = 0.85f;
    private static float punchBeginDelay = 0.25f;   // needs to be public if different for different enemies (will be overriden in this case)
    private static float punchEndDelay = 0.15f;     // needs to be public if different for different enemies (will be overriden in this case)
    private Camera cam;
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
    /// defines behavior while in the Jump state.
    /// </summary>
    protected abstract void Jump();
    #endregion

    #region protected abstract void SetDirectionFacing();
    /// <summary>
    /// sets the direction the Fighter is facing.
    /// </summary>
    protected abstract void SetDirectionFacing();
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

    #region protected void Knockback();
    /// <summary>
    /// called from OnTriggerEnter2D when taking damage from opponent.
    /// moves character by knockbackDist in direction opposite the one they are facing.
    /// if moving by knockbackDist puts Fighter beyond bounds of camera, adjusts distance
    ///  Fighter will be moved to keep them in frame.
    /// </summary>
    protected void Knockback() {

        float translateDist = knockbackDist;
        // distance from player's position.x to x-coordinate of edge of collider
        float halfWidth = gameObject.GetComponent<CapsuleCollider2D>().bounds.size.x / 2;

        if (transform.position.x < opponent.position.x) {   // Fighter is to left of opponent

            float leftEdge = cam.ViewportToWorldPoint(Vector2.zero).x;

            // if translating by knockbackDist puts Fighter beyond camera's left edge, adjust translateDist
            if (transform.position.x - halfWidth - knockbackDist < leftEdge)
                translateDist = leftEdge - (transform.position.x - halfWidth);
            else
                translateDist *= -1;

        } else {    // Fighter is to right of opponent

            float rightEdge = cam.ViewportToWorldPoint(Vector2.right).x;

            // if translating by knockbackDist puts Fighter beyond camera's right edge, adjust translateDist
            if (transform.position.x + halfWidth + knockbackDist > rightEdge)
                translateDist = rightEdge - (transform.position.x + halfWidth);

        }

        gameObject.transform.Translate(Vector2.right * translateDist);

    }
    #endregion

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
    /// if the character is standing, zero out its velocity.
    /// if the character is jumping, set initialGroundedCheck to true (flag for coroutine) 
    ///  and add an upward force.
    /// values used for transitions between states in the animator correspond to int values
    ///  of the State enum, so we simply set the animator's state parameter to the int value
    ///  of the character's updated state.
    /// </summary>
    /// <param name="newState">the character's state following execution of this function</param>
    protected void ChangeState(State newState) {

        state = newState;

        switch (state) {

            case State.Stand:
                rgb.velocity = Vector2.zero;
                break;

            case State.Jump:
                initialGroundedCheck = true;
                rgb.AddForce(Vector2.up * jumpPower);
                break;
        }

        anim.SetInteger("state", (int)state);

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
        }

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
    /// </summary>
    /// <returns>an amount of time to wait over multiple frames</returns>
    protected IEnumerator DelayGroundedCheck() {

        if (initialGroundedCheck) {

            yield return new WaitForSeconds(groundCheckDelay);
            initialGroundedCheck = false;

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

    }
    #endregion
    #endregion
    #endregion
}
