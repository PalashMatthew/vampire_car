using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckBrake : MonoBehaviour
{
    EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_enemyController.brakeDamage);
        }
    }
}
