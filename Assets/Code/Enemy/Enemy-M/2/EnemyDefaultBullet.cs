using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefaultBullet : MonoBehaviour
{
    [HideInInspector] public EnemyGun _gunController;
    [HideInInspector] public EnemyController _controller;


    private void Update()
    {
        transform.Translate(Vector3.forward * _gunController.bulletMoveSpeed * Time.deltaTime);

        if (transform.position.z < -20f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_gunController.damage);
            _controller.BackDamage(_gunController.damage);

            Destroy(gameObject);
        }

        if (other.tag == "obstacle")
        {
            Destroy(gameObject);
        }
    }
}
