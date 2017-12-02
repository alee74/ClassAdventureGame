using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Class for enemies, serves as base class for player
public class Fighter : MonoBehaviour {

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

    // members specific to enemy
    private float xAttackDist = 1f;         // needs to be public if different for different enemies
    private float yAttackDist = 2f;         // needs to be public if different for different enemies
    #endregion


    #region Unity methods

    #region protected void Start();
    /// <summary>
    /// called when character is enabled.
    /// calls OnStart() to initialize enemy/player specific members.
    /// initializes remaining members.
    /// sets starting state to Stand.
    /// </summary>
    protected void Start() {

        OnStart();

        health = maxHealth;
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        punchBox = transform.Find("PunchBox").gameObject;
        state = State.Stand;

	}
    #endregion

    #region protected void Update();
    /// <summary>
    /// called once per frame.
    /// adjust health slider.
    /// check for and handle death.
    /// set direction facing.
    /// execute code for current state.
    /// </summary>
    protected void Update() {

        healthSlider.value = health / maxHealth;

        if (health <= 0) {
            Death();
            return;
        }

        SetDirectionFacing();
        ExecuteCurrentState();

	}
    #endregion

    #region protected virtual void OnTriggerEnter2D(Collider2D other);
    /// <summary>
    /// called when character collides with a collider set to trigger.
    /// if it's the player's fist, take damage and get knocked back.
    /// </summary>
    /// <param name="other">the collider that triggered the collision</param>
    protected virtual void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "PlayerFist") {
            health--;
            Knockback();
        }

    }
    #endregion
    #endregion

    #region User defined methods

    #region protected virtual void OnStart();
    /// <summary>
    /// called from Start().
    /// performs enemy specific initialization.
    /// </summary>
    protected virtual void OnStart() {

        maxHealth = 5f;
        healthSlider = GameObject.Find("EnemyHealth").GetComponent<Slider>();
        opponent = GameObject.FindGameObjectWithTag("Player").transform;

    }
    #endregion

    #region protected void Knockback();
    /// <summary>
    /// called from OnTriggerEnter2D when taking damage from opponent.
    /// moves character by knockbackDist in direction opposite the one they are facing.
    /// </summary>
    protected void Knockback() {

        if (transform.position.x < opponent.position.x)
            gameObject.transform.Translate(Vector3.left * knockbackDist);
        else
            gameObject.transform.Translate(Vector3.right * knockbackDist);

    }
    #endregion

    #region protected void SetDirectionFacing();
    /// <summary>
    /// sets the direction the character is facing.
    /// - only changes direction if moving;
    /// - sets SpriteRenderer.flipX, which indicates if we want the sprite to be flipped;
    ///   -- because of this option, we don't need to do anything else!
    ///   
    /// functionality is same for player and enemy, so not marked virtual, which
    ///  means this function cannot be overriden.
    /// </summary>
    protected void SetDirectionFacing() {

        float xVelocity = rgb.velocity.x;

        if (xVelocity > 0)
            isFacingRight = true;
        else if (xVelocity < 0)
            isFacingRight = false;

        gameObject.GetComponent<SpriteRenderer>().flipX = !isFacingRight;

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

    // FIXME
    #region protected virtual void Death();
    /// <summary>
    /// handles death of character.
    /// sets endGameText.
    /// loads world map scene.
    /// destroys this character.
    /// ***Need to set variable in world indicating we won before loading scene
    /// ***should probably also set players health in world
    /// </summary>
    protected virtual void Death() {

        GameObject.Find("EndGameText").GetComponent<Text>().text = "YOU WON!";
        SceneManager.LoadScene("WorldMapMainScene");
        Destroy(gameObject);

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
                Walk();
                break;
            case State.Walk:
                Stand();
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

    #region protected virtual void Stand();
    /// <summary>
    /// defines the character's behavior while in the Stand state.
    /// if the player is outside of our horizontal attack range, we walk.
    /// otherwise, ensure player is within vertical attack range and attack (punch).
    /// </summary>
    protected virtual void Stand() {

        if (Mathf.Abs(transform.position.x - opponent.position.x) > xAttackDist)
            ChangeState(State.Walk);
        else if (Mathf.Abs(transform.position.y - opponent.position.y) <= yAttackDist)
            ChangeState(State.Punch);

    }
    #endregion

    #region protected virtual void Walk();
    /// <summary>
    /// defines character's behavior while in the Walk state.
    /// if character is grounded, sets velocity to be in the direction
    ///  towards the player and 
    /// changes state to punch if player is within horizontal attack distance.
    /// </summary>
    protected virtual void Walk() {

        if (Grounded()) {

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

    #region protected virtual void Punch();
    /// <summary>
    /// defines character's behavior while in the Punch state.
    /// if the player is beyond the horizontal attack range, we walk.
    /// otherwise, we stand.
    /// </summary>
    protected virtual void Punch() {

        StartCoroutine(ControlPunchTiming());

        if (Mathf.Abs(transform.position.x - opponent.position.x) >= xAttackDist)
            ChangeState(State.Walk);
        else
            ChangeState(State.Stand);

    }
    #endregion

    // FIXME
    #region protected virtual void Jump();
    /// <summary>
    /// defines character's behavior while in the Jump state.
    /// ***currently only starts coroutine. need to fix.
    /// </summary>
    protected virtual void Jump() {

        StartCoroutine(DelayGroundedCheck());

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
