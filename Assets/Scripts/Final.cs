using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
    public GameObject canvasObject;
    public Text notificationText;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasObject.SetActive(true);
            
            Time.timeScale = 0f;
        }
    }
}
