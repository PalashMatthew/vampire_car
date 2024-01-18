using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckLazerController : MonoBehaviour
{
    public bool isLookAt;
    GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        if (isLookAt)
        {
            LookAt();
        }        
    }

    void LookAt()
    {
        Vector3 relativePos = _player.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1f);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
