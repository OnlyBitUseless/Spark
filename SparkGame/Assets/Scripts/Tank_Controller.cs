using UnityEngine;
 
 namespace Tank
 {
     public class Tank_Controller : MonoBehaviour
     {
         #region Variables
         [Header("Movement properties")]
         public float tank_speed = 15f;
         public float tank_max_speed = 100f;
         public float tank_rotation_speed = 200f;
         public float tank_rotation_at_max_speed = 15f;
 
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
            float forward_speed = Vector3.Dot(transform.forward, rb.linearVelocity);
            float speed_factor = Mathf.InverseLerp(0, tank_max_speed, Mathf.Abs(forward_speed));

            float current_speed = Mathf.Lerp(tank_speed, 0, speed_factor);
            float current_rotation = Mathf.Lerp(tank_rotation_speed, tank_rotation_at_max_speed, speed_factor);

            Vector3 movement = transform.forward * input.ForwardInput * current_speed;
            rb.AddForce(movement);
 
            Quaternion target_rotation = transform.rotation * Quaternion.Euler(Vector3.up * (current_rotation* input.RotationInput * Time.deltaTime));
            rb.MoveRotation(target_rotation);
         }
         #endregion
     }
 }

