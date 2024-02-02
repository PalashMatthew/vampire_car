using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGunObj : MonoBehaviour
{
    public GameObject target;
    public Gun _gunController;

    private void Start()
    {
        if (target.tag == "enemy")
        {
            _gunController.DamageEnemy(target, gameObject);
        }

        if (target.tag == "boss")
        {
            //_gunController.DamageBoss(target);
            _gunController.DamageBoss(target, gameObject);
        }

        if (target.tag == "obstacle")
        {
            target.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
        }

        Destroy(gameObject, 2);
    }
}
