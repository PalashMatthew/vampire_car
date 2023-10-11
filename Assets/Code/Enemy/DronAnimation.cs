using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronAnimation : MonoBehaviour
{
    public float rotateSpeed;
    public List<GameObject> propellers;


    private void Update()
    {
        foreach (GameObject _propeller in propellers)
        {
            _propeller.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }
}
