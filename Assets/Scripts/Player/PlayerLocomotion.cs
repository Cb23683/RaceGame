using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager input;
    private CharacterController controller;
    private Rigidbody rb;

    public Transform camParent;
    public Transform cam;

    public float speed = 10f;
    public float rotationSpeed = 10f;


    void Start()
    {
        input = InputManager.instance;
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement(Time.deltaTime);

    
        HandleRotation(Time.deltaTime);
    }

    private void HandleMovement(float delta)
    {
        Vector3 movement = new Vector3(input.move.x, 0, input.move.y);
        Debug.Log(movement);

        // controller.Move(movement * speed * delta);
       // rb.AddForce(movement * speed * delta, ForceMode.VelocityChange);
        rb.MovePosition(transform.position + movement);

    }

    private void HandleRotation(float delta)
    {
        Vector3 targetDir;

        targetDir = cam.forward * input.move.y;

        targetDir += cam.right * input.move.x;

        targetDir.Normalize();

        targetDir.y = 0;

        if(targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion tr = Quaternion.LookRotation(targetDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * delta);
    }
}
