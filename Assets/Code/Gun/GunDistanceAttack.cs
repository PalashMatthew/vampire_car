using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDistanceAttack : MonoBehaviour
{
    public Vector3 startCoord;
    public bool dontCheckCoord;


    private void Start()
    {
        if (!dontCheckCoord)
            startCoord = transform.position;
    }
}
