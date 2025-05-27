using UnityEngine;

namespace Tank
{
    public class Enemy_Turret : MonoBehaviour
    {
        #region Variables
        public Transform tankBase;
        public Transform tankBarrel;
        public Transform tankBarrelEndPoint;

        public LayerMask groundLayer;
        public ProjectileController projectile;
        public GameObject target;
        public float turretRotationSpeed = 150f;
        public float turretBulletSpeed = 12f;
        public float impulse = 30f;
        public float cooldown = 0f;
        private float angle;
        private Vector3 targetPoint;
        
        #endregion
        
        #region BuildinMethods
        void Start()
        {
            InvokeRepeating("CalculateRotationAngle", 0.0f, 0.1f);
        }

        void FixedUpdate()
        {
            cooldown = cooldown + Time.deltaTime;
            HandleTurretRotation();
            
            if (cooldown >= 3f)
            {
            HandleShooting();
            cooldown = 0;
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
            Vector3 targetPoint = target.transform.position;

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
            
            instance.Initialize(turretBulletSpeed, 100.0f);
            instance.gameObject.SetActive(true);
            
        }
        #endregion
    }
}

