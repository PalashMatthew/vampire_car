using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilObj : MonoBehaviour
{
    public Gun _gunController;

    public void Initialize()
    {
        StartCoroutine(EndAttack());
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
        }
    }
}
