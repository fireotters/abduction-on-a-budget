using System;
using In_Game_Items;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class AlienController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator _anim;

        [Header("Only enable if textSwingTime is assigned")]
        public bool enableDebugging = false;
        [SerializeField] private Text textSwingTime;

        [Header("Mid-air Swing")]
        [SerializeField] private float ThrustSwing = 50000;
        [SerializeField] private float CooldownBetweenSwings = 0.5f, SwingNeutralForgiveness = 5f, SwingTooFast = 7f;
        private float lastSwingTimer = 0f;

        [Header("Rope Pull")]
        [SerializeField] private RopeCrank ropeCrank;
        private const float CooldownBetweenPullsDefault = 0.2f;
        private float timeSpentHoldingSameDir = 0f, currentCooldownBetweenPulls, lastPullTimer = 0f;

        [Header("Platforming - Ground & Water Check")]
        public bool isTouchingGround = false;
        public bool isFloatingOnWater = false, isSwimmingInWater = false, isTouchingCeiling = false;
        [SerializeField] private Transform groundCheck, deepUnderwaterCheck, ceilingCheck;
        [SerializeField] private LayerMask whatIsGround, whatIsWater;

        private bool lastFrameWasGrounded = false, lastFrameWasFloating = false;

        [Header("Platforming - Movement")]
        public bool slowEnoughToPlatform = false;
        [SerializeField] private float SlowEnoughToPlatformForgiveness = 0.5f, MoveSpeed = 1000f;

        [Header("Sound Effects")]
        [SerializeField] private AttachedSoundEffect _sfxLand;
        [SerializeField] private AttachedSoundEffect _sfxSplash, _sfxScream, _sfxWalk, _sfxDragged;
        private float lastWalkSfx, lastDragSfx;
        private const float WalkSfxCooldown = 0.5f, DragSfxCooldown = 0.15f;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();


        }

        private void Update()
        {
            if (enableDebugging)
                DebugText();


            if (!isTouchingGround)
                SetAnimations(flying: true);

            CheckForFirstTimeLandWaterContact();

            // Keep in panic animation if the game is over
            if (GameManager.i.gameIsOver)
            {
                _anim.SetBool("flying", true);
            }
        }

        private void CheckForFirstTimeLandWaterContact()
        {
            if (isTouchingGround && !lastFrameWasGrounded)
            {
                _sfxLand.PlaySound();
            }
            lastFrameWasGrounded = isTouchingGround;

            if (isFloatingOnWater && !lastFrameWasFloating)
            {
                _sfxSplash.PlaySound();
            }
            lastFrameWasFloating = isFloatingOnWater;
        }

        // Copied from Hold Space to Play's ground check
        private void FixedUpdate()
        {
            if (!GameManager.i.gameIsOver)
            {
                var horizontalInput = Input.GetAxisRaw("P2 Horizontal");
                var verticalInput = Input.GetAxisRaw("P2 Vertical");

                GroundedOrWaterCheck();

                if (isTouchingGround && !isFloatingOnWater)
                    PlatformingMovement();
                else
                    MidairMovement(horizontalInput);
                OtherMovement(verticalInput);
            }
        }

        private void GroundedOrWaterCheck()
        {
            // Reset state checks
            isTouchingGround = false;
            isTouchingCeiling = false;
            isFloatingOnWater = false;
            isSwimmingInWater = false;

            // Is the player touching ground or ceiling?
            Collider2D[] colGroundTouchCheck = Physics2D.OverlapCircleAll(groundCheck.position, 0.05f, whatIsGround);
            if (colGroundTouchCheck.Length > 0)
                isTouchingGround = true;
            Collider2D[] colCeilingTouchCheck = Physics2D.OverlapCircleAll(ceilingCheck.position, 0.05f, whatIsGround);
            if (colCeilingTouchCheck.Length > 0)
                isTouchingCeiling = true;

            // Is the player floating on or underwater?
            Collider2D[] colWaterFloatCheck = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, whatIsWater); // Radius for float check bumped up, alien bobs in the water
            Collider2D[] colWaterSwimCheck = Physics2D.OverlapCircleAll(deepUnderwaterCheck.position, 0.05f, whatIsWater);
            if (colWaterSwimCheck.Length > 0)
                isSwimmingInWater = true;
            else if (colWaterFloatCheck.Length > 0)
                isFloatingOnWater = true;
        }

        private void PlatformingMovement()
        {
            if (rb.velocity[0] < SlowEnoughToPlatformForgiveness && rb.velocity[0] > -SlowEnoughToPlatformForgiveness)
            {
                var horizontalAxis = Input.GetAxis("P2 Horizontal");

                float xForce = horizontalAxis * MoveSpeed * Time.deltaTime;
                Vector2 force = new Vector2(xForce, 0);
                rb.AddForce(force);

                // Walking anims
                if (horizontalAxis > 0)
                {
                    SetAnimations(walkLeft: true);
                }
                else if (horizontalAxis < 0)
                {
                    SetAnimations(walkRight: true);
                }
                else if (horizontalAxis == 0 &&
                         (xForce < SlowEnoughToPlatformForgiveness && xForce > -SlowEnoughToPlatformForgiveness))
                {
                    SetAnimations();
                }

                // Walking sfx
                if (horizontalAxis != 0 && Time.time > lastWalkSfx + WalkSfxCooldown)
                {
                    _sfxWalk.PlaySound();
                    lastWalkSfx = Time.time;
                }
            }
            else
            {
                SetAnimations(dragAnim: true);
                // Dragging sfx
                if (Time.time > lastDragSfx + DragSfxCooldown)
                {
                    _sfxDragged.PlaySound();
                    lastDragSfx = Time.time;
                }
            }
        }

        private void SetAnimations(bool dragAnim = false, bool walkLeft = false, bool walkRight = false,
            bool flying = false)
        {
            _anim.SetBool("drag", dragAnim);
            _anim.SetBool("left", walkLeft);
            _anim.SetBool("right", walkRight);
            _anim.SetBool("flying", flying);
        }

        private void MidairMovement(float horizontalAxis)
        {
            // Left swing
            if (horizontalAxis == -1 && CanSwing("left"))
                rb.AddRelativeForce(transform.right * -ThrustSwing);

            // Right swing
            if (horizontalAxis == 1 && CanSwing("right"))
                rb.AddRelativeForce(transform.right * ThrustSwing);
        }

        private void OtherMovement(float verticalMovement)
        {
            // If rope buttons are held for long enough, reduce cooldown between rope pulls
            if (verticalMovement != 0)
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

                // Climb rope. Disallow when touching ceiling
                if (verticalMovement == 1 && !isTouchingCeiling)
                {
                    ropeCrank.Rotate(-1);
                }
                // Descend rope
                else if (verticalMovement == -1)
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

            //if (direction == "left" && rb.velocity[0] > SwingNeutralForgiveness)
            //    return false;
            //if (direction == "right" && rb.velocity[0] < -SwingNeutralForgiveness)
            //    return false;

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
            if (isFloatingOnWater)
            {
                plr2State = "Swimming. Can launch <-- or -->";
            }
            else if (!isTouchingGround)
            {
                if (rb.velocity[0] > SwingTooFast || rb.velocity[0] < -SwingTooFast ||
                    rb.velocity[1] > SwingTooFast || rb.velocity[1] < -SwingTooFast)
                    plr2State = "Moving too fast to swing!";
                else if (rb.velocity[0] > -SwingNeutralForgiveness && rb.velocity[0] < SwingNeutralForgiveness)
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.transform.tag)
            {
                case "Lock":
                    var lockGate = other.gameObject.GetComponent<LockedGate>();
                    if (GameManager.i.keyCount > 0 && lockGate.unlockable)
                    {
                        GameManager.i.keyCount--;
                        lockGate.DestroyLock();
                    }
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Key":
                    var key = other.gameObject.GetComponent<Key>();
                    GameManager.i.keyCount++;
                    key.DestroyCollectible();
                    break;
                case "Human":
                    var human = other.gameObject.GetComponent<Human>();
                    GameManager.i.humanCount++;
                    human.DestroyCollectible();
                    break;
                case "Respawn":
                    if (!GameManager.i.gameIsOver)
                    {
                        GameManager.i.PlayerDied();
                        _anim.SetBool("fear", true);
                        _sfxScream.PlaySound();
                    }
                    break;
            }
        }
    }
}