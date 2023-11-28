using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public int upgradeLevelCount;

    public List<UpgradeCard> cardsGun;
    public List<UpgradeCard> cardsPassive;
    public List<UpgradeCardController> cardsGunController;
    public List<UpgradeCardController> cardsPassiveController;

    public UpgradeCardController cardGunAccept;  //Выбранная оружейная карта
    public UpgradeCardController cardPassiveAccept;  //Выбранная пассивная карта

    public List<UpgradeCardController> cardsChoose;

    PlayerController _playerController;
    PlayerPassiveController _playerPassiveController;
    PlayerStats _playerStats;
    PlayerGuns _playerGuns;
    WaveController _waveController;

    [Header("Slots")]
    public List<UpgradeCard> activeGunCard;
    public List<Image> slotImage;
    public List<TMP_Text> tSlotLevels;
    private bool isSlotFull;

    [Header("Rarity Chance")]
    [SerializeField] public List<RarityChance> rarityList;
    public float commonChance;
    public float rareChance;
    public float legendaryChance;
    public float luckyChance = 1;

    //Level Cards Gun
    [Header("Level Cards Guns")]
    public int maxLevelGunUpgrade;
    public int Boomerang_Level;
    public int Dron_Level;
    public int Ice_Level;
    public int Lazer_Level;
    public int Lightning_Level;
    public int Partner_Level;
    public int RocketLauncher_Level;
    public int DefaultGun_Level;
    public int GrowingShot_Level;
    public int FanGun_Level;
    public int Tornado_Level;
    public int Mines_Level;
    public int Grenade_Level;
    public int GodGun_Level;
    public int Ricochet_Level;
    public int Bow_Level;
    public int PinPong_Level;

    private int generalMaxGunUpgradeCount;  //Сколько оружий прокачено на максимум

    [Header("Reroll")]
    public float rerollBasePrice;
    public List<float> rerollPrice;

    [Header("New Upgrade System")]
    public List<int> gunUpgradeChanceInwave;


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

        #region Добавляем оружие в слот если на старте его взяли
        foreach (UpgradeCard _card in cardsGun)
        {
            switch (_card.upgradeGunType)
            {
                case UpgradeCard.UpgradeGunType.Boomerang:
                    if (Boomerang_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Bow:
                    if (Bow_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.DefaultGun:
                    if (DefaultGun_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Dron:
                    if (Dron_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.FanGun:
                    if (FanGun_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.GodGun:
                    if (GodGun_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Grenade:
                    if (Grenade_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.GrowingShotGun:
                    if (GrowingShot_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Ice:
                    if (Ice_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Lazer:
                    if (Lazer_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Mines:
                    if (Mines_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Partner:
                    if (Partner_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.PinPong:
                    if (PinPong_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Ricochet:
                    if (Ricochet_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.RocketLauncher:
                    if (RocketLauncher_Level > 0)
                        AddCardToSlot(_card);
                    break;
                case UpgradeCard.UpgradeGunType.Tornado:
                    if (Tornado_Level > 0)
                        AddCardToSlot(_card);
                    break;
            }            
        }        
        #endregion

        generalMaxGunUpgradeCount = 0;

        UpdateTextLevels();
    }

    public void GenerateGunCards()
    {
        List<UpgradeCard> _createdCards = new List<UpgradeCard>();

        int cardCount = 3;

        if (generalMaxGunUpgradeCount == 4)
            cardCount = 2;

        if (generalMaxGunUpgradeCount == 5)
            cardCount = 1;

        if (generalMaxGunUpgradeCount == 6)
        {
            GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().ButClosed();
            _waveController.StartWave();
            return;
        }

        GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().ActivateCardObj(cardCount);

        for (int i = 0; i < cardCount; i++)
        {
            bool isGenerateUpgrade;
            int findCardCount = 0;

            if (MaxUpgradeGunCheck())
            {
                int _chance = Random.Range(0, 101);
                if (_chance <= gunUpgradeChanceInwave[_waveController.currentWave - 1])
                {
                    isGenerateUpgrade = true;
                } 
                else
                {
                    isGenerateUpgrade = false;
                }
            } 
            else
            {
                isGenerateUpgrade = false;
            }

            newTry:
            UpgradeCard _card;
            _card = cardsGun[Random.Range(0, cardsGun.Count)];

            if (isGenerateUpgrade && i != 2)
            {
                if (!activeGunCard.Contains(_card))
                {
                    findCardCount++;

                    if (findCardCount < 20)
                        goto newTry;
                }
            }

            if (!_createdCards.Contains(_card))
            {
                switch (_card.upgradeGunType)
                {
                    case UpgradeCard.UpgradeGunType.Boomerang:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Boomerang)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Boomerang_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Boomerang_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Dron:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Dron)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Dron_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Dron_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Ice:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Ice)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Ice_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Ice_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Lazer:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Lazer)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Lazer_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Lazer_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Lightning:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Lightning)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Lightning_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Lightning_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Partner:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Partner)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Partner_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Partner_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.RocketLauncher:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.RocketLauncher)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (RocketLauncher_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = RocketLauncher_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.DefaultGun:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.DefaultGun)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (DefaultGun_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = DefaultGun_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.GrowingShotGun:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.GrowingShotGun)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (GrowingShot_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = GrowingShot_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.FanGun:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.FanGun)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (FanGun_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = FanGun_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Tornado:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Tornado)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Tornado_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Tornado_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Mines:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Mines)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Mines_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Mines_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Grenade:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Grenade)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Grenade_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Grenade_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.GodGun:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.GodGun)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (GodGun_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = GodGun_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Ricochet:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Ricochet)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Ricochet_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Ricochet_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.Bow:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.Bow)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (Bow_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = Bow_Level;
                        }
                        else goto newTry;
                        break;

                    case UpgradeCard.UpgradeGunType.PinPong:
                        if (isSlotFull)
                        {
                            bool isContains = false;

                            foreach (UpgradeCard card in activeGunCard)
                            {
                                if (card.upgradeGunType == UpgradeCard.UpgradeGunType.PinPong)
                                {
                                    isContains = true;
                                }
                            }

                            if (!isContains)
                            {
                                goto newTry;
                            }
                        }

                        if (PinPong_Level < maxLevelGunUpgrade)
                        {
                            cardsGunController[i].levelNum = PinPong_Level;
                        }
                        else goto newTry;
                        break;
                }
            }
            else
            {
                goto newTry;
            }

            #region Выбираем редкость карты         
            if (cardsGunController[i].levelNum == 0)
            {
                _card.cardRarity = UpgradeCard.CardRarity.Common;
            }
            else
            {
                List<string> _rarity = new List<string>();

                for (int r = 0; r < rarityList[_waveController.currentWave - 1].commonChance; r++)
                {
                    _rarity.Add("common");
                }

                for (int r = 0; r < rarityList[_waveController.currentWave - 1].rareChance; r++)
                {
                    _rarity.Add("rare");
                }

                float _legendaryChance = rarityList[_waveController.currentWave - 1].legendaryChance * luckyChance;

                for (int r = 0; r < _legendaryChance; r++)
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
            }
            #endregion

            _createdCards.Add(_card);
            cardsGunController[i].card = _card;
        }
    }

    public void GeneratePassiveCards()
    {
        List<UpgradeCard> _createdCards = new List<UpgradeCard>();

        for (int i = 0; i < 3; i++)
        {
            newTry:
            UpgradeCard _card;
            _card = cardsPassive[Random.Range(0, cardsPassive.Count)];

            if (_createdCards.Contains(_card))
            {
                goto newTry;
            }

            #region Выбираем редкость карты            
            List<string> _rarity = new List<string>();

            for (int r = 0; r < rarityList[_waveController.currentWave - 1].commonChance; r++)
            {
                _rarity.Add("common");
            }

            for (int r = 0; r < rarityList[_waveController.currentWave - 1].rareChance; r++)
            {
                _rarity.Add("rare");
            }

            float _legendaryChance = rarityList[_waveController.currentWave - 1].legendaryChance * luckyChance;

            for (int r = 0; r < _legendaryChance; r++)
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
            cardsPassiveController[i].card = _card;
        }
    }

    public void CardAccept(UpgradeCardController _card, string _cardType)  //Если нажати на карту, она становится выбранной
    {
        if (_cardType == "gun")
        {
            foreach (UpgradeCardController obj in cardsGunController)
            {
                if (obj != _card)
                {
                    obj.gameObject.transform.DOScale(0f, 0.2f).SetUpdate(true);
                } 
                else
                {
                    obj.gameObject.transform.DOScale(1.05f, 0.2f).SetUpdate(true);
                    obj.gameObject.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f).SetUpdate(true);

                    cardGunAccept = obj;
                }
            }
        }

        if (_cardType == "passive")
        {
            foreach (UpgradeCardController obj in cardsPassiveController)
            {
                if (obj != _card)
                {
                    obj.gameObject.transform.DOScale(0f, 0.2f).SetUpdate(true);
                }
                else
                {
                    obj.gameObject.transform.DOScale(1.05f, 0.2f).SetUpdate(true);
                    obj.gameObject.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f).SetUpdate(true);

                    cardPassiveAccept = obj;
                }
            }
        }
    }

    public void Reroll(string _type)
    {
        //GenerateUpgrades();

        if (_type == "gun")
        {
            GenerateGunCards();
        }

        if (_type == "passive")
        {
            GeneratePassiveCards();
        }
    }

    bool MaxUpgradeGunCheck()
    {
        int levelSum = 0;

        foreach (UpgradeCard upgCard in activeGunCard)
        {
            switch (upgCard.upgradeGunType)
            {
                case UpgradeCard.UpgradeGunType.Boomerang:
                    levelSum += Boomerang_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Dron:
                    levelSum += Dron_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Ice:
                    levelSum += Ice_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Lazer:
                    levelSum += Lazer_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Lightning:
                    levelSum += Lightning_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Partner:
                    levelSum += Partner_Level;
                    break;

                case UpgradeCard.UpgradeGunType.RocketLauncher:
                    levelSum += RocketLauncher_Level;
                    break;

                case UpgradeCard.UpgradeGunType.DefaultGun:
                    levelSum += DefaultGun_Level;
                    break;

                case UpgradeCard.UpgradeGunType.GrowingShotGun:
                    levelSum += GrowingShot_Level;
                    break;

                case UpgradeCard.UpgradeGunType.FanGun:
                    levelSum += FanGun_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Tornado:
                    levelSum += Tornado_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Mines:
                    levelSum += Mines_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Grenade:
                    levelSum += Grenade_Level;
                    break;

                case UpgradeCard.UpgradeGunType.GodGun:
                    levelSum += GodGun_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Ricochet:
                    levelSum += Ricochet_Level;
                    break;

                case UpgradeCard.UpgradeGunType.Bow:
                    levelSum += Bow_Level;
                    break;

                case UpgradeCard.UpgradeGunType.PinPong:
                    levelSum += PinPong_Level;
                    break;
            }
        }

        if (levelSum < maxLevelGunUpgrade * activeGunCard.Count)
        {
            //Debug.Log(levelSum + " - " + maxLevelGunUpgrade * activeGunCard.Count + " GOOD");
            return true;
        } 
        else
        {
            return false;
        }
    }

    #region Slots
    public void SlotInitialize()
    {
        for (int i = 0; i < slotImage.Count; i++)
        {
            slotImage[i].DOFade(0f, 0f).SetUpdate(true);
        }

        if (activeGunCard != null)
        {
            for (int i = 0; i < activeGunCard.Count; i++)
            {
                slotImage[i].sprite = activeGunCard[i].imageItem;
                slotImage[i].DOFade(1f, 0f).SetUpdate(true);
            }
        }
    }

    public void AddCardToSlot(UpgradeCard _card)
    {
        activeGunCard.Add(_card);

        for (int i = 0; i < activeGunCard.Count; i++)
        {
            slotImage[i].sprite = activeGunCard[i].imageItem;
            slotImage[i].DOFade(1f, 0f).SetUpdate(true);
        }

        if (activeGunCard.Count >= 6)
            isSlotFull = true;

        GameObject.Find("PopUp Pause").GetComponent<PopUpPause>().AddCardToSlot();
    }

    public void UpdateTextLevels()
    {
        foreach (TMP_Text _t in tSlotLevels)
        {
            _t.text = "";
        }

        if (activeGunCard != null)
        {
            for (int i = 0; i < activeGunCard.Count; i++)
            {
                switch (activeGunCard[i].upgradeGunType)
                {
                    case UpgradeCard.UpgradeGunType.DefaultGun:
                        if (DefaultGun_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = DefaultGun_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.RocketLauncher:
                        if (RocketLauncher_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = RocketLauncher_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Lightning:
                        if (Lightning_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Lightning_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Boomerang:
                        if (Boomerang_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Boomerang_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Partner:
                        if (Partner_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Partner_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Dron:
                        if (Dron_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Dron_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Ice:
                        if (Ice_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Ice_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Lazer:
                        if (Lazer_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Lazer_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Bow:
                        if (Bow_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Bow_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.FanGun:
                        if (FanGun_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = FanGun_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.GodGun:
                        if (GodGun_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = GodGun_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Grenade:
                        if (Grenade_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Grenade_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.GrowingShotGun:
                        if (GrowingShot_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = GrowingShot_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Mines:
                        if (Mines_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Mines_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.PinPong:
                        if (PinPong_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = PinPong_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Ricochet:
                        if (Ricochet_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Ricochet_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;

                    case UpgradeCard.UpgradeGunType.Tornado:
                        if (Tornado_Level < maxLevelGunUpgrade)
                            tSlotLevels[i].text = Tornado_Level + "/" + maxLevelGunUpgrade;
                        else tSlotLevels[i].text = "MAX";
                        break;
                }
            }
        }
    }
    #endregion

    public void Upgrade_Passive(string _rarity)
    {
        float _value;

        _value = PlayerPrefs.GetFloat(cardPassiveAccept.card.cardName + "passiveUpgrade" + _rarity);

        switch (cardPassiveAccept.card.upgradePassiveType)
        {
            case UpgradeCard.UpgradePassiveType.MaxHpUp:
                float _hp = _playerStats.maxHpBase + _playerStats.maxHpCoeff;
                float regen = _hp / 100 * _value;
                _playerStats.maxHp += regen;
                _playerStats.currentHp += regen;
                break;

            case UpgradeCard.UpgradePassiveType.HealthRecovery:
                _playerPassiveController.healthRecoveryProcent += _value;

                _playerPassiveController.isPassiveHealthRecovery = true;
                break;

            case UpgradeCard.UpgradePassiveType.Rage:    
                if (_playerPassiveController.isPassiveRage)
                {
                    _playerStats.rageCoeff += _value;

                    _playerStats.rageValue = 1 + (1 / 100 * _playerStats.rageCoeff);
                }

                if (!_playerPassiveController.isPassiveRage)
                {
                    _playerPassiveController.isPassiveRage = true;

                    _playerStats.rageCoeff += _value;

                    _playerStats.rageValue = 1 + (1 / 100 * _playerStats.rageCoeff);
                }
                break;

            case UpgradeCard.UpgradePassiveType.AttackSpeedUp:
                foreach (Gun _gun in _playerGuns.guns)
                {
                    _gun.shotSpeedCoeffPassive = _value;
                }
                break;

            case UpgradeCard.UpgradePassiveType.DamageUp:
                foreach (Gun _gun in _playerGuns.guns)
                {
                    _gun.damageCoeffPassive = _value;
                }
                break;

            case UpgradeCard.UpgradePassiveType.KritDamageUp:
                _playerStats.kritDamage += _value;
                break;

            case UpgradeCard.UpgradePassiveType.KritChanceUp:
                _playerStats.kritChance += _value;
                break;

            case UpgradeCard.UpgradePassiveType.Vampirizm:
                _playerStats.vampirizm += _value;
                _playerPassiveController.isVampirizm = true;
                break;

            case UpgradeCard.UpgradePassiveType.BackDamage:
                _playerStats.backDamageProcent += _value;
                _playerPassiveController.isBackDamage = true;
                break;

            case UpgradeCard.UpgradePassiveType.Dodge:
                _playerStats.dodgeProcent += _value;
                _playerPassiveController.isDodge = true;
                break;

            case UpgradeCard.UpgradePassiveType.Armor:
                _playerStats.armorProcent += _value;
                break;

            case UpgradeCard.UpgradePassiveType.Headshot:
                _playerStats.headshotProcent += _value;
                _playerPassiveController.isHeadshot = true;
                break;

            case UpgradeCard.UpgradePassiveType.Lucky:
                luckyChance *= luckyChance / 100 * _value;
                break;

            case UpgradeCard.UpgradePassiveType.DistanceDamage:
                _playerStats.distanceDamageCoeff += _value;
                _playerPassiveController.isDistanceDamage = true;
                break;

            case UpgradeCard.UpgradePassiveType.MassEnemyDamage:
                _playerStats.massEnemyDamage += _value;
                break;

            case UpgradeCard.UpgradePassiveType.EffectsDuration:
                _playerStats.effectDuration += _value;
                break;
        }

        foreach (Gun _gun in _playerGuns.guns)
        {
            _gun.CalculateStats();
        }
    }

    public void Upgrade_Gun(string _rarity)
    {       
        if (cardGunAccept.levelNum == 0)
        {
            switch (cardGunAccept.card.upgradeGunType)
            {
                case UpgradeCard.UpgradeGunType.Boomerang:
                    _playerGuns._isBoomerang = true;
                    break;
                case UpgradeCard.UpgradeGunType.Bow:
                    _playerGuns._isBow = true;
                    break;
                case UpgradeCard.UpgradeGunType.DefaultGun:
                    _playerGuns._isDefaultGun = true;
                    break;
                case UpgradeCard.UpgradeGunType.Dron:
                    _playerGuns._isDron = true;
                    break;
                case UpgradeCard.UpgradeGunType.FanGun:
                    _playerGuns._isFanGun = true;
                    break;
                case UpgradeCard.UpgradeGunType.GodGun:
                    _playerGuns._isGodGun = true;
                    break;
                case UpgradeCard.UpgradeGunType.Grenade:
                    _playerGuns._isGrenade = true;
                    break;
                case UpgradeCard.UpgradeGunType.GrowingShotGun:
                    _playerGuns._isGrowingShot = true;
                    break;
                case UpgradeCard.UpgradeGunType.Ice:
                    _playerGuns._isIce = true;
                    break;
                case UpgradeCard.UpgradeGunType.Lazer:
                    _playerGuns._isLazer = true;
                    break;
                case UpgradeCard.UpgradeGunType.Mines:
                    _playerGuns._isMines = true;
                    break;
                case UpgradeCard.UpgradeGunType.Partner:
                    _playerGuns._isPartner = true;
                    break;
                case UpgradeCard.UpgradeGunType.PinPong:
                    _playerGuns._isPinPong = true;
                    break;
                case UpgradeCard.UpgradeGunType.Ricochet:
                    _playerGuns._isRicochet = true;
                    break;
                case UpgradeCard.UpgradeGunType.RocketLauncher:
                    _playerGuns._isRocketLauncher = true;
                    break;
                case UpgradeCard.UpgradeGunType.Tornado:
                    _playerGuns._isTornado = true;
                    break;
            }

            _playerGuns.GunActivate();
        } 
        else
        {
            if (_rarity == "Common") _rarity = "common";
            if (_rarity == "Rare") _rarity = "rare";
            if (_rarity == "Legendary") _rarity = "legendary";

            string _type1 = "";
            string _type2 = "";

            Gun gunObj = null;

            foreach (Gun gm in _playerGuns.guns)
            {
                if (gm.gameObject.name == cardGunAccept.card.cardName)
                {
                    gunObj = gm;
                }
            }

            #region Определяем какие именно параметры улучшаем
            if (cardGunAccept.levelNum == 1)
            {
                if (_rarity == "common")
                {
                    switch (cardGunAccept.card.lv1UpgradeCommon1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv1UpgradeCommon2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }

                if (_rarity == "rare")
                {
                    switch (cardGunAccept.card.lv1UpgradeRare1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv1UpgradeRare2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }

                if (_rarity == "legendary")
                {
                    switch (cardGunAccept.card.lv1UpgradeLegendary1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv1UpgradeLegendary2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }
            }

            if (cardGunAccept.levelNum == 2)
            {
                if (_rarity == "common")
                {
                    switch (cardGunAccept.card.lv2UpgradeCommon1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv2UpgradeCommon2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }

                if (_rarity == "rare")
                {
                    switch (cardGunAccept.card.lv2UpgradeRare1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv2UpgradeRare2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }

                if (_rarity == "legendary")
                {
                    switch (cardGunAccept.card.lv2UpgradeLegendary1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv2UpgradeLegendary2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }
            }

            if (cardGunAccept.levelNum == 3)
            {
                if (_rarity == "common")
                {
                    switch (cardGunAccept.card.lv3UpgradeCommon1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv3UpgradeCommon2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }

                if (_rarity == "rare")
                {
                    switch (cardGunAccept.card.lv3UpgradeRare1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv3UpgradeRare2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }

                if (_rarity == "legendary")
                {
                    switch (cardGunAccept.card.lv3UpgradeLegendary1)
                    {
                        case UpgradeCard.LvUpgrade.Damage:
                            _type1 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type1 = "Projectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type1 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type1 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type1 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type1 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type1 = "RotateSpeed";
                            break;
                    }

                    switch (cardGunAccept.card.lv3UpgradeLegendary2)
                    {
                        case UpgradeCard.LvUpgrade.none:
                            _type2 = "none";
                            break;
                        case UpgradeCard.LvUpgrade.Damage:
                            _type2 = "Damage";
                            break;
                        case UpgradeCard.LvUpgrade.Projectile:
                            _type2 = "ProjectileProjectile";
                            break;
                        case UpgradeCard.LvUpgrade.ShotSpeed:
                            _type2 = "ShotSpeed";
                            break;
                        case UpgradeCard.LvUpgrade.Area:
                            _type2 = "Area";
                            break;
                        case UpgradeCard.LvUpgrade.Ricochet:
                            _type2 = "Ricochet";
                            break;
                        case UpgradeCard.LvUpgrade.TimeOfAction:
                            _type2 = "TimeOfAction";
                            break;
                        case UpgradeCard.LvUpgrade.RotateSpeed:
                            _type2 = "RotateSpeed";
                            break;
                    }
                }
            }
            #endregion

            #region Type 1
            if (_type1 == "Damage")
            {
                gunObj.damageCoeffPassive = PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_1_" + _rarity);
            }

            if (_type1 == "Projectile")
            {
                gunObj.projectileValue += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_1_" + _rarity);
            }

            if (_type1 == "ShotSpeed")
            {
                gunObj.shotSpeedCoeffPassive = PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_1_" + _rarity);
            }

            if (_type1 == "Area")
            {
                gunObj.areaCoeff += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_1_" + _rarity);
            }

            if (_type1 == "Ricochet")
            {
                gunObj.ricochetCount += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_1_" + _rarity);
            }

            if (_type1 == "TimeOfAction")
            {
                gunObj.timeOfActionCoeff += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_1_" + _rarity);
            }

            if (_type1 == "RotateSpeed")
            {
                gunObj.rotateSpeedCoeff += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_1_" + _rarity);
            }
            #endregion

            #region Type 2
            if (_type2 == "Damage")
            {
                gunObj.damageCoeff += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_2_" + _rarity);
            }

            if (_type2 == "Projectile")
            {
                gunObj.projectileValue += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_2_" + _rarity);
            }

            if (_type2 == "ShotSpeed")
            {
                gunObj.shotSpeedCoeff += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_2_" + _rarity);
            }

            if (_type2 == "Area")
            {
                gunObj.areaCoeff += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_2_" + _rarity);
            }

            if (_type2 == "Ricochet")
            {
                gunObj.ricochetCount += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_2_" + _rarity);
            }

            if (_type2 == "TimeOfAction")
            {
                gunObj.timeOfActionCoeff += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_2_" + _rarity);
            }

            if (_type2 == "RotateSpeed")
            {
                gunObj.rotateSpeedCoeff += PlayerPrefs.GetFloat(cardGunAccept.card.cardName + "lv" + cardGunAccept.levelNum + "_2_" + _rarity);
            }
            #endregion

            gunObj.CalculateStats();
        }

        switch (cardGunAccept.card.upgradeGunType)
        {
            case UpgradeCard.UpgradeGunType.Boomerang:
                Boomerang_Level++;

                if (Boomerang_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Bow:
                Bow_Level++;

                if (Bow_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.DefaultGun:
                DefaultGun_Level++;

                if (DefaultGun_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Dron:
                Dron_Level++;

                if (Dron_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.FanGun:
                FanGun_Level++;

                if (FanGun_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.GodGun:
                GodGun_Level++;

                if (GodGun_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Grenade:
                Grenade_Level++;

                if (Grenade_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.GrowingShotGun:
                GrowingShot_Level++;

                if (GrowingShot_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Ice:
                Ice_Level++;

                if (Ice_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Lazer:
                Lazer_Level++;

                if (Lazer_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Mines:
                Mines_Level++;

                if (Mines_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Partner:
                Partner_Level++;

                if (Partner_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.PinPong:
                PinPong_Level++;

                if (PinPong_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Ricochet:
                Ricochet_Level++;

                if (Ricochet_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.RocketLauncher:
                RocketLauncher_Level++;

                if (RocketLauncher_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
            case UpgradeCard.UpgradeGunType.Tornado:
                Tornado_Level++;

                if (Tornado_Level == maxLevelGunUpgrade)
                    generalMaxGunUpgradeCount++;
                break;
        }
    }
}

[System.Serializable]
public class RarityChance
{
    [Header("Rarity Chance")]
    public float commonChance;
    public float rareChance;
    public float legendaryChance;
}
