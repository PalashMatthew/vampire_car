using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Rocket : MonoBehaviour
{
    [HideInInspector] public EnemyController _controller;
    [HideInInspector] public float damage;

    public GameObject areaObj;
    [HideInInspector] public Vector3 target;
    public float bulletMoveSpeed;


    private void Update()
    {
        transform.Translate(Vector3.forward * bulletMoveSpeed * Time.deltaTime);

        if (transform.position.z < -20f)
            Destroy(gameObject);

        if (WaveController.isWaveEnd)
            Destroy(gameObject);

        if (Vector3.Distance(transform.position, target) <= 1)
        {
            GameObject gm = Instantiate(areaObj, transform.position, transform.rotation);

            gm.transform.eulerAngles = new Vector3(0, 0, 0);
            gm.GetComponent<Boss4AreaAttack>()._controller = _controller;
            gm.GetComponent<Boss4AreaAttack>().damage = damage;

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(damage);
            _controller.BackDamage(damage);

            Destroy(gameObject);
        }

        if (other.tag == "obstacle")
        {
            Destroy(gameObject);
        }
    }
}
