using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Cars",menuName ="Tools",order =0)]
public class CarListSO : ScriptableObject
{
    public List<Car> Cars;
}

[System.Serializable]
public class Car
{
    public string Name;
    public CarDriver CarScript;
    public bool IsActiveCar;
}
