using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Car : MonoBehaviour
{
    public Rigidbody carRB;
    public WheelMeshes wheelMeshes;
    public WheelColliders wheelColliders;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;

    public float motorPower;

    public float speed;

    public AnimationCurve steeringCurve;

    public float brakePower;

    public float slipAngle;

    public float movingDirection;

    void Start()
    {
        carRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        speed = carRB.velocity.magnitude;
        CheckInputs();
        ApplyMotor();
        ApplySteering();
        ApplyBreake();
        UpdateWheels();
    }

    void ApplyMotor()
    {
        wheelColliders.RRWheel.motorTorque = motorPower * gasInput;
        wheelColliders.RLWheel.motorTorque = motorPower * gasInput;
    }

    void CheckInputs()
    {
        gasInput = Input.GetAxis("Vertical");

        steeringInput = Input.GetAxis("Horizontal");

        slipAngle = Vector3.Angle(transform.forward, carRB.velocity - transform.forward);
        movingDirection = Vector3.Dot(transform.forward, carRB.velocity);
        if (slipAngle < 120f)
        {
            if (movingDirection < -0.5f && gasInput > 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else if (movingDirection > 0.5f && gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else
            {
                brakeInput = 0;
            }

        }
        else
        {
            brakeInput = 0;
        }

    }

    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (movingDirection > .5f)
        {
            steeringAngle += Vector3.Angle(transform.forward, carRB.velocity + transform.forward);
            steeringAngle = Mathf.Clamp(steeringAngle, -90, 90);
        }

        wheelColliders.FRWheel.steerAngle = steeringAngle;
        wheelColliders.FLWheel.steerAngle = steeringAngle;
    }

    void ApplyBreake()
    {
        wheelColliders.FRWheel.brakeTorque = brakeInput * brakePower * .7f;
        wheelColliders.FLWheel.brakeTorque = brakeInput * brakePower * .7f;
        wheelColliders.RRWheel.brakeTorque = brakeInput * brakePower * .3f;
        wheelColliders.RLWheel.brakeTorque = brakeInput * brakePower * .3f;

    }

    void UpdateWheels()
    {
        UpdateWheel(wheelColliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(wheelColliders.RLWheel, wheelMeshes.RLWheel);
        UpdateWheel(wheelColliders.RRWheel, wheelMeshes.RRWheel);
        UpdateWheel(wheelColliders.FRWheel, wheelMeshes.FRWheel);

    }

    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }
}
