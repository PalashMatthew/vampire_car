using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [HideInInspector]
    public GameObject parent;
    [HideInInspector]
    public float dist;

    public bool isFixPlace = true;

    public float distValue = 178.5f;

    private void Start()
    {
        if (isFixPlace)
        {
            if (parent != null)
            {
                float _dist = transform.position.z - parent.transform.position.z;
                if (_dist > distValue)
                {
                    transform.position = new Vector3(0, 0, parent.transform.position.z + distValue);
                }
            }
        }
    }
}
