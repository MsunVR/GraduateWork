using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageTrigger : MonoBehaviour
{
    public UIManager UIManager;

    private void OnTriggerEnter(Collider other)
    {
        UIManager.GarageMessage.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        UIManager.GarageMessage.SetActive(false);
    }
}
