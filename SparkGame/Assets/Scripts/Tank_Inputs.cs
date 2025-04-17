using UnityEngine;

namespace Tank
{
    public class Tank_Inputs : MonoBehaviour
    {
        #region Variables
        [Header("Input Properties")]
        public Camera mainCamera;
        #endregion

        #region Properties
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
        #endregion

        #region BuildinMethods

        void Update()
        {
            if (mainCamera)
            {
                HandleInputs();
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("GroundTag"))
            {
            trackOnGround = true;
            }
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
            if (Physics.Raycast(screenRay, out hit))
            {
                reticlePosition = hit.point;
                reticleNormal = hit.normal;
            }
            
            forwardInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");
        }
        #endregion
    }
}
