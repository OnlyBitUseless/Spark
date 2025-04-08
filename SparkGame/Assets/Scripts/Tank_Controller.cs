using UnityEngine;

namespace Tank
{
    public class Tank_Controller : MonoBehaviour
    {
        #region Variables
        [Header("Movement properties")]
        public float tank_speed = 15f;
        public float tank_rotation_speed = 20f;


        private Rigidbody rb;
        private Tank_Inputs input;
        #endregion
        
        #region BuildinMethods
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<Tank_Inputs>();
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
            Vector3 target_postion = transform.position + (transform.forward * input.ForwardInput * tank_speed * Time.deltaTime);
            rb.MovePosition(target_postion);

            Quaternion target_rotation = transform.rotation * Quaternion.Euler(Vector3.up * (tank_rotation_speed * input.RotationInput * Time.deltaTime));
            rb.MoveRotation(target_rotation);
        }
        #endregion
    }
}

