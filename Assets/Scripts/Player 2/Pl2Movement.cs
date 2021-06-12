using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pl2Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Plr2")]
    [SerializeField] private const float ThrustSwing = 500, CooldownBetweenSwings = 1.2f, SwingSwapForgiveness = 0.1f;
    private float lastSwingTimer = 0f;
    [SerializeField] private Text textSwingTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        CheckControls();
    }

    private void CheckControls()
    {
        if (Input.GetKeyDown("a") && CanSwing("left"))
        {
            rb.AddRelativeForce(transform.right * -ThrustSwing);
        }
        if (Input.GetKeyDown("d") && CanSwing("right"))
        {
            rb.AddRelativeForce(transform.right * ThrustSwing);
        }
        if (Input.GetKeyDown("w"))
        {

        }
        if (Input.GetKeyDown("s"))
        {

        }

        DebugText();
    }

    // Player is allowed to swing if:
    // 1. If player is swinging in the same direction as an input, or is moving at a forgiveable opposite speed.
    // 2. If player has not swung too recently.
    private bool CanSwing(string direction)
    {
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
        string swingDirDisplay = "";
        if (rb.velocity[0] > -SwingSwapForgiveness && rb.velocity[0] < SwingSwapForgiveness)
            swingDirDisplay = "May press either <-- or -->";
        else if (rb.velocity[0] > 0)
            swingDirDisplay = "Can only press -->";
        else if (rb.velocity[0] < 0)
            swingDirDisplay = "Can only press <--";



        double swingTimeDisplay = Math.Round(lastSwingTimer + CooldownBetweenSwings - Time.time, 2);
        if (swingTimeDisplay < 0)
            swingTimeDisplay = 0;
        textSwingTime.text = "Swing timer: " + swingTimeDisplay.ToString() + "\n" + swingDirDisplay;
    }
}
