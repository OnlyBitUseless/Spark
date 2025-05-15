using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlaceTarget : MonoBehaviour
{
    public GameObject target;  // Get the target point, pay attention to the value in the panel
    NavMeshAgent mr;   // Declaration variable
                       // Use this for initialization
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
    }
}