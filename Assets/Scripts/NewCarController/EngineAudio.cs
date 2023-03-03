using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    public AudioSource runningSound;
    public float runningMaxVolume;
    public float runningMaxPitch;

    public AudioSource reversSound;
    public float reversMaxVolume;
    public float reversMaxPitch;

    public AudioSource idleSound;
    public float idleMaxVolume;
    private float speedRatio;
    private float revLimiter;
    public float LimiterSound = 1f;
    public float LimiterFrequency = 3f;
    public float LimiterEngage = 0.8f;

    public bool isEngineRunning = false;
    public AudioSource startingSound;

    private CarDriftController CarDriftController;
    // Start is called before the first frame update
    void Start()
    {
        CarDriftController = GetComponent<CarDriftController>();
        idleSound.volume = 0;
        runningSound.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CarDriftController)
        {
            speedRatio = CarDriftController.GetSpeedRatio();
        }
        if (speedRatio > LimiterEngage)
        {
            revLimiter = (Mathf.Sin(Time.time * LimiterFrequency) + 1f) * LimiterSound * (speedRatio - LimiterEngage);
        }
        if (isEngineRunning)
        {
            idleSound.volume = Mathf.Lerp(0.1f, idleMaxVolume, speedRatio);
            runningSound.volume = Mathf.Lerp(0.3f, runningMaxVolume, speedRatio);
            runningSound.pitch = Mathf.Lerp(runningSound.pitch, Mathf.Lerp(0.3f, runningMaxPitch, speedRatio) + revLimiter, Time.deltaTime);
        }
        else
        {
            idleSound.volume = 0;
            runningSound.volume = 0;
        }
    }

    public IEnumerator StartEngine()
    {
        startingSound.Play();
        CarDriftController.isEngineRunning = 1;
        yield return new WaitForSeconds(0.6f);
        isEngineRunning = true;
        yield return new WaitForSeconds(0.4f);
        CarDriftController.isEngineRunning = 2;
    }
}
