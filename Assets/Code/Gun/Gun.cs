using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float shotSpeed;
    public float bulletMoveSpeed;
    public float timeOfAction;
    public float freezeTime;
    public float projectileValue;
    public float areaValue;

    PlayerStats _playerStats;


    private void Start()
    {
        _playerStats = GetComponentInParent<PlayerStats>();
    }

    public float CalculateDamage()
    {
        int _rand = Random.Range(1, 101);
        if (_rand <= _playerStats.kritChance)
        {
            float _damage = (damage * _playerStats.rageValue) * _playerStats.kritDamage;
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            return _damage;
        } 
        else
        {
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(damage * _playerStats.rageValue);
            return damage * _playerStats.rageValue;
        }
    }

    public void DamageEnemy(GameObject _gm)
    {
        int _rand = Random.Range(1, 101);
        if (_rand <= _playerStats.kritChance)
        {
            float _damage = (damage * _playerStats.rageValue) * _playerStats.kritDamage;
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            _gm.gameObject.GetComponent<EnemyController>().Hit(_damage, true);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(damage * _playerStats.rageValue);
            _gm.gameObject.GetComponent<EnemyController>().Hit(damage * _playerStats.rageValue, false);
        }
    }

    public void DamageBoss(GameObject _gm)
    {
        int _rand = Random.Range(1, 101);
        if (_rand <= _playerStats.kritChance)
        {
            float _damage = (damage * _playerStats.rageValue) * _playerStats.kritDamage;
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            _gm.gameObject.GetComponentInParent<EnemyController>().Hit(_damage, true);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(damage * _playerStats.rageValue);
            _gm.gameObject.GetComponentInParent<EnemyController>().Hit(damage * _playerStats.rageValue, false);
        }
    }
}
