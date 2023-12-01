using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallBulletCollider : MonoBehaviour
{
    [HideInInspector] public EnemyGun _gunController;
    [HideInInspector] public EnemyController _controller;

    public GameObject brain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_gunController.damage);
            _controller.BackDamage(_gunController.damage);

            Destroy(brain);
        }
    }
}
