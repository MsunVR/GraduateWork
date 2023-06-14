using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
    public GameObject canvasObject;
    public Text notificationText;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasObject.SetActive(true);
            
            Time.timeScale = 0f;
            audioSource1.Pause(); // Останавливаем звук
            audioSource2.Pause();
            audioSource3.Pause();
        }
    }
}
