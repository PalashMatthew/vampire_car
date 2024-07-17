using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class SaveTry : MonoBehaviour
{
    WaveController waveController;
    PlayerStats playerStats;
    PlayerGuns playerGuns;
    PlayerPassiveController playerPassiveController;
    UpgradeController upgradeController;
    PopUpRecovery popUpRecovery;

    private void Start()
    {
        StartCoroutine(SaveEnum());
    }

    IEnumerator SaveEnum()
    {
        yield return new WaitForSeconds(2);
        Save();
        StartCoroutine(SaveEnum());
    }

    private void OnApplicationPause(bool pause)
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    void Save()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        waveController = GameObject.Find("GameplayController").GetComponent<WaveController>();
        playerGuns = GameObject.Find("Player").GetComponent<PlayerGuns>();
        playerPassiveController = GameObject.Find("Player").GetComponent<PlayerPassiveController>();
        upgradeController = GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>();
        popUpRecovery = GameObject.Find("PopUp Recovery").GetComponent<PopUpRecovery>();

        PlayerPrefs.SetString("locationNameSave", Application.loadedLevelName);

        //PlayerPrefs.SetInt("locationContinue", 1);
        PlayerPrefs.SetInt("saveTryCurrentWaveNum", WaveController.fakeCurrentWave);

        if (WaveController.fakeCurrentWave >= 10)
        {
            PlayerPrefs.SetInt("saveTryCurrentWaveNum", 10);
        }


        PlayerPrefs.SetString("saveTryRecovery", popUpRecovery.isRecovery.ToString());

        #region PlayerStats
        PlayerPrefs.SetFloat("saveTryMaxHpBase", playerStats.maxHpBase);
        PlayerPrefs.SetFloat("saveTryMaxHpCoeff", playerStats.maxHpCoeff);
        PlayerPrefs.SetFloat("saveTryMaxHp", playerStats.maxHp);
        PlayerPrefs.SetFloat("saveTryCurrentHp", playerStats.currentHp);

        PlayerPrefs.SetFloat("saveTryKritChance", playerStats.kritChance);

        PlayerPrefs.SetFloat("saveTryDodgeProcent", playerStats.dodgeProcent);

        PlayerPrefs.SetFloat("saveTryDamageCoeff", playerStats.damageCoeff);

        PlayerPrefs.SetFloat("saveTryRecoveryHpInFirstAidKit", playerStats.recoveryHpInFirstAidKit);

        PlayerPrefs.SetFloat("saveTryBlock", playerStats.block);

        PlayerPrefs.SetFloat("saveTryIron", playerStats.iron);

        PlayerPrefs.SetFloat("saveTryDronDamage", playerStats.dronDamage);

        PlayerPrefs.SetFloat("saveTryShotSpeed", playerStats.shotSpeed);

        PlayerPrefs.SetFloat("saveTryKritDamage", playerStats.kritDamage);

        PlayerPrefs.SetFloat("saveTryBackDamageProcent", playerStats.backDamageProcent);

        PlayerPrefs.SetFloat("saveTryVampirizm", playerStats.vampirizm);

        PlayerPrefs.SetFloat("saveTryArmorProcent", playerStats.armorProcent);

        PlayerPrefs.SetFloat("saveTryHealthRecovery", playerStats.healthRecovery);

        PlayerPrefs.SetFloat("saveTryRageValue", playerStats.rageValue);

        PlayerPrefs.SetFloat("saveTryRageCoeff", playerStats.rageCoeff);

        PlayerPrefs.SetFloat("saveTryDistanceDamageCoeff", playerStats.distanceDamageCoeff);

        PlayerPrefs.SetFloat("saveTryLucky", playerStats.lucky);

        PlayerPrefs.SetFloat("saveTryCarDamage", playerStats.carDamage);

        PlayerPrefs.SetFloat("saveTryMassEnemyDamage", playerStats.massEnemyDamage);

        PlayerPrefs.SetFloat("saveTryHeadshotProcent", playerStats.headshotProcent);

        PlayerPrefs.SetFloat("saveTryEffectDuration", playerStats.effectDuration);
        #endregion

        #region PlayerGuns
        PlayerPrefs.SetInt("saveTryGunCount", PlayerGuns.gunCount);

        PlayerPrefs.SetString("saveTryRocketLauncher", playerGuns._isRocketLauncher.ToString());
        PlayerPrefs.SetString("saveTryBoomerang", playerGuns._isBoomerang.ToString());
        PlayerPrefs.SetString("saveTryPartner", playerGuns._isPartner.ToString());
        PlayerPrefs.SetString("saveTryDron", playerGuns._isDron.ToString());
        PlayerPrefs.SetString("saveTryIce", playerGuns._isIce.ToString());
        PlayerPrefs.SetString("saveTryLazer", playerGuns._isLazer.ToString());
        PlayerPrefs.SetString("saveTryGrowingShot", playerGuns._isGrowingShot.ToString());
        PlayerPrefs.SetString("saveTryFanGun", playerGuns._isFanGun.ToString());
        PlayerPrefs.SetString("saveTryTornado", playerGuns._isTornado.ToString());
        PlayerPrefs.SetString("saveTryMines", playerGuns._isMines.ToString());
        PlayerPrefs.SetString("saveTryGrenade", playerGuns._isGrenade.ToString());
        PlayerPrefs.SetString("saveTryGodGun", playerGuns._isGodGun.ToString());
        PlayerPrefs.SetString("saveTryRicochet", playerGuns._isRicochet.ToString());
        PlayerPrefs.SetString("saveTryBow", playerGuns._isBow.ToString());
        PlayerPrefs.SetString("saveTryPinPong", playerGuns._isPinPong.ToString());
        #endregion

        #region PlayerPassive
        PlayerPrefs.SetString("saveTryIsVampirizm", playerPassiveController.isVampirizm.ToString());
        PlayerPrefs.SetString("saveTryIsPassiveRage", playerPassiveController.isPassiveRage.ToString());
        PlayerPrefs.SetString("saveTryIsBackDamage", playerPassiveController.isBackDamage.ToString());
        PlayerPrefs.SetString("saveTryIsDodge", playerPassiveController.isDodge.ToString());
        PlayerPrefs.SetString("saveTryIsPunching", playerPassiveController.isPunching.ToString());
        PlayerPrefs.SetString("saveTryIsHeadshot", playerPassiveController.isHeadshot.ToString());
        PlayerPrefs.SetString("saveTryIsScrewValueUp", playerPassiveController.isScrewValueUp.ToString());
        PlayerPrefs.SetString("saveTryIsDistanceDamage", playerPassiveController.isDistanceDamage.ToString());
        PlayerPrefs.SetString("saveTryIsPassiveHealthRecovery", playerPassiveController.isPassiveHealthRecovery.ToString());

        PlayerPrefs.SetFloat("saveTryHealthRecoveryProcent", playerPassiveController.healthRecoveryProcent);
        #endregion

        #region GunsSettings
        for (int i = 0; i < playerGuns.guns.Count; i++)
        {
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "damage", playerGuns.guns[i].damage);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "baseDamage", playerGuns.guns[i].baseDamage);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "damageCoeff", playerGuns.guns[i].damageCoeff);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "damageCoeffPassive", playerGuns.guns[i].damageCoeffPassive);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "shotSpeed", playerGuns.guns[i].shotSpeed);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "baseShotSpeed", playerGuns.guns[i].baseShotSpeed);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "shotSpeedCoeff", playerGuns.guns[i].shotSpeedCoeff);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "shotSpeedCoeffPassive", playerGuns.guns[i].shotSpeedCoeffPassive);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "bulletMoveSpeed", playerGuns.guns[i].bulletMoveSpeed);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "baseBulletMoveSpeed", playerGuns.guns[i].baseBulletMoveSpeed);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "bulletMoveSpeedCoeff", playerGuns.guns[i].bulletMoveSpeedCoeff);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "timeOfAction", playerGuns.guns[i].timeOfAction);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "baseTimeOfAction", playerGuns.guns[i].baseTimeOfAction);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "timeOfActionCoeff", playerGuns.guns[i].timeOfActionCoeff);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "freezeTime", playerGuns.guns[i].freezeTime);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "baseFreezeTime", playerGuns.guns[i].baseFreezeTime);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "freezeTimeCoeff", playerGuns.guns[i].freezeTimeCoeff);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "projectileValue", playerGuns.guns[i].projectileValue);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "areaValue", playerGuns.guns[i].areaValue);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "baseAreaValue", playerGuns.guns[i].baseAreaValue);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "areaCoeff", playerGuns.guns[i].areaCoeff);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "multiplyDamage", playerGuns.guns[i].multiplyDamage);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "baseMultiplyDamage", playerGuns.guns[i].baseMultiplyDamage);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "multiplyDamageCoeff", playerGuns.guns[i].multiplyDamageCoeff);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "rotateSpeed", playerGuns.guns[i].rotateSpeed);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "baseRotateSpeed", playerGuns.guns[i].baseRotateSpeed);
            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "rotateSpeedCoeff", playerGuns.guns[i].rotateSpeedCoeff);

            PlayerPrefs.SetFloat("saveTry" + playerGuns.guns[i].gunName + "ricochetCount", playerGuns.guns[i].ricochetCount);
        }
        #endregion

        #region Level Cards Guns
        PlayerPrefs.SetInt("saveTryBoomerang_Level", upgradeController.Boomerang_Level);
        PlayerPrefs.SetInt("saveTryDron_Level", upgradeController.Dron_Level);
        PlayerPrefs.SetInt("saveTryIce_Level", upgradeController.Ice_Level);
        PlayerPrefs.SetInt("saveTryLazer_Level", upgradeController.Lazer_Level);
        PlayerPrefs.SetInt("saveTryLightning_Level", upgradeController.Lightning_Level);
        PlayerPrefs.SetInt("saveTryPartner_Level", upgradeController.Partner_Level);
        PlayerPrefs.SetInt("saveTryRocketLauncher_Level", upgradeController.RocketLauncher_Level);
        PlayerPrefs.SetInt("saveTryDefaultGun_Level", upgradeController.DefaultGun_Level);
        PlayerPrefs.SetInt("saveTryGrowingShot_Level", upgradeController.GrowingShot_Level);
        PlayerPrefs.SetInt("saveTryFanGun_Level", upgradeController.FanGun_Level);
        PlayerPrefs.SetInt("saveTryTornado_Level", upgradeController.Tornado_Level);
        PlayerPrefs.SetInt("saveTryMines_Level", upgradeController.Mines_Level);
        PlayerPrefs.SetInt("saveTryGrenade_Level", upgradeController.Grenade_Level);
        PlayerPrefs.SetInt("saveTryGodGun_Level", upgradeController.GodGun_Level);
        PlayerPrefs.SetInt("saveTryRicochet_Level", upgradeController.RocketLauncher_Level);
        PlayerPrefs.SetInt("saveTryBow_Level", upgradeController.Bow_Level);
        PlayerPrefs.SetInt("saveTryPinPong_Level", upgradeController.PinPong_Level);
        #endregion

        Debug.Log(PlayerPrefs.GetInt("saveTryPassiveCount") + " - PassiveCount");
        Debug.Log("Save Try");
    }
}
