using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoGunBulletObj : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    public bool isPlayerAttack = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            _gunController.DamageEnemy(other.gameObject, gameObject);
        }

        if (other.tag == "boss")
        {
            _gunController.DamageBoss(other.gameObject);
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
        }

        if (other.tag == "player" && isPlayerAttack)
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_gunController.CalculateDamage());
        }
    }
}
