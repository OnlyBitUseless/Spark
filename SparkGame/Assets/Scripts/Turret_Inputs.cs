using UnityEngine;

namespace Tank
{
    public class Turret_Inputs : MonoBehaviour
    {
        #region Variables
        public Transform kansi;
        public Transform piippu;
        private Camera mainCamera;
        void Awake()
        {
            mainCamera = Camera.main;
        }
        public LayerMask groundLayer;
        public float turret_rotation_speed = 150f;
        private Rigidbody rb;
        private Vector3 targetPoint;
        public Vector3 TargetPoint
        {
            get { return targetPoint; }
        }
        private Tank_Inputs input;
        #endregion
        
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<Tank_Inputs>();
        }

        

        // Update is called once per frame
        void FixedUpdate()
        {
            rotateToMouse();
        }

        #region CustomMethods  
        protected virtual void rotateToMouse() 
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 targetPoint = hit.point;

                targetPoint.y = kansi.position.y;
                
                Vector3 direction = targetPoint - kansi.position;

                Vector3 fromDirection = piippu.position - kansi.position;
                Vector3 toDirection = direction;

                float angle = Vector3.SignedAngle(fromDirection, toDirection, Vector3.up);
                

                float maxAngleThisFrame = turret_rotation_speed * Time.deltaTime;
                float clampedAngle = Mathf.Clamp(angle, -maxAngleThisFrame, maxAngleThisFrame);


                piippu.RotateAround(kansi.position, Vector3.up, clampedAngle);
            }
        }
        #endregion
    }
}

