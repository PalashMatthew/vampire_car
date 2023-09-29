using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefaultBullet : MonoBehaviour
{
    [HideInInspector]
    public EnemyGun _gunController;

    private void Start()
    {
        Destroy(gameObject, 9);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _gunController.bulletMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_gunController.damage);
            Destroy(gameObject);
        }
    }
}
