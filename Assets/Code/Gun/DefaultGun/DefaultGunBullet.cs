using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGunBullet : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    public bool isPlayerAttack = false;


    private void Update()
    {
        transform.Translate(Vector3.forward * _gunController.bulletMoveSpeed * Time.deltaTime);

        if (transform.position.z > 80)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            _gunController.DamageEnemy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "boss")
        {
            _gunController.DamageBoss(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
            Destroy(gameObject);
        }

        if (other.tag == "player" && isPlayerAttack)
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_gunController.CalculateDamage());
            Destroy(gameObject);
        }
    }
}
