using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
        target = GameObject.Find("Player").transform;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.DOLookAt(target.position, rotateSpeed);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (mesh != null)
        {
            mesh.transform.Rotate(0, 0, meshRotateSpeed * Time.deltaTime);
        }
    }
}
