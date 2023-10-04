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

    public float attackSpeed;

    public float projectileCount;

    public float rageValue;
    public float rageCoeff;

    private void Awake()
    {
        currentHp = maxHp;
    }
}
