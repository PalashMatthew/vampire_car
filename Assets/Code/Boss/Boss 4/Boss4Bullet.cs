using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Bullet : MonoBehaviour
{
    [HideInInspector] public EnemyController _controller;
    [HideInInspector] public float damage;
    public float bulletMoveSpeed;

    public int ricochetCountMax;
    private int ricochetCountCurrent;

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletMoveSpeed * Time.deltaTime);

        if (transform.position.x < -18 && ricochetCountCurrent < ricochetCountMax)
        {
            transform.position = new Vector3(-18, transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
            ricochetCountCurrent++;
        }

        if (transform.position.x > 18 && ricochetCountCurrent < ricochetCountMax)
        {
            transform.position = new Vector3(18, transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
            ricochetCountCurrent++;
        }

        if (transform.position.z > 70 && ricochetCountCurrent < ricochetCountMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 70f);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 180f, 0);
            ricochetCountCurrent++;
        }

        if (transform.position.z < -8 && ricochetCountCurrent < ricochetCountMax)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -8);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180f, 0);
            ricochetCountCurrent++;
        }

        if (transform.position.z < -20f)
            Destroy(gameObject);

        if (WaveController.isWaveEnd)
            Destroy(gameObject);
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
