using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles1Controller : MonoBehaviour
{
    public float timeActive;
    public GameObject shieldObj;

    private void Start()
    {
        shieldObj.SetActive(false);
        StartCoroutine(ShowShield());
    }

    IEnumerator ShowShield()
    {
        yield return new WaitForSeconds(timeActive);
        shieldObj.SetActive(true);
        yield return new WaitForSeconds(timeActive);
        shieldObj.SetActive(false);
        StartCoroutine(ShowShield());
    }
}
