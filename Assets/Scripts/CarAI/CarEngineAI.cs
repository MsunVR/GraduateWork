using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngineAI : MonoBehaviour
{
    [Header("Path")]

    public Transform path;

    [Header("CarSettings")]

    public float maxSteerAngle = 45f;
    public float turneSpeed = 5f;
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
    private bool avoiding = false;
    private float targetSteerAngle = 0;

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
        LerpToSteerAngle();
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPosition.z;
        sensorStartPos += transform.up * frontSensorPosition.y;
        float avoidMultiplier = 0;
        avoiding = false;
        
        

        //front right sensor
        sensorStartPos += transform.right * frontsideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 1f;
            }
        }
        

        //front right Angle sensor
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }


        //front left sensor
        sensorStartPos -= transform.right * frontsideSensorPosition * 2;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 1f;
            }
        }
        

        //front left Angle sensor
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }

        //front center sensor
        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                if (!hit.collider.CompareTag("Terrain"))
                {
                    Debug.DrawLine(sensorStartPos, hit.point);
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplier = -1;
                    }
                    else
                    {
                        avoidMultiplier = 1;
                    }
                }
            }
        }
        if (avoiding)
        {
            targetSteerAngle = maxSteerAngle * avoidMultiplier;
        }
    }

    private void ApplySteer()
    {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        relativeVector = relativeVector / relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        targetSteerAngle=newSteer;
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
    private void LerpToSteerAngle()
    {
        WheelFL.steerAngle = Mathf.Lerp(WheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turneSpeed);
        WheelFR.steerAngle = Mathf.Lerp(WheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turneSpeed);
    }
}
