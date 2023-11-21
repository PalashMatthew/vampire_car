using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Newtonsoft.Json.Linq;

public class UpgradeCardController : MonoBehaviour
{
    public int cardNum;
    public UpgradeCard card;

    public TMP_Text tName;
    public TMP_Text tDescription;

    public int levelNum;
    public Image imgStar1, imgStar2, imgStar3;
    public Sprite sprStarFull, sprStarEmpty;

    public Image imgIcon;

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
        StopAllCoroutines();

        _upgradeController = GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>();
        _popUpUpgrade = GetComponentInParent<PopUpUpgrade>();

        #region Rarity
        if (card.cardRarity == UpgradeCard.CardRarity.Common)
        {
            cardRarity = CardRarity.Common;

            if (card.upgradeType == UpgradeCard.UpgradeType.Passive)
            {
                tDescription.text = card.descriptionCommon;
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

            imgCard.sprite = sprLegendaryCard;

            if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
            {
                imgStarPanel.gameObject.SetActive(true);
                imgStarPanel.sprite = sprLegendaryStarPanel;
            }
        }
        #endregion

        #region Stars
        //coroutine = StarAnim(imgStar1);

        //StopCoroutine(coroutine);        

        imgStar1.GetComponent<RectTransform>().DOScale(1f, 0f).SetUpdate(true);
        imgStar2.GetComponent<RectTransform>().DOScale(1f, 0f).SetUpdate(true);
        imgStar3.GetComponent<RectTransform>().DOScale(1f, 0f).SetUpdate(true);

        imgStar1.DOFade(1, 0).SetUpdate(true);
        imgStar2.DOFade(1, 0).SetUpdate(true);
        imgStar3.DOFade(1, 0).SetUpdate(true);

        switch (levelNum)
        {
            case 0:
                imgStar1.sprite = sprStarFull;
                imgStar2.sprite = sprStarEmpty;
                imgStar3.sprite = sprStarEmpty;

                StartCoroutine(StarAnim(imgStar1));
                break;

            case 1:
                imgStar1.sprite = sprStarFull;
                imgStar2.sprite = sprStarFull;
                imgStar3.sprite = sprStarEmpty;

                StartCoroutine(StarAnim(imgStar2));
                break;

            case 2:
                imgStar1.sprite = sprStarFull;
                imgStar2.sprite = sprStarFull;
                imgStar3.sprite = sprStarFull;

                StartCoroutine(StarAnim(imgStar3));
                break;
        }
        #endregion

        CardSettings();
    }

    public void ButAccept(string _type)
    {
        _upgradeController.CardAccept(gameObject.GetComponent<UpgradeCardController>(), _type);

        if (_type == "passive")
        {
            _popUpUpgrade.StartCoroutine(_popUpUpgrade.ChoicePassiveCard());
        }            

        if (_type == "gun")
        {
            _popUpUpgrade.StartCoroutine(_popUpUpgrade.ChoiceGunCard());
        }
    }

    public void ChoiceCard()
    {   
        if (card.upgradeType == UpgradeCard.UpgradeType.Passive)
        {
            _upgradeController.Upgrade_Passive(cardRarity.ToString());            
        }

        if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
        {
            if (!_upgradeController.activeGunCard.Contains(card))
            {
                _upgradeController.AddCardToSlot(card);
            }

            _upgradeController.Upgrade_Gun(cardRarity.ToString());
        }

        _upgradeController.UpdateTextLevels();
    }

