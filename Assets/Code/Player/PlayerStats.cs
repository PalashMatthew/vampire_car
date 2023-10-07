using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector]
    public float currentHp;
    public float maxHp;

    public float damage;

    public float kritChance;
    public float kritDamage;

    public float projectileCount;

    public float rageValue;
    public float rageCoeff;

    public float vampirizm;

    private void Awake()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
}
