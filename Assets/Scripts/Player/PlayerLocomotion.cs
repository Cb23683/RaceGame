using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManager input;
    private CharacterController controller;
    private AnimatorHandler animatorHandler;

    public Transform camParent;
    public Transform cam;

    public float speed = 5f;
    public float rotationSpeed = 10f;


    void Start()
    {
        input = InputManager.instance;
        controller = GetComponent<CharacterController>();
        animatorHandler = GetComponent<AnimatorHandler>();

        animatorHandler.Initialize();
    }

    void Update()
    {
        HandleMovement(Time.deltaTime);

        if (animatorHandler.canRotate)
        {
            HandleRotation(Time.deltaTime);
        }

        animatorHandler.UpdateAnimatorValues(input.moveAmount, 0);
    }

    private void HandleMovement(float delta)
    {
        Vector3 movement = (input.move.x * camParent.right) + (input.move.y * camParent.forward);

        controller.Move(movement * speed * delta);
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
