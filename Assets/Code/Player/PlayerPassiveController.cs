using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassiveController : MonoBehaviour
{
    [Header("Passive")]
    public bool isVampirizm;
    public bool isPassiveRage;

    [Header("Health Recovery")]
    public bool isPassiveHealthRecovery;
    public float healthRecoveryProcent;

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
            //PassiveHealthRecovery();
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
            _playerStats.currentHp += _playerStats.maxHp / 100 * healthRecoveryProcent;
        }
    }

    public void PassiveRage()
    {
        if (isPassiveRage && _playerStats.currentHp <= _playerStats.maxHp / 100 * 30)
        {
            //float _procent = (_playerStats.maxHp - _playerStats.currentHp) / _playerStats.maxHp * _playerStats.rageCoeff;            

            _playerStats.rageValue = _playerStats.rageCoeff;
        } 
        else
        {
            _playerStats.rageValue = 1;
        }
    }

    public void Vampirizm(float _damage)
    {
        if (isVampirizm)
        {
            float _procent = _damage / 100 * _playerStats.vampirizm;
            _playerStats.currentHp += _procent;
        }
    }
}
