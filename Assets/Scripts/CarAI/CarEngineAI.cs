using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngineAI : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45f;
    public float maxMotorTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 60f;

    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    

    private List<Transform> nodes;
    private int currectNode = 0;

    void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        ApplySteer();
        Drive();
        //CheckWayPointDistance();

    }
    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        relativeVector = relativeVector / relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        WheelFL.steerAngle = newSteer;
        WheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * WheelFL.radius * WheelFL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed)
        {
            WheelFL.motorTorque = maxMotorTorque;
            WheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            WheelFL.motorTorque = 0;
            WheelFR.motorTorque = 0;
        }
    }
    
    //private void CheckWayPointDistance()
    //{
    //    if (Vector3.Distance(transform.position, nodes[currectNode].position) < 0.05f)
    //    {
    //        if (currectNode == nodes.Count - 1)
    //        {
    //            currectNode = 0;
    //        }
    //        else
    //        {
    //            currectNode++;
    //        }
    //    }
    //}
}
