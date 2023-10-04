using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public List<UpgradeCard> cards;
    public List<UpgradeCardController> cardsController;

    PlayerController _playerController;
    PlayerPassiveController _playerPassiveController;

    //Level Cards
    public int MaxHpUp_Level;
    public int HealthRecovery_Level;
    public int Rage_Level;
    public int AttackSpeedUp_Level;
    public int DamageUp_Level;
    public int KritDamageUp_level;
    public int ProjectileUp_Level;


    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerPassiveController = GameObject.Find("Player").GetComponent<PlayerPassiveController>();

        MaxHpUp_Level = 0;
        HealthRecovery_Level = 0;
        Rage_Level = 0;
        AttackSpeedUp_Level = 0;
        DamageUp_Level = 0;
        KritDamageUp_level = 0;
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
        cardsController[0].card = cards[Random.Range(0, cards.Count)];
        cardsController[0].Initialize();
    }

    #region Passive Upgrades
    // Увеличение максимального HP
    public void MaxHpUp_Passive()
    {
        //_playerController.maxHp += _playerController.maxHp / 100 * 10;
        Debug.Log("MaxHpUp");
    }
    
    //Восстановление здоровья
    public void HealthRecovery_Passive()
    {
        //_playerPassiveController.isPassiveHealthRecovery = true;
        Debug.Log("HealthRecovery");
    }

    public void Rage_Passive()
    {
        Debug.Log("Rage");
    }

    public void AttackSpeedUp_Passive()
    {
        Debug.Log("AttackSpeedUp");
    }

    public void DamageUp_Passive()
    {
        Debug.Log("DamageUp");
    }

    public void KritDamageUp_Passive()
    {
        Debug.Log("Krit");
    }

    public void ProjectileUp_Passive()
    {
        Debug.Log("Projectile");
    }
    #endregion
}
