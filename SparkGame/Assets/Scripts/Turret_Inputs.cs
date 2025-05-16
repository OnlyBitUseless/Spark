using UnityEngine;

namespace Tank
{
    public class Turret_Inputs : MonoBehaviour
    {
        #region Variables
        public Transform tankBase;
        public Transform tankBarrel;
        public Transform tankBarrelEndPoint;

        public LayerMask groundLayer;
        public ProjectileController projectile;


        public float turretRotationSpeed = 150f;
        public float turretBulletSpeed= 12f;

        private float angle;
        private Vector3 targetPoint;
        private Tank_Inputs input;
        
        public int maxBulletAmount = 5;
        
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
                if (input.FireInput)
                {
                    HandleShooting();
                }
            }
        }

        #endregion

        #region CustomMethods  
        

        protected virtual void HandleTurretRotation()
        {
            float rotationDelta = turretRotationSpeed * Time.deltaTime;
            float rotationOffset = Mathf.Clamp(angle, -rotationDelta, rotationDelta);
            
            tankBarrel.RotateAround(tankBase.position, transform.up, rotationOffset);
            angle -= rotationOffset;
        }

        protected virtual void CalculateRotationAngle()
        {
            targetPoint = input.ReticlePosition;

            Vector3 toDirection = targetPoint - tankBase.position;
            toDirection.y = 0;

            Vector3 currentDirection = tankBarrel.position - tankBase.position;
            currentDirection.y = 0;

            angle = Vector3.SignedAngle(currentDirection, toDirection, transform.up);
        }

        protected virtual void HandleShooting()
        {
            ProjectileController instance = ObjectPooler.DequeueObject<ProjectileController>("Projectile");
            Physics.IgnoreCollision(instance.GetComponent<Collider>(), tankBarrel.GetComponent<Collider>());

            instance.transform.position = tankBarrelEndPoint.position;
            instance.transform.rotation = tankBarrel.rotation;
            instance.gameObject.SetActive(true);
            instance.Initialize(100.0f, 100.0f);
        }
        #endregion
    }
}

