using UnityEngine;

namespace Tank
{
    public class Turret_Inputs : MonoBehaviour
    {
        #region Variables
        public float turret_rotation_speed = 100f;
        private Rigidbody rb;
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
            handleRotation();
            transform.Rotate(10.0f * input.ReticlePosition * Time.deltaTime);
        }

        #region CustomMethods
        protected virtual void handleRotation() 
        {
            Debug.Log(input.ReticlePosition);
        #endregion
        }
    }
}

