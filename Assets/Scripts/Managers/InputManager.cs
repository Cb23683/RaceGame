using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager instance { get; private set; }
    public static InputManager instance2 { get; private set; }

    private Controls controls;
    private Controls controls2;

    public Vector2 move { get; private set; }
    public Vector2 look { get; private set; }

    public Vector2 move2 { get; private set; }
    public Vector2 look2 { get; private set; }

    public float moveAmount { get; private set; }
    public float moveAmount2 { get; private set; }


    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

        if (instance2 != null)
        {
            Destroy(this);
        }
        else
        {
            instance2 = this;
        }

        controls = new Controls();
        controls.Enable();

        controls2 = new Controls();
        controls2.Enable();
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

        controls2.Locomotion2.Move.performed += controls2 =>
        {
            move2 = controls2.ReadValue<Vector2>();
            moveAmount2 = Mathf.Clamp01(Mathf.Abs(move.x) + Mathf.Abs(move.y));
        };

        controls.Locomotion.Look.performed += controls => look = controls.ReadValue<Vector2>();

        controls2.Locomotion2.Look.performed += controls2 => look2 = controls2.ReadValue<Vector2>();
    }


}
