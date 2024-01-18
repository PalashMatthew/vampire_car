using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazerObj : MonoBehaviour
{
    public EnemyController _controller;
    public EnemyGun _gunController;

    public int damage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            if (_gunController != null)
            {
                other.gameObject.GetComponent<PlayerController>().Hit(_gunController.damage);
                _controller.BackDamage(_gunController.damage);
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().Hit(damage);
                _controller.BackDamage(damage);
            }           
        }
    }
}
