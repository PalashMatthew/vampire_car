using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopter1 : MonoBehaviour
{
    public GameObject areaObj;
    public GameObject meteorObj;

    public float attackTime;
    public GameObject propeller;
    public float rotateSpeed;

    Transform target;


    private void Start()
    {
        target = GameObject.Find("Player").transform;
        meteorObj.SetActive(false);
        areaObj.SetActive(false);

        meteorObj.GetComponent<EnemyBallBulletCollider>()._gunController = gameObject.GetComponent<EnemyGun>();
        meteorObj.GetComponent<EnemyBallBulletCollider>()._controller = gameObject.GetComponent<EnemyController>();

        StartCoroutine(Attack());
    }

    private void Update()
    {
        propeller.transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(3);

        areaObj.SetActive(true);
        areaObj.transform.position = target.position;
        areaObj.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(1);

        meteorObj.SetActive(true);
        meteorObj.transform.position = areaObj.transform.position;
        meteorObj.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(4);

        meteorObj.GetComponent<ParticleSystem>().Stop();
        meteorObj.SetActive(false);
        areaObj.SetActive(false);

        yield return new WaitForSeconds(attackTime - 3);

        StartCoroutine(Attack());
    }
}
