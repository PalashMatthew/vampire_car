using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelCharacteristics : MonoBehaviour
{
    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    public ItemRarity itemRarity;

    public enum ItemCharacters
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
    public ItemCharacters itemCharacteristic;
    public float itemCharacteristicValue;
    public float itemCharacteristicStepValue;

    public bool isUnlock;

    public TMP_Text tStats;
    public TMP_Text tRarity;
    public GameObject objLock;

    public void Initialize()
    {
        transform.localScale = Vector3.one;

        string _stats = "";

        switch (itemCharacteristic)
        {
            case ItemCharacters.HpUp:
                _stats = "Здоровье";
                break;

            case ItemCharacters.RecoveryHpInFirstAidKit:
                _stats = "Здоровье в аптечке";
                break;

            case ItemCharacters.Dodge:
                _stats = "Уклонение";
                break;

            case ItemCharacters.DronDamage:
                _stats = "Урон по дронам";
                break;

            case ItemCharacters.ShotSpeed:
                _stats = "Скорость стрельбы";
                break;

            case ItemCharacters.KritDamage:
                _stats = "Крит. урон";
                break;

            case ItemCharacters.KritChance:
                _stats = "Крит. шанс";
                break;

            case ItemCharacters.BackDamage:
                _stats = "Обратный урон";
                break;

            case ItemCharacters.Vampirizm:
                _stats = "Вампиризм";
                break;

            case ItemCharacters.Armor:
                _stats = "Броня";
                break;

            case ItemCharacters.HealthRecovery:
                _stats = "Восстановление здоровья";
                break;

            case ItemCharacters.Rage:
                _stats = "Ярость";
                break;

            case ItemCharacters.DistanceDamage:
                _stats = "Урон от расстояния";
                break;

            case ItemCharacters.Lucky:
                _stats = "Удача";
                break;

            case ItemCharacters.Magnet:
                _stats = "Магнит";
                break;

            case ItemCharacters.Damage:
                _stats = "Урон";
                break;

            case ItemCharacters.CarDamage:
                _stats = "Урон по машинам";
                break;
        }

        _stats += ": " + itemCharacteristicValue;

        if (itemCharacteristicStepValue != 0)
        {
            _stats += " (<color=green>+" + itemCharacteristicStepValue + "</color>)";
        }

        tStats.text = _stats;

        string _rarity = "";
        switch (itemRarity)
        {
            case ItemRarity.Common:
                _rarity = "(<color=#808B96>Обычное</color>)";
                break;
            case ItemRarity.Rare:
                _rarity = "(<color=#3498DB>Редкое</color>)";
                break;
            case ItemRarity.Epic:
                _rarity = "(<color=#CE33FF>Эпическое</color>)";
                break;
            case ItemRarity.Legendary:
                _rarity = "(<color=yellow>Легендарное</color>)";
                break;
        }
        tRarity.text = _rarity;

        if (isUnlock)
            objLock.SetActive(false);
        else
            objLock.SetActive(true);
    }
}
