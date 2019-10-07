//Made with GDW group - 100655510

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    public Rigidbody rb;
    public SphereCollider col;
    public bool playIsTrue = false;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    //jumping var
    private float distToGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }




    void Update()
    {

        if (Input.GetButton("Fire3"))
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;
    }

    void FixedUpdate()
    {
        if (playIsTrue == true)
        rb.velocity = moveVelocity;
    }

    public void Play()
    {
        if (playIsTrue == false)
            playIsTrue = true;
        else
        {
            playIsTrue = false;
            //rb.position = (-5.5, 3.5, -3);
        }
    }
}