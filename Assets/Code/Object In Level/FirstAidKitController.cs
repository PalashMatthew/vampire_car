using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKitController : MonoBehaviour
{
    public float value;

    public float moveSpeed;
    public float rotateSpeed;

    public GameObject icon;


    private void Update()
    {
        transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);

        icon.transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }
}
