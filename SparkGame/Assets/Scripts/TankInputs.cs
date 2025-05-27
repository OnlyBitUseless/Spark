using UnityEngine;

namespace Tank
{
    public class TankInputs : MonoBehaviour
    {
        #region Variables
        [Header("Input Properties")]
        public Camera mainCamera;
        public float groundCheckDistance = 1.5f;
        #endregion

        #region Properties
        private int groundLayerMask;

        private Vector3 reticlePosition;
        public Vector3 ReticlePosition
        {
            get { return reticlePosition; }
        }

        private Vector3 reticleNormal;
        public Vector3 ReticleNormal
        {
            get { return reticleNormal; }
        }

        private float forwardInput;
        public float ForwardInput
        {
            get { return forwardInput; }
        }

        private float rotationInput;
        public float RotationInput
        {
            get { return rotationInput; }
        }

        private bool trackOnGround;
        public bool TrackOnGround
        {
            get { return trackOnGround; }
        }

        private bool fireInput;
        public bool FireInput
        {
            get { return fireInput; }
        }

        private bool isGrounded;
        public bool IsGrounded
        {
            get { return isGrounded; }
        }
        #endregion

        #region BuildinMethods

        void FixedUpdate()
        {
            if (mainCamera)
            {
                HandleInputs();
            }
        }

        void Awake()
        {
            groundLayerMask = LayerMask.GetMask("groundLayer");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(reticlePosition, 0.5f);
        }
        
        #endregion

        #region CustomMethods
        protected virtual void HandleInputs()
        {
            Ray screenRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit, Mathf.Infinity, groundLayerMask))
            {
                reticlePosition = hit.point;
                reticleNormal = hit.normal;
            }
            
            forwardInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");
            fireInput = (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space));

            Vector3 RaycastOrigin = transform.position + Vector3.up;

            isGrounded = Physics.Raycast(RaycastOrigin, Vector3.down, groundCheckDistance);
        }
        #endregion
    }
}
