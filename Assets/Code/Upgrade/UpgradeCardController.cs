using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpgradeCardController : MonoBehaviour
{
    public UpgradeCard card;

    public TMP_Text tName;
    public TMP_Text tDescription;

    public int levelNum;
    public Image imgStar1, imgStar2, imgStar3;
    public Sprite sprStarFull, sprStarEmpty;

    UpgradeController _upgradeController;

    public void Initialize()
    {
        tName.text = card.cardName;
        tDescription.text = card.description;

        _upgradeController = GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>();

        #region Stars
        switch (levelNum)
        {
            case 0:
                imgStar1.sprite = sprStarFull;
                imgStar2.sprite = sprStarEmpty;
                imgStar3.sprite = sprStarEmpty;

                StartCoroutine(StarAnimation(imgStar1));
                break;

            case 1:
                imgStar1.sprite = sprStarFull;
                imgStar2.sprite = sprStarFull;
                imgStar3.sprite = sprStarEmpty;

                StartCoroutine(StarAnimation(imgStar2));
                break;

            case 2:
                imgStar1.sprite = sprStarFull;
                imgStar2.sprite = sprStarFull;
                imgStar3.sprite = sprStarFull;

                StartCoroutine(StarAnimation(imgStar3));
                break;
        }
        #endregion
    }

    IEnumerator StarAnimation(Image _imgStar)
    {
        _imgStar.GetComponent<RectTransform>().DOScale(1.2f, 0.4f);
        yield return new WaitForSeconds(0.4f);
        _imgStar.GetComponent<RectTransform>().DOScale(1f, 0.4f);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(StarAnimation(_imgStar));
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
