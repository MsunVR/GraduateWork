using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Car : MonoBehaviour
{
    public Rigidbody Rig;
    public float Force;
    public float Rot_Force;
    private void Start()
    {
        Application.targetFrameRate = 90;
    }
    // Update is called once per frame
    void Update()
    {
        var Vertical = Input.GetAxis("Vertical");
        var Horizontal = Input.GetAxis("Horizontal");
        
        Rig.AddRelativeForce(0, 0, Vertical * Force);
        Rig.AddRelativeTorque(0, Horizontal * Rot_Force, 0);

    }
}
