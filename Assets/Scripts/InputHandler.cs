using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    Controls controls;

    public static InputHandler instance;

    public Vector2 move;

    // Start is called before the first frame update
    void Start()
    {
        //arguments => whatever logic should get executed
        controls.Movement.Move.performed += controls => move = controls.ReadValue<Vector2>();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
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



}
