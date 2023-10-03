using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstObjectMove : MonoBehaviour
{
    public float moveSpeed;
    public bool isDontDestroy;


    private void Start()
    {
        if (!isDontDestroy)
            Destroy(gameObject, 35);
    }

    private void Update()
    {
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
