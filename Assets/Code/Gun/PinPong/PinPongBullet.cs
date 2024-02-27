using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPongBullet : MonoBehaviour
{
    public Gun _gunController;
    public GameObject target;
    private int _ricochetCount = 0;


    private void Start()
    {
        if (target != null)
            transform.LookAt(target.transform.position);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        _ricochetCount = 0;

        Destroy(gameObject, 20);
    }

    private void Update()
    {
        if (transform.position.x < -18 && _ricochetCount < _gunController.ricochetCount)
        {
            transform.position = new Vector3(-18, transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
            _ricochetCount++;
        }

        if (transform.position.x > 18 && _ricochetCount < _gunController.ricochetCount)
        {
            transform.position = new Vector3(18, transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
            _ricochetCount++;
        }

        if (transform.position.z > 76 && _ricochetCount < _gunController.ricochetCount)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 76f);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 180f, 0);
            _ricochetCount++;
        }

        transform.Translate(Vector3.forward * _gunController.bulletMoveSpeed * Time.deltaTime);

        if (transform.position.z > 80)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            _gunController.DamageEnemy(other.gameObject, gameObject);

            if (_ricochetCount < _gunController.ricochetCount)
            {
                transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
                _ricochetCount++;
            }
        }

        if (other.tag == "boss")
        {
            //_gunController.DamageBoss(other.gameObject);
            _gunController.DamageBoss(other.gameObject, gameObject);

            if (_ricochetCount < _gunController.ricochetCount)
            {
                transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
                _ricochetCount++;
            }
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());

            if (_ricochetCount < _gunController.ricochetCount)
            {
                transform.eulerAngles = new Vector3(0, -transform.eulerAngles.y, 0);
                _ricochetCount++;
            }
        }
    }
}
