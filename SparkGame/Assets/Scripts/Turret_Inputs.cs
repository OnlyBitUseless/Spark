using UnityEngine;

namespace Tank
{
    public class Turret_Inputs : MonoBehaviour
    {
        #region Variables
        public Transform kansi;
        public Transform piippu;
        public LayerMask groundLayer;

        public float turret_rotation_speed = 150f;

        private float angle;
        private Rigidbody rb;
        private Tank_Inputs input;
        
        #endregion
        
        #region BuildinMethods
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<Tank_Inputs>();
            InvokeRepeating("rotateToMouse", 2.0f, 0.1f);
        }

        

        // Update is called once per frame
        void FixedUpdate()
        {
            HandleTurretRotation();
        }

        #endregion

        #region CustomMethods  

        protected virtual void HandleTurretRotation()
        {
            float maxAngleThisFrame = turret_rotation_speed * Time.deltaTime;
            float clampedAngle = Mathf.Clamp(angle, -maxAngleThisFrame, maxAngleThisFrame);
            piippu.RotateAround(kansi.position, Vector3.up, clampedAngle);
            angle = angle-clampedAngle;
        }

        protected virtual void RotationAngleToMouse() 
        {
            Vector3 targetPoint = input.ReticlePosition;

            targetPoint.y = kansi.position.y;
                
            Vector3 direction = targetPoint - kansi.position;

            Vector3 fromDirection = piippu.position - kansi.position;
            Vector3 toDirection = direction;

            angle = Vector3.SignedAngle(fromDirection, toDirection, Vector3.up);
        }
        #endregion
    }
}

