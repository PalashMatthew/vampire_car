using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazerObj : MonoBehaviour
{
    public EnemyController _controller;
    public EnemyGun _gunController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_gunController.damage);
            _controller.BackDamage(_gunController.damage);
        }
    }
}
