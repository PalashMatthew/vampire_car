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

    //Level Cards
    public int MaxHpUp_Level;
    public int HealthRecovery_Level;
    public int Rage_Level;
    public int AttackSpeedUp_Level;
    public int DamageUp_Level;
    public int KritDamageUp_Level;
    public int ProjectileUp_Level;


    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _playerPassiveController = GameObject.Find("Player").GetComponent<PlayerPassiveController>();

        MaxHpUp_Level = 0;
        HealthRecovery_Level = 0;
        Rage_Level = 0;
        AttackSpeedUp_Level = 0;
        DamageUp_Level = 0;
        KritDamageUp_Level = 0;
        ProjectileUp_Level = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateUpgrades();
        }
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
            cardsController[i].Initialize();
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
        _playerController.maxHp += _playerController.maxHp / 100f * 10f;
        Debug.Log("MaxHpUp");
        MaxHpUp_Level++;
    }
    
    //Восстановление здоровья
    public void HealthRecovery_Passive()
    {
        _playerPassiveController.isPassiveHealthRecovery = true;
        Debug.Log("HealthRecovery");
        HealthRecovery_Level++;
    }

    public void Rage_Passive()
    {
        _playerPassiveController.isPassiveRage = true;
        Debug.Log("Rage");
        Rage_Level++;
    }

    public void AttackSpeedUp_Passive()
    {
        _playerStats.attackSpeed += _playerStats.attackSpeed / 100f * 10f;
        Debug.Log("AttackSpeedUp");
        AttackSpeedUp_Level++;
    }

    public void DamageUp_Passive()
    {
        Debug.Log("DamageUp");
        DamageUp_Level++;
    }

    public void KritDamageUp_Passive()
    {
        Debug.Log("Krit");
        KritDamageUp_Level++;
    }

    public void ProjectileUp_Passive()
    {
        Debug.Log("Projectile");
        ProjectileUp_Level++;
    }
    #endregion
    
}
