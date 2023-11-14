using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpDetail : MonoBehaviour
{
    PopUpController _popUpController;
    public GarageController garageController;

    public bool isSlotItem;

    public TMP_Text tName;
    public TMP_Text tRarity;
    public Image imgBackIcon;
    public Image imgIcon;
    public TMP_Text tDescription;

    public GameObject butSelect;
    public GameObject butUnselect;

    [Header("Item Info")]
    public Sprite sprRarity;
    public Sprite itemIcon;
    public string itemName;
    public string itemRarity;

    public Sprite sprCommonCard;
    public Sprite sprRareCard;
    public Sprite sprEpicCard;
    public Sprite sprLegendaryCard;

    public GameObject panelDescription;
    public GameObject panelSet;

    [Header("Characteristics Panel")]
    public GameObject panelChar;
    public Transform panelCharParent;
    private List<GameObject> panelsInst = new List<GameObject>();

    private float itemCharacters1StepValue;
    private float itemCharacters2StepValue;

    [Header("Upgrade")]
    public TMP_Text tUpgradePrice;
    public TMP_Text tDrawing;  //Чертеж
    public List<int> upgradePrice;
    public List<int> drawingCount;
    public GameObject butUpgrade;
    public GameObject butUpgradeGray;

    [Header("Level")]
    public Image fillLevel;
    public Image fillEndLevel;
    public TMP_Text tCurrentLevel;
    int maxLevel;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        tName.text = itemName;

        switch (itemRarity)
        {
            case "common":
                imgBackIcon.sprite = sprCommonCard;
                tRarity.text = "<color=#808B96>Обычная</color>";

                maxLevel = 10;
                break;

            case "rare":
                imgBackIcon.sprite = sprRareCard;
                tRarity.text = "<color=#3498DB>Редкая</color>";

                maxLevel = 20;
                break;

            case "epic":
                imgBackIcon.sprite = sprEpicCard;
                tRarity.text = "<color=#CE33FF>Эпическая</color>";

                maxLevel = 30;
                break;

            case "legendary":
                imgBackIcon.sprite = sprLegendaryCard;
                tRarity.text = "<color=yellow>Легендарная</color>";

                maxLevel = 40;
                break;
        }

        imgIcon.sprite = itemIcon;

        if (isSlotItem)
        {
            butSelect.SetActive(false);
            butUnselect.SetActive(true);
        } 
        else
        {
            butSelect.SetActive(true);
            butUnselect.SetActive(false);
        }

        if (garageController.activeItem.itemObj.itemType == DetailCard.ItemType.Gun)
        {
            panelDescription.SetActive(true);
            panelSet.SetActive(false);
        } 
        else
        {
            panelDescription.SetActive(false);
            panelSet.SetActive(true);
        }

        CreateCharactersPanel();
        CalculateUpgradePrice();
        LevelUpdate();
    }

    public void CreateCharactersPanel()
    {
        if (panelsInst.Count > 0)
        {
            foreach (GameObject gm in panelsInst)
            {
                Destroy(gm);
            }

            panelsInst.Clear();
        }

        GameObject _panel;
        DetailCard _card;

        #region Base Common
        if (itemRarity == "common")
        {
            //Первая характеристика
            _panel = Instantiate(panelChar, transform.position, transform.rotation);
            _panel.transform.parent = panelCharParent;

            _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
            _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Common;

            _card = garageController.activeItem.itemObj;

            switch (_card.baseItemCharactersCommon1)
            {
                case DetailCard.ItemCharacters.HpUp:
                    _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                    break;

                case DetailCard.ItemCharacters.DamageUp:
                    _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                    break;
            }

            if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterCommon1Value" + garageController.activeItem.itemNumInInventory))
            {
                PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterCommon1Value" + garageController.activeItem.itemNumInInventory, _card.baseItemCharactersCommon1Value);
            }

            itemCharacters1StepValue = _card.baseItemCharactersCommon1StepValue;

            _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = PlayerPrefs.GetFloat("item" + _card.itemType.ToString() + "baseCharacterCommon1Value" + garageController.activeItem.itemNumInInventory);
            _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = itemCharacters1StepValue;
            panelsInst.Add(_panel);
            _panel.GetComponent<PanelCharacteristics>().Initialize();

            //Вторая характеристика
            if (_card.baseItemCharactersCommon2 != DetailCard.ItemCharacters.none)
            {
                _panel = Instantiate(panelChar, transform.position, transform.rotation);
                _panel.transform.parent = panelCharParent;

                _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
                _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Common;

                _card = garageController.activeItem.itemObj;

                switch (_card.baseItemCharactersCommon2)
                {
                    case DetailCard.ItemCharacters.HpUp:
                        _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                        break;

                    case DetailCard.ItemCharacters.DamageUp:
                        _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                        break;
                }

                if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterCommon2Value" + garageController.activeItem.itemNumInInventory))
                {
                    PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterCommon2Value" + garageController.activeItem.itemNumInInventory, _card.baseItemCharactersCommon2Value);
                }

                itemCharacters2StepValue = _card.baseItemCharactersCommon2StepValue;

                _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = PlayerPrefs.GetFloat("item" + _card.itemType.ToString() + "baseCharacterCommon2Value" + garageController.activeItem.itemNumInInventory);
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = itemCharacters2StepValue;
                panelsInst.Add(_panel);
                _panel.GetComponent<PanelCharacteristics>().Initialize();
            }
        }
        #endregion

        #region Base Rare
        if (itemRarity == "rare")
        {
            //Первая характеристика
            _panel = Instantiate(panelChar, transform.position, transform.rotation);
            _panel.transform.parent = panelCharParent;

            _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
            _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Common;

            _card = garageController.activeItem.itemObj;

            switch (_card.baseItemCharactersRare1)
            {
                case DetailCard.ItemCharacters.HpUp:
                    _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                    break;

                case DetailCard.ItemCharacters.DamageUp:
                    _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                    break;
            }

            if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterRare1Value" + garageController.activeItem.itemNumInInventory))
            {
                PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterRare1Value" + garageController.activeItem.itemNumInInventory, _card.baseItemCharactersRare1Value);
            }

            itemCharacters1StepValue = _card.baseItemCharactersRare1StepValue;

            _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = PlayerPrefs.GetFloat("item" + _card.itemType.ToString() + "baseCharacterRare1Value" + garageController.activeItem.itemNumInInventory);
            _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = itemCharacters1StepValue;
            panelsInst.Add(_panel);
            _panel.GetComponent<PanelCharacteristics>().Initialize();

            //Вторая характеристика
            if (_card.baseItemCharactersRare2 != DetailCard.ItemCharacters.none)
            {
                _panel = Instantiate(panelChar, transform.position, transform.rotation);
                _panel.transform.parent = panelCharParent;

                _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
                _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Common;

                _card = garageController.activeItem.itemObj;

                switch (_card.baseItemCharactersRare2)
                {
                    case DetailCard.ItemCharacters.HpUp:
                        _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                        break;

                    case DetailCard.ItemCharacters.DamageUp:
                        _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                        break;
                }

                if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterRare2Value" + garageController.activeItem.itemNumInInventory))
                {
                    PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterRare2Value" + garageController.activeItem.itemNumInInventory, _card.baseItemCharactersRare2Value);
                }

                itemCharacters2StepValue = _card.baseItemCharactersRare2StepValue;

                _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = PlayerPrefs.GetFloat("item" + _card.itemType.ToString() + "baseCharacterRare2Value" + garageController.activeItem.itemNumInInventory);
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = itemCharacters2StepValue;
                panelsInst.Add(_panel);
                _panel.GetComponent<PanelCharacteristics>().Initialize();
            }
        }
        #endregion

        #region Base Epic
        if (itemRarity == "epic")
        {
            //Первая характеристика
            _panel = Instantiate(panelChar, transform.position, transform.rotation);
            _panel.transform.parent = panelCharParent;

            _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
            _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Common;

            _card = garageController.activeItem.itemObj;

            switch (_card.baseItemCharactersEpic1)
            {
                case DetailCard.ItemCharacters.HpUp:
                    _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                    break;

                case DetailCard.ItemCharacters.DamageUp:
                    _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                    break;
            }

            if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterEpic1Value" + garageController.activeItem.itemNumInInventory))
            {
                PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterEpic1Value" + garageController.activeItem.itemNumInInventory, _card.baseItemCharactersEpic1Value);
            }

            itemCharacters1StepValue = _card.baseItemCharactersEpic1StepValue;

            _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = PlayerPrefs.GetFloat("item" + _card.itemType.ToString() + "baseCharacterEpic1Value" + garageController.activeItem.itemNumInInventory);
            _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = itemCharacters1StepValue;
            panelsInst.Add(_panel);
            _panel.GetComponent<PanelCharacteristics>().Initialize();

            //Вторая характеристика
            if (_card.baseItemCharactersEpic2 != DetailCard.ItemCharacters.none)
            {
                _panel = Instantiate(panelChar, transform.position, transform.rotation);
                _panel.transform.parent = panelCharParent;

                _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
                _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Common;

                _card = garageController.activeItem.itemObj;

                switch (_card.baseItemCharactersEpic2)
                {
                    case DetailCard.ItemCharacters.HpUp:
                        _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                        break;

                    case DetailCard.ItemCharacters.DamageUp:
                        _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                        break;
                }

                if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterEpic2Value" + garageController.activeItem.itemNumInInventory))
                {
                    PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterEpic2Value" + garageController.activeItem.itemNumInInventory, _card.baseItemCharactersEpic2Value);
                }

                itemCharacters2StepValue = _card.baseItemCharactersEpic2StepValue;

                _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = PlayerPrefs.GetFloat("item" + _card.itemType.ToString() + "baseCharacterEpic2Value" + garageController.activeItem.itemNumInInventory);
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = itemCharacters2StepValue;
                panelsInst.Add(_panel);
                _panel.GetComponent<PanelCharacteristics>().Initialize();
            }
        }
        #endregion

        #region Base Legendary
        if (itemRarity == "legendary")
        {
            //Первая характеристика
            _panel = Instantiate(panelChar, transform.position, transform.rotation);
            _panel.transform.parent = panelCharParent;

            _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
            _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Common;

            _card = garageController.activeItem.itemObj;

            switch (_card.baseItemCharactersLegendary1)
            {
                case DetailCard.ItemCharacters.HpUp:
                    _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                    break;

                case DetailCard.ItemCharacters.DamageUp:
                    _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                    break;
            }

            if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterLegendary1Value" + garageController.activeItem.itemNumInInventory))
            {
                PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterLegendary1Value" + garageController.activeItem.itemNumInInventory, _card.baseItemCharactersLegendary1Value);
            }

            itemCharacters1StepValue = _card.baseItemCharactersLegendary1StepValue;

            _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = PlayerPrefs.GetFloat("item" + _card.itemType.ToString() + "baseCharacterLegendary1Value" + garageController.activeItem.itemNumInInventory);
            _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = itemCharacters1StepValue;
            panelsInst.Add(_panel);
            _panel.GetComponent<PanelCharacteristics>().Initialize();

            //Вторая характеристика
            if (_card.baseItemCharactersLegendary2 != DetailCard.ItemCharacters.none)
            {
                _panel = Instantiate(panelChar, transform.position, transform.rotation);
                _panel.transform.parent = panelCharParent;

                _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
                _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Common;

                _card = garageController.activeItem.itemObj;

                switch (_card.baseItemCharactersLegendary2)
                {
                    case DetailCard.ItemCharacters.HpUp:
                        _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                        break;

                    case DetailCard.ItemCharacters.DamageUp:
                        _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                        break;
                }

                if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterLegendary2Value" + garageController.activeItem.itemNumInInventory))
                {
                    PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterLegendary2Value" + garageController.activeItem.itemNumInInventory, _card.baseItemCharactersLegendary2Value);
                }

                itemCharacters2StepValue = _card.baseItemCharactersLegendary2StepValue;

                _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = PlayerPrefs.GetFloat("item" + _card.itemType.ToString() + "baseCharacterLegendary2Value" + garageController.activeItem.itemNumInInventory);
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = itemCharacters2StepValue;
                panelsInst.Add(_panel);
                _panel.GetComponent<PanelCharacteristics>().Initialize();
            }
        }
        #endregion

        //Rare
        _panel = Instantiate(panelChar, transform.position, transform.rotation);
        _panel.transform.parent = panelCharParent;

        if (itemRarity == "rare" || itemRarity == "epic" || itemRarity == "legendary")
            _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
        else
            _panel.GetComponent<PanelCharacteristics>().isUnlock = false;

        _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Rare;

        _card = garageController.activeItem.itemObj;

        switch (_card.rareItemCharacters)
        {
            case DetailCard.RarityItemCharacters.HpUp:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                break;

            case DetailCard.RarityItemCharacters.RecoveryHpInFirstAidKit:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.RecoveryHpInFirstAidKit;
                break;

            case DetailCard.RarityItemCharacters.Dodge:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Dodge;
                break;

            case DetailCard.RarityItemCharacters.DronDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.DronDamage;
                break;

            case DetailCard.RarityItemCharacters.ShotSpeed:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.ShotSpeed;
                break;

            case DetailCard.RarityItemCharacters.KritDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.KritDamage;
                break;

            case DetailCard.RarityItemCharacters.KritChance:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.KritChance;
                break;

            case DetailCard.RarityItemCharacters.BackDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.BackDamage;
                break;

            case DetailCard.RarityItemCharacters.Vampirizm:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Vampirizm;
                break;

            case DetailCard.RarityItemCharacters.Armor:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Armor;
                break;

            case DetailCard.RarityItemCharacters.HealthRecovery:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HealthRecovery;
                break;

            case DetailCard.RarityItemCharacters.Rage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Rage;
                break;

            case DetailCard.RarityItemCharacters.DistanceDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.DistanceDamage;
                break;

            case DetailCard.RarityItemCharacters.Lucky:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Lucky;
                break;

            case DetailCard.RarityItemCharacters.Magnet:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Magnet;
                break;

            case DetailCard.RarityItemCharacters.Damage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                break;

            case DetailCard.RarityItemCharacters.CarDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.CarDamage;
                break;
        }

        _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = _card.rareItemCharactersValue;
        _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = 0;
        panelsInst.Add(_panel);
        _panel.GetComponent<PanelCharacteristics>().Initialize();

        //Epic
        _panel = Instantiate(panelChar, transform.position, transform.rotation);
        _panel.transform.parent = panelCharParent;

        if (itemRarity == "epic" || itemRarity == "legendary")
            _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
        else
            _panel.GetComponent<PanelCharacteristics>().isUnlock = false;

        _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Epic;

        _card = garageController.activeItem.itemObj;

        switch (_card.epicItemCharacters)
        {
            case DetailCard.RarityItemCharacters.HpUp:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                break;

            case DetailCard.RarityItemCharacters.RecoveryHpInFirstAidKit:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.RecoveryHpInFirstAidKit;
                break;

            case DetailCard.RarityItemCharacters.Dodge:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Dodge;
                break;

            case DetailCard.RarityItemCharacters.DronDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.DronDamage;
                break;

            case DetailCard.RarityItemCharacters.ShotSpeed:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.ShotSpeed;
                break;

            case DetailCard.RarityItemCharacters.KritDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.KritDamage;
                break;

            case DetailCard.RarityItemCharacters.KritChance:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.KritChance;
                break;

            case DetailCard.RarityItemCharacters.BackDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.BackDamage;
                break;

            case DetailCard.RarityItemCharacters.Vampirizm:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Vampirizm;
                break;

            case DetailCard.RarityItemCharacters.Armor:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Armor;
                break;

            case DetailCard.RarityItemCharacters.HealthRecovery:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HealthRecovery;
                break;

            case DetailCard.RarityItemCharacters.Rage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Rage;
                break;

            case DetailCard.RarityItemCharacters.DistanceDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.DistanceDamage;
                break;

            case DetailCard.RarityItemCharacters.Lucky:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Lucky;
                break;

            case DetailCard.RarityItemCharacters.Magnet:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Magnet;
                break;

            case DetailCard.RarityItemCharacters.Damage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                break;

            case DetailCard.RarityItemCharacters.CarDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.CarDamage;
                break;
        }

        _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = _card.epicItemCharactersValue;
        _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = 0;
        panelsInst.Add(_panel);
        _panel.GetComponent<PanelCharacteristics>().Initialize();

        //Legendary
        _panel = Instantiate(panelChar, transform.position, transform.rotation);
        _panel.transform.parent = panelCharParent;

        if (itemRarity == "legendary")
            _panel.GetComponent<PanelCharacteristics>().isUnlock = true;
        else
            _panel.GetComponent<PanelCharacteristics>().isUnlock = false;

        _panel.GetComponent<PanelCharacteristics>().itemRarity = PanelCharacteristics.ItemRarity.Legendary;

        _card = garageController.activeItem.itemObj;

        switch (_card.legendaryItemCharacters)
        {
            case DetailCard.RarityItemCharacters.HpUp:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HpUp;
                break;

            case DetailCard.RarityItemCharacters.RecoveryHpInFirstAidKit:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.RecoveryHpInFirstAidKit;
                break;

            case DetailCard.RarityItemCharacters.Dodge:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Dodge;
                break;

            case DetailCard.RarityItemCharacters.DronDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.DronDamage;
                break;

            case DetailCard.RarityItemCharacters.ShotSpeed:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.ShotSpeed;
                break;

            case DetailCard.RarityItemCharacters.KritDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.KritDamage;
                break;

            case DetailCard.RarityItemCharacters.KritChance:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.KritChance;
                break;

            case DetailCard.RarityItemCharacters.BackDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.BackDamage;
                break;

            case DetailCard.RarityItemCharacters.Vampirizm:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Vampirizm;
                break;

            case DetailCard.RarityItemCharacters.Armor:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Armor;
                break;

            case DetailCard.RarityItemCharacters.HealthRecovery:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.HealthRecovery;
                break;

            case DetailCard.RarityItemCharacters.Rage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Rage;
                break;

            case DetailCard.RarityItemCharacters.DistanceDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.DistanceDamage;
                break;

            case DetailCard.RarityItemCharacters.Lucky:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Lucky;
                break;

            case DetailCard.RarityItemCharacters.Magnet:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Magnet;
                break;

            case DetailCard.RarityItemCharacters.Damage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.Damage;
                break;

            case DetailCard.RarityItemCharacters.CarDamage:
                _panel.GetComponent<PanelCharacteristics>().itemCharacteristic = PanelCharacteristics.ItemCharacters.CarDamage;
                break;
        }

        _panel.GetComponent<PanelCharacteristics>().itemCharacteristicValue = _card.legendaryItemCharactersValue;
        _panel.GetComponent<PanelCharacteristics>().itemCharacteristicStepValue = 0;
        panelsInst.Add(_panel);
        _panel.GetComponent<PanelCharacteristics>().Initialize();
    }

    void CalculateUpgradePrice()
    {
        string _type = "";

        switch (garageController.activeItem.itemObj.itemType)
        {
            case DetailCard.ItemType.Gun:
                _type = "Gun";
                break;
            case DetailCard.ItemType.Engine:
                _type = "Engine";
                break;
            case DetailCard.ItemType.Brakes:
                _type = "Brakes";
                break;
            case DetailCard.ItemType.FuelSystem:
                _type = "FuelSystem";
                break;
            case DetailCard.ItemType.Suspension:
                _type = "Suspension";
                break;
            case DetailCard.ItemType.Transmission:
                _type = "Transmission";
                break;
        }

        if (PlayerPrefs.GetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory) < maxLevel)
        {
            butUpgrade.SetActive(true);
            butUpgradeGray.SetActive(false);

            switch (garageController.activeItem.itemObj.itemType)
            {
                case DetailCard.ItemType.Gun:
                    tDrawing.text = "Чертеж оружия: " + PlayerPrefs.GetInt("drawingGunCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.Engine:
                    tDrawing.text = "Чертеж двигателя: " + PlayerPrefs.GetInt("drawingEngineCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.Brakes:
                    tDrawing.text = "Чертеж тормозов: " + PlayerPrefs.GetInt("drawingBrakesCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.FuelSystem:
                    tDrawing.text = "Чертеж топливной системы: " + PlayerPrefs.GetInt("drawingFuelSystemCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.Suspension:
                    tDrawing.text = "Чертеж подвески: " + PlayerPrefs.GetInt("drawingSuspensionCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.Transmission:
                    tDrawing.text = "Чертеж трансмиссии: " + PlayerPrefs.GetInt("drawingTransmissionCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
            }

            tUpgradePrice.text = upgradePrice[garageController.activeItem.currentLevel] + "";
        } 
        else
        {
            butUpgrade.SetActive(false);
            butUpgradeGray.SetActive(true);

            switch (garageController.activeItem.itemObj.itemType)
            {
                case DetailCard.ItemType.Gun:
                    tDrawing.text = "Чертеж оружия: " + PlayerPrefs.GetInt("drawingGunCount") + "/MAX";
                    break;
                case DetailCard.ItemType.Engine:
                    tDrawing.text = "Чертеж двигателя: " + PlayerPrefs.GetInt("drawingEngineCount") + "/MAX";
                    break;
                case DetailCard.ItemType.Brakes:
                    tDrawing.text = "Чертеж тормозов: " + PlayerPrefs.GetInt("drawingBrakesCount") + "/MAX";
                    break;
                case DetailCard.ItemType.FuelSystem:
                    tDrawing.text = "Чертеж топливной системы: " + PlayerPrefs.GetInt("drawingFuelSystemCount") + "/MAX";
                    break;
                case DetailCard.ItemType.Suspension:
                    tDrawing.text = "Чертеж подвески: " + PlayerPrefs.GetInt("drawingSuspensionCount") + "/MAX";
                    break;
                case DetailCard.ItemType.Transmission:
                    tDrawing.text = "Чертеж трансмиссии: " + PlayerPrefs.GetInt("drawingTransmissionCount") + "/MAX";
                    break;
            }
        }        
    }

    void LevelUpdate()
    {
        string _type = "";

        switch (garageController.activeItem.itemObj.itemType)
        {
            case DetailCard.ItemType.Gun:
                _type = "Gun";
                break;
            case DetailCard.ItemType.Engine:
                _type = "Engine";
                break;
            case DetailCard.ItemType.Brakes:
                _type = "Brakes";
                break;
            case DetailCard.ItemType.FuelSystem:
                _type = "FuelSystem";
                break;
            case DetailCard.ItemType.Suspension:
                _type = "Suspension";
                break;
            case DetailCard.ItemType.Transmission:
                _type = "Transmission";
                break;
        }

        tCurrentLevel.text = PlayerPrefs.GetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory) + "/" + maxLevel;
        fillLevel.DOFillAmount((float)PlayerPrefs.GetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory) / maxLevel, 0.5f);
        fillEndLevel.GetComponent<RectTransform>().DOAnchorPos(new Vector2((float)PlayerPrefs.GetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory) / maxLevel * 233f, 0), 0.5f);
    }

    public void CreateStatsSave(DetailCard _card, ItemCell _itemCell)  //Создаем сохранение характеристик итема
    {
        if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterCommon1Value" + _itemCell.itemNumInInventory))
        {
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterCommon1Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersCommon1Value);
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterCommon2Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersCommon2Value);

            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterRare1Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersRare1Value);
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterRare2Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersRare2Value);

            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterEpic1Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersEpic1Value);
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterEpic2Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersEpic2Value);

            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterLegendary1Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersLegendary1Value);
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterLegendary2Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersLegendary2Value);
        }
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        Initialize();
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }

    public void ButSelect()
    {
        garageController.ItemSelect();

        switch (garageController.activeItem.slotNum)
        {
            case 1:
                if (garageController.slot1Card != null)
                {
                    garageController.slot1Card.gameObject.SetActive(true);
                }

                garageController.slot1Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot1_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot1_itemNumInInventory", garageController.activeItem.itemNumInInventory);

                SaveDetailStats("Gun");
                break;

            case 2:
                if (garageController.slot2Card != null)
                {
                    garageController.slot2Card.gameObject.SetActive(true);
                }

                garageController.slot2Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot2_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot2_itemNumInInventory", garageController.activeItem.itemNumInInventory);

                SaveDetailStats("Engine");
                break;

            case 3:
                if (garageController.slot3Card != null)
                {
                    garageController.slot3Card.gameObject.SetActive(true);
                }

                garageController.slot3Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot3_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot3_itemNumInInventory", garageController.activeItem.itemNumInInventory);

                SaveDetailStats("Brakes");
                break;

            case 4:
                if (garageController.slot4Card != null)
                {
                    garageController.slot4Card.gameObject.SetActive(true);
                }

                garageController.slot4Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot4_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot4_itemNumInInventory", garageController.activeItem.itemNumInInventory);

                SaveDetailStats("FuelSystem");
                break;

            case 5:
                if (garageController.slot5Card != null)
                {
                    garageController.slot5Card.gameObject.SetActive(true);
                }

                garageController.slot5Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot5_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot5_itemNumInInventory", garageController.activeItem.itemNumInInventory);

                SaveDetailStats("Suspension");
                break;

            case 6:
                if (garageController.slot6Card != null)
                {
                    garageController.slot6Card.gameObject.SetActive(true);
                }

                garageController.slot6Card = garageController.activeItem;

                PlayerPrefs.SetInt("slot6_itemID", garageController.activeItem.itemID);
                PlayerPrefs.SetInt("slot6_itemNumInInventory", garageController.activeItem.itemNumInInventory);

                SaveDetailStats("Transmission");
                break;
        }

        garageController.CalculateViewStats();

        garageController.activeItem.gameObject.SetActive(false);

        ButClosed();
    }

    public void ButUnselect()
    {
        garageController.ItemUnselect();
        ButClosed();
    }

    public void ButUpgrade()  //Добавить сюда счетчик чертежей
    {
        if (PlayerPrefs.GetInt("playerMoney") >= upgradePrice[garageController.activeItem.currentLevel])
        {
            PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") - upgradePrice[garageController.activeItem.currentLevel]);

            string _type = "";

            switch (garageController.activeItem.itemObj.itemType)
            {
                case DetailCard.ItemType.Gun:
                    _type = "Gun";
                    break;
                case DetailCard.ItemType.Engine:
                    _type = "Engine";
                    break;
                case DetailCard.ItemType.Brakes:
                    _type = "Brakes";
                    break;
                case DetailCard.ItemType.FuelSystem:
                    _type = "FuelSystem";
                    break;
                case DetailCard.ItemType.Suspension:
                    _type = "Suspension";
                    break;
                case DetailCard.ItemType.Transmission:
                    _type = "Transmission";
                    break;
            }

            PlayerPrefs.SetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory, 
                               PlayerPrefs.GetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory) + 1);

            garageController.activeItem.currentLevel += 1;

            if (itemRarity == "common")
            {
                PlayerPrefs.SetFloat("item" + _type + "baseCharacterCommon1Value" + garageController.activeItem.itemNumInInventory,
                                 PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon1Value" + garageController.activeItem.itemNumInInventory) + itemCharacters1StepValue);

                if (itemCharacters2StepValue != 0)
                {
                    PlayerPrefs.SetFloat("item" + _type + "baseCharacterCommon2Value" + garageController.activeItem.itemNumInInventory,
                                     PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon2Value" + garageController.activeItem.itemNumInInventory) + itemCharacters2StepValue);
                }
            }

            if (itemRarity == "rare")
            {
                PlayerPrefs.SetFloat("item" + _type + "baseCharacterRare1Value" + garageController.activeItem.itemNumInInventory,
                                 PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare1Value" + garageController.activeItem.itemNumInInventory) + itemCharacters1StepValue);

                if (itemCharacters2StepValue != 0)
                {
                    PlayerPrefs.SetFloat("item" + _type + "baseCharacterRare2Value" + garageController.activeItem.itemNumInInventory,
                                     PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare2Value" + garageController.activeItem.itemNumInInventory) + itemCharacters2StepValue);
                }
            }

            if (itemRarity == "epic")
            {
                PlayerPrefs.SetFloat("item" + _type + "baseCharacterEpic1Value" + garageController.activeItem.itemNumInInventory,
                                 PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic1Value" + garageController.activeItem.itemNumInInventory) + itemCharacters1StepValue);

                if (itemCharacters2StepValue != 0)
                {
                    PlayerPrefs.SetFloat("item" + _type + "baseCharacterEpic2Value" + garageController.activeItem.itemNumInInventory,
                                     PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic2Value" + garageController.activeItem.itemNumInInventory) + itemCharacters2StepValue);
                }
            }

            if (itemRarity == "legendary")
            {
                PlayerPrefs.SetFloat("item" + _type + "baseCharacterLegendary1Value" + garageController.activeItem.itemNumInInventory,
                                 PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary1Value" + garageController.activeItem.itemNumInInventory) + itemCharacters1StepValue);

                if (itemCharacters2StepValue != 0)
                {
                    PlayerPrefs.SetFloat("item" + _type + "baseCharacterLegendary2Value" + garageController.activeItem.itemNumInInventory,
                                     PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary2Value" + garageController.activeItem.itemNumInInventory) + itemCharacters2StepValue);
                }
            }

            #region SaveSelectItem
            PlayerPrefs.SetInt(_type + "IDSelect", garageController.activeItem.itemID);
            PlayerPrefs.SetString(_type + "RaritySelect", garageController.activeItem.itemRarity);
            PlayerPrefs.SetInt(_type + "LevelSelect", garageController.activeItem.currentLevel);
            #endregion

            SaveDetailStats(_type);

            CreateCharactersPanel();
            CalculateUpgradePrice();
            LevelUpdate();

            if (garageController.slot1Card == garageController.activeItem ||
                garageController.slot2Card == garageController.activeItem ||
                garageController.slot3Card == garageController.activeItem ||
                garageController.slot4Card == garageController.activeItem ||
                garageController.slot5Card == garageController.activeItem ||
                garageController.slot6Card == garageController.activeItem)
            garageController.CalculateViewStats();
        }
    }

    void SaveDetailStats(string _type)
    {
        DetailCard _card = garageController.activeItem.itemObj;

        #region Base Stats
        switch (itemRarity)
        {
            case "common":

                if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.HpUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon1Value" + garageController.activeItem.itemNumInInventory));
                }

                if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.DamageUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon1Value" + garageController.activeItem.itemNumInInventory));
                }

                if (_card.baseItemCharactersCommon2 != DetailCard.ItemCharacters.none)
                {
                    if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.HpUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon2Value" + garageController.activeItem.itemNumInInventory));
                    }

                    if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon2Value" + garageController.activeItem.itemNumInInventory));
                    }
                }

                break;

            case "rare":

                if (_card.baseItemCharactersRare1 == DetailCard.ItemCharacters.HpUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare1Value" + garageController.activeItem.itemNumInInventory));
                }

                if (_card.baseItemCharactersRare1 == DetailCard.ItemCharacters.DamageUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare1Value" + garageController.activeItem.itemNumInInventory));
                }

                if (_card.baseItemCharactersRare2 != DetailCard.ItemCharacters.none)
                {
                    if (_card.baseItemCharactersRare2 == DetailCard.ItemCharacters.HpUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare2Value" + garageController.activeItem.itemNumInInventory));
                    }

                    if (_card.baseItemCharactersRare2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare2Value" + garageController.activeItem.itemNumInInventory));
                    }
                }

                break;

            case "epic":

                if (_card.baseItemCharactersEpic1 == DetailCard.ItemCharacters.HpUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic1Value" + garageController.activeItem.itemNumInInventory));
                }

                if (_card.baseItemCharactersEpic1 == DetailCard.ItemCharacters.DamageUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic1Value" + garageController.activeItem.itemNumInInventory));
                }

                if (_card.baseItemCharactersEpic2 != DetailCard.ItemCharacters.none)
                {
                    if (_card.baseItemCharactersEpic2 == DetailCard.ItemCharacters.HpUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic2Value" + garageController.activeItem.itemNumInInventory));
                    }

                    if (_card.baseItemCharactersEpic2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic2Value" + garageController.activeItem.itemNumInInventory));
                    }
                }

                break;

            case "legendary":

                if (_card.baseItemCharactersLegendary1 == DetailCard.ItemCharacters.HpUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary1Value" + garageController.activeItem.itemNumInInventory));
                }

                if (_card.baseItemCharactersLegendary1 == DetailCard.ItemCharacters.DamageUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary1Value" + garageController.activeItem.itemNumInInventory));
                }

                if (_card.baseItemCharactersLegendary2 != DetailCard.ItemCharacters.none)
                {
                    if (_card.baseItemCharactersLegendary2 == DetailCard.ItemCharacters.HpUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary2Value" + garageController.activeItem.itemNumInInventory));
                    }

                    if (_card.baseItemCharactersLegendary2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary2Value" + garageController.activeItem.itemNumInInventory));
                    }
                }

                break;
        }
        #endregion

        #region UnicalStats
        if (itemRarity == "rare" || itemRarity == "epic" || itemRarity == "legendary")
        {
            switch (_card.rareItemCharacters)
            {
                case DetailCard.RarityItemCharacters.HpUp:
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat(_type + "SelectHealth") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.RecoveryHpInFirstAidKit:
                    PlayerPrefs.SetFloat(_type + "SelectRecoveryHpInFirstAidKit", PlayerPrefs.GetFloat(_type + "SelectRecoveryHpInFirstAidKit") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Dodge:
                    PlayerPrefs.SetFloat(_type + "SelectDodge", PlayerPrefs.GetFloat(_type + "SelectDodge") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.DronDamage:
                    PlayerPrefs.SetFloat(_type + "SelectDronDamage", PlayerPrefs.GetFloat(_type + "SelectDronDamage") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.ShotSpeed:
                    PlayerPrefs.SetFloat(_type + "SelectShotSpeed", PlayerPrefs.GetFloat(_type + "SelectShotSpeed") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.KritDamage:
                    PlayerPrefs.SetFloat(_type + "SelectKritDamage", PlayerPrefs.GetFloat(_type + "SelectKritDamage") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.KritChance:
                    PlayerPrefs.SetFloat(_type + "SelectKritChance", PlayerPrefs.GetFloat(_type + "SelectKritChance") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.BackDamage:
                    PlayerPrefs.SetFloat(_type + "SelectBackDamage", PlayerPrefs.GetFloat(_type + "SelectBackDamage") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Vampirizm:
                    PlayerPrefs.SetFloat(_type + "SelectVampirizm", PlayerPrefs.GetFloat(_type + "SelectVampirizm") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Armor:
                    PlayerPrefs.SetFloat(_type + "SelectArmor", PlayerPrefs.GetFloat(_type + "SelectArmor") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.HealthRecovery:
                    PlayerPrefs.SetFloat(_type + "SelectHealthRecovery", PlayerPrefs.GetFloat(_type + "SelectHealthRecovery") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Rage:
                    PlayerPrefs.SetFloat(_type + "SelectRage", PlayerPrefs.GetFloat(_type + "SelectRage") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.DistanceDamage:
                    PlayerPrefs.SetFloat(_type + "SelectDistanceDamage", PlayerPrefs.GetFloat(_type + "SelectDistanceDamage") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Lucky:
                    PlayerPrefs.SetFloat(_type + "SelectLucky", PlayerPrefs.GetFloat(_type + "SelectLucky") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Magnet:
                    PlayerPrefs.SetFloat(_type + "SelectMagnet", PlayerPrefs.GetFloat(_type + "SelectMagnet") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Damage:
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat(_type + "SelectDamage") + _card.rareItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.CarDamage:
                    PlayerPrefs.SetFloat(_type + "SelectCarDamage", PlayerPrefs.GetFloat(_type + "SelectCarDamage") + _card.rareItemCharactersValue);
                    break;
            }
        }

        if (itemRarity == "epic" || itemRarity == "legendary")
        {
            switch (_card.epicItemCharacters)
            {
                case DetailCard.RarityItemCharacters.HpUp:
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat(_type + "SelectHealth") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.RecoveryHpInFirstAidKit:
                    PlayerPrefs.SetFloat(_type + "SelectRecoveryHpInFirstAidKit", PlayerPrefs.GetFloat(_type + "SelectRecoveryHpInFirstAidKit") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Dodge:
                    PlayerPrefs.SetFloat(_type + "SelectDodge", PlayerPrefs.GetFloat(_type + "SelectDodge") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.DronDamage:
                    PlayerPrefs.SetFloat(_type + "SelectDronDamage", PlayerPrefs.GetFloat(_type + "SelectDronDamage") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.ShotSpeed:
                    PlayerPrefs.SetFloat(_type + "SelectShotSpeed", PlayerPrefs.GetFloat(_type + "SelectShotSpeed") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.KritDamage:
                    PlayerPrefs.SetFloat(_type + "SelectKritDamage", PlayerPrefs.GetFloat(_type + "SelectKritDamage") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.KritChance:
                    PlayerPrefs.SetFloat(_type + "SelectKritChance", PlayerPrefs.GetFloat(_type + "SelectKritChance") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.BackDamage:
                    PlayerPrefs.SetFloat(_type + "SelectBackDamage", PlayerPrefs.GetFloat(_type + "SelectBackDamage") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Vampirizm:
                    PlayerPrefs.SetFloat(_type + "SelectVampirizm", PlayerPrefs.GetFloat(_type + "SelectVampirizm") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Armor:
                    PlayerPrefs.SetFloat(_type + "SelectArmor", PlayerPrefs.GetFloat(_type + "SelectArmor") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.HealthRecovery:
                    PlayerPrefs.SetFloat(_type + "SelectHealthRecovery", PlayerPrefs.GetFloat(_type + "SelectHealthRecovery") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Rage:
                    PlayerPrefs.SetFloat(_type + "SelectRage", PlayerPrefs.GetFloat(_type + "SelectRage") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.DistanceDamage:
                    PlayerPrefs.SetFloat(_type + "SelectDistanceDamage", PlayerPrefs.GetFloat(_type + "SelectDistanceDamage") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Lucky:
                    PlayerPrefs.SetFloat(_type + "SelectLucky", PlayerPrefs.GetFloat(_type + "SelectLucky") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Magnet:
                    PlayerPrefs.SetFloat(_type + "SelectMagnet", PlayerPrefs.GetFloat(_type + "SelectMagnet") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Damage:
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat(_type + "SelectDamage") + _card.epicItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.CarDamage:
                    PlayerPrefs.SetFloat(_type + "SelectCarDamage", PlayerPrefs.GetFloat(_type + "SelectCarDamage") + _card.epicItemCharactersValue);
                    break;
            }
        }

        if (itemRarity == "legendary")
        {
            switch (_card.legendaryItemCharacters)
            {
                case DetailCard.RarityItemCharacters.HpUp:
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat(_type + "SelectHealth") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.RecoveryHpInFirstAidKit:
                    PlayerPrefs.SetFloat(_type + "SelectRecoveryHpInFirstAidKit", PlayerPrefs.GetFloat(_type + "SelectRecoveryHpInFirstAidKit") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Dodge:
                    PlayerPrefs.SetFloat(_type + "SelectDodge", PlayerPrefs.GetFloat(_type + "SelectDodge") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.DronDamage:
                    PlayerPrefs.SetFloat(_type + "SelectDronDamage", PlayerPrefs.GetFloat(_type + "SelectDronDamage") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.ShotSpeed:
                    PlayerPrefs.SetFloat(_type + "SelectShotSpeed", PlayerPrefs.GetFloat(_type + "SelectShotSpeed") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.KritDamage:
                    PlayerPrefs.SetFloat(_type + "SelectKritDamage", PlayerPrefs.GetFloat(_type + "SelectKritDamage") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.KritChance:
                    PlayerPrefs.SetFloat(_type + "SelectKritChance", PlayerPrefs.GetFloat(_type + "SelectKritChance") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.BackDamage:
                    PlayerPrefs.SetFloat(_type + "SelectBackDamage", PlayerPrefs.GetFloat(_type + "SelectBackDamage") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Vampirizm:
                    PlayerPrefs.SetFloat(_type + "SelectVampirizm", PlayerPrefs.GetFloat(_type + "SelectVampirizm") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Armor:
                    PlayerPrefs.SetFloat(_type + "SelectArmor", PlayerPrefs.GetFloat(_type + "SelectArmor") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.HealthRecovery:
                    PlayerPrefs.SetFloat(_type + "SelectHealthRecovery", PlayerPrefs.GetFloat(_type + "SelectHealthRecovery") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Rage:
                    PlayerPrefs.SetFloat(_type + "SelectRage", PlayerPrefs.GetFloat(_type + "SelectRage") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.DistanceDamage:
                    PlayerPrefs.SetFloat(_type + "SelectDistanceDamage", PlayerPrefs.GetFloat(_type + "SelectDistanceDamage") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Lucky:
                    PlayerPrefs.SetFloat(_type + "SelectLucky", PlayerPrefs.GetFloat(_type + "SelectLucky") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Magnet:
                    PlayerPrefs.SetFloat(_type + "SelectMagnet", PlayerPrefs.GetFloat(_type + "SelectMagnet") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.Damage:
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat(_type + "SelectDamage") + _card.legendaryItemCharactersValue);
                    break;

                case DetailCard.RarityItemCharacters.CarDamage:
                    PlayerPrefs.SetFloat(_type + "SelectCarDamage", PlayerPrefs.GetFloat(_type + "SelectCarDamage") + _card.legendaryItemCharactersValue);
                    break;
            }
        }
        #endregion
    }
}
