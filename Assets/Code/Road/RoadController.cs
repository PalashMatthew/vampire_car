using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public GameObject parent;
    public float dist;

    private void Start()
    {
        if (parent != null)
        {
            float _dist = transform.position.z - parent.transform.position.z;
            if (_dist > 178.5f)
            {
                transform.position = new Vector3(0, 0, parent.transform.position.z + 178.5f);
                Debug.Log("FIX PLACE");
            }
        }
    }
}
