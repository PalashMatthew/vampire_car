using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;

    [Header("Damage")]
    public float damage;
    public float baseDamage;
    public float damageCoeff;

    [Header("ShotSpeed")]
    public float shotSpeed;
    public float baseShotSpeed;
    public float shotSpeedCoeff;

    [Header("BulletMoveSpeed")]
    public float bulletMoveSpeed;
    public float baseBulletMoveSpeed;
    public float bulletMoveSpeedCoeff;

    [Header("TimeOfAction")]
    public float timeOfAction;
    public float baseTimeOfAction;
    public float timeOfActionCoeff;

    [Header("FreezeTime")]
    public float freezeTime;
    public float baseFreezeTime;
    public float freezeTimeCoeff;

    [Header("Projectile")]
    public float projectileValue;

    [Header("Area")]
    public float areaValue;
    public float baseAreaValue;
    public float areaCoeff;

    [Header("MultiplyDamage")]
    public float multiplyDamage;
    public float baseMultiplyDamage;
    public float multiplyDamageCoeff;

    [Header("RotateSpeed")]
    public float rotateSpeed;
    public float baseRotateSpeed;
    public float rotateSpeedCoeff;

    [Header("Ricochet")]
    public float ricochetCount;

    PlayerStats _playerStats;
    PlayerPassiveController _passiveController;


    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _playerStats = GetComponentInParent<PlayerStats>();
        _passiveController = GetComponentInParent<PlayerPassiveController>();

        //Заполняе базовые значения
        baseDamage = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsDamage");
        baseShotSpeed = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsShotSpeed");
        baseBulletMoveSpeed = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsBulletMoveSpeed");
        baseTimeOfAction = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsTimeOfAction");
        baseFreezeTime = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsFreezeTime");
        projectileValue = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsProjectile");
        baseAreaValue = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsArea");
        baseRotateSpeed = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsRotateSpeed");
        ricochetCount = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsRicochet");
        baseMultiplyDamage = PlayerPrefs.GetFloat(gunName + "gunBaseSettingsMultiplyDamage");

        //Заполняем значения коэффициентов
        //damageCoeff = PlayerPrefs.GetFloat(gunName + "gunCoeffSettingsDamage") + PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carDamage");
        damageCoeff = _playerStats.damageCoeff;
        shotSpeedCoeff = PlayerPrefs.GetFloat(gunName + "gunCoeffSettingsShotSpeed");
        bulletMoveSpeedCoeff = PlayerPrefs.GetFloat(gunName + "gunCoeffSettingsBulletMoveSpeed");
        timeOfActionCoeff = PlayerPrefs.GetFloat(gunName + "gunCoeffSettingsTimeOfAction");
        freezeTimeCoeff = PlayerPrefs.GetFloat(gunName + "gunCoeffSettingsFreezeTime");
        areaCoeff = PlayerPrefs.GetFloat(gunName + "gunCoeffSettingsArea");
        rotateSpeedCoeff = PlayerPrefs.GetFloat(gunName + "gunCoeffSettingsRotateSpeed");
        multiplyDamageCoeff = PlayerPrefs.GetFloat(gunName + "gunCoeffSettingsMultiplyDamage");

        CalculateStats();

        PlayerPrefs.SetFloat(gunName + "_Damage = ", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(gunName + " Damage = " + PlayerPrefs.GetFloat(gunName + "_Damage = "));
        }
    }

    public void CalculateStats()
    {
        if (damageCoeff == 0)
            damage = baseDamage;
        else damage = baseDamage + (baseDamage / 100 * damageCoeff);

        if (shotSpeedCoeff == 0)
            shotSpeed = baseShotSpeed;
        else shotSpeed = baseShotSpeed - (baseShotSpeed / 100 * shotSpeedCoeff);

        if (bulletMoveSpeedCoeff == 0)
            bulletMoveSpeed = baseBulletMoveSpeed;
        else bulletMoveSpeed = baseBulletMoveSpeed + (baseBulletMoveSpeed / 100 * bulletMoveSpeedCoeff);

        if (timeOfActionCoeff == 0)
            timeOfAction = baseTimeOfAction;
        else timeOfAction = baseTimeOfAction + (baseTimeOfAction / 100 * timeOfActionCoeff);

        if (freezeTimeCoeff == 0)
            freezeTime = baseFreezeTime;
        else freezeTime = baseFreezeTime + (baseFreezeTime / 100 * freezeTimeCoeff);

        if (areaCoeff == 0)
            areaValue = baseAreaValue;
        else areaValue = baseAreaValue + (baseAreaValue / 100 * areaCoeff);

        if (rotateSpeedCoeff == 0)
            rotateSpeed = baseRotateSpeed;
        else rotateSpeed = baseRotateSpeed + (baseRotateSpeed / 100 * rotateSpeedCoeff);

        if (multiplyDamageCoeff == 0)
            multiplyDamage = baseMultiplyDamage;
        else multiplyDamage = baseMultiplyDamage + (baseMultiplyDamage / 100 * multiplyDamageCoeff);
    }

    #region Calculate Damage
    public float CalculateDamage()
    {
        int _rand = Random.Range(1, 101);
        if (_rand <= _playerStats.kritChance)
        {
            float _damage = (damage * _playerStats.rageValue) * ((damage * _playerStats.rageValue) / 100 * _playerStats.kritDamage);
            //GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);

            PlayerPrefs.SetFloat(gunName + "_Damage = ", PlayerPrefs.GetFloat(gunName + "_Damage = ") + _damage);
            return _damage;
        } 
        else
        {
            //GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(damage * _playerStats.rageValue);
            PlayerPrefs.SetFloat(gunName + "_Damage = ", PlayerPrefs.GetFloat(gunName + "_Damage = ") + damage * _playerStats.rageValue);
            return damage * _playerStats.rageValue;
        }
    }

    public void DamageEnemy(GameObject _gm, GameObject _bullet)
    {
        int headshotRand = Random.Range(0, 101);

        if (headshotRand <= _playerStats.headshotProcent)
        {
            _gm.gameObject.GetComponent<EnemyController>().Headshot();
            return;
        }

        int _rand = Random.Range(1, 101);
        float _dmg = damage;

        if (_passiveController.isDistanceDamage)
        {
            float _dist = Vector3.Distance(_bullet.GetComponent<GunDistanceAttack>().startCoord, _bullet.transform.position);
            float _damageCoeff = _dist / 80;
            _damageCoeff = 1 - _damageCoeff;
            _damageCoeff *= _playerStats.distanceDamageCoeff;

            _dmg = _dmg * _damageCoeff;
        }

        if (_rand <= _playerStats.kritChance)
        {
            float _damage = (_dmg * _playerStats.rageValue) * ((_dmg * _playerStats.rageValue) / 100 * _playerStats.kritDamage);
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            _gm.gameObject.GetComponent<EnemyController>().Hit(_damage, true);
            PlayerPrefs.SetFloat(gunName + "_Damage = ", PlayerPrefs.GetFloat(gunName + "_Damage = ") + _damage);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_dmg * _playerStats.rageValue);
            _gm.gameObject.GetComponent<EnemyController>().Hit(damage * _playerStats.rageValue, false);
            PlayerPrefs.SetFloat(gunName + "_Damage = ", PlayerPrefs.GetFloat(gunName + "_Damage = ") + damage * _playerStats.rageValue);
        }
    }

    public void DamageEnemyPunching(GameObject _gm, float dmg)
    {
        int _rand = Random.Range(1, 101);
        if (_rand <= _playerStats.kritChance)
        {
            float _damage = (dmg * _playerStats.rageValue) * ((dmg * _playerStats.rageValue) / 100 * _playerStats.kritDamage);
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            _gm.gameObject.GetComponent<EnemyController>().Hit(_damage, true);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(dmg * _playerStats.rageValue);
            _gm.gameObject.GetComponent<EnemyController>().Hit(dmg * _playerStats.rageValue, false);
        }
    }

    public void DamageBoss(GameObject _gm)
    {
        int _rand = Random.Range(1, 101);
        if (_rand <= _playerStats.kritChance)
        {
            float _damage = (damage * _playerStats.rageValue) * ((damage * _playerStats.rageValue) / 100 * _playerStats.kritDamage);
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            _gm.gameObject.GetComponentInParent<EnemyController>().Hit(_damage, true);
            PlayerPrefs.SetFloat(gunName + "_Damage = ", PlayerPrefs.GetFloat(gunName + "_Damage = ") + _damage);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(damage * _playerStats.rageValue);
            _gm.gameObject.GetComponentInParent<EnemyController>().Hit(damage * _playerStats.rageValue, false);
            PlayerPrefs.SetFloat(gunName + "_Damage = ", PlayerPrefs.GetFloat(gunName + "_Damage = ") + damage * _playerStats.rageValue);
        }
    }

    public void DamageBossPunching(GameObject _gm, float dmg)
    {
        int _rand = Random.Range(1, 101);
        if (_rand <= _playerStats.kritChance)
        {
            float _damage = (dmg * _playerStats.rageValue) * ((dmg * _playerStats.rageValue) / 100 * _playerStats.kritDamage);
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            _gm.gameObject.GetComponentInParent<EnemyController>().Hit(_damage, true);
        }
        else
        {
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(dmg * _playerStats.rageValue);
            _gm.gameObject.GetComponentInParent<EnemyController>().Hit(dmg * _playerStats.rageValue, false);
        }
    }
    #endregion
}
