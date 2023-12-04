using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackingBullet : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed;

    private void Start()
    {
        target = GameObject.Find("Player").transform;

        Destroy(gameObject, 7);
    }

    private void Update()
    {
        transform.DOLookAt(target.position, rotateSpeed);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        //transform.DORotate(new )
    }
}
