using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public float radius = 2f;
    public float forceValue = 5f;

    private Rigidbody Fairlady_240zCar = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            GetComponent<Collider>().enabled = true;

            Distructebl script = other.GetComponent<Distructebl>();
            script.DestroyObj();

            Collider[] colls = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider col in colls)
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb !=null)
                {
                    rb.AddForce(Fairlady_240zCar.velocity * forceValue);
                }
            }
        }
    }
}
