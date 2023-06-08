using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    public float fallSpeed = 10f;
    public bool isFalling = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            isFalling = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }
}
