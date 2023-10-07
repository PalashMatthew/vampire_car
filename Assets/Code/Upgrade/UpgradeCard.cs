using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "UpgradeCard")]
public class UpgradeCard : ScriptableObject
{
    [Header("Base")]
    public string cardName;
    public string descriptionCommon;
    public string descriptionRare;
    public string descriptionLegendary;

    [Header("Level 1 Description")]
    public string descriptionCommonLVL1;
    public string descriptionRareLVL1;
    public string descriptionLegendaryLVL1;

    [Header("Level 2 Description")]
    public string descriptionCommonLVL2;
    public string descriptionRareLVL2;
    public string descriptionLegendaryLVL2;

    [Header("Level 3 Description")]
    public string descriptionCommonLVL3;
    public string descriptionRareLVL3;
    public string descriptionLegendaryLVL3;

    [Header("Settings")]
    public Sprite imageItem;

    public enum UpgradeType
    {
        Passive,
        Gun
    }
    public UpgradeType upgradeType;

    public enum CardRarity
    {
        Common,
        Rare,
        Legendary
    }
    [HideInInspector]
    public CardRarity cardRarity;

    public enum UpgradePassiveType
    {
        none,
        MaxHpUp,
        HealthRecovery,
        Rage,
        AttackSpeedUp,
        DamageUp,
        KritDamageUp,
        Vampirizm,
        ProjectileUp,
        KritChanceUp
    }
    public UpgradePassiveType upgradePassiveType;

    public enum UpgradeGunType
    {
        none,
        RocketLauncher,
        Partner,
        Lazer,
        Lightning,
        Oil,
        Boomerang,
        Dron,
        Ice,
        DefaultGun
    }
    public UpgradeGunType upgradeGunType;
}
