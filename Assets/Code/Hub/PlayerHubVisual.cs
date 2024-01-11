using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHubVisual : MonoBehaviour
{
    [Header("Car Mesh")]
    public List<GameObject> carMesh;

    public List<GameObject> wheels;
    public float rotateSpeed;


    private void Start()
    {
        ChangeCar();
    }

    private void Update()
    {
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
        }
    }

    public void ChangeCar()
    {
        foreach (GameObject car in carMesh)
        {
            if (car.GetComponent<HubCarMesh>().carName == PlayerPrefs.GetString("selectedCarID"))
            {
                car.GetComponent<HubCarMesh>().mesh.SetActive(true);
                wheels.Clear();

                foreach (GameObject wheel in car.GetComponent<HubCarMesh>().wheels)
                {
                    wheels.Add(wheel);
                }
            } 
            else
            {
                car.GetComponent<HubCarMesh>().mesh.SetActive(false);
            }
        }
    }
}
