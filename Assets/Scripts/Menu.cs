using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play1()
    {
        SceneManager.LoadScene("Primer");  
    }
    public void Play2()
    {
        SceneManager.LoadScene("Rocks");
    }
    private void Start()
    {
        Application.targetFrameRate = 144;
    }

    public void ExitGame()
    {
        Debug.Log("���� ���������");
        Application.Quit();
    }
}
