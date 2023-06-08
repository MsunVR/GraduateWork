using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField]
    public Light Blinklight;
    public float blinkInterval = 0.5f;

    private float timer;

    void Start()
    {
        timer = 0f;
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            Blinklight.enabled = !Blinklight.enabled;
            timer = 0f;
        }
    }
}
