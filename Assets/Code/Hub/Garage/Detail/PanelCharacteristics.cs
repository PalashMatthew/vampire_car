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
                _stats = "��������";
                break;

            case ItemCharacters.RecoveryHpInFirstAidKit:
                _stats = "�������� � �������";
                break;

            case ItemCharacters.Dodge:
                _stats = "���������";
                break;

            case ItemCharacters.DronDamage:
                _stats = "���� �� ������";
                break;

            case ItemCharacters.ShotSpeed:
                _stats = "�������� ��������";
                break;

            case ItemCharacters.KritDamage:
                _stats = "����. ����";
                break;

            case ItemCharacters.KritChance:
                _stats = "����. ����";
                break;

            case ItemCharacters.BackDamage:
                _stats = "�������� ����";
                break;

            case ItemCharacters.Vampirizm:
                _stats = "���������";
                break;

            case ItemCharacters.Armor:
                _stats = "�����";
                break;

            case ItemCharacters.HealthRecovery:
                _stats = "�������������� ��������";
                break;

            case ItemCharacters.Rage:
                _stats = "������";
                break;

            case ItemCharacters.DistanceDamage:
                _stats = "���� �� ����������";
                break;

            case ItemCharacters.Lucky:
                _stats = "�����";
                break;

            case ItemCharacters.Magnet:
                _stats = "������";
                break;

            case ItemCharacters.Damage:
                _stats = "����";
                break;

            case ItemCharacters.CarDamage:
                _stats = "���� �� �������";
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
                _rarity = "(<color=#808B96>�������</color>)";
                break;
            case ItemRarity.Rare:
                _rarity = "(<color=#3498DB>������</color>)";
                break;
            case ItemRarity.Epic:
                _rarity = "(<color=#CE33FF>���������</color>)";
                break;
            case ItemRarity.Legendary:
                _rarity = "(<color=yellow>�����������</color>)";
                break;
        }
        tRarity.text = _rarity;

        if (isUnlock)
            objLock.SetActive(false);
        else
            objLock.SetActive(true);
    }
}
