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
        private Vector3 targetPoint;
        private Tank_Inputs input;
        
        #endregion
        
        #region BuildinMethods
        void Start()
        {
            input = GetComponent<Tank_Inputs>();
            InvokeRepeating("CalculateRotationAngle", 0.0f, 0.1f);
        }

        

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Mathf.Abs(angle) > 1) HandleTurretRotation();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(targetPoint, 0.5f);
        }

        #endregion

        #region CustomMethods  
        

        protected virtual void HandleTurretRotation()
        {
            Debug.Log(angle);
            float maxAngleThisFrame = turret_rotation_speed * Time.deltaTime;
            float clampedAngle = Mathf.Clamp(angle, -maxAngleThisFrame, maxAngleThisFrame);
            piippu.RotateAround(kansi.position, transform.up, clampedAngle);
            angle = angle-clampedAngle;
        }

        protected virtual void CalculateRotationAngle()
        {
            targetPoint = input.ReticlePosition;
            Vector3 direction = targetPoint - kansi.position;
            float k = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2)) * Mathf.Tan(kansi.rotation.y);
            targetPoint.y = kansi.position.y+k;

            Vector3 fromDirection = piippu.position - kansi.position;
            Vector3 toDirection = direction;
            
            angle = Vector3.SignedAngle(fromDirection, toDirection, transform.up);
        }

        #endregion
    }
}

