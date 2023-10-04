using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassiveController : MonoBehaviour
{
    [Header("Passive")]
    public bool isPassiveHealthRecovery;
    public bool isPassiveRage;

    PlayerController _playerController;
    PlayerStats _playerStats;

    //Rage
    private float _savePlayerDamage;


    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (isPassiveHealthRecovery)
        {
            PassiveHealthRecovery();
        }

        if (isPassiveRage)
        {
            PassiveRage();
        }
    }

    public void PassiveHealthRecovery()
    {
        if (isPassiveHealthRecovery)
        {
            int _rand = Random.Range(1, 4);
            _playerStats.currentHp += _playerStats.maxHp / 100 * _rand;
        }
    }

    public void PassiveRage()
    {
        if (isPassiveRage && _playerStats.currentHp <= _playerStats.maxHp / 100 * 30)
        {
            float _procent = (_playerStats.maxHp - _playerStats.currentHp) / _playerStats.maxHp * _playerStats.rageCoeff;

            _playerStats.rageValue = _procent;
        }
    }
}
