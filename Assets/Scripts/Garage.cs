using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Garage : MonoBehaviour
{
    public CarListSO CarsConteiner;
    public CarCam Camera;
    public Transform SpawnPoint;
    public CarDriver SpawnedCar;
    public int GameSceneIndex = 0;
    public float RotateSpeed;

    void Start()
    {
        foreach (var car in CarsConteiner.Cars)
        {
            if (car.IsActiveCar)
            {
                car.CarScript.enabled = false;

                SpawnedCar = Instantiate(car.CarScript, SpawnPoint.position, Quaternion.identity);

                Debug.Log(car.Name + car.CarScript.enabled);
                Debug.Log(car.Name + car.IsActiveCar);
                break;
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (int i = 0; i <= CarsConteiner.Cars.Count - 1; i++)
            {
                if (CarsConteiner.Cars[i].IsActiveCar)
                {

                    Debug.Log(CarsConteiner.Cars[i].Name + CarsConteiner.Cars[i].CarScript.enabled);
                    Debug.Log(CarsConteiner.Cars[i].Name + CarsConteiner.Cars[i].IsActiveCar);

                    var oldCar = CarsConteiner.Cars[i];
                    var nextCarIndex = i >= CarsConteiner.Cars.Count - 1 ? 0 : i + 1;
                    var newCar = CarsConteiner.Cars[nextCarIndex];
                    Destroy(SpawnedCar.gameObject);
                    SpawnedCar = Instantiate(CarsConteiner.Cars[nextCarIndex].CarScript, SpawnPoint);
                    oldCar.IsActiveCar = false;
                    oldCar.CarScript.enabled = false;
                    newCar.IsActiveCar = true;
                    newCar.CarScript.enabled = false;

                    Debug.Log(newCar.Name + newCar.CarScript.enabled);
                    Debug.Log(newCar.Name + newCar.IsActiveCar);

                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync(GameSceneIndex);
        }
        Vector3 rotate = new Vector3(0, Input.GetAxis("Horizontal"), 0) * RotateSpeed;
        SpawnedCar.transform.Rotate(rotate);
    }
}
