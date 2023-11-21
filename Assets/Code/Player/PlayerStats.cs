using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float currentLevel;
    public float currentExp;
    public List<float> levelExpNeed;
    PlayerController playerController;

    [HideInInspector]
    public float currentHp;
    public float maxHp;
    public float maxHpBase;
    public float maxHpCoeff;

    public float damageCoeff;

    public float kritChance;
    public float kritDamage;

    public float projectileCount;

    public float rageValue;
    public float rageCoeff;

    public float vampirizm;

    public float backDamageProcent;

    public float dodgeProcent;

    public float armorProcent;

    public float punchingCount;
    public float punchingProcent;

    public float headshotProcent;

    public float screwValueUp;
    public float baseScrewValue;
    public float screwValueCoeff;

    public float screwPickUpDistance;
    public float baseScrewPickUpDistance;
    public float screwPickUpDistanceCoeff;

    public float distanceDamageCoeff;

    public float recoveryHpInFirstAidKit;

    public float dronDamage;

    public float carDamage;

    public float shotSpeed;

    public float healthRecovery;

    public float lucky;

    public float magnet;

    public float massEnemyDamage;

    public float effectDuration;

    public float block;

    public float iron;


    private void Awake()
    {
        currentLevel = 1;
        playerController = GetComponent<PlayerController>();

        StatsInitialize();
    }

    void StatsInitialize()
    {
        #region Health
        maxHpBase = PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carHealth");
        maxHpBase += maxHpBase / 100 * PlayerPrefs.GetInt("talentCarImprovementCurrentValue");

        float hpDetailSum = PlayerPrefs.GetFloat("GunSelectHealth")
                     + PlayerPrefs.GetFloat("EngineSelectHealth")
                     + PlayerPrefs.GetFloat("BrakesSelectHealth")
                     + PlayerPrefs.GetFloat("FuelSystemSelectHealth")
                     + PlayerPrefs.GetFloat("SuspensionSelectHealth")
                     + PlayerPrefs.GetFloat("TransmissionSelectHealth");

        maxHpCoeff = hpDetailSum + (hpDetailSum / 100 * PlayerPrefs.GetInt("talentEquipmentImprovementCurrentValue"));
        maxHpCoeff += PlayerPrefs.GetFloat("carGlobalCoeffhealth");
        maxHpCoeff += PlayerPrefs.GetInt("talentHealthCurrentValue");

        if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s10")  //Если у нас сет Таран активен
        {
            maxHpCoeff += PlayerPrefs.GetFloat("setValue");
        }

        maxHp = maxHpBase + maxHpCoeff;
        currentHp = maxHp;
        #endregion

        #region KritChance
        kritChance = PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carKritChance");
        kritChance += kritChance / 100 * PlayerPrefs.GetInt("talentCarImprovementCurrentValue");

        kritChance += PlayerPrefs.GetFloat("GunSelectKritChance")
                     + PlayerPrefs.GetFloat("EngineSelectKritChance")
                     + PlayerPrefs.GetFloat("BrakesSelectKritChance")
                     + PlayerPrefs.GetFloat("FuelSystemSelectKritChance")
                     + PlayerPrefs.GetFloat("SuspensionSelectKritChance")
                     + PlayerPrefs.GetFloat("TransmissionSelectKritChance")
                     + PlayerPrefs.GetFloat("carGlobalCoeffkritChance");
        #endregion

        #region Dodge
        dodgeProcent = PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carDodge");
        dodgeProcent += dodgeProcent / 100 * PlayerPrefs.GetInt("talentCarImprovementCurrentValue");

        dodgeProcent += PlayerPrefs.GetFloat("GunSelectDodge")
                     + PlayerPrefs.GetFloat("EngineSelectDodge")
                     + PlayerPrefs.GetFloat("BrakesSelectDodge")
                     + PlayerPrefs.GetFloat("FuelSystemSelectDodge")
                     + PlayerPrefs.GetFloat("SuspensionSelectDodge")
                     + PlayerPrefs.GetFloat("TransmissionSelectDodge")
                     + PlayerPrefs.GetFloat("carGlobalCoeffdodge");
        #endregion

        #region Damage
        damageCoeff = PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carDamage");
        damageCoeff += damageCoeff / 100 * PlayerPrefs.GetInt("talentCarImprovementCurrentValue");

        float damageDetailSum = PlayerPrefs.GetFloat("GunSelectDamage")
                     + PlayerPrefs.GetFloat("EngineSelectDamage")
                     + PlayerPrefs.GetFloat("BrakesSelectDamage")
                     + PlayerPrefs.GetFloat("FuelSystemSelectDamage")
                     + PlayerPrefs.GetFloat("SuspensionSelectDamage")
                     + PlayerPrefs.GetFloat("TransmissionSelectDamage");

        damageCoeff += PlayerPrefs.GetFloat("carGlobalCoeffdamage");
        damageCoeff += PlayerPrefs.GetInt("talentDamageCurrentValue");

        damageCoeff += damageDetailSum + (damageDetailSum / 100 * PlayerPrefs.GetInt("talentEquipmentImprovementCurrentValue"));
        #endregion

        #region RecoveryHpInFirstAidKit
        recoveryHpInFirstAidKit = PlayerPrefs.GetFloat("GunSelectRecoveryHpInFirstAidKit")
                     + PlayerPrefs.GetFloat("EngineSelectRecoveryHpInFirstAidKit")
                     + PlayerPrefs.GetFloat("BrakesSelectRecoveryHpInFirstAidKit")
                     + PlayerPrefs.GetFloat("FuelSystemSelectRecoveryHpInFirstAidKit")
                     + PlayerPrefs.GetFloat("SuspensionSelectRecoveryHpInFirstAidKit")
                     + PlayerPrefs.GetFloat("TransmissionSelectRecoveryHpInFirstAidKit")
                     + PlayerPrefs.GetInt("talentRecoveryHpInFirstAidKitCurrentValue");
        #endregion

        block = PlayerPrefs.GetInt("talentBlockCurrentValue");

        iron = PlayerPrefs.GetInt("talentIronCurrentValue");

        dronDamage = PlayerPrefs.GetFloat("GunSelectDronDamage")
                     + PlayerPrefs.GetFloat("EngineSelectDronDamage")
                     + PlayerPrefs.GetFloat("BrakesSelectDronDamage")
                     + PlayerPrefs.GetFloat("FuelSystemSelectDronDamage")
                     + PlayerPrefs.GetFloat("SuspensionSelectDronDamage")
                     + PlayerPrefs.GetFloat("TransmissionSelectDronDamage");

        shotSpeed = PlayerPrefs.GetFloat("GunSelectShotSpeed")
                     + PlayerPrefs.GetFloat("EngineSelectShotSpeed")
                     + PlayerPrefs.GetFloat("BrakesSelectShotSpeed")
                     + PlayerPrefs.GetFloat("FuelSystemSelectShotSpeed")
                     + PlayerPrefs.GetFloat("SuspensionSelectShotSpeed")
                     + PlayerPrefs.GetFloat("TransmissionSelectShotSpeed")
                     + PlayerPrefs.GetFloat("carGlobalCoeffshotSpeed")
                     + PlayerPrefs.GetInt("talentShotSpeedCurrentValue");

        kritDamage = PlayerPrefs.GetFloat("GunSelectKritDamage")
                     + PlayerPrefs.GetFloat("EngineSelectKritDamage")
                     + PlayerPrefs.GetFloat("BrakesSelectKritDamage")
                     + PlayerPrefs.GetFloat("FuelSystemSelectKritDamage")
                     + PlayerPrefs.GetFloat("SuspensionSelectKritDamage")
                     + PlayerPrefs.GetFloat("TransmissionSelectKritDamage")
                     + PlayerPrefs.GetFloat("carGlobalCoeffkritDamage");        

        backDamageProcent = PlayerPrefs.GetFloat("GunSelectBackDamage")
                     + PlayerPrefs.GetFloat("EngineSelectBackDamage")
                     + PlayerPrefs.GetFloat("BrakesSelectBackDamage")
                     + PlayerPrefs.GetFloat("FuelSystemSelectBackDamage")
                     + PlayerPrefs.GetFloat("SuspensionSelectBackDamage")
                     + PlayerPrefs.GetFloat("TransmissionSelectBackDamage")
                     + PlayerPrefs.GetFloat("carGlobalCoeffbackDamage");

        vampirizm = PlayerPrefs.GetFloat("GunSelectVampirizm")
                     + PlayerPrefs.GetFloat("EngineSelectVampirizm")
                     + PlayerPrefs.GetFloat("BrakesSelectVampirizm")
                     + PlayerPrefs.GetFloat("FuelSystemSelectVampirizm")
                     + PlayerPrefs.GetFloat("SuspensionSelectVampirizm")
                     + PlayerPrefs.GetFloat("TransmissionSelectVampirizm")
                     + PlayerPrefs.GetFloat("carGlobalCoeffvampirizm");

        armorProcent = PlayerPrefs.GetFloat("GunSelectArmor")
                     + PlayerPrefs.GetFloat("EngineSelectArmor")
                     + PlayerPrefs.GetFloat("BrakesSelectArmor")
                     + PlayerPrefs.GetFloat("FuelSystemSelectArmor")
                     + PlayerPrefs.GetFloat("SuspensionSelectArmor")
                     + PlayerPrefs.GetFloat("TransmissionSelectArmor")
                     + PlayerPrefs.GetFloat("carGlobalCoeffarmor");

        healthRecovery = PlayerPrefs.GetFloat("GunSelectHealthRecovery")
                     + PlayerPrefs.GetFloat("EngineSelectHealthRecovery")
                     + PlayerPrefs.GetFloat("BrakesSelectHealthRecovery")
                     + PlayerPrefs.GetFloat("FuelSystemSelectHealthRecovery")
                     + PlayerPrefs.GetFloat("SuspensionSelectHealthRecovery")
                     + PlayerPrefs.GetFloat("TransmissionSelectHealthRecovery");

        rageValue = 1;
        rageValue += PlayerPrefs.GetFloat("GunSelectRage")
                     + PlayerPrefs.GetFloat("EngineSelectRage")
                     + PlayerPrefs.GetFloat("BrakesSelectRage")
                     + PlayerPrefs.GetFloat("FuelSystemSelectRage")
                     + PlayerPrefs.GetFloat("SuspensionSelectRage")
                     + PlayerPrefs.GetFloat("TransmissionSelectRage");

        distanceDamageCoeff = PlayerPrefs.GetFloat("GunSelectDistanceDamage")
                     + PlayerPrefs.GetFloat("EngineSelectDistanceDamage")
                     + PlayerPrefs.GetFloat("BrakesSelectDistanceDamage")
                     + PlayerPrefs.GetFloat("FuelSystemSelectDistanceDamage")
                     + PlayerPrefs.GetFloat("SuspensionSelectDistanceDamage")
                     + PlayerPrefs.GetFloat("TransmissionSelectDistanceDamage")
                     + PlayerPrefs.GetFloat("carGlobalCoeffdistanceDamage");

        lucky = PlayerPrefs.GetFloat("GunSelectLucky")
                     + PlayerPrefs.GetFloat("EngineSelectLucky")
                     + PlayerPrefs.GetFloat("BrakesSelectLucky")
                     + PlayerPrefs.GetFloat("FuelSystemSelectLucky")
                     + PlayerPrefs.GetFloat("SuspensionSelectLucky")
                     + PlayerPrefs.GetFloat("TransmissionSelectLucky")
                     + PlayerPrefs.GetFloat("carGlobalCoefflucky");

        screwPickUpDistanceCoeff = PlayerPrefs.GetFloat("GunSelectMagnet")
                     + PlayerPrefs.GetFloat("EngineSelectMagnet")
                     + PlayerPrefs.GetFloat("BrakesSelectMagnet")
                     + PlayerPrefs.GetFloat("FuelSystemSelectMagnet")
                     + PlayerPrefs.GetFloat("SuspensionSelectMagnet")
                     + PlayerPrefs.GetFloat("TransmissionSelectMagnet")
                     + PlayerPrefs.GetFloat("carGlobalCoeffscrewValueUp");

        carDamage = PlayerPrefs.GetFloat("GunSelectCarDamage")
                     + PlayerPrefs.GetFloat("EngineSelectCarDamage")
                     + PlayerPrefs.GetFloat("BrakesSelectCarDamage")
                     + PlayerPrefs.GetFloat("FuelSystemSelectCarDamage")
                     + PlayerPrefs.GetFloat("SuspensionSelectCarDamage")
                     + PlayerPrefs.GetFloat("TransmissionSelectCarDamage");

        massEnemyDamage = PlayerPrefs.GetFloat("carGlobalCoeffmassEnemyDamage");

        headshotProcent = PlayerPrefs.GetFloat("carGlobalCoeffheadshot");

        effectDuration = PlayerPrefs.GetFloat("carGlobalCoeffeffectsDuration");
    }

    private void Update()
    {
        //maxHp = maxHpBase + maxHpCoeff;

        screwValueUp = baseScrewValue + (baseScrewValue / 100 * screwValueCoeff);
        screwPickUpDistance = baseScrewPickUpDistance + (baseScrewPickUpDistance / 100 * screwPickUpDistanceCoeff);

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void CheckLevel()
    {
        //if (currentExp >= levelExpNeed[(int)currentLevel - 1])
        //{
        //    currentLevel++;
        //    currentExp = 0;
        //    playerController.StartCoroutine(playerController.NewLevel());
        //    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().upgradeLevelCount++;
        //}
    }
}
