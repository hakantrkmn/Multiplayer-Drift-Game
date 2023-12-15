using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Car : MonoBehaviour
{
    public Rigidbody carRB;

    public float moveInput;

    public List<Wheel> wheels;
    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensivity = 1.0f;
    public float maxSteerAngle = 30f;


    Vector3 _centerOfMass;

    float steerInput;

    void Start()
    {
        carRB = GetComponent<Rigidbody>();
    }

    private void Update() {
        GetInputs();
        AnimateWheels();
    }


    private void LateUpdate() {
        Move();
        Steer();
        Brake();
    }

    void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");

    }


    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.motorTorque = moveInput * maxAcceleration * 1500 * Time.deltaTime;
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.collider.brakeTorque = brakeAcceleration*700*Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.collider.brakeTorque = 0;
            }
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var steerAngle = steerInput * turnSensivity * maxSteerAngle;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle,steerAngle,.6f);
            }
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.collider.GetWorldPose(out pos,out rot);

            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}
