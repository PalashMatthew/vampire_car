using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBullet : MonoBehaviour
{
    public Gun _gunController;

    public Vector3 target;
    public float minDistanceToAttack;

    public GameObject oilObj;

    [HideInInspector]
    public float _timeOfAction;
    [HideInInspector]
    public float _damage;


    private void Update()
    {
        if (Vector3.Distance(transform.position, target) > minDistanceToAttack)
        {
            Movement();
        } else
        {
            Attack();
        }
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, _gunController.bulletMoveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        GameObject _inst = Instantiate(oilObj, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
        _inst.GetComponent<OilObj>()._gunController = _gunController;
        _inst.GetComponent<OilObj>().Initialize();

        Destroy(gameObject);
    }
}
