using UnityEngine;

namespace Player
{
    public class UfoController : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        private static Animator _anim;
        public Material _mat;
        public Texture2D _mainTexture;
        public float _vibrationIntensity = 3f;
        public Color _borderColor;
        public float _borderThickness = 0.01f;
        [SerializeField] private float velocityFactor = 6f;
        private const float MultiAxisThreshold = 0.1f;
        private const float SlowdownFactor = 1.5f;

        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _mat.SetTexture("Texture2D_3c01c3ce242341a781b39a6abb8e7cab", _mainTexture);
            _mat.SetFloat("Vector1_61d4bf80d40f40efb005708b991807fa", _vibrationIntensity);
            _mat.SetColor("Color_4a10e2379d244746a98ee648a335658d", _borderColor);
            _mat.SetFloat("Vector1_aad1dfb20c0d4c9f811619471f7664b4", _borderThickness);
        }

        private void FixedUpdate()
        {
            var horizontalAxis = Input.GetAxis("P1 Horizontal");
            var verticalAxis = Input.GetAxis("P1 Vertical");

            if (IsHorizontalAxisInThresholdForSpeedReduction(horizontalAxis) && IsVerticalAxisInThresholdForSpeedReduction(verticalAxis))
            {
                horizontalAxis /= SlowdownFactor;
                verticalAxis /= SlowdownFactor;
            }

            SetAnim(CalculateForce(horizontalAxis, verticalAxis));

            _rb2d.MovePosition(_rb2d.position + CalculateForce(horizontalAxis, verticalAxis) * Time.fixedDeltaTime);
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

        private static void SetAnim(Vector2 force)
        {
            if (force.x == 0 && force.y > 0) //Goes up
            {
                _anim.SetBool("up", true);
                _anim.SetBool("down", false);
                _anim.SetBool("left", false);
                _anim.SetBool("right", false);
            }
            else if (force.x == 0 && force.y < 0) //Goes down
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", true);
                _anim.SetBool("left", false);
                _anim.SetBool("right", false);
            }
            else if (force.x > 0 && force.y == 0) //Goes right
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", false);
                _anim.SetBool("left", false);
                _anim.SetBool("right", true);
            }
            else if (force.x < 0 && force.y == 0) //Goes left
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", false);
                _anim.SetBool("left", true);
                _anim.SetBool("right", false);
            }
            else if (force.x < 0 && force.y > 0) //Goes up left
            {
                _anim.SetBool("up", true);
                _anim.SetBool("down", false);
                _anim.SetBool("left", true);
                _anim.SetBool("right", false);
            }
            else if (force.x < 0 && force.y < 0) //Goes down left
            {
                _anim.SetBool("up", false);
                _anim.SetBool("down", true);
                _anim.SetBool("left", true);
                _anim.SetBool("right", false);
            }
            else if (force.x > 0 && force.y > 0) //Goes up right
            {
                _anim.SetBool("up", true);
                _anim.SetBool("down", false);
                _anim.SetBool("left", false);
                _anim.SetBool("right", true);
            }
            else if (force.x > 0 && force.y < 0) //Goes down right
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