using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager instance { get; private set; }

    private Controls controls;

    public Vector2 move { get; private set; }
    public Vector2 look { get; private set; }

    public float moveAmount { get; private set; }

    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

        controls = new Controls();
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.Locomotion.Move.performed += controls =>
        {
            move = controls.ReadValue<Vector2>();
            moveAmount = Mathf.Clamp01(Mathf.Abs(move.x) + Mathf.Abs(move.y));
        };

        controls.Locomotion.Look.performed += controls => look = controls.ReadValue<Vector2>();
    }


}
