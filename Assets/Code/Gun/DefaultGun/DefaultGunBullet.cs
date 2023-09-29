using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGunBullet : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _gunController.bulletMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Hit(_gunController.damage);
            Destroy(gameObject);
        }        
    }
}
