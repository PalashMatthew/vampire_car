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
            other.gameObject.GetComponent<EnemyController>().Hit(_gunController.CalculateDamage());
        }
    }
}
