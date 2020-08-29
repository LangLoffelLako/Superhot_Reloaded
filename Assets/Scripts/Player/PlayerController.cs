using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController Controller;
    public float speed = 12f;
    
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private Vector3 velocity;

    private void Start()
    {
        //equip possible weapon
        transform.Find("Weapon").GetChild(1)

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z);
        velocity.y += gravity * Time.deltaTime;

        if (Controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && Controller.isGrounded)
        {
            Debug.Log("Jumped");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        Controller.Move(move * speed * Time.deltaTime);
        Controller.Move(velocity * Time.deltaTime);
    }
}
