using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Detail", menuName = "DetailCard")]
public class DetailCard : ScriptableObject
{
    [Header("Base")]
    public int itemID;

    public enum ItemType
    {
        Gun,
        Engine,
        Brakes,
        FuelSystem,
        Suspension,
        Transmission
    }
    public ItemType itemType;

    public Sprite sprItem;
    public string itemName;

    public enum ItemCharacters
    {
        none,
        HpUp,
        DamageUp
    }

    [Header("Base Characteristics Common 1")]
    public ItemCharacters baseItemCharactersCommon1;

    public float baseItemCharactersCommon1Value;
    public float baseItemCharactersCommon1StepValue;

    [Header("Base Characteristics Common 2")]
    public ItemCharacters baseItemCharactersCommon2;

    public float baseItemCharactersCommon2Value;
    public float baseItemCharactersCommon2StepValue;

    [Header("Base Characteristics Rare 1")]
    public ItemCharacters baseItemCharactersRare1;

    public float baseItemCharactersRare1Value;
    public float baseItemCharactersRare1StepValue;

    [Header("Base Characteristics Rare 2")]
    public ItemCharacters baseItemCharactersRare2;

    public float baseItemCharactersRare2Value;
    public float baseItemCharactersRare2StepValue;

    [Header("Base Characteristics Epic 1")]
    public ItemCharacters baseItemCharactersEpic1;

    public float baseItemCharactersEpic1Value;
    public float baseItemCharactersEpic1StepValue;

    [Header("Base Characteristics Epic 2")]
    public ItemCharacters baseItemCharactersEpic2;

    public float baseItemCharactersEpic2Value;
    public float baseItemCharactersEpic2StepValue;

    [Header("Base Characteristics Legendary 1")]
    public ItemCharacters baseItemCharactersLegendary1;

    public float baseItemCharactersLegendary1Value;
    public float baseItemCharactersLegendary1StepValue;

    [Header("Base Characteristics Legendary 2")]
    public ItemCharacters baseItemCharactersLegendary2;

    public float baseItemCharactersLegendary2Value;
    public float baseItemCharactersLegendary2StepValue;

    public enum RarityItemCharacters
    {
        none,
        HpUp,
        RecoveryHpInFirstAidKit,
        Dodge,
        DronDamage,
        ShotSpeed,
        KritDamage,
        KritChance,
        BackDamage,
        Vampirizm,
        Armor,
        HealthRecovery,
        Rage,
        DistanceDamage,
        Lucky,
        Magnet,
        Damage,
        CarDamage
    }
    [Header("Rare")]
    public RarityItemCharacters rareItemCharacters;
    public float rareItemCharactersValue;

    [Header("Epic")]
    public RarityItemCharacters epicItemCharacters;
    public float epicItemCharactersValue;

    [Header("Legendary")]
    public RarityItemCharacters legendaryItemCharacters;
    public float legendaryItemCharactersValue;
}
