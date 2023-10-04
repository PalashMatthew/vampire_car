using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public List<UpgradeCard> cards;
    public List<UpgradeCardController> cardsController;

    PlayerController _playerController;
    PlayerPassiveController _playerPassiveController;


    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerPassiveController = GameObject.Find("Player").GetComponent<PlayerPassiveController>();
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
        Debug.Log("MaxHpUp");
    }

    public void DamageUp_Passive()
    {

    }

    public void KritDamageUp_Passive()
    {

    }

    public void ProjectileUp_Passive()
    {

    }
    #endregion
}
