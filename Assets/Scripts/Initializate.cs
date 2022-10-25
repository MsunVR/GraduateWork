using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializate : MonoBehaviour
{
    public CarListSO CarsConteiner;
    public CarCam Camera;
    public Speedometer Speedometer;
    public Transform SpawnPoint;

    void Start()
    {
        foreach (var car in CarsConteiner.Cars)
        {
            if (car.IsActiveCar)
            {
                car.CarScript.enabled = true;

                var carObj = Instantiate(car.CarScript, SpawnPoint);

                Debug.Log(car.Name + car.CarScript.enabled);
                Debug.Log(car.Name + car.IsActiveCar);

                if (Camera && Speedometer)
                {
                    Camera.car = carObj.transform;
                    Speedometer.target = carObj.GetComponent<Rigidbody>();
                }
                break;
            }
        }
    }
}
