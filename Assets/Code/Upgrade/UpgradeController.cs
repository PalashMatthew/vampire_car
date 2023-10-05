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

    //Level Cards Passive
    [Header("Level Cards Passive")]
    public int MaxHpUp_Level;
    public int HealthRecovery_Level;
    public int Rage_Level;
    public int AttackSpeedUp_Level;
    public int DamageUp_Level;
    public int KritDamageUp_Level;
    public int ProjectileUp_Level;

    [Header("Level Cards Guns")]
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

        MaxHpUp_Level = 0;
        HealthRecovery_Level = 0;
        Rage_Level = 0;
        AttackSpeedUp_Level = 0;
        DamageUp_Level = 0;
        KritDamageUp_Level = 0;
        ProjectileUp_Level = 0;
    }

    public void GenerateUpgrades()
    {
        List<UpgradeCard> _createdCards = new List<UpgradeCard>();

        for (int i = 0; i < 3; i++)
        {
            newTry:
            UpgradeCard _card;
            _card = cards[Random.Range(0, cards.Count)];

            if (!_createdCards.Contains(_card))
            {
                if (_card.upgradeType == UpgradeCard.UpgradeType.Passive)
                {
                    switch (_card.upgradePassiveType)
                    {
                        case UpgradeCard.UpgradePassiveType.MaxHpUp:
                            if (MaxHpUp_Level < 3)
                            {
                                cardsController[i].levelNum = MaxHpUp_Level;
                            }
                            else goto newTry;                            
                            break;

                        case UpgradeCard.UpgradePassiveType.HealthRecovery:
                            if (HealthRecovery_Level < 3)
                            {
                                cardsController[i].levelNum = HealthRecovery_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradePassiveType.Rage:
                            if (Rage_Level < 3)
                            {
                                cardsController[i].levelNum = Rage_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradePassiveType.AttackSpeedUp:
                            if (AttackSpeedUp_Level < 3)
                            {
                                cardsController[i].levelNum = AttackSpeedUp_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradePassiveType.DamageUp:
                            if (DamageUp_Level < 3)
                            {
                                cardsController[i].levelNum = DamageUp_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradePassiveType.KritDamageUp:
                            if (KritDamageUp_Level < 3)
                            {
                                cardsController[i].levelNum = KritDamageUp_Level;
                            }
                            else goto newTry;
                            break;

                        case UpgradeCard.UpgradePassiveType.ProjectileUp:
                            if (ProjectileUp_Level < 3)
                            {
                                cardsController[i].levelNum = ProjectileUp_Level;
                            }
                            else goto newTry;
                            break;
                    }
                }
            } else
            {
                goto newTry;
            }

            _createdCards.Add(_card);
            cardsController[i].card = _card;
        }
    }

    public void Reroll()
    {
        GenerateUpgrades();
    }

    #region Passive Upgrades
    // Увеличение максимального HP
    public void MaxHpUp_Passive()
    {
        float _newHp = _playerStats.maxHp / 100f * 10f;
        _playerStats.maxHp += _newHp;
        _playerStats.currentHp += _newHp;

        MaxHpUp_Level++;
    }
    
    //Восстановление здоровья
    public void HealthRecovery_Passive()
    {       
        _playerPassiveController.isPassiveHealthRecovery = true;

        HealthRecovery_Level++;
    }

    public void Rage_Passive()
    {
        _playerPassiveController.isPassiveRage = true;

        Rage_Level++;

        if (Rage_Level == 1)
            _playerStats.rageCoeff = 2;

        if (Rage_Level == 2)
            _playerStats.rageCoeff = 3;

        if (Rage_Level == 3)
            _playerStats.rageCoeff = 4;
    }

    public void AttackSpeedUp_Passive()
    {
        _playerStats.attackSpeedCoeff += 10;

        AttackSpeedUp_Level++;
    }

    public void DamageUp_Passive()
    {
        _playerStats.damage += _playerStats.damage / 100f * 10f;
        _playerGuns.GunDamageUpgrade(10f);

        DamageUp_Level++;
    }

    public void KritDamageUp_Passive()
    {
        _playerStats.kritDamage = _playerStats.kritDamage / 100 * 10;

        KritDamageUp_Level++;
    }

    public void ProjectileUp_Passive()
    {
        _playerStats.projectileCount++;

        ProjectileUp_Level++;
    }
    #endregion

    #region Guns Upgrades
    public void DefaultGun_Gun()
    {
        if (DefaultGun_Level == 0)
        {

        }


        float _newHp = _playerStats.maxHp / 100f * 10f;
        _playerStats.maxHp += _newHp;
        _playerStats.currentHp += _newHp;

        MaxHpUp_Level++;
    }
    #endregion
}
