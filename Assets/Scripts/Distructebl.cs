using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distructebl : MonoBehaviour
{
    public GameObject destroyVersion;

    public void DestroyObj()
    {
        Instantiate(destroyVersion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
