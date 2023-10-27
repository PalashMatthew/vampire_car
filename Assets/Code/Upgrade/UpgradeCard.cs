using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "UpgradeCard")]
public class UpgradeCard : ScriptableObject
{
    public enum LvUpgrade
    {
        none,
        Damage,
        Projectile,
        ShotSpeed,
        Area,
        Ricochet,
        TimeOfAction,
        RotateSpeed
    }

    [Header("Base")]
    public string cardName;
    public string descriptionCommon;
    public string descriptionRare;
    public string descriptionLegendary;

    [Header("Level 1 Common")]
    public LvUpgrade lv1UpgradeCommon1;
    public LvUpgrade lv1UpgradeCommon2;

    [Header("Level 1 Rare")]
    public LvUpgrade lv1UpgradeRare1;
    public LvUpgrade lv1UpgradeRare2;

    [Header("Level 1 Legendary")]
    public LvUpgrade lv1UpgradeLegendary1;
    public LvUpgrade lv1UpgradeLegendary2;

    [Header("Level 2 Common")]
    public LvUpgrade lv2UpgradeCommon1;
    public LvUpgrade lv2UpgradeCommon2;

    [Header("Level 2 Rare")]
    public LvUpgrade lv2UpgradeRare1;
    public LvUpgrade lv2UpgradeRare2;

    [Header("Level 2 Legendary")]
    public LvUpgrade lv2UpgradeLegendary1;
    public LvUpgrade lv2UpgradeLegendary2;

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
        KritChanceUp,
        BackDamage,
        DistanceDamage,
        Dodge,
        Armor,
        Punching,
        MassEnemyDamage,
        Headshot,
        ScrewValueUp,
        Magnet,
        Lucky,
        EffectsDuration
    }
    public UpgradePassiveType upgradePassiveType;

    public enum UpgradeGunType
    {
        none,
        RocketLauncher,
        Partner,
        Lazer,
        Lightning,
        Boomerang,
        Dron,
        Ice,
        DefaultGun,
        GrowingShotGun,
        FanGun,
        Tornado,
        Mines,
        Grenade,
        GodGun,
        Ricochet,
        Bow,
        PinPong
    }
    public UpgradeGunType upgradeGunType;
}
