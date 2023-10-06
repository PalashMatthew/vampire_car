using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UpgradeCardController : MonoBehaviour
{
    public int cardNum;
    public UpgradeCard card;

    public TMP_Text tName;
    public TMP_Text tDescription;

    public int levelNum;
    public Image imgStar1, imgStar2, imgStar3;
    public Sprite sprStarFull, sprStarEmpty;

    UpgradeController _upgradeController;
    PopUpUpgrade _popUpUpgrade;

    public Image imgCard;
    public Sprite sprCommonCard, sprRareCard, sprLegendaryCard;
    public Image imgStarPanel;
    public Sprite sprCommonStarPanel, sprRareStarPanel, sprLegendaryStarPanel;

    public Image imgCardType;
    public Sprite sprCardTypePassive, sprCardTypeGun;

    public enum CardRarity
    {
        Common,
        Rare,
        Legendary
    }
    public CardRarity cardRarity;

    public void Initialize()
    {
        tName.text = card.cardName;        

        _upgradeController = GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>();
        _popUpUpgrade = GetComponentInParent<PopUpUpgrade>();

        if (card.upgradeType == UpgradeCard.UpgradeType.Passive)
        {
            imgStarPanel.gameObject.SetActive(false);
            imgCardType.sprite = sprCardTypePassive;
        }

        if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
        {
            imgStarPanel.gameObject.SetActive(true);
            imgCardType.sprite = sprCardTypeGun;
        }

        #region Rarity
        if (card.cardRarity == UpgradeCard.CardRarity.Common)
        {
            cardRarity = CardRarity.Common;

            if (card.upgradeType == UpgradeCard.UpgradeType.Passive)
            {
                tDescription.text = card.descriptionCommon;
            }

            if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
            {
                switch (levelNum)
                {
                    case 0:
                        tDescription.text = card.descriptionCommonLVL1;
                        break;

                    case 1:
                        tDescription.text = card.descriptionCommonLVL2;
                        break;

                    case 2:
                        tDescription.text = card.descriptionCommonLVL3;
                        break;
                }                
            }

            imgCard.sprite = sprCommonCard;

            if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
            {
                imgStarPanel.gameObject.SetActive(true);
                imgStarPanel.sprite = sprCommonStarPanel;
            }
        }

        if (card.cardRarity == UpgradeCard.CardRarity.Rare)
        {
            cardRarity = CardRarity.Rare;

            if (card.upgradeType == UpgradeCard.UpgradeType.Passive)
            {
                tDescription.text = card.descriptionRare;
            }

            if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
            {
                switch (levelNum)
                {
                    case 0:
                        tDescription.text = card.descriptionRareLVL1;
                        break;

                    case 1:
                        tDescription.text = card.descriptionRareLVL2;
                        break;

                    case 2:
                        tDescription.text = card.descriptionRareLVL3;
                        break;
                }
            }

            imgCard.sprite = sprRareCard;

            if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
            {
                imgStarPanel.gameObject.SetActive(true);
                imgStarPanel.sprite = sprRareStarPanel;
            }
        }

        if (card.cardRarity == UpgradeCard.CardRarity.Legendary)
        {
            cardRarity = CardRarity.Legendary;

            if (card.upgradeType == UpgradeCard.UpgradeType.Passive)
            {
                tDescription.text = card.descriptionLegendary;
            }

            if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
            {
                switch (levelNum)
                {
                    case 0:
                        tDescription.text = card.descriptionLegendaryLVL1;
                        break;

                    case 1:
                        tDescription.text = card.descriptionLegendaryLVL2;
                        break;

                    case 2:
                        tDescription.text = card.descriptionLegendaryLVL3;
                        break;
                }
            }

            imgCard.sprite = sprLegendaryCard;

            if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
            {
                imgStarPanel.gameObject.SetActive(true);
                imgStarPanel.sprite = sprLegendaryStarPanel;
            }
        }
        #endregion

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
                    _upgradeController.MaxHpUp_Passive(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradePassiveType.HealthRecovery:
                    _upgradeController.HealthRecovery_Passive(cardRarity.ToString()); 
                    break;

                case UpgradeCard.UpgradePassiveType.Rage:
                    _upgradeController.Rage_Passive(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradePassiveType.AttackSpeedUp:
                    _upgradeController.AttackSpeedUp_Passive(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradePassiveType.DamageUp:
                    _upgradeController.DamageUp_Passive(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradePassiveType.KritDamageUp:
                    _upgradeController.KritDamageUp_Passive(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradePassiveType.ProjectileUp:
                    _upgradeController.ProjectileUp_Passive(cardRarity.ToString());
                    break;
            }
        }

        if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
        {
            switch (card.upgradeGunType)
            {
                case UpgradeCard.UpgradeGunType.Boomerang:
                    _upgradeController.Boomerand_Gun(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradeGunType.Dron:
                    _upgradeController.Dron_Gun(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradeGunType.Ice:
                    _upgradeController.Ice_Gun(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradeGunType.Lazer:
                    _upgradeController.Lazer_Gun(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradeGunType.Lightning:
                    _upgradeController.Lightning_Gun(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradeGunType.Oil:
                    _upgradeController.Oil_Gun(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradeGunType.Partner:
                    _upgradeController.Partner_Gun(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradeGunType.RocketLauncher:
                    _upgradeController.RocketLauncher(cardRarity.ToString());
                    break;

                case UpgradeCard.UpgradeGunType.DefaultGun:
                    _upgradeController.DefaultGun_Gun(cardRarity.ToString());
                    break;
            }
        }

        _popUpUpgrade.ChoiceCard(cardNum);
    }
}
