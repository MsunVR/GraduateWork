using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
   
    public Text timerText;
    static float timer;

    public Text lapText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Primer");
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string time = string.Format("{0:0}:{1:00}", minutes, seconds);

        timerText.text = time;

        if (Input.GetKeyDown("9"))
        {
            RestartGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        lapText.text = "lap: " + timerText.text;
        timer = 0.0f;
    }
}
