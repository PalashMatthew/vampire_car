using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronBullet : MonoBehaviour
{
    Gun _gunController;

    private void Start()
    {
        _gunController = GetComponentInParent<Gun>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            //other.gameObject.GetComponent<EnemyController>().Hit(_gunController.CalculateDamage());
            _gunController.DamageEnemy(other.gameObject, gameObject);
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
        }

        if (other.tag == "boss")
        {
            _gunController.DamageBoss(other.gameObject);
        }
    }
}
