using UnityEngine;

namespace Tank
{
    public class Turret_Inputs : MonoBehaviour
    {
        #region Variables
        public Transform tankBase;
        public Transform tankBarrel;
        public GameObject tankProjectile;
        public LayerMask groundLayer;

        public float turretRotationSpeed = 150f;
        public float projectileLaunchVelocity = 700f;

        private float angle;
        private Vector3 targetPoint;
        private Tank_Inputs input;
        
        #endregion
        
        #region BuildinMethods
        void Start()
        {
            input = GetComponent<Tank_Inputs>();
            InvokeRepeating("CalculateRotationAngle", 0.0f, 0.1f);
        }

        void FixedUpdate()
        {
            if (input)
            {
                HandleTurretRotation();
                if (input.FireInput) HandleShooting();
            }
        }

        #endregion

        #region CustomMethods  
        

        protected virtual void HandleTurretRotation()
        {
            float maxAngleThisFrame = turretRotationSpeed * Time.deltaTime;
            float clampedAngle = Mathf.Clamp(angle, -maxAngleThisFrame, maxAngleThisFrame);
            tankBarrel.RotateAround(tankBase.position, transform.up, clampedAngle);
            angle = angle-clampedAngle;
        }

        protected virtual void CalculateRotationAngle()
        {
            targetPoint = input.ReticlePosition;

            Vector3 direction = targetPoint - tankBase.position;

            Vector3 fromDirection = tankBarrel.position - tankBase.position;
            Vector3 toDirection = direction;

            angle = Vector3.SignedAngle(fromDirection, toDirection, transform.up);
        }

        protected virtual void HandleShooting()
        {
            
            GameObject ball = Instantiate(tankProjectile, tankBarrel.position,  tankBarrel.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(-1*tankBarrel.forward * projectileLaunchVelocity);
        }

        #endregion
    }
}

