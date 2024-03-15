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
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_health");
                break;

            case ItemCharacters.RecoveryHpInFirstAidKit:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_recoveryHpInFirstAidKit");
                break;

            case ItemCharacters.Dodge:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_dodge");
                break;

            case ItemCharacters.DronDamage:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_dronDamage");
                break;

            case ItemCharacters.ShotSpeed:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_shotSpeed");
                break;

            case ItemCharacters.KritDamage:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_kritDamage");
                break;

            case ItemCharacters.KritChance:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_kritChance");
                break;

            case ItemCharacters.BackDamage:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_backDamage");
                break;

            case ItemCharacters.Vampirizm:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_vampirizm");
                break;

            case ItemCharacters.Armor:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_armor");
                break;

            case ItemCharacters.HealthRecovery:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_recoveryHP");
                break;

            case ItemCharacters.Rage:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rage");
                break;

            case ItemCharacters.DistanceDamage:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_distanceDamage");
                break;

            case ItemCharacters.Lucky:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_lucky");
                break;

            case ItemCharacters.Magnet:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_magnet");
                break;

            case ItemCharacters.Damage:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage");
                break;

            case ItemCharacters.CarDamage:
                _stats = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_carDamage");
                break;
        }

        if (itemCharacteristic == ItemCharacters.HpUp)
        {
            _stats += ": " + itemCharacteristicValue;
        }
        else
        {
            _stats += ": " + itemCharacteristicValue + "%";
        }            

        if (itemCharacteristicStepValue != 0)
        {
            if (itemCharacteristic == ItemCharacters.HpUp)
            {
                _stats += " (<color=green>+" + itemCharacteristicStepValue + "</color>)";
            } 
            else
            {
                _stats += " (<color=green>+" + itemCharacteristicStepValue + "%</color>)";
            }
            
        }

        tStats.text = _stats;

        string _rarity = "";
        switch (itemRarity)
        {
            case ItemRarity.Common:
                _rarity = "(<color=#808B96>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_common") + "</color>)";
                break;
            case ItemRarity.Rare:
                _rarity = "(<color=#3498DB>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rare")  + "</color>)";
                break;
            case ItemRarity.Epic:
                _rarity = "(<color=#CE33FF>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_epic") + "</color>)";
                break;
            case ItemRarity.Legendary:
                _rarity = "(<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_legendary") + "</color>)";
                break;
        }
        tRarity.text = _rarity;

        if (isUnlock)
            objLock.SetActive(false);
        else
            objLock.SetActive(true);
    }
}