    IEnumerator StarAnim(Image _imgStar)
    {
        _imgStar.GetComponent<RectTransform>().DOScale(1.2f, 0.5f).SetUpdate(true);
        _imgStar.DOFade(1, 0.5f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(0.5f);

        _imgStar.GetComponent<RectTransform>().DOScale(0.5f, 0.5f).SetUpdate(true);
        _imgStar.DOFade(0.5f, 0.5f).SetUpdate(true);

        yield return new WaitForSecondsRealtime(0.5f);

        StartCoroutine(StarAnim(_imgStar));
    }

    public void Reroll()
    {
        StopAllCoroutines();

        imgStar1.GetComponent<RectTransform>().DOScale(1f, 0f).SetUpdate(true);
        imgStar2.GetComponent<RectTransform>().DOScale(1f, 0f).SetUpdate(true);
        imgStar3.GetComponent<RectTransform>().DOScale(1f, 0f).SetUpdate(true);

        imgStar1.DOFade(1, 0).SetUpdate(true);
        imgStar2.DOFade(1, 0).SetUpdate(true);
        imgStar3.DOFade(1, 0).SetUpdate(true);
    }

    void CardSettings()
    {
        tName.text = card.cardName;
        imgCardType.sprite = sprCardTypeGun;

        if (card.imageItem != null)
            imgIcon.sprite = card.imageItem;

        if (card.upgradeType == UpgradeCard.UpgradeType.Passive)
        {
            imgStarPanel.gameObject.SetActive(false);

            string _desk;
            string _rarity = "";

            switch (cardRarity)
            {
                case CardRarity.Common:
                    _rarity = "Common";
                    break;
                case CardRarity.Rare:
                    _rarity = "Rare";
                    break;
                case CardRarity.Legendary:
                    _rarity = "Legendary";
                    break;
            }

            float upgValue = PlayerPrefs.GetFloat(card.cardName + "passiveUpgrade" + _rarity);            

            switch (card.upgradePassiveType)
            {
                case UpgradeCard.UpgradePassiveType.MaxHpUp:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.HealthRecovery:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.Rage:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.AttackSpeedUp:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.DamageUp:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.KritDamageUp:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.KritChanceUp:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.Vampirizm:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.BackDamage:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.Dodge:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.Armor:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.Punching:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.Headshot:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.ScrewValueUp:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.Magnet:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.Lucky:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.DistanceDamage:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.MassEnemyDamage:
                    tDescription.text = "+" + upgValue;
                    break;

                case UpgradeCard.UpgradePassiveType.EffectsDuration:
                    tDescription.text = "+" + upgValue;
                    break;
            }
        }

        if (card.upgradeType == UpgradeCard.UpgradeType.Gun)
        {
            imgStarPanel.gameObject.SetActive(true);

            #region Card Settings
            switch (card.upgradeGunType)
            {
                case UpgradeCard.UpgradeGunType.Boomerang:
                    levelNum = _upgradeController.Boomerang_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Bow:
                    levelNum = _upgradeController.Bow_Level;
                    break;
                case UpgradeCard.UpgradeGunType.DefaultGun:
                    levelNum = _upgradeController.DefaultGun_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Dron:
                    levelNum = _upgradeController.Dron_Level;
                    break;
                case UpgradeCard.UpgradeGunType.FanGun:
                    levelNum = _upgradeController.FanGun_Level;
                    break;
                case UpgradeCard.UpgradeGunType.GodGun:
                    levelNum = _upgradeController.GodGun_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Grenade:
                    levelNum = _upgradeController.Grenade_Level;
                    break;
                case UpgradeCard.UpgradeGunType.GrowingShotGun:
                    levelNum = _upgradeController.GrowingShot_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Ice:
                    levelNum = _upgradeController.Ice_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Lazer:
                    levelNum = _upgradeController.Lazer_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Mines:
                    levelNum = _upgradeController.Mines_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Partner:
                    levelNum = _upgradeController.Partner_Level;
                    break;
                case UpgradeCard.UpgradeGunType.PinPong:
                    levelNum = _upgradeController.PinPong_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Ricochet:
                    levelNum = _upgradeController.Ricochet_Level;
                    break;
                case UpgradeCard.UpgradeGunType.RocketLauncher:
                    levelNum = _upgradeController.RocketLauncher_Level;
                    break;
                case UpgradeCard.UpgradeGunType.Tornado:
                    levelNum = _upgradeController.Tornado_Level;
                    break;
            }

            if (levelNum > 0)
            {
                string _desk;
                string _rarity = "";

                switch (cardRarity)
                {
                    case CardRarity.Common:
                        _rarity = "common";
                        break;
                    case CardRarity.Rare:
                        _rarity = "rare";
                        break;
                    case CardRarity.Legendary:
                        _rarity = "legendary";
                        break;
                }

                float upgValue1 = PlayerPrefs.GetFloat(card.cardName + "lv" + levelNum + "_1_" + _rarity);
                float upgValue2 = PlayerPrefs.GetFloat(card.cardName + "lv" + levelNum + "_2_" + _rarity);

                if (upgValue2 != 0)
                {
                    _desk = CheckGunUpgradeType1(levelNum, _rarity) + upgValue1 + "; " + CheckGunUpgradeType2(levelNum, _rarity) + upgValue2;
                }
                else
                {
                    _desk = CheckGunUpgradeType1(levelNum, _rarity) + upgValue1;
                }

                tDescription.text = _desk;
            } 
            else
            {
                tDescription.text = "New Gun";
            }
            #endregion
        }
    }

    string CheckGunUpgradeType1(int currLevel, string _rarity)
    {
        if (currLevel == 1)
        {
            if (_rarity == "common")
            {
                switch (card.lv1UpgradeCommon1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "rare")
            {
                switch (card.lv1UpgradeRare1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + "  +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "legendary")
            {
                switch (card.lv1UpgradeLegendary1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + "  +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }
        }

        if (currLevel == 2)
        {
            if (_rarity == "common")
            {
                switch (card.lv2UpgradeCommon1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + "  +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "rare")
            {
                switch (card.lv2UpgradeRare1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "legendary")
            {
                switch (card.lv2UpgradeLegendary1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + "  +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }
        }

        if (currLevel == 3)
        {
            if (_rarity == "common")
            {
                switch (card.lv3UpgradeCommon1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "rare")
            {
                switch (card.lv3UpgradeRare1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + "  +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "legendary")
            {
                switch (card.lv3UpgradeLegendary1)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }
        }

        return "";
    }

    string CheckGunUpgradeType2(int currLevel, string _rarity)
    {
        if (currLevel == 1)
        {
            if (_rarity == "common")
            {
                switch (card.lv1UpgradeCommon2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "rare")
            {
                switch (card.lv1UpgradeRare2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "legendary")
            {
                switch (card.lv1UpgradeLegendary2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }
        }

        if (currLevel == 2)
        {
            if (_rarity == "common")
            {
                switch (card.lv2UpgradeCommon2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "rare")
            {
                switch (card.lv2UpgradeRare2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "legendary")
            {
                switch (card.lv2UpgradeLegendary2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }
        }

        if (currLevel == 3)
        {
            if (_rarity == "common")
            {
                switch (card.lv3UpgradeCommon2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "rare")
            {
                switch (card.lv3UpgradeRare2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }

            if (_rarity == "legendary")
            {
                switch (card.lv3UpgradeLegendary2)
                {
                    case UpgradeCard.LvUpgrade.Damage:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_damage") + " +";
                    case UpgradeCard.LvUpgrade.Projectile:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_projectile") + " +";
                    case UpgradeCard.LvUpgrade.ShotSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_speed") + " +";
                    case UpgradeCard.LvUpgrade.Area:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_area") + " +";
                    case UpgradeCard.LvUpgrade.Ricochet:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_ricochet") + "  +";
                    case UpgradeCard.LvUpgrade.TimeOfAction:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_duration") + " +";
                    case UpgradeCard.LvUpgrade.RotateSpeed:
                        return PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rotateSpeed") + " +";
                }
            }
        }

        return "";
    }
}
