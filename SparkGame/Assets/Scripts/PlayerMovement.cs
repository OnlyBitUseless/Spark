using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   
    public float speed = 10;

    private Rigidbody tankBase;


    private float movementX;
    private float movementY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tankBase = GetComponent<Rigidbody>();
    }
    // Update is called once per frames
    void Update()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        tankBase.AddForce(speed * movement);
    }
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
}
