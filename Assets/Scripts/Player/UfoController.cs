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
        //[SerializeField] private AudioClip[] collisionUnderwaterSound;
        private const float MultiAxisThreshold = 0.1f;
        private const float SlowdownFactor = 1.5f;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _rb2d = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (!GameManager.i.gameIsOver)
            {
                var horizontalAxis = Input.GetAxis("P1 Horizontal");
                var verticalAxis = Input.GetAxis("P1 Vertical");

                if (IsHorizontalAxisInThresholdForSpeedReduction(horizontalAxis) && IsVerticalAxisInThresholdForSpeedReduction(verticalAxis))
                {
                    horizontalAxis /= SlowdownFactor;
                    verticalAxis /= SlowdownFactor;
                }

                var calculatedForce = CalculateForce(horizontalAxis, verticalAxis);
                
                SetAnim(calculatedForce);

                _rb2d.MovePosition(_rb2d.position + calculatedForce * Time.fixedDeltaTime);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlayCollisionSound();
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

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.name == "WaterLayerTileMap")
                _anim.SetBool("water", true);
        }

        void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.name == "WaterLayerTileMap")
                _anim.SetBool("water", false);
        }

        private static void SetAnim(Vector2 force)
        {
            if (Input.GetAxis("P1 Horizontal") == 0 && Input.GetAxis("P1 Vertical") > 0) //Goes up
            {
                _anim.SetBool("up", true);
                _anim.SetBool("down", false);
                _anim.SetBool("left", false);
                _anim.SetBool("right", false);
            }
            else if (Input.GetAxis("P1 Horizontal") == 0 && Input.GetAxis("P1 Vertical") < 0) //Goes down
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", true);
                _anim.SetBool("left", false);
                _anim.SetBool("right", false);
            }
            else if (Input.GetAxis("P1 Horizontal") > 0 && Input.GetAxis("P1 Vertical") == 0) //Goes right
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", false);
                _anim.SetBool("left", false);
                _anim.SetBool("right", true);
            }
            else if (Input.GetAxis("P1 Horizontal") < 0 && Input.GetAxis("P1 Vertical") == 0) //Goes left
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", false);
                _anim.SetBool("left", true);
                _anim.SetBool("right", false);
            }
            else if (Input.GetAxis("P1 Horizontal") < 0 && Input.GetAxis("P1 Vertical") > 0) //Goes up left
            {
                _anim.SetBool("up", true);
                _anim.SetBool("down", false);
                _anim.SetBool("left", true);
                _anim.SetBool("right", false);
            }
            else if (Input.GetAxis("P1 Horizontal") < 0 && Input.GetAxis("P1 Vertical") < 0) //Goes down left
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", true);
                _anim.SetBool("left", true);
                _anim.SetBool("right", false);
            }
            else if (Input.GetAxis("P1 Horizontal") > 0 && Input.GetAxis("P1 Vertical") > 0) //Goes up right
            {
                _anim.SetBool("up", true);
                _anim.SetBool("down", false);
                _anim.SetBool("left", false);
                _anim.SetBool("right", true);
            }
            else if (Input.GetAxis("P1 Horizontal") > 0 && Input.GetAxis("P1 Vertical") < 0) //Goes down right
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", true);
                _anim.SetBool("left",false);
                _anim.SetBool("right", true);
            }
            else
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", false);
                _anim.SetBool("left", false);
                _anim.SetBool("right", false);
            }
        }
    }
}