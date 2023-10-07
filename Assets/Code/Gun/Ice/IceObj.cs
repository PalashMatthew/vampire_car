using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObj : MonoBehaviour
{
    public Gun _gunController;

    public void Initialize()
    {
        StartCoroutine(EndAttack());
        StartCoroutine(OffCollider());
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(_gunController.timeOfAction);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Hit(_gunController.CalculateDamage());
            other.gameObject.GetComponent<EnemyController>().Freeze(_gunController.freezeTime);
        }
    }

    IEnumerator OffCollider()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SphereCollider>().enabled = false;
    }
}
