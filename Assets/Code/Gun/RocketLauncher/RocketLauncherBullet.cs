using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RocketLauncherBullet : MonoBehaviour
{
    public Gun _gunController;

    public float minDistanceToAttack;
    public GameObject target;

    public GameObject boomObj;

    Vector3 targetPos;


    private void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > minDistanceToAttack)
            {
                targetPos = target.transform.position;
                Movement();
            }
            else
            {
                Attack();
            }
        } 
        else
        {
            if (Vector3.Distance(transform.position, targetPos) > minDistanceToAttack)
            {
                transform.LookAt(targetPos);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, _gunController.bulletMoveSpeed * Time.deltaTime);
            }
            else
            {
                Attack();
            }
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
        _inst.GetComponent<BoomObj>()._gunController = _gunController;
        _inst.transform.localScale = new Vector3(_gunController.areaValue, _gunController.areaValue, _gunController.areaValue);
        _inst.GetComponent<BoomObj>().parentObj = gameObject;
        _inst.GetComponent<BoomObj>().Initialize();

        Destroy(gameObject);
    }
}
