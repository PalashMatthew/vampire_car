using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstObjectMove : MonoBehaviour
{
    public float moveSpeed;


    private void Start()
    {
        Destroy(gameObject, 20);
    }

    private void Update()
    {
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
