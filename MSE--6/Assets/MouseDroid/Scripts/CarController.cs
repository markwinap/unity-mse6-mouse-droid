using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public InputManager im;
    public List<WheelCollider> accelerationWheels;
    public List<GameObject> steerWheels;
    public List<GameObject> wheelMeshes;
    public float strengthCoefficient = 10000f;
    public float maxTurn = 20f;
    public Transform centerOfMass;
    public Rigidbody targetRigidbody;
    public float breakStrength;
    public float wheelRadius = 0.025f;
    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<InputManager>();
        targetRigidbody = GetComponent<Rigidbody>();
        if (centerOfMass) {
            //Set Target center Of Mass to our custom center Of Mass
            targetRigidbody.centerOfMass = centerOfMass.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Wheell Collider Scripting
        //https://docs.unity3d.com/ScriptReference/WheelCollider.html
        foreach (WheelCollider wheel in accelerationWheels) 
        {

            if (im.brake) {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = breakStrength * Time.deltaTime;
            }
            else {
                //Motor torque on the wheel axle expressed in Newton metres. Positive or negative depending on direction.
                wheel.motorTorque = strengthCoefficient * Time.deltaTime * im.acceleration;
                wheel.brakeTorque = 0f;
            }
        }
        foreach (GameObject wheel in steerWheels)
        {
            //Steering angle in degrees, always around the local y-axis.
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0f, im.steer * maxTurn, 0f);//Turn Wheel Messh
        }
        foreach (GameObject mesh in wheelMeshes)//Turn Wheel Mesh
        {
            //magnitude - > speed
            //InverseTransformDirection - > Transforms the direction x, y, z from world space to local space.
            //speed * forward ? 1 : -1 / wheel circunference
            //5 * 1 * 0.50265482457 = 2.51327412285 ()degrees
            mesh.transform.Rotate(targetRigidbody.velocity.magnitude * (transform.InverseTransformDirection(targetRigidbody.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * wheelRadius), 0f, 0f);
        }
        //Debug.Log("magnitude: " + targetRigidbody.velocity.magnitude);

        //velocity (0, 0, 3)Forward
        //velocity (0, 0, -3)Backward
    }
}
