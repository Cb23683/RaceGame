using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerTwo : MonoBehaviour
{
    //get input
    //use input to move sphere


    public Rigidbody sphereRB;
    private InputManager input;
    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;

    public float airDrag;
    public float groundDrag;

    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;




    void Start()
    {
        input = InputManager.instance;
        sphereRB.transform.parent = null;

    }


    void Update()
    {
        moveInput = input.move2.y;
        turnInput = input.move2.x;

        // adjust speed for car
        moveInput *= fwdSpeed > 0 ? fwdSpeed : revSpeed;


        // set cars position to sphere
        transform.position = sphereRB.transform.position;

        // set rotation
        float newRotation = turnInput * turnSpeed * Time.deltaTime * input.move2.y;
        transform.Rotate(0, newRotation, 0, Space.World);

        //raycast ground check
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        // rotate car to be parallel to ground
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        if (isCarGrounded)
        {
            sphereRB.drag = groundDrag;
        }
        else
        {
            sphereRB.drag = airDrag;
        }
    }



    private void FixedUpdate()
    {
        if (isCarGrounded)
        {
            //move car
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            //add extra gravity
            sphereRB.AddForce(transform.up * -30f);
        }

    }
}
