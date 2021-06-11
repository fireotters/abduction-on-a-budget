using UnityEngine;

namespace Player
{
    public class UfoController : MonoBehaviour
    {
        private Rigidbody2D _rb2d;
        [SerializeField] private float velocityFactor = 6f;
        private const float MultiAxisThreshold = 0.1f;
        private const float SlowdownFactor = 1.5f;

        private void Start()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var horizontalAxis = Input.GetAxis("Horizontal");
            var verticalAxis = Input.GetAxis("Vertical");

            if (IsHorizontalAxisInThresholdForSpeedReduction(horizontalAxis) && IsVerticalAxisInThresholdForSpeedReduction(verticalAxis))
            {
                horizontalAxis /= SlowdownFactor;
                verticalAxis /= SlowdownFactor;
            }

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
    }
}