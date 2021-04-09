﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    private InputHandler inputHandler;
    public CharacterController controller;
    public TMP_Text scoreTxt;


    public float speed = 5;
    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputHandler = InputHandler.instance;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0, 0, inputHandler.move.y);
        controller.Move(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            Destroy(other.gameObject);
            score++;
            scoreTxt.SetText("Score: " + score);
            if (score >= 11)
            {
                scoreTxt.SetText("You Win!");
            }
        }
    }
}