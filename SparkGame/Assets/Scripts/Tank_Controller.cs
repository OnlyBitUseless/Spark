using UnityEngine;

namespace Tank
{
    public class Tank_Controller : MonoBehaviour
    {
        #region Variables
        [Header("Movement properties")]
        public float motorTorque = 2000f;
        public float brakeTorque = 2000f;
        public float maxSpeed = 20f;
        public float steeringRange = 30f;
        public float steeringRangeAtMaxSpeed = 10f;
        public float centreOfGravityOffset = -1f;

        private WheelControl[] wheels;
        private Rigidbody rb;
        private Tank_Inputs input;

        #endregion
        
        #region BuildinMethods
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<Tank_Inputs>();

            Vector3 centerOfMass = rb.centerOfMass;
            centerOfMass.y += centreOfGravityOffset;
            rb.centerOfMass = centerOfMass;

            wheels = GetComponentsInChildren<WheelControl>();
        }

        void FixedUpdate()
        {
            if (rb && input)
            {
                HandleMovement();
            }
            
        }
        #endregion

        #region CustomMethods
        protected virtual void HandleMovement()
        {
            float forwardSpeed = Vector3.Dot(transform.forward, rb.linearVelocity);
            float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed)); // Normalized speed factor

            // Reduce motor torque and steering at high speeds for better handling
            float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

            // Determine if the player is accelerating or trying to reverse
            bool isAccelerating = Mathf.Sign(input.ForwardInput) == Mathf.Sign(forwardSpeed);

            foreach (var wheel in wheels)
            {
                if (isAccelerating)
                {
                    // Apply torque to motorized wheels
                    if (wheel.motorized)
                    {
                        if (wheel.transform.localPosition.x < 0){
                            wheel.WheelCollider.motorTorque = (input.ForwardInput * currentMotorTorque) / (2 + input.RotationInput);
                        } else {
                            wheel.WheelCollider.motorTorque = (input.ForwardInput * currentMotorTorque) / (2 - input.RotationInput);
                        }
                    }
                    // Release brakes when accelerating
                    wheel.WheelCollider.brakeTorque = 0f;
                }
                else
                {
                    // Apply brakes when reversing direction
                    wheel.WheelCollider.motorTorque = 0f;
                    wheel.WheelCollider.brakeTorque = Mathf.Abs(input.ForwardInput) * brakeTorque;
                }
            }
        }
        #endregion
    }
}

