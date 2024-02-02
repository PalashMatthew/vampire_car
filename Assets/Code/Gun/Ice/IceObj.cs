using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObj : MonoBehaviour
{
    public Gun _gunController;
    public GameObject parentObj;

    public void Initialize()
    {
        GetComponent<GunDistanceAttack>().dontCheckCoord = true;
        GetComponent<GunDistanceAttack>().startCoord = parentObj.GetComponent<GunDistanceAttack>().startCoord;

        StartCoroutine(EndAttack());
        StartCoroutine(OffCollider());

        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            //other.gameObject.GetComponent<EnemyController>().Hit(_gunController.CalculateDamage());
            _gunController.DamageEnemy(other.gameObject, gameObject);
            other.gameObject.GetComponent<EnemyController>().Freeze(_gunController.freezeTime);
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
        }

        if (other.tag == "boss")
        {
            //_gunController.DamageBoss(other.gameObject);
            _gunController.DamageBoss(other.gameObject, gameObject);
        }
    }

    IEnumerator OffCollider()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SphereCollider>().enabled = false;
    }
}
