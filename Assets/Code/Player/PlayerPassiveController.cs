using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassiveController : MonoBehaviour
{
    [Header("Passive")]
    public bool isVampirizm;
    public bool isPassiveRage;
    public bool isBackDamage;
    public bool isDodge;
    public bool isPunching;
    public bool isHeadshot;
    public bool isScrewValueUp;
    public bool isDistanceDamage;
    

    [Header("Health Recovery")]
    public bool isPassiveHealthRecovery;
    public float healthRecoveryProcent;

    PlayerController _playerController;
    PlayerStats _playerStats;


    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerStats = GetComponent<PlayerStats>();

        StartCoroutine(PassiveHP());
    }

    private void Update()
    {
        if (isPassiveRage)
        {
            //PassiveRage();
        }
    }

    IEnumerator PassiveHP()
    {
        yield return new WaitForSeconds(1);
        if (isPassiveHealthRecovery)
        {
            if (_playerStats.currentHp < _playerStats.maxHp)
                _playerStats.currentHp += _playerStats.maxHp / 100 * healthRecoveryProcent;

            if (_playerStats.currentHp > _playerStats.maxHp)
                _playerStats.currentHp = _playerStats.maxHp;
        }

        StartCoroutine(PassiveHP());
    }

    public void PassiveRage()
    {
        if (isPassiveRage && _playerStats.currentHp <= _playerStats.maxHp / 100 * 30)
        {     
            _playerStats.rageValue = _playerStats.rageCoeff;
        } 
        else
        {
            _playerStats.rageValue = 1;
        }
    }

    public void Vampirizm(float _damage)
    {
        if (_playerStats.vampirizm > 0)
        {
            float _procent = _damage / 100 * _playerStats.vampirizm;
            _playerStats.currentHp += _procent;
        }
    }
}
