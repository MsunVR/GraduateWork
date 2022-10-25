using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarChanger : MonoBehaviour
{
    public CarListSO CarsConteiner;
    public CarCam Camera;
    public Speedometer Speedometer;
    
    void Start()
    {
        foreach (var car in CarsConteiner.Cars)
        {
            if (!car.IsActiveCar)
                car.CarScript.enabled = false;
        }   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i <= CarsConteiner.Cars.Count - 1; i++)
            {
                if (CarsConteiner.Cars[i].IsActiveCar)
                {
                    var oldCar = CarsConteiner.Cars[i];
                    var nextCarIndex = i >= CarsConteiner.Cars.Count - 1 ? 0 : i + 1;
                    var newCar = CarsConteiner.Cars[nextCarIndex];
                    oldCar.IsActiveCar = false;
                    oldCar.CarScript.enabled = false;
                    newCar.IsActiveCar = true;
                    newCar.CarScript.enabled = true;
                    Debug.Log(newCar.Name + newCar.CarScript.enabled);
                    Debug.Log(newCar.Name + newCar.IsActiveCar);

                    Camera.car = newCar.CarScript.transform;

                    Speedometer.target = newCar.CarScript.GetComponent<Rigidbody>();
                    break;
                }
            }
        }
    }
}
