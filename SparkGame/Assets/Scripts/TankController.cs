using UnityEngine;
 
 namespace Tank
 {
    public class TankController : MonoBehaviour
    {
        #region Variables
        [Header("Movement properties")]
        public float tank_speed = 15f;
        public float tank_max_speed = 100f;
        public float tank_rotation_speed = 200f;
        public float tank_rotation_at_max_speed = 15f;
        public float groundCheckDistance = 1.5f;

        public float turretRotationSpeed = 150f;
        public float turretBulletSpeed = 12f;
        public float impulse = 30f;

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

            Vector3 RaycastOrigin = transform.position + Vector3.up;

            bool isGrounded = Physics.Raycast(RaycastOrigin, Vector3.down, groundCheckDistance);

            //Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.blue);

            if (rb && input && isGrounded)
            {
                if (isGrounded) HandleMovement();

                HandleTurretRotation();
                if (input.FireInput)
                {
                    HandleShooting();
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
