using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class UfoController : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        private static Animator _anim;
        private AudioSource _audioSource;
        [SerializeField] private float velocityFactor = 6f;
        [SerializeField] private AudioClip[] collisionSound;
        [SerializeField] private AudioClip moveSound;
        [SerializeField] private ParticleSystem sparkParticles;
        //[SerializeField] private AudioClip[] collisionUnderwaterSound;
        private const float MultiAxisThreshold = 0.1f;
        private const float SlowdownFactor = 1.5f;
        private Vector2 calculatedForce = new Vector2();

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _rb2d = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            var horizontalAxis = Input.GetAxis("P1 Horizontal");
            var verticalAxis = Input.GetAxis("P1 Vertical");

            if (IsHorizontalAxisInThresholdForSpeedReduction(horizontalAxis) && IsVerticalAxisInThresholdForSpeedReduction(verticalAxis))
            {
                horizontalAxis /= SlowdownFactor;
                verticalAxis /= SlowdownFactor;
            }

            calculatedForce = CalculateForce(horizontalAxis, verticalAxis);

            // Disallow input if game is over, or level only just started
            if (GameManager.i.gameIsOver || Time.timeSinceLevelLoad < 1f)
            {
                calculatedForce = new Vector2(0, 0);
            }

            SetAnim(calculatedForce.x, calculatedForce.y);
        }

        private void FixedUpdate()
        {
            _rb2d.MovePosition(_rb2d.position + calculatedForce * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlayCollisionSound();
            Instantiate(sparkParticles, other.contacts[0].point, Quaternion.identity);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            Instantiate(sparkParticles, other.contacts[0].point, Quaternion.identity);
        }

        private void PlayCollisionSound()
        {
            _audioSource.clip = collisionSound[Random.Range(0, collisionSound.Length)];
            _audioSource.Play();
        }
        private void PlayMoveSound()
        {
            _audioSource.clip = moveSound;
            _audioSource.Play();
        }
        
        private static bool IsHorizontalAxisInThresholdForSpeedReduction(float horizontalAxis)
        {
            return horizontalAxis > MultiAxisThreshold || horizontalAxis < -MultiAxisThreshold;
        }

        private static bool IsVerticalAxisInThresholdForSpeedReduction(float verticalAxis)
        {
            return verticalAxis > MultiAxisThreshold || verticalAxis < -MultiAxisThreshold;
        }

        private Vector2 CalculateForce(float horizontalAxis, float verticalAxis)
        {
            var newPos = new Vector2(horizontalAxis, verticalAxis);

            return newPos * velocityFactor;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "WaterLayerTileMap")
                _anim.SetBool("water", true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "WaterLayerTileMap")
                _anim.SetBool("water", false);
        }

        private void SetAnim(float horizontalInput, float verticalInput)
        {
            if (horizontalInput == 0 && verticalInput > 0) //Goes up
            {
                SetAnimatorValues(true);
            }
            else if (horizontalInput == 0 && verticalInput < 0) //Goes down
            {
                SetAnimatorValues(down: true);
            }
            else if (horizontalInput > 0 && verticalInput == 0) //Goes right
            {
                SetAnimatorValues(right: true);
            }
            else if (horizontalInput < 0 && verticalInput == 0) //Goes left
            {
                SetAnimatorValues(left: true);
            }
            else if (horizontalInput < 0 && verticalInput > 0) //Goes up left
            {
                SetAnimatorValues(true, left: true);
            }
            else if (horizontalInput < 0 && verticalInput < 0) //Goes down left
            {
                SetAnimatorValues(down: true, left: true);
            }
            else if (horizontalInput > 0 && verticalInput > 0) //Goes up right
            {
                SetAnimatorValues(true, right: true);
            }
            else if (horizontalInput > 0 && verticalInput < 0) //Goes down right
            {
                SetAnimatorValues(down: true, right: true);
            }
            else
            {
                SetAnimatorValues();
            }
        }

        private static void SetAnimatorValues(bool up = false, bool down = false, bool left = false, bool right = false)
        {
            _anim.SetBool("up", up);
            _anim.SetBool("down", down);
            _anim.SetBool("left", left);
            _anim.SetBool("right", right);
        }
    }
}