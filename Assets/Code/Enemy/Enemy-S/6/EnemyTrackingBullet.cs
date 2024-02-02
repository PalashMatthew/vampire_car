using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyTrackingBullet : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed;

    public float lifeTime;

    public GameObject mesh;

    public float meshRotateSpeed;

    private void Start()
    {       
        Destroy(gameObject, lifeTime);
    }

    private void LateUpdate()
    {
        target = GameObject.Find("Player").transform;

        var rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

        //transform.DOLookAt(target.position, rotateSpeed);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (mesh != null)
        {
            mesh.transform.Rotate(0, 0, meshRotateSpeed * Time.deltaTime);
        }

        //transform.position = new Vector3(transform.position.x, 1.361f, transform.position.z);
    }
}
