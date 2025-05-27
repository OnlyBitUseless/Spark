using UnityEngine;
 
 namespace Tank
 {
    public class TankController : MonoBehaviour
    {
        #region Variables
        [Header("Movement properties")]
        public float tank_speed = 15f;
        public float tank_max_speed = 100f;
        public float tank_rotation_speed = 160f;
        public float tank_rotation_at_max_speed = 15f;

        public float turretRotationSpeed = 150f;

        public float minProjectileSpeed = 20f;

        public float minProjectileCooldown = 1f;
        public float maxHoldMultiplier = 3.0f;

        private float startTime;
        private float lastTime;

        private bool firingState;

        private float angle;
        private Vector3 targetPoint;

        [Header("Game objects")]
        private Rigidbody rb;
        public Transform tankBase;
        public Transform tankBarrel;
        public Transform tankBarrelEndPoint;

        [Header("Layers")]
        public LayerMask groundLayer;

        [Header("Scripts")]
        public ProjectileController projectile;
        private TankInputs input;


        #endregion

        #region BuildinMethods
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<TankInputs>();
            InvokeRepeating("CalculateRotationAngle", 0.0f, 0.1f);
        }
 
        void FixedUpdate()
        {
            if (rb && input)
            {
                HandleTurretRotation();
                if (input.IsGrounded) HandleMovement();

                if (!firingState)
                {
                    Debug.Log(Time.time-lastTime);
                    if (input.FireInputStarted) {
                        startTime = Time.time;
                    }

                    if (input.FireInputEnded)
                    {
                        firingState = true;
                        Invoke("HandleShooting", Mathf.Max(0, minProjectileCooldown - (Time.time - lastTime)));
                    }
                }
                
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
            float forward_speed = Vector3.Dot(tankBarrel.forward, rb.linearVelocity);
            float holdBonusToSpeed = Mathf.Min(Time.time-startTime, maxHoldMultiplier);
            float projectileStartingSpeed = forward_speed + minProjectileSpeed + (holdBonusToSpeed)*10;

            ProjectileController instance = ObjectPooler.DequeueObject<ProjectileController>("Projectile");
            Physics.IgnoreCollision(instance.GetComponent<Collider>(), tankBarrel.GetComponent<Collider>());
            instance.transform.position = tankBarrelEndPoint.position;
            instance.transform.rotation = tankBarrel.rotation;
            
            instance.Initialize(projectileStartingSpeed, projectileStartingSpeed*10);
            instance.gameObject.SetActive(true);
            
            rb.AddForce(-1*(projectileStartingSpeed/5) * tankBarrel.up, ForceMode.Impulse);

            firingState = false;
            lastTime = Time.time;
        }

        #endregion
    }
}
