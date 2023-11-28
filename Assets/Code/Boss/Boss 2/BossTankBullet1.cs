using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankBullet1 : MonoBehaviour
{
    [HideInInspector] public EnemyController _controller;
    public float damage;


    private void Update()
    {
        transform.Translate(Vector3.forward * 70 * Time.deltaTime);

        if (transform.position.z < -20f)
            Destroy(gameObject);

        if (WaveController.isWaveEnd)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(damage);
            _controller.BackDamage(damage);

            Destroy(gameObject);
        }

        if (other.tag == "obstacle")
        {
            Destroy(gameObject);
        }
    }
}
