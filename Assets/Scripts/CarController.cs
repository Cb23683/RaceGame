using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //get input
    //use input to move sphere
   

    public Rigidbody sphereRB;
    private InputManager input;
    public float moveInput;

    public float fwdSpeed;
    public float revSpeed;




    void Start()
    {
        input = InputManager.instance;
        sphereRB.transform.parent = null;

    }


    void Update()
    {
        moveInput = input.move.y;
        moveInput *= fwdSpeed > 0 ? fwdSpeed : revSpeed;

        
        // set cars position to sphere
        transform.position = sphereRB.transform.position;
    }

    private void FixedUpdate()
    {
        sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }
}
