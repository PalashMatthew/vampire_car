using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public Gun _gunController;

    public float minDistanceToAttack;
    public GameObject target;

    public GameObject boomObj;


    private void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > minDistanceToAttack)
        {
            Movement();
        }
        else
        {
            Attack();
        }
    }

    void Movement()
    {
        transform.LookAt(target.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _gunController.bulletMoveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        GameObject _inst = Instantiate(boomObj, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
        _inst.GetComponent<IceObj>()._gunController = _gunController;
        _inst.GetComponent<IceObj>().Initialize();

        Destroy(gameObject);
    }
}
