using UnityEngine;

namespace Tank
{
    public class Ampuminen : MonoBehaviour
    {

        private Turret_Inputs input;
        private Rigidbody rb;
        private Vector3 bulletDirection;
        private float bulletSpeed = 10f;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<Turret_Inputs>();
        }

        // Update is called once per frame
        void Update()
        {
            bulletDirection = transform.forward;
        }
    }

}
