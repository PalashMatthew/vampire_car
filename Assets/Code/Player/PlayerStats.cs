using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
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


    private void Awake()
    {
        maxHpBase = PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carHealth");
        kritChance = PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carKritChance");
        dodgeProcent = PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carDodge");
        damageCoeff = PlayerPrefs.GetFloat(PlayerPrefs.GetString("selectedCarID") + "carDamage");

        maxHp = maxHpBase + (maxHpBase / 100 * maxHpCoeff);
        currentHp = maxHp;
    }

    private void Update()
    {
        maxHp = maxHpBase + (maxHpBase / 100 * maxHpCoeff);

        screwValueUp = baseScrewValue + (baseScrewValue / 100 * screwValueCoeff);
        screwPickUpDistance = baseScrewPickUpDistance + (baseScrewPickUpDistance / 100 * screwPickUpDistanceCoeff);

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
}
