using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGunBullet : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    private PlayerStats _playerStats;
    private PlayerPassiveController _playerPassiveController;

    private float _damage;

    public bool isPlayerAttack = false;

    private float punchingCount;

    


    private void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _playerPassiveController = GameObject.Find("Player").GetComponent<PlayerPassiveController>();

        _damage = _gunController.damage;

        punchingCount = _playerStats.punchingCount;

        
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * _gunController.bulletMoveSpeed * Time.deltaTime);

        if (transform.position.z > 80)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            _gunController.DamageEnemy(other.gameObject, gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "boss")
        {
            _gunController.DamageBoss(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
            Destroy(gameObject);
        }

        if (other.tag == "player" && isPlayerAttack)
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_gunController.CalculateDamage());
            Destroy(gameObject);
        }
    }
}
