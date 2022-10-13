using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngineAI : MonoBehaviour
{
    [Header("Path")]

    public Transform path;

    [Header("CarSettings")]

    public float maxSteerAngle = 45f;
    public float maxMotorTorque = 150f;
    public float maxBrakeTorque = 1000f;
    public float currentSpeed;
    public float maxSpeed = 60f;

    public bool isBracking = false;
    public Renderer Lamp;

    [Header("Wheels")]

    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelRL;
    public WheelCollider WheelRR;

    private List<Transform> nodes;
    private int currectNode = 0;

    [Header("Sensors")]

    public float sensorLength = 5f;
    public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    public float frontsideSensorPosition = 0.2f;
    public float frontSensorAngle = 30f;

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
        CheckWayPointDistance();
        Bracking();
        Sensors();
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position + frontSensorPosition;
        

        //front center sensor
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
        }
        

        //front right sensor
        sensorStartPos.x += frontsideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
        }
        

        //front right Angle sensor
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
        }
        

        //front left sensor
        sensorStartPos.x -= 2 * frontsideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
        }
        

        //front left Angle sensor
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
        }
        
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

        if (currentSpeed < maxSpeed && !isBracking)
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

    private void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currectNode].position) < 5f)
        {
            if (currectNode == nodes.Count - 1)
            {
                currectNode = 0;
            }
            else
            {
                currectNode++;
            }
        }
    }
    private void Bracking()
    {
        if (isBracking)
        {
            Lamp.materials[1].EnableKeyword("_EMISSION");
            WheelRL.brakeTorque = maxBrakeTorque;
            WheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            Lamp.materials[1].DisableKeyword("_EMISSION");
            WheelRL.brakeTorque = 0;
            WheelRR.brakeTorque = 0;
        }
    }
}
