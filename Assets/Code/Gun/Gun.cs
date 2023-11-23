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
    public float damageCoeffPassive;

    [Header("ShotSpeed")]
    public float shotSpeed;
    public float baseShotSpeed;
    public float shotSpeedCoeff;
    public float shotSpeedCoeffPassive;

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

        if (damageCoeff != 0)
            baseDamage = baseDamage + (baseDamage / 100 * damageCoeff);

        if (PlayerPrefs.GetString("activeCarID") == "Taiowa")
        {
            if (gunName == "Dron"|| gunName == "Lazer")
            {
                baseDamage *= 2;
            }
        }

        if (PlayerPrefs.GetString("activeCarID") == "P-Run")
        {
            if (gunName == "RocketLauncher" || gunName == "Ice")
            {
                baseDamage = baseDamage + (baseDamage / 100 * 70);
            }
        }

        damage = baseDamage;

        //if (damageCoeff == 0)
        //    damage = baseDamage;
        //else damage = baseDamage + (baseDamage / 100 * damageCoeff);

        if (shotSpeedCoeff != 0)
            baseShotSpeed = baseShotSpeed - (baseShotSpeed / 100 * shotSpeedCoeff);

        shotSpeed = baseShotSpeed;

        //if (shotSpeedCoeff == 0)
        //    shotSpeed = baseShotSpeed;
        //else shotSpeed = baseShotSpeed - (baseShotSpeed / 100 * shotSpeedCoeff);

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
        if (damageCoeffPassive != 0)
        {
            damage = baseDamage + (baseDamage / 100 * damageCoeffPassive);
            damageCoeffPassive = 0;
        }

        if (shotSpeedCoeffPassive != 0)
        {
            shotSpeed = shotSpeed + (shotSpeed / 100 * shotSpeedCoeffPassive);
            shotSpeedCoeffPassive = 0;
        }

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
        if (_playerStats.headshotProcent > 0)
        {
            int headshotRand = Random.Range(0, 101);

            if (headshotRand <= _playerStats.headshotProcent)
            {
                _gm.gameObject.GetComponent<EnemyController>().Headshot();
                return;
            }
        }

        int _rand = Random.Range(1, 101);
        float _dmg = damage;

        if (_playerStats.distanceDamageCoeff > 0)
        {
            float _dist = Vector3.Distance(_bullet.GetComponent<GunDistanceAttack>().startCoord, _bullet.transform.position);
            float _damageCoeff = _dist / 80;
            _damageCoeff = 1 - _damageCoeff;
            _damageCoeff = _playerStats.distanceDamageCoeff * _damageCoeff;

            _dmg = _dmg * _damageCoeff;
        }

        if (_playerStats.massEnemyDamage > 0)
        {
            float value = 0;

            if (GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count < 7)
                value = 0;

            if (GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count >= 7 &&
                GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count < 14)
                value = 1;

            if (GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count >= 14 &&
                GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count < 21)
                value = 2;

            if (GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count >= 21 &&
                GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count < 28)
                value = 3;

            if (GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count >= 28 &&
                GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count < 35)
                value = 4;

            if (GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Count >= 35)
                value = 5;

            value = value * _playerStats.massEnemyDamage;

            _dmg = _dmg + (_dmg / 100 * value);
        }

        if (_rand <= _playerStats.kritChance && _playerStats.kritChance > 0)
        {
            float _damage = _dmg;

            if (_playerStats.currentHp < _playerStats.maxHp / 100 * 50)
            {
                float _maxHP = _playerStats.maxHp / 100 * 50;

                float r = _playerStats.currentHp / _maxHP;
                r = 1 - r;

                r *= _playerStats.rageValue;

                _damage += _damage / 100 * r;
            }                

            if (_gm.GetComponent<EnemyController>().enemyKind == "car")
            {
                _damage += _damage / 100 * _playerStats.carDamage;
            }

            if (_gm.GetComponent<EnemyController>().enemyKind == "dron")
            {
                _damage += _damage / 100 * _playerStats.dronDamage;
            }

            _damage += _damage / 100 * _playerStats.kritDamage;

            if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s07")  //Если у нас сет Таран активен
            {
                _damage = _damage + (_damage / 100 * PlayerPrefs.GetFloat("setValue"));
            }

            if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s08")  //Если у нас сет Таран активен
            {
                _damage = _damage + (_damage / 100 * (PlayerPrefs.GetFloat("setValue") * PlayerGuns.gunCount));
            }

            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            _gm.gameObject.GetComponent<EnemyController>().Hit(_damage, true);
            PlayerPrefs.SetFloat(gunName + "_Damage = ", PlayerPrefs.GetFloat(gunName + "_Damage = ") + _damage);

            #region Effects
            if ((PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s02") || PlayerPrefs.GetString("activeCarID") == "Lyssa")  //Если у нас сет Огня активен
            {
                int rand = Random.Range(1, 101);

                float value = 0;
                if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s02")
                {
                    value += PlayerPrefs.GetFloat("setValue");
                }

                if (PlayerPrefs.GetString("activeCarID") == "Lyssa")
                {
                    value += 3;
                }

                if (rand <= value)
                {
                    _gm.GetComponent<EnemyEffects>().FireEffect(_damage);
                }
            }

            if ((PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s03") || PlayerPrefs.GetString("activeCarID") == "Aeolus")  //Если у нас сет Яда активен
            {
                int rand = Random.Range(1, 101);

                float value = 0;
                if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s03")
                {
                    value += PlayerPrefs.GetFloat("setValue");
                }

                if (PlayerPrefs.GetString("activeCarID") == "Aeolus")
                {
                    value += 5;
                }

                if (rand <= value)
                {
                    _gm.GetComponent<EnemyEffects>().PoisonEffect(_damage);
                }
            }
            #endregion
        }
        else
        {
            float _damage = _dmg;

            if (_playerStats.currentHp < _playerStats.maxHp / 100 * 50)
                _damage += _damage / 100 * _playerStats.rageValue;

            if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s07")  //Если у нас сет Таран активен
            {
                _damage = _damage + (_damage / 100 * PlayerPrefs.GetFloat("setValue"));
            }

            if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s08")  //Если у нас сет Таран активен
            {
                _damage = _damage + (_damage / 100 * (PlayerPrefs.GetFloat("setValue") * PlayerGuns.gunCount));
            }

            if (_gm.GetComponent<EnemyController>().enemyKind == "car")
            {
                _damage += _damage / 100 * _playerStats.carDamage;
            }

            if (_gm.GetComponent<EnemyController>().enemyKind == "dron")
            {
                _damage += _damage / 100 * _playerStats.dronDamage;
            }

            GameObject.Find("Player").GetComponent<PlayerPassiveController>().Vampirizm(_damage);
            _gm.gameObject.GetComponent<EnemyController>().Hit(_damage, false);
            PlayerPrefs.SetFloat(gunName + "_Damage = ", PlayerPrefs.GetFloat(gunName + "_Damage = ") + _damage);

            #region Effects
            if ((PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s02") || PlayerPrefs.GetString("activeCarID") == "Lyssa")  //Если у нас сет Огня активен
            {
                int rand = Random.Range(1, 101);

                float value = 0;
                if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s02")
                {
                    value += PlayerPrefs.GetFloat("setValue");
                }

                if (PlayerPrefs.GetString("activeCarID") == "Lyssa")
                {
                    value += 3;
                }

                if (rand <= value)
                {
                    _gm.GetComponent<EnemyEffects>().FireEffect(damage * _playerStats.rageValue);
                }
            }

            if ((PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s03") || PlayerPrefs.GetString("activeCarID") == "Aeolus")  //Если у нас сет Яда активен
            {
                int rand = Random.Range(1, 101);

                float value = 0;
                if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s03")
                {
                    value += PlayerPrefs.GetFloat("setValue");
                }

                if (PlayerPrefs.GetString("activeCarID") == "Aeolus")
                {
                    value += 5;
                }

                if (rand <= value)
                {
                    _gm.GetComponent<EnemyEffects>().PoisonEffect(damage * _playerStats.rageValue);
                }
            }
            #endregion
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
    #endregion
}
