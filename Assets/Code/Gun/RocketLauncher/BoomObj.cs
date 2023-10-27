using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomObj : MonoBehaviour
{
    public Gun _gunController;

    public void Initialize()
    {
        StartCoroutine(EndAttack());
        StartCoroutine(OffCollider());
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
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

    IEnumerator OffCollider()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SphereCollider>().enabled = false;
    }
}
