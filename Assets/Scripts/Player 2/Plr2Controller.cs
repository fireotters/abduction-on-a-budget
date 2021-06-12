using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plr2Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Mid-air Swing")]
    [SerializeField] private Text textSwingTime;
    private const float ThrustSwing = 50000, CooldownBetweenSwings = 1.2f, SwingSwapForgiveness = 0.1f, SwingTooFast = 7f;
    private float lastSwingTimer = 0f;

    [Header("Rope Pull")]
    [SerializeField] private RopeCrank ropeCrank;
    private const float CooldownBetweenPullsDefault = 0.5f;
    private float timeSpentHoldingSameDir = 0f, currentCooldownBetweenPulls, lastPullTimer = 0f;

    [Header("Platforming - Ground Check")]
    public bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundedRadius = 0f;
    [SerializeField] private Vector2 platformingVelocity = new Vector2();

    [Header("Platforming - Movement")]
    public bool slowEnoughToPlatform = false;
    [SerializeField] private float SlowEnoughToPlatformForgiveness = 0.2f, MoveSpeed = 1000f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    private void Update()
    {
        print(rb.velocity);
        DebugText();
    }

    // Copied from Hold Space to Play's ground check
    private void FixedUpdate()
    {
        GroundedCheck();

        if (isGrounded)
            PlatformingMovement();
        else
            MidairMovement();
        OtherMovement();
    }

    private void GroundedCheck()
    {
        isGrounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    }

    private void PlatformingMovement()
    {
        float xForce = Input.GetAxis("P2 Horizontal") * MoveSpeed * Time.deltaTime;
        Vector2 force = new Vector2(xForce, 0);
        rb.AddForce(force);
    }

    private void MidairMovement()
    {
        // Left swing
        if (Input.GetAxisRaw("P2 Horizontal") == -1 && CanSwing("left"))
            rb.AddRelativeForce(transform.right * -ThrustSwing);

        // Right swing
        if (Input.GetAxisRaw("P2 Horizontal") == 1 && CanSwing("right"))
            rb.AddRelativeForce(transform.right * ThrustSwing);
    }

    private void OtherMovement()
    {
        // If rope buttons are held for long enough, reduce cooldown between rope pulls
        if (Input.GetAxisRaw("P2 Vertical") != 0)
        {
            timeSpentHoldingSameDir += Time.deltaTime;
            if (timeSpentHoldingSameDir >= 1.2f)
            {
                currentCooldownBetweenPulls = CooldownBetweenPullsDefault / 3f;
            }
        }
        else
        {
            currentCooldownBetweenPulls = CooldownBetweenPullsDefault;
        }

        // Rope ascend/descend
        if (Time.time > lastPullTimer + currentCooldownBetweenPulls)
        {
            lastPullTimer = Time.time;

            // Climb rope
            if (Input.GetAxisRaw("P2 Vertical") == 1)
            {
                ropeCrank.Rotate(-1);
            }
            // Descend rope
            else if (Input.GetAxisRaw("P2 Vertical") == -1)
            {
                ropeCrank.Rotate(1);
            }
        }
    }

    // Player is allowed to swing if:
    // - If player isn't moving too fast.
    // - If player is swinging in the same direction as an input, or is moving at a forgiveable opposite speed.
    // - If player has not swung too recently.
    private bool CanSwing(string direction)
    {
        if (rb.velocity[0] > SwingTooFast || rb.velocity[0] < -SwingTooFast)
            return false;
        if (rb.velocity[1] > SwingTooFast || rb.velocity[1] < -SwingTooFast)
            return false;

        if (direction == "left" && rb.velocity[0] > SwingSwapForgiveness)
            return false;
        if (direction == "right" && rb.velocity[0] < -SwingSwapForgiveness)
            return false;

        if (Time.time > lastSwingTimer + CooldownBetweenSwings)
        {
            {
                lastSwingTimer = Time.time;
                return true;
            }
        }
        return false;
    }

    private void DebugText()
    {
        string plr2State = "";
        if (!isGrounded)
        {
            if (rb.velocity[0] > SwingTooFast || rb.velocity[0] < -SwingTooFast || 
                rb.velocity[1] > SwingTooFast || rb.velocity[1] < -SwingTooFast)
                plr2State = "Moving too fast to swing!";
            else if (rb.velocity[0] > -SwingSwapForgiveness && rb.velocity[0] < SwingSwapForgiveness)
                plr2State = "May press either <-- or -->";
            else if (rb.velocity[0] > 0)
                plr2State = "Can only press -->";
            else if (rb.velocity[0] < 0)
                plr2State = "Can only press <--";
        }
        else
        {
            if (rb.velocity[0] < SlowEnoughToPlatformForgiveness && rb.velocity[0] > -SlowEnoughToPlatformForgiveness)
                plr2State = "Grounded. Can walk <->";
            else
                plr2State = "Grounded. Pulled too fast to walk.";
        }



        double swingTimeDisplay = Math.Round(lastSwingTimer + CooldownBetweenSwings - Time.time, 2);
        if (swingTimeDisplay < 0)
            swingTimeDisplay = 0;
        textSwingTime.text = "Swing timer: " + swingTimeDisplay.ToString() + "\n" + plr2State;
    }
}
