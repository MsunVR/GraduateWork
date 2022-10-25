using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGarage : MonoBehaviour
{
    public int GarageSceneIndex = default;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadSceneAsync(GarageSceneIndex);
        }
    }
}
