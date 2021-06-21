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
        private Transform _ufoTransform;

        [Header("Mid-air Swing")]
        [SerializeField] private float ThrustSwing = 50000;

        [Header("Touching Ceiling Underwater Swing")]
        [SerializeField] private float swingCeilingUnderwaterMaxSpeed = 2f;
        [SerializeField] private float currentSwingModifier = 1f, SwingWhenTouchingCeilingUnderwaterModifier = 5f;

        [Header("Rope Pull")]
        [SerializeField] private RopeCrank ropeCrank;
        private bool _isRopeInvertOn;
        private const float CooldownBetweenPullsDefault = 0.2f;
        private float timeSpentHoldingSameDir = 0f, currentCooldownBetweenPulls = 0f, lastPullTimer = 0f;

        [Header("Platforming - Ground & Water Check")]
        public bool isTouchingGround = false;
        public bool isFloatingOnWater = false, isSwimmingInWater = false, isTouchingCeiling = false;
        [SerializeField] private Transform groundCheck, deepUnderwaterCheck, ceilingCheck;
        [SerializeField] private LayerMask whatIsGround, whatIsWater;

        private bool lastFrameWasGrounded = false, lastFrameWasFloating = false;

        [Header("Platforming - Movement")]
        [SerializeField] private float MoveSpeed = 700000f;
        [SerializeField] private float VelocityWalkSpeedLimit = 3f, VelocityDragThreshold = 4f;

        [Header("Sound Effects")]
        [SerializeField] private AttachedSoundEffect _sfxLand;
        [SerializeField] private AttachedSoundEffect _sfxSplash, _sfxScream, _sfxWalk, _sfxDragged;
        private float lastWalkSfx, lastDragSfx;
        private const float WalkSfxCooldown = 0.5f, DragSfxCooldown = 0.15f;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _ufoTransform = transform.parent.Find("Ufo").transform;
            _isRopeInvertOn = PlayerPrefs.GetInt("RopeInvert") == 1;
        }

        private void Update()
        {
            if (!isTouchingGround)
                SetAnimations(flying: true);

            CheckForFirstTimeLandWaterContact();

            // Keep in panic animation if the game is over
            if (GameManager.i.gameIsOver)
            {
                _anim.SetBool("flying", true);
            }

            if (rb.velocity[0] > 0.5f)
                transform.localScale = new Vector2(1, 1);
            else if (rb.velocity[0] < -0.5f)
                transform.localScale = new Vector2(-1, 1);
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
                RopeMovement(verticalInput);
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
            var horizontalAxis = Input.GetAxis("P2 Horizontal");

            // Walking animations
            WalkAnims(horizontalAxis);

            // Walking sfx
            if (horizontalAxis != 0 && Time.time > lastWalkSfx + WalkSfxCooldown)
            {
                _sfxWalk.PlaySound();
                lastWalkSfx = Time.time;
            }

            // Walk player side to side if told to by input, only on frames where their velocity is low enough
            if (horizontalAxis != 0 && rb.velocity[0] < VelocityWalkSpeedLimit && rb.velocity[0] > -VelocityWalkSpeedLimit)
            {
                float xForce = horizontalAxis * MoveSpeed * Time.deltaTime;
                Vector2 force = new Vector2(xForce, 0);
                rb.AddForce(force);
            }
        }

        private void WalkAnims(float horizontalAxis)
        {
            // Drag player along floor if their velocity is too high
            if (rb.velocity[0] > VelocityDragThreshold || rb.velocity[0] < -VelocityDragThreshold)
            {
                SetAnimations(dragAnim: true);
                // Dragging sfx
                if (Time.time > lastDragSfx + DragSfxCooldown)
                {
                    _sfxDragged.PlaySound();
                    lastDragSfx = Time.time;
                }

            }
            // Walking anims
            else if (horizontalAxis != 0)
            {
                SetAnimations(walk: true);
            }
            else if (horizontalAxis == 0)
            {
                SetAnimations();
            }
        }

        private void SetAnimations(bool dragAnim = false, bool walk = false,
            bool flying = false)
        {
            _anim.SetBool("drag", dragAnim);
            _anim.SetBool("left", walk);
            _anim.SetBool("flying", flying);
        }

        private void MidairMovement(float horizontalAxis)
        {
            // If the player is underwater touching ceiling, give them some control of alien's movement, limited to a low speed.
            currentSwingModifier = 1f;
            if (isTouchingCeiling && isSwimmingInWater)
            {
                if (rb.velocity[0] < swingCeilingUnderwaterMaxSpeed && rb.velocity[0] > -swingCeilingUnderwaterMaxSpeed)
                    currentSwingModifier = SwingWhenTouchingCeilingUnderwaterModifier;
            }

            // Left swing
            if (horizontalAxis == -1)
                rb.AddRelativeForce(transform.right * -ThrustSwing * currentSwingModifier);

            // Right swing
            if (horizontalAxis == 1)
                rb.AddRelativeForce(transform.right * ThrustSwing * currentSwingModifier);
        }

        private void RopeMovement(float verticalMovement)
        {
            if (verticalMovement != 0)
            {
                // Invert controls if rope invert is enabled, and alien is above UFO for any reason
                if (_isRopeInvertOn && transform.position.y >= _ufoTransform.position.y)
                    verticalMovement = verticalMovement == 1 ? -1 : 1;

                // If rope buttons are held for long enough, reduce cooldown between rope pulls
                timeSpentHoldingSameDir += Time.deltaTime;
                if (timeSpentHoldingSameDir >= 1.2f)
                    currentCooldownBetweenPulls = CooldownBetweenPullsDefault / 3f;

                // Rope retreact/release
                if (Time.time > lastPullTimer + currentCooldownBetweenPulls)
                {
                    lastPullTimer = Time.time;

                    // Retract rope. Disallow when touching ceiling while above water, or when touching ground while below water
                    if (verticalMovement == 1)
                    {
                        if (!isSwimmingInWater && !isTouchingCeiling)
                            ropeCrank.Rotate(-1);
                        else if (isSwimmingInWater && !isTouchingGround)
                            ropeCrank.Rotate(-1);
                    }
                    // Release rope. Allow at all times
                    else if (verticalMovement == -1)
                    {
                        ropeCrank.Rotate(1);
                    }
                }
            }
            else
            {
                // Reset rope pulling speed when no input is detected
                currentCooldownBetweenPulls = CooldownBetweenPullsDefault;
            }
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