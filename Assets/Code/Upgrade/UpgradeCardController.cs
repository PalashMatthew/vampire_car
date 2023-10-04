using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCardController : MonoBehaviour
{
    public UpgradeCard card;

    public TMP_Text tName;
    public TMP_Text tDescription;

    private int levelNum;
    public Image imgStar1, imgStar2, imgStar3;
    public Sprite sprStarFull, sprStarEmpty;

    UpgradeController _upgradeController;

    public void Initialize()
    {
        tName.text = card.cardName;
        tDescription.text = card.description;

        _upgradeController = GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>();
    }

    public void ChoiceCard()
    {
        if (card.upgradeType == UpgradeCard.UpgradeType.Passive)
        {
            switch (card.upgradePassiveType)
            {
                case UpgradeCard.UpgradePassiveType.MaxHpUp:
                    _upgradeController.MaxHpUp_Passive();
                    break;

                case UpgradeCard.UpgradePassiveType.HealthRecovery:
                    _upgradeController.HealthRecovery_Passive(); 
                    break;

                case UpgradeCard.UpgradePassiveType.Rage:
                    _upgradeController.Rage_Passive();
                    break;

                case UpgradeCard.UpgradePassiveType.AttackSpeedUp:
                    _upgradeController.AttackSpeedUp_Passive();
                    break;

                case UpgradeCard.UpgradePassiveType.DamageUp:
                    _upgradeController.DamageUp_Passive();
                    break;

                case UpgradeCard.UpgradePassiveType.KritDamageUp:
                    _upgradeController.KritDamageUp_Passive();
                    break;

                case UpgradeCard.UpgradePassiveType.ProjectileUp:
                    _upgradeController.ProjectileUp_Passive();
                    break;
            }
        }
    }
}
