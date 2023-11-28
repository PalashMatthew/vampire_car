using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckRicochetBullet : MonoBehaviour
{
    public int ricochetCount;
    private int _ricochetCount = 1;
    public float bulletMoveSpeed;

    public float damage;


    private void Start()
    {
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        _ricochetCount = 0;
    }

    private void Update()
    {
        if (transform.position.x < -18 && _ricochetCount < ricochetCount)
        {
            transform.position = new Vector3(-18, transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
            _ricochetCount++;
        }

        if (transform.position.x > 18 && _ricochetCount < ricochetCount)
        {
            transform.position = new Vector3(18, transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
            _ricochetCount++;
        }

        if (transform.position.z > 70 && _ricochetCount < ricochetCount)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 70f);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 180f, 0);
            _ricochetCount++;
        }

        transform.Translate(Vector3.forward * bulletMoveSpeed * Time.deltaTime);

        if (transform.position.z > 80)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(damage);
        }
    }
}
