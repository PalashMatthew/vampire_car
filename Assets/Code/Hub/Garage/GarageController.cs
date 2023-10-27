using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageController : MonoBehaviour
{
    public GameObject canvasChangeCar;

    public void ButChangeCar()
    {
        canvasChangeCar.GetComponent<PopUpController>().OpenPopUp();
        StartCoroutine(OffGarage());
    }

    IEnumerator OffGarage()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
