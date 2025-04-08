using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   
    public float speed = 10;
    public float rotation_speed = 100f;

    private Rigidbody tank_base;
    
    private float rotation_input;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tank_base = GetComponent<Rigidbody>();
    }
    // Update is called once per frames
    void Update()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        tank_base.AddForce(speed * movement);
    }

    void OnRotate (InputAction.CallbackContext rotateValue)
    {
        rotation_input = rotateValue.ReadValue<float>();
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
}
