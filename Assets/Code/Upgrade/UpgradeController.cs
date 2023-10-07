using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public List<UpgradeCard> cards;
    public List<UpgradeCardController> cardsController;

    PlayerController _playerController;
    PlayerPassiveController _playerPassiveController;
    PlayerStats _playerStats;
    PlayerGuns _playerGuns;
    WaveController _waveController;

    [Header("Rarity Chance")]
    [SerializeField] public List<RarityChance> rarityList;
    public float commonChance;
    public float rareChance;
    public float legendaryChance;

    //Level Cards Gun
    [Header("Level Cards Guns")]
    public int Boomerang_Level;
    public int Dron_Level;
    public int Ice_Level;
    public int Lazer_Level;
    public int Lightning_Level;
    public int Oil_Level;
    public int Partner_Level;
    public int RocketLauncher_Level;
    public int DefaultGun_Level;


    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _playerPassiveController = GameObject.Find("Player").GetComponent<PlayerPassiveController>();
        _playerGuns = GameObject.Find("Player").GetComponent<PlayerGuns>();
        _waveController = GameObject.Find("GameplayController").GetComponent<WaveController>();
    }

    public void GenerateUpgrades()
    {
        List<UpgradeCard> _createdCards = new List<UpgradeCard>();

        int _generateGunNum = Random.Range(0, 3);
        int _generatePassiveNum = 0;

        while (_generatePassiveNum == _generateGunNum)
        {
            _generatePassiveNum = Random.Range(0, 3);
        }

        for (int i = 0; i < 3; i++)
        {
            newTry:
            UpgradeCard _card;
            _card = cards[Random.Range(0, cards.Count)];

            //ѕровер€ем, чтобы заспавнилось как минимум одно оружие
            if (i == _generateGunNum)
            {
                if (_card.upgradeType != UpgradeCard.UpgradeType.Gun)
                    goto newTry;
            }

            //ѕровер€ем, чтобы заспавнилось как минимум одна пассивка
            if (i == _generatePassiveNum)
            {
                if (_card.upgradeType != UpgradeCard.UpgradeType.Passive)
                    goto newTry;
            }

            if (!_createdCards.Contains(_card))
            {
                if (_card.upgradeType == UpgradeCard.UpgradeType.Gun)
                {
                    switch (_card.upgradeGunType)
                    {
                        case UpgradeCard.UpgradeGunType.Boomerang:
                            if (Boomerang_Level < 3)
                            {
                                cardsController[i].levelNum = Boomerang_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradeGunType.Dron:
                            if (Dron_Level < 3)
                            {
                                cardsController[i].levelNum = Dron_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradeGunType.Ice:
                            if (Ice_Level < 3)
                            {
                                cardsController[i].levelNum = Ice_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradeGunType.Lazer:
                            if (Lazer_Level < 3)
                            {
                                cardsController[i].levelNum = Lazer_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradeGunType.Lightning:
                            if (Lightning_Level < 3)
                            {
                                cardsController[i].levelNum = Lightning_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradeGunType.Oil:
                            if (Oil_Level < 3)
                            {
                                cardsController[i].levelNum = Oil_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradeGunType.Partner:
                            if (Partner_Level < 3)
                            {
                                cardsController[i].levelNum = Partner_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradeGunType.RocketLauncher:
                            if (RocketLauncher_Level < 3)
                            {
                                cardsController[i].levelNum = RocketLauncher_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradeGunType.DefaultGun:
                            if (DefaultGun_Level < 3)
                            {
                                cardsController[i].levelNum = DefaultGun_Level;
                            }
                            else goto newTry;
                            break;
                    }
                }
            } else
            {
                goto newTry;
            }

            #region ¬ыбираем редкость карты            
            List<string> _rarity = new List<string>();

            for (int r = 0; r < rarityList[_waveController.currentWave-1].commonChance; r++)
            {
                _rarity.Add("common");
            }

            for (int r = 0; r < rarityList[_waveController.currentWave - 1].rareChance; r++)
            {
                _rarity.Add("rare");
            }

            for (int r = 0; r < rarityList[_waveController.currentWave - 1].legendaryChance; r++)
            {
                _rarity.Add("legendary");
            }

            int _rand = Random.Range(0, _rarity.Count);

            switch (_rarity[_rand])
            {
                case "common":
                    _card.cardRarity = UpgradeCard.CardRarity.Common;
                    break;

                case "rare":
                    _card.cardRarity = UpgradeCard.CardRarity.Rare;
                    break;

                case "legendary":
                    _card.cardRarity = UpgradeCard.CardRarity.Legendary;
                    break;
            }
            #endregion

            _createdCards.Add(_card);
            cardsController[i].card = _card;
        }
    }

    public void Reroll()
    {
        GenerateUpgrades();
    }

    #region Passive Upgrades
    // ”величение максимального HP
    public void MaxHpUp_Passive(string _rarity)
    {
        float _procent = 1;        

        switch (_rarity)
        {
            case "Common":
                _procent = 1.1f;
                break;

            case "Rare":
                _procent = 1.2f;
                break;

            case "Legendary":
                _procent = 1.3f;
                break;
        }

        _playerStats.maxHp *= _procent;
        _playerStats.currentHp *= _procent;
    }
    
    //¬осстановление здоровь€
    public void HealthRecovery_Passive(string _rarity)
    {
        float _procent = 1;

        switch (_rarity)
        {
            case "Common":
                _procent = 1.01f;
                break;

            case "Rare":
                _procent = 1.02f;
                break;

            case "Legendary":
                _procent = 1.03f;
                break;
        }

        _playerPassiveController.isPassiveHealthRecovery = true;
        _playerPassiveController.healthRecoveryProcent = _procent;
    }

    public void Rage_Passive(string _rarity)
    {
        float _procent = 1;

        switch (_rarity)
        {
            case "Common":
                _procent = 2f;
                break;

            case "Rare":
                _procent = 2.2f;
                break;

            case "Legendary":
                _procent = 2.4f;
                break;
        }

        _playerPassiveController.isPassiveRage = true;
        _playerStats.rageCoeff = _procent;
    }

    public void AttackSpeedUp_Passive(string _rarity)
    {
        float _procent = 1;

        switch (_rarity)
        {
            case "Common":
                _procent = 0.9f;
                break;

            case "Rare":
                _procent = 0.8f;
                break;

            case "Legendary":
                _procent = 0.7f;
                break;
        }

        foreach (Gun _gun in _playerGuns.guns)
        {
            _gun.shotSpeed *= _procent;
        }
    }

    public void DamageUp_Passive(string _rarity)
    {
        float _procent = 1;

        switch (_rarity)
        {
            case "Common":
                _procent = 1.1f;
                break;

            case "Rare":
                _procent = 1.2f;
                break;

            case "Legendary":
                _procent = 1.3f;
                break;
        }

        foreach (Gun _gun in _playerGuns.guns)
        {
            _gun.damage *= _procent;
        }

        _playerStats.damage *= _procent;
    }

    public void KritDamageUp_Passive(string _rarity)
    {
        float _procent = 1;

        switch (_rarity)
        {
            case "Common":
                _procent = 1.1f;
                break;

            case "Rare":
                _procent = 1.2f;
                break;

            case "Legendary":
                _procent = 1.3f;
                break;
        }

        _playerStats.kritDamage *= _procent;
    }

    public void ProjectileUp_Passive(string _rarity)
    {
        _playerStats.projectileCount++;
    }

    public void KritChanceUp_Passive(string _rarity)
    {
        float _procent = 1;

        switch (_rarity)
        {
            case "Common":
                _procent = 1.05f;
                break;

            case "Rare":
                _procent = 1.1f;
                break;

            case "Legendary":
                _procent = 1.15f;
                break;
        }

        _playerStats.kritChance *= _procent;
    }

    public void Vampirizm_Passive(string _rarity)
    {
        float _procent = 1;

        switch (_rarity)
        {
            case "Common":
                _procent = 1.05f;
                break;

            case "Rare":
                _procent = 1.1f;
                break;

            case "Legendary":
                _procent = 1.15f;
                break;
        }

        _playerStats.vampirizm *= _procent;
        _playerPassiveController.isVampirizm = true;
    }
    #endregion

    #region Guns Upgrades
    public void Boomerand_Gun(string _rarity)
    {
        if (Boomerang_Level == 0)
        {
            _playerGuns._isBoomerang = true;
            _playerGuns.GunActivate();

            switch (_rarity)
            {
                case "Common":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }            
        }

        if (Boomerang_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().projectileValue += 1;
                    break;

                case "Rare":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().projectileValue += 2;
                    break;

                case "Legendary":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().projectileValue += 2;
                    _playerGuns.BoomerangObj.GetComponent<Gun>().shotSpeed *= 1.1f;
                    break;
            }
        }

        if (Boomerang_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().shotSpeed *= 0.8f;
                    break;

                case "Rare":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().shotSpeed *= 0.7f;
                    break;

                case "Legendary":
                    _playerGuns.BoomerangObj.GetComponent<Gun>().shotSpeed *= 0.6f;
                    break;
            }
        }

        Boomerang_Level++;
    }

    public void Dron_Gun(string _rarity)
    {
        if (Dron_Level == 0)
        {
            _playerGuns._isDron = true;
            _playerGuns.GunActivate();

            switch (_rarity)
            {
                case "Common":
                    _playerGuns.DronObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.DronObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.DronObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }
        }

        if (Dron_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.DronObj.GetComponent<Gun>().projectileValue += 1;
                    break;

                case "Rare":
                    _playerGuns.DronObj.GetComponent<Gun>().projectileValue += 2;
                    break;

                case "Legendary":
                    _playerGuns.DronObj.GetComponent<Gun>().projectileValue += 2;
                    _playerGuns.DronObj.GetComponent<Gun>().bulletMoveSpeed *= 1.1f;
                    break;
            }
        }

        if (Dron_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.DronObj.GetComponent<Gun>().bulletMoveSpeed *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.DronObj.GetComponent<Gun>().bulletMoveSpeed *= 1.3f;
                    break;

                case "Legendary":
                    _playerGuns.DronObj.GetComponent<Gun>().bulletMoveSpeed *= 1.4f;
                    break;
            }
        }
        Dron_Level++;
    }

    public void Ice_Gun(string _rarity)
    {
        if (Ice_Level == 0)
        {
            _playerGuns._isIce = true;
            _playerGuns.GunActivate();

            switch (_rarity)
            {
                case "Common":
                    _playerGuns.IceObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.IceObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.IceObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }
        }

        if (Ice_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.IceObj.GetComponent<Gun>().projectileValue += 1;
                    break;

                case "Rare":
                    _playerGuns.IceObj.GetComponent<Gun>().projectileValue += 2;
                    break;

                case "Legendary":
                    _playerGuns.IceObj.GetComponent<Gun>().projectileValue += 2;
                    _playerGuns.IceObj.GetComponent<Gun>().freezeTime *= 1.1f;
                    break;
            }
        }

        if (Ice_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.IceObj.GetComponent<Gun>().freezeTime *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.IceObj.GetComponent<Gun>().freezeTime *= 1.3f;
                    break;

                case "Legendary":
                    _playerGuns.IceObj.GetComponent<Gun>().freezeTime *= 1.4f;
                    break;
            }
        }
        Ice_Level++;
    }

    public void Lazer_Gun(string _rarity)
    {
        if (Lazer_Level == 0)
        {
            _playerGuns._isLazer = true;
            _playerGuns.GunActivate();

            switch (_rarity)
            {
                case "Common":
                    _playerGuns.LazerObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.LazerObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.LazerObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }
        }

        if (Lazer_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.LazerObj.GetComponent<Gun>().areaValue *= 1.15f;
                    break;

                case "Rare":
                    _playerGuns.LazerObj.GetComponent<Gun>().areaValue *= 1.3f;
                    break;

                case "Legendary":
                    _playerGuns.LazerObj.GetComponent<Gun>().areaValue *= 1.45f;
                    break;
            }
        }

        if (Lazer_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.LazerObj.GetComponent<Gun>().timeOfAction *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.LazerObj.GetComponent<Gun>().timeOfAction *= 1.3f;
                    break;

                case "Legendary":
                    _playerGuns.LazerObj.GetComponent<Gun>().timeOfAction *= 1.4f;
                    break;
            }
        }

        Lazer_Level++;
    }

    public void Lightning_Gun(string _rarity)
    {
        if (Lightning_Level == 0)
        {
            _playerGuns._isLightning = true;
            _playerGuns.GunActivate();

            switch (_rarity)
            {
                case "Common":
                    _playerGuns.LightningObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.LightningObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.LightningObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }
        }

        if (Lightning_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.LightningObj.GetComponent<Gun>().projectileValue += 1;
                    break;

                case "Rare":
                    _playerGuns.LightningObj.GetComponent<Gun>().projectileValue += 2;
                    break;

                case "Legendary":
                    _playerGuns.LightningObj.GetComponent<Gun>().projectileValue += 2;
                    _playerGuns.LightningObj.GetComponent<Gun>().timeOfAction *= 1.1f;
                    break;
            }
        }

        if (Lightning_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.LightningObj.GetComponent<Gun>().timeOfAction *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.LightningObj.GetComponent<Gun>().timeOfAction *= 1.3f;
                    break;

                case "Legendary":
                    _playerGuns.LightningObj.GetComponent<Gun>().timeOfAction *= 1.4f;
                    break;
            }
        }

        Lightning_Level++;
    }

    public void Oil_Gun(string _rarity)
    {
        if (Oil_Level == 0)
        {
            _playerGuns._isOil = true;
            _playerGuns.GunActivate();

            switch (_rarity)
            {
                case "Common":
                    _playerGuns.OilObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.OilObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.OilObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }
        }

        if (Oil_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.OilObj.GetComponent<Gun>().projectileValue += 1f;
                    break;

                case "Rare":
                    _playerGuns.OilObj.GetComponent<Gun>().projectileValue += 2f;
                    break;

                case "Legendary":
                    _playerGuns.OilObj.GetComponent<Gun>().projectileValue += 2f;
                    _playerGuns.OilObj.GetComponent<Gun>().timeOfAction *= 1.1f;
                    break;
            }
        }

        if (Oil_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.OilObj.GetComponent<Gun>().timeOfAction *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.OilObj.GetComponent<Gun>().timeOfAction *= 1.3f;
                    break;

                case "Legendary":
                    _playerGuns.OilObj.GetComponent<Gun>().timeOfAction *= 1.4f;
                    break;
            }
        }

        Oil_Level++;
    }

    public void Partner_Gun(string _rarity)
    {
        if (Partner_Level == 0)
        {
            _playerGuns._isPartner = true;
            _playerGuns.GunActivate();

            switch (_rarity)
            {
                case "Common":
                    _playerGuns.PartnerObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.PartnerObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.PartnerObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }
        }

        if (Partner_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.PartnerObj.GetComponent<Gun>().projectileValue += 1;
                    break;

                case "Rare":
                    _playerGuns.PartnerObj.GetComponent<Gun>().projectileValue += 2;
                    break;

                case "Legendary":
                    _playerGuns.PartnerObj.GetComponent<Gun>().projectileValue += 2;
                    _playerGuns.PartnerObj.GetComponent<Gun>().shotSpeed *= 0.9f;
                    break;
            }
        }

        if (Partner_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.PartnerObj.GetComponent<Gun>().shotSpeed *= 0.8f;
                    break;

                case "Rare":
                    _playerGuns.PartnerObj.GetComponent<Gun>().shotSpeed *= 0.7f;
                    break;

                case "Legendary":
                    _playerGuns.PartnerObj.GetComponent<Gun>().shotSpeed *= 0.6f;
                    break;
            }
        }

        Partner_Level++;
    }

    public void RocketLauncher(string _rarity)
    {
        if (RocketLauncher_Level == 0)
        {
            _playerGuns._isRocketLauncher = true;
            _playerGuns.GunActivate();

            switch (_rarity)
            {
                case "Common":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }
        }

        if (RocketLauncher_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().projectileValue += 1;
                    break;

                case "Rare":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().projectileValue += 2;
                    break;

                case "Legendary":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().projectileValue += 2;
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().areaValue *= 1.1f;
                    break;
            }
        }

        if (RocketLauncher_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().areaValue *= 1.1f;
                    break;

                case "Rare":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().areaValue *= 1.2f;
                    break;

                case "Legendary":
                    _playerGuns.RocketLauncherObj.GetComponent<Gun>().areaValue *= 1.3f;
                    break;
            }
        }

        RocketLauncher_Level++;
    }

    public void DefaultGun_Gun(string _rarity)
    {
        if (DefaultGun_Level == 0)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().damage *= 1.2f;
                    break;

                case "Rare":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().damage *= 1.4f;
                    break;

                case "Legendary":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().damage *= 1.6f;
                    break;
            }
        }

        if (DefaultGun_Level == 1)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().projectileValue += 1;
                    break;

                case "Rare":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().projectileValue += 2;
                    break;

                case "Legendary":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().projectileValue += 2;
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().shotSpeed *= 0.8f;
                    break;
            }
        }

        if (DefaultGun_Level == 2)
        {
            switch (_rarity)
            {
                case "Common":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().shotSpeed *= 0.8f;
                    break;

                case "Rare":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().shotSpeed *= 0.7f;
                    break;

                case "Legendary":
                    _playerGuns.DefaultGunObj.GetComponent<Gun>().shotSpeed *= 0.6f;
                    break;
            }
        }

        DefaultGun_Level++;
    }
    #endregion
}

[System.Serializable]
public class RarityChance
{
    [Header("Rarity Chance")]
    public float commonChance;
    public float rareChance;
    public float legendaryChance;
}
