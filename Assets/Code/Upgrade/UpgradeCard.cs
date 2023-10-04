using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "UpgradeCard")]
public class UpgradeCard : ScriptableObject
{
    public string cardName;
    public string description;

    public Sprite imageItem;

    public enum UpgradeType
    {
        Passive,
        Gun
    }
    public UpgradeType upgradeType;

    public enum UpgradePassiveType
    {
        MaxHpUp,
        HealthRecovery,
        Rage,
        AttackSpeedUp,
        DamageUp,
        KritDamageUp,
        ProjectileUp
    }
    public UpgradePassiveType upgradePassiveType;
}
