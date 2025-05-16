using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaceTarget : MonoBehaviour
{
    public GameObject target;
    public Transform body;
    NavMeshAgent mr;
    public float rotationSpeed = 2f;
    public LayerMask groundLayer;
    void Start()
    {
        // Get your own NavMeshagent component
        mr = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Use properties to pass the coordinates of the target point
        //mr.destination = target.transform.position;
        // Use method to get the target point coordinate, and the previous line of code is the same.
        mr.SetDestination(target.transform.position);
        AlignToGround();
    }

    private void AlignToGround()
    {
        // Cast a ray downward to detect the ground
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f, groundLayer))
        {
            // Align the tank's position and orientation to the ground
            transform.position = hit.point;
            transform.up = hit.normal; // Align to the ground's normal
        }
    }
}