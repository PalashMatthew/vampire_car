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
    public GameObject butUpgradeGrayNotValue;

    [Header("Level")]
    public Image fillLevel;
    public Image fillEndLevel;
    public TMP_Text tCurrentLevel;
    int maxLevel;

    [Header("Set")]
    public TMP_Text tSetName;
    public Image imgSetIcon1;
    public Image imgSetIcon2;
    public Image imgSetIcon3;
    public Image imgSetIcon4;
    public Image imgSetIcon5;
    public Image imgSetImageCell1;
    public Image imgSetImageCell2;
    public Image imgSetImageCell3;
    public Image imgSetImageCell4;
    public Image imgSetImageCell5;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        tName.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + garageController.activeItem.itemID + "Name");

        switch (itemRarity)
        {
            case "common":
                imgBackIcon.sprite = sprCommonCard;
                tRarity.text = "<color=#808B96>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_common") + "</color>";

                maxLevel = 10;
                break;

            case "rare":
                imgBackIcon.sprite = sprRareCard;
                tRarity.text = "<color=#3498DB>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_rare") + "</color>";

                maxLevel = 20;
                break;

            case "epic":
                imgBackIcon.sprite = sprEpicCard;
                tRarity.text = "<color=#CE33FF>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_epic") + "</color>";

                maxLevel = 30;
                break;

            case "legendary":
                imgBackIcon.sprite = sprLegendaryCard;
                tRarity.text = "<color=yellow>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_legendary") + "</color>";

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
            tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + garageController.activeItem.itemObj.itemID + "Desk");
        } 
        else
        {
            panelDescription.SetActive(false);
            panelSet.SetActive(true);
        }

        SetSettings();
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
            butUpgradeGrayNotValue.SetActive(false);

            switch (garageController.activeItem.itemObj.itemType)
            {
                case DetailCard.ItemType.Gun:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingGun") + ": " + PlayerPrefs.GetInt("drawingGunCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.Engine:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingEngine") + ": " + PlayerPrefs.GetInt("drawingEngineCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.Brakes:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingBrakes") + ": " + PlayerPrefs.GetInt("drawingBrakesCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.FuelSystem:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingFuelSystem") + ": " + PlayerPrefs.GetInt("drawingFuelSystemCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.Suspension:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingSuspension") + ": " + PlayerPrefs.GetInt("drawingSuspensionCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
                case DetailCard.ItemType.Transmission:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingTransmission") + ": " + PlayerPrefs.GetInt("drawingTransmissionCount") + "/" + drawingCount[garageController.activeItem.currentLevel];
                    break;
            }

            tUpgradePrice.text = upgradePrice[garageController.activeItem.currentLevel] + "";
        } 
        else
        {
            butUpgrade.SetActive(false);
            butUpgradeGray.SetActive(true);
            butUpgradeGrayNotValue.SetActive(false);

            switch (garageController.activeItem.itemObj.itemType)
            {
                case DetailCard.ItemType.Gun:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingGun") + ": " + PlayerPrefs.GetInt("drawingGunCount") + "/MAX";
                    break;
                case DetailCard.ItemType.Engine:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingEngine") + ": " + PlayerPrefs.GetInt("drawingEngineCount") + "/MAX";
                    break;
                case DetailCard.ItemType.Brakes:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingBrakes") + ": " + PlayerPrefs.GetInt("drawingBrakesCount") + "/MAX";
                    break;
                case DetailCard.ItemType.FuelSystem:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingFuelSystem") + ": " + PlayerPrefs.GetInt("drawingFuelSystemCount") + "/MAX";
                    break;
                case DetailCard.ItemType.Suspension:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingSuspension") + ": " + PlayerPrefs.GetInt("drawingSuspensionCount") + "/MAX";
                    break;
                case DetailCard.ItemType.Transmission:
                    tDrawing.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_drawingTransmission") + ": " + PlayerPrefs.GetInt("drawingTransmissionCount") + "/MAX";
                    break;
            }
        }

        #region Button Check
        if (!butUpgradeGray.activeSelf)
        {
            int currentDrawing = 0;

            switch (garageController.activeItem.itemObj.itemType)
            {
                case DetailCard.ItemType.Gun:
                    currentDrawing = PlayerPrefs.GetInt("drawingGunCount");
                    break;
                case DetailCard.ItemType.Engine:
                    currentDrawing = PlayerPrefs.GetInt("drawingEngineCount");
                    break;
                case DetailCard.ItemType.Brakes:
                    currentDrawing = PlayerPrefs.GetInt("drawingBrakesCount");
                    break;
                case DetailCard.ItemType.FuelSystem:
                    currentDrawing = PlayerPrefs.GetInt("drawingFuelSystemCount");
                    break;
                case DetailCard.ItemType.Suspension:
                    currentDrawing = PlayerPrefs.GetInt("drawingSuspensionCount");
                    break;
                case DetailCard.ItemType.Transmission:
                    currentDrawing = PlayerPrefs.GetInt("drawingTransmissionCount");
                    break;
            }

            if (PlayerPrefs.GetInt("playerMoney") >= upgradePrice[garageController.activeItem.currentLevel] &&
                currentDrawing >= drawingCount[garageController.activeItem.currentLevel] && PlayerPrefs.GetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory) < PlayerPrefs.GetInt("playerLevel"))
            {
                butUpgradeGrayNotValue.SetActive(false);
                butUpgrade.SetActive(true);
            }
            else
            {
                butUpgradeGrayNotValue.SetActive(true);
                butUpgrade.SetActive(false);
            }
        }
        #endregion
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
        }

        if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterRare1Value" + _itemCell.itemNumInInventory))
        {
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterRare1Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersRare1Value);
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterRare2Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersRare2Value);
        }

        if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterEpic1Value" + _itemCell.itemNumInInventory))
        {
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterEpic1Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersEpic1Value);
            PlayerPrefs.SetFloat("item" + _card.itemType.ToString() + "baseCharacterEpic2Value" + _itemCell.itemNumInInventory, _card.baseItemCharactersEpic2Value);
        }

        if (!PlayerPrefs.HasKey("item" + _card.itemType.ToString() + "baseCharacterLegendary1Value" + _itemCell.itemNumInInventory))
        {
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

        if (garageController.activeItem.slotNum != 1)
        {
            GameObject.Find("PopUp Set").GetComponent<PopUpSet>().setID = garageController.activeItem.itemObj.setID;
            GameObject.Find("PopUp Set").GetComponent<PopUpSet>().CheckSet();
        }        

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
            int currentDrawing = 0;
            string s = "";

            string _invType = "";

            switch (garageController.activeItem.itemObj.itemType)
            {
                case DetailCard.ItemType.Gun:
                    currentDrawing = PlayerPrefs.GetInt("drawingGunCount");
                    s = "drawingGunCount";

                    if (PlayerPrefs.GetInt("GunIDSelect") == garageController.activeItem.itemID)
                    {
                        _invType = "Apply";
                    } 
                    else
                    {
                        _invType = "TakeOff";
                    }
                    break;
                case DetailCard.ItemType.Engine:
                    currentDrawing = PlayerPrefs.GetInt("drawingEngineCount");
                    s = "drawingEngineCount";

                    if (PlayerPrefs.GetInt("EngineIDSelect") == garageController.activeItem.itemID)
                    {
                        _invType = "Apply";
                    }
                    else
                    {
                        _invType = "TakeOff";
                    }
                    break;
                case DetailCard.ItemType.Brakes:
                    currentDrawing = PlayerPrefs.GetInt("drawingBrakesCount");
                    s = "drawingBrakesCount";

                    if (PlayerPrefs.GetInt("BrakesIDSelect") == garageController.activeItem.itemID)
                    {
                        _invType = "Apply";
                    }
                    else
                    {
                        _invType = "TakeOff";
                    }
                    break;
                case DetailCard.ItemType.FuelSystem:
                    currentDrawing = PlayerPrefs.GetInt("drawingFuelSystemCount");
                    s = "drawingFuelSystemCount";

                    if (PlayerPrefs.GetInt("FuelSystemIDSelect") == garageController.activeItem.itemID)
                    {
                        _invType = "Apply";
                    }
                    else
                    {
                        _invType = "TakeOff";
                    }
                    break;
                case DetailCard.ItemType.Suspension:
                    currentDrawing = PlayerPrefs.GetInt("drawingSuspensionCount");
                    s = "drawingSuspensionCount";

                    if (PlayerPrefs.GetInt("SuspensionIDSelect") == garageController.activeItem.itemID)
                    {
                        _invType = "Apply";
                    }
                    else
                    {
                        _invType = "TakeOff";
                    }
                    break;
                case DetailCard.ItemType.Transmission:
                    currentDrawing = PlayerPrefs.GetInt("drawingTransmissionCount");
                    s = "drawingTransmissionCount";

                    if (PlayerPrefs.GetInt("TransmissionIDSelect") == garageController.activeItem.itemID)
                    {
                        _invType = "Apply";
                    }
                    else
                    {
                        _invType = "TakeOff";
                    }
                    break;
            }

            if (currentDrawing >= drawingCount[garageController.activeItem.currentLevel])
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

                #region Event
                string _resBalance;
                string _resType = "";

                if (PlayerPrefs.GetInt("playerMoney") >= upgradePrice[garageController.activeItem.currentLevel + 1] &&
                currentDrawing >= drawingCount[garageController.activeItem.currentLevel + 1] && PlayerPrefs.GetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory) < PlayerPrefs.GetInt("playerLevel"))
                {
                    _resBalance = "NotEmptyRes";
                    _resType = "none";
                }
                else
                {
                    _resBalance = "EmptyRes";

                    if (PlayerPrefs.GetInt("playerMoney") < upgradePrice[garageController.activeItem.currentLevel + 1])
                    {
                        _resType += "_Money";
                    }

                    if (currentDrawing >= drawingCount[garageController.activeItem.currentLevel + 1])
                    {
                        _resType += "_Drawing";
                    }

                    if (PlayerPrefs.GetInt("item" + _type + "Level" + garageController.activeItem.itemNumInInventory) >= PlayerPrefs.GetInt("playerLevel"))
                    {
                        _resType += "_PlayerLevel";
                    }
                }

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_DetailUpgrade(_invType, _type, garageController.activeItem.itemID, garageController.activeItem.currentLevel + 1, garageController.activeItem.itemRarity, _resBalance, _resType);
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

                PlayerPrefs.SetInt(s, PlayerPrefs.GetInt(s) - drawingCount[garageController.activeItem.currentLevel]);

                garageController.activeItem.currentLevel += 1;

                CalculateUpgradePrice();
            }            
        }
    }

    void SaveDetailStats(string _type)
    {
        DetailCard _card = garageController.activeItem.itemObj;

        //ClearSave
        string s = _type;

        PlayerPrefs.SetFloat(s + "SelectHealth", 0);
        PlayerPrefs.SetFloat(s + "SelectDamage", 0);
        PlayerPrefs.SetFloat(s + "SelectRecoveryHpInFirstAidKit", 0);
        PlayerPrefs.SetFloat(s + "SelectDodge", 0);
        PlayerPrefs.SetFloat(s + "SelectDronDamage", 0);
        PlayerPrefs.SetFloat(s + "SelectShotSpeed", 0);
        PlayerPrefs.SetFloat(s + "SelectKritDamage", 0);
        PlayerPrefs.SetFloat(s + "SelectKritChance", 0);
        PlayerPrefs.SetFloat(s + "SelectBackDamage", 0);
        PlayerPrefs.SetFloat(s + "SelectVampirizm", 0);
        PlayerPrefs.SetFloat(s + "SelectArmor", 0);
        PlayerPrefs.SetFloat(s + "SelectHealthRecovery", 0);
        PlayerPrefs.SetFloat(s + "SelectRage", 0);
        PlayerPrefs.SetFloat(s + "SelectDistanceDamage", 0);
        PlayerPrefs.SetFloat(s + "SelectLucky", 0);
        PlayerPrefs.SetFloat(s + "SelectMagnet", 0);
        PlayerPrefs.SetFloat(s + "SelectCarDamage", 0);

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

    public void ButOpenSet()
    {
        GameObject.Find("PopUp Set").GetComponent<PopUpSet>().setID = garageController.activeItem.itemObj.setID;
        GameObject.Find("PopUp Set").GetComponent<PopUpSet>().OpenPopUp();
    }

    void SetSettings()
    {
        PopUpSet popUpSet = GameObject.Find("PopUp Set").GetComponent<PopUpSet>();
        tSetName.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_set" + garageController.activeItem.itemObj.setID + "Name");

        Color colorActive = new Color(1, 1, 1, 1);
        Color colorInactive = new Color(0.5f, 0.5f, 0.5f, 1);

        #region Выставляем иконки
        int engineID = 0;
        int brakesID = 0;
        int fuelSystemID = 0;
        int suspensionID = 0;
        int transmissionID = 0;

        switch (garageController.activeItem.itemObj.setID)
        {
            case "s01":
                engineID = 2001;
                brakesID = 3001;
                fuelSystemID = 4001;
                suspensionID = 5001;
                transmissionID = 6001;
                break;
            case "s02":
                engineID = 2002;
                brakesID = 3002;
                fuelSystemID = 4002;
                suspensionID = 5002;
                transmissionID = 6002;
                break;
            case "s03":
                engineID = 2003;
                brakesID = 3003;
                fuelSystemID = 4003;
                suspensionID = 5003;
                transmissionID = 6003;
                break;
            case "s04":
                engineID = 2004;
                brakesID = 3004;
                fuelSystemID = 4004;
                suspensionID = 5004;
                transmissionID = 6004;
                break;
            case "s05":
                engineID = 2005;
                brakesID = 3005;
                fuelSystemID = 4005;
                suspensionID = 5005;
                transmissionID = 6005;
                break;
            case "s06":
                engineID = 2006;
                brakesID = 3006;
                fuelSystemID = 4006;
                suspensionID = 5006;
                transmissionID = 6006;
                break;
            case "s07":
                engineID = 2007;
                brakesID = 3007;
                fuelSystemID = 4007;
                suspensionID = 5007;
                transmissionID = 6007;
                break;
            case "s08":
                engineID = 2008;
                brakesID = 3008;
                fuelSystemID = 4008;
                suspensionID = 5008;
                transmissionID = 6008;
                break;
            case "s09":
                engineID = 2009;
                brakesID = 3009;
                fuelSystemID = 4009;
                suspensionID = 5009;
                transmissionID = 6009;
                break;
            case "s10":
                engineID = 2010;
                brakesID = 3010;
                fuelSystemID = 4010;
                suspensionID = 5010;
                transmissionID = 6010;
                break;
        }

        foreach (DetailCard _card in popUpSet.engineCards)
        {
            if (_card.itemID == engineID)
            {
                imgSetIcon1.sprite = _card.sprItem;
            }
        }

        foreach (DetailCard _card in popUpSet.brakesCards)
        {
            if (_card.itemID == brakesID)
            {
                imgSetIcon2.sprite = _card.sprItem;
            }
        }

        foreach (DetailCard _card in popUpSet.fuelSystemCards)
        {
            if (_card.itemID == fuelSystemID)
            {
                imgSetIcon3.sprite = _card.sprItem;
            }
        }

        foreach (DetailCard _card in popUpSet.suspensionCards)
        {
            if (_card.itemID == suspensionID)
            {
                imgSetIcon4.sprite = _card.sprItem;
            }
        }

        foreach (DetailCard _card in popUpSet.transmissionCards)
        {
            if (_card.itemID == transmissionID)
            {
                imgSetIcon5.sprite = _card.sprItem;
            }
        }
        #endregion

        #region Настраиваем отображение
        if (PlayerPrefs.GetInt("slot2_itemID") == engineID)
        {
            imgSetImageCell1.color = colorActive;
            imgSetIcon1.color = colorActive;

            imgSetImageCell1.sprite = popUpSet.imgGarageCell1.sprite;
        }
        else
        {
            imgSetImageCell1.color = colorInactive;
            imgSetIcon1.color = colorInactive;

            imgSetImageCell1.sprite = popUpSet.sprCellCommon;
        }

        if (PlayerPrefs.GetInt("slot3_itemID") == brakesID)
        {
            imgSetImageCell2.color = colorActive;
            imgSetIcon2.color = colorActive;

            imgSetImageCell2.sprite = popUpSet.imgGarageCell2.sprite;
        }
        else
        {
            imgSetImageCell2.color = colorInactive;
            imgSetIcon2.color = colorInactive;

            imgSetImageCell2.sprite = popUpSet.sprCellCommon;
        }

        if (PlayerPrefs.GetInt("slot4_itemID") == fuelSystemID)
        {
            imgSetImageCell3.color = colorActive;
            imgSetIcon3.color = colorActive;

            imgSetImageCell3.sprite = popUpSet.imgGarageCell3.sprite;
        }
        else
        {
            imgSetImageCell3.color = colorInactive;
            imgSetIcon3.color = colorInactive;

            imgSetImageCell3.sprite = popUpSet.sprCellCommon;
        }

        if (PlayerPrefs.GetInt("slot5_itemID") == suspensionID)
        {
            imgSetImageCell4.color = colorActive;
            imgSetIcon4.color = colorActive;

            imgSetImageCell4.sprite = popUpSet.imgGarageCell4.sprite;
        }
        else
        {
            imgSetImageCell4.color = colorInactive;
            imgSetIcon4.color = colorInactive;

            imgSetImageCell4.sprite = popUpSet.sprCellCommon;
        }

        if (PlayerPrefs.GetInt("slot6_itemID") == transmissionID)
        {
            imgSetImageCell5.color = colorActive;
            imgSetIcon5.color = colorActive;

            imgSetImageCell5.sprite = popUpSet.imgGarageCell5.sprite;
        }
        else
        {
            imgSetImageCell5.color = colorInactive;
            imgSetIcon5.color = colorInactive;

            imgSetImageCell5.sprite = popUpSet.sprCellCommon;
        }
        #endregion
    }
}
