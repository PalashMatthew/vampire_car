using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.z <= -90)
        {
            Instantiate(gameObject, new Vector3(transform.position.x, 0, 111.75f), transform.rotation);
            Destroy(gameObject);
        }
    }
}
