using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GarageController : MonoBehaviour
{
    public GameObject canvasChangeCar;
    [HideInInspector] public CarButtonController activeCarObj;
    public List<GameObject> carsButtons;

    public ItemCell activeItem;
    public PopUpDetail popUpDetail;

    [Header("Detail Obj")]
    public List<DetailCard> gunItem;
    public List<DetailCard> engineItem;
    public List<DetailCard> brakesItem;
    public List<DetailCard> fuelSystemItem;
    public List<DetailCard> suspensionItem;
    public List<DetailCard> transmissionItem;

    [Header("Cell")]
    public GameObject cellPrefab;

    [Header("Scroll Gun")]
    public Transform globalContentPanel;
    public List<GameObject> itemGunInst;
    public List<GameObject> itemEngineInst;
    public List<GameObject> itemBrakesInst;
    public List<GameObject> itemFuelSystemInst;
    public List<GameObject> itemSuspensionInst;
    public List<GameObject> itemTransmissionInst;

    [Header("Car Settings")]
    public TMP_Text tCarName;
    public Image imgCar;
    public TMP_Text tDamage;
    public TMP_Text tHealth;

    [Header("Car Level")]
    public TMP_Text tCarLevel;
    public Image fillCarLevel;
    public Image fillEndCarLevel;

    [Header("Slots")]
    public Sprite sprSlotDefault;
    public Image imgSlot1;
    public Image imgSlot2;
    public Image imgSlot3;
    public Image imgSlot4;
    public Image imgSlot5;
    public Image imgSlot6;
    public Image imgIconSlot1;
    public Image imgIconSlot2;
    public Image imgIconSlot3;
    public Image imgIconSlot4;
    public Image imgIconSlot5;
    public Image imgIconSlot6;

    [Header("Slots Card")]
    public ItemCell slot1Card;
    public ItemCell slot2Card;
    public ItemCell slot3Card;
    public ItemCell slot4Card;
    public ItemCell slot5Card;
    public ItemCell slot6Card;

    [Header("Slots Level")]
    public TMP_Text tSlot1Level;
    public TMP_Text tSlot2Level;
    public TMP_Text tSlot3Level;
    public TMP_Text tSlot4Level;
    public TMP_Text tSlot5Level;
    public TMP_Text tSlot6Level;

    public List<GameObject> itemMass;


    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        SaveItem();
    }

    private void Update()
    {
        #region Slot Levels
        //1
        if (slot1Card != null)
            tSlot1Level.text = "Lv. " + slot1Card.currentLevel;
        else tSlot1Level.text = "";

        //2
        if (slot2Card != null)
            tSlot2Level.text = "Lv. " + slot2Card.currentLevel;
        else tSlot2Level.text = "";

        //3
        if (slot3Card != null)
            tSlot3Level.text = "Lv. " + slot3Card.currentLevel;
        else tSlot3Level.text = "";

        //4
        if (slot4Card != null)
            tSlot4Level.text = "Lv. " + slot4Card.currentLevel;
        else tSlot4Level.text = "";

        //5
        if (slot5Card != null)
            tSlot5Level.text = "Lv. " + slot5Card.currentLevel;
        else tSlot5Level.text = "";

        //6
        if (slot6Card != null)
            tSlot6Level.text = "Lv. " + slot6Card.currentLevel;
        else tSlot6Level.text = "";
        #endregion
    }

    void Initialize()
    {       
        if (activeCarObj == null)
        {
            for (int i = 0; i < carsButtons.Count; i++)
            {
                carsButtons[i].GetComponent<CarButtonController>().Initialize();
            }

            foreach (GameObject _car in carsButtons)
            {
                if (_car.GetComponent<CarButtonController>().carName == PlayerPrefs.GetString("selectedCarID"))
                {
                    _car.GetComponent<CarButtonController>().ButSelect();
                    activeCarObj = _car.GetComponent<CarButtonController>();
                }
            }
        } 
        else
        {
            foreach (GameObject _car in carsButtons)
            {
                if (_car.GetComponent<CarButtonController>().carName == PlayerPrefs.GetString("selectedCarID"))
                {
                    activeCarObj = _car.GetComponent<CarButtonController>();
                }
            }
        }

        tCarName.text = activeCarObj.carName;
        imgCar.sprite = activeCarObj.imgCar.sprite;

        #region Car Level
        tCarLevel.text = PlayerPrefs.GetInt(activeCarObj.carName + "carLevel") + "/40";
        fillCarLevel.DOFillAmount((float)PlayerPrefs.GetInt(activeCarObj.carName + "carLevel") / 40, 0.5f);
        fillEndCarLevel.GetComponent<RectTransform>().DOAnchorPos(new Vector2((float)PlayerPrefs.GetInt(activeCarObj.carName + "carLevel") / 40 * 233f, 0), 0.5f);
        #endregion

        CalculateViewStats();

        LoadItem();
        LoadSlots();
    }

    void LoadItem()
    {
        int cellCount;
        itemMass.Clear();

        #region Gun
        cellCount = PlayerPrefs.GetInt("itemCountGun");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemGunID" + i);

            if (!PlayerPrefs.HasKey("itemGunLevel" + i))
                PlayerPrefs.SetInt("itemGunLevel" + i, 1);

            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemGunLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemGunRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemGunType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            foreach (DetailCard _item in gunItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemGunID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            itemGunInst.Add(_cell);
            itemMass.Add(_cell);
        }
        #endregion

        #region Engine
        cellCount = PlayerPrefs.GetInt("itemCountEngine");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemEngineID" + i);

            if (!PlayerPrefs.HasKey("itemEngineLevel" + i))
                PlayerPrefs.SetInt("itemEngineLevel" + i, 1);

            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemEngineLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemEngineRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemEngineType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            foreach (DetailCard _item in engineItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemEngineID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            itemEngineInst.Add(_cell);
            itemMass.Add(_cell);
        }        
        #endregion

        #region Brakes
        cellCount = PlayerPrefs.GetInt("itemCountBrakes");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemBrakesID" + i);

            if (!PlayerPrefs.HasKey("itemBrakesLevel" + i))
                PlayerPrefs.SetInt("itemBrakesLevel" + i, 1);

            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemBrakesLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemBrakesRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemBrakesType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            foreach (DetailCard _item in brakesItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemBrakesID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            itemBrakesInst.Add(_cell);
            itemMass.Add(_cell);
        }
        #endregion

        #region FuelSystem
        cellCount = PlayerPrefs.GetInt("itemCountFuelSystem");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemFuelSystemID" + i);

            if (!PlayerPrefs.HasKey("itemFuelSystemLevel" + i))
                PlayerPrefs.SetInt("itemFuelSystemLevel" + i, 1);

            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemFuelSystemLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemFuelSystemRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemFuelSystemType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            foreach (DetailCard _item in fuelSystemItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemFuelSystemID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            itemFuelSystemInst.Add(_cell);
            itemMass.Add(_cell);
        }
        #endregion

        #region Suspension
        // Suspension
        cellCount = PlayerPrefs.GetInt("itemCountSuspension");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemSuspensionID" + i);

            if (!PlayerPrefs.HasKey("itemSuspensionLevel" + i))
                PlayerPrefs.SetInt("itemSuspensionLevel" + i, 1);

            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemSuspensionLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemSuspensionRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemSuspensionType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            foreach (DetailCard _item in suspensionItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemSuspensionID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            itemSuspensionInst.Add(_cell);
            itemMass.Add(_cell);
        }
        #endregion

        #region Transmission
        // Transmission
        cellCount = PlayerPrefs.GetInt("itemCountTransmission");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemTransmissionID" + i);

            if (!PlayerPrefs.HasKey("itemTransmissionLevel" + i))
                PlayerPrefs.SetInt("itemTransmissionLevel" + i, 1);

            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemTransmissionLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemTransmissionRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemTransmissionType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            foreach (DetailCard _item in transmissionItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemTransmissionID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            itemTransmissionInst.Add(_cell);
            itemMass.Add(_cell);
        }
        #endregion

        //Legendary
        for (int i = 0; i < itemMass.Count; i++)
        {
            GameObject _cell = itemMass[i];

            if (_cell.GetComponent<ItemCell>().itemRarity == "legendary")
            {
                _cell.transform.parent = globalContentPanel.transform;
                _cell.transform.localScale = Vector3.one;
            }
        }

        //Epic
        for (int i = 0; i < itemMass.Count; i++)
        {
            GameObject _cell = itemMass[i];

            if (_cell.GetComponent<ItemCell>().itemRarity == "epic")
            {
                _cell.transform.parent = globalContentPanel.transform;
                _cell.transform.localScale = Vector3.one;
            }
        }

        //Rare
        for (int i = 0; i < itemMass.Count; i++)
        {
            GameObject _cell = itemMass[i];

            if (_cell.GetComponent<ItemCell>().itemRarity == "rare")
            {
                _cell.transform.parent = globalContentPanel.transform;
                _cell.transform.localScale = Vector3.one;
            }
        }

        //Common
        for (int i = 0; i < itemMass.Count; i++)
        {
            GameObject _cell = itemMass[i];

            if (_cell.GetComponent<ItemCell>().itemRarity == "common")
            {
                _cell.transform.parent = globalContentPanel.transform;
                _cell.transform.localScale = Vector3.one;
            }
        }
    }

    void LoadSlots()
    {
        imgIconSlot1.gameObject.SetActive(false);
        imgIconSlot2.gameObject.SetActive(false);
        imgIconSlot3.gameObject.SetActive(false);
        imgIconSlot4.gameObject.SetActive(false);
        imgIconSlot5.gameObject.SetActive(false);
        imgIconSlot6.gameObject.SetActive(false);

        if (PlayerPrefs.GetInt("slot1_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot1_itemNumInInventory"))
        {
            foreach (GameObject gm in itemGunInst)
            {
                if (gm.GetComponent<ItemCell>().itemNumInInventory == PlayerPrefs.GetInt("slot1_itemNumInInventory"))
                {
                    slot1Card = gm.GetComponent<ItemCell>();                    
                }
            }

            imgIconSlot1.gameObject.SetActive(true);

            imgIconSlot1.sprite = slot1Card.sprIcon;

            imgSlot1.sprite = slot1Card.imgCard.sprite;
            slot1Card.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("slot2_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot2_itemNumInInventory"))
        {
            foreach (GameObject gm in itemEngineInst)
            {
                if (gm.GetComponent<ItemCell>().itemNumInInventory == PlayerPrefs.GetInt("slot2_itemNumInInventory"))
                {
                    slot2Card = gm.GetComponent<ItemCell>();
                }
            }

            imgIconSlot2.gameObject.SetActive(true);

            imgIconSlot2.sprite = slot2Card.sprIcon;

            imgSlot2.sprite = slot2Card.imgCard.sprite;
            slot2Card.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("slot3_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot3_itemNumInInventory"))
        {
            foreach (GameObject gm in itemBrakesInst)
            {
                if (gm.GetComponent<ItemCell>().itemNumInInventory == PlayerPrefs.GetInt("slot3_itemNumInInventory"))
                {
                    slot3Card = gm.GetComponent<ItemCell>();
                }
            }

            imgIconSlot3.gameObject.SetActive(true);

            imgIconSlot3.sprite = slot3Card.sprIcon;

            imgSlot3.sprite = slot3Card.imgCard.sprite;
            slot3Card.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("slot4_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot4_itemNumInInventory"))
        {
            foreach (GameObject gm in itemFuelSystemInst)
            {
                if (gm.GetComponent<ItemCell>().itemNumInInventory == PlayerPrefs.GetInt("slot4_itemNumInInventory"))
                {
                    slot4Card = gm.GetComponent<ItemCell>();
                }
            }

            imgIconSlot4.gameObject.SetActive(true);

            imgIconSlot4.sprite = slot4Card.sprIcon;

            imgSlot4.sprite = slot4Card.imgCard.sprite;
            slot4Card.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("slot5_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot5_itemNumInInventory"))
        {
            foreach (GameObject gm in itemSuspensionInst)
            {
                if (gm.GetComponent<ItemCell>().itemNumInInventory == PlayerPrefs.GetInt("slot5_itemNumInInventory"))
                {
                    slot5Card = gm.GetComponent<ItemCell>();
                }
            }

            imgIconSlot5.gameObject.SetActive(true);

            imgIconSlot5.sprite = slot5Card.sprIcon;

            imgSlot5.sprite = slot5Card.imgCard.sprite;
            slot5Card.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("slot6_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot6_itemNumInInventory"))
        {
            foreach (GameObject gm in itemTransmissionInst)
            {
                if (gm.GetComponent<ItemCell>().itemNumInInventory == PlayerPrefs.GetInt("slot6_itemNumInInventory"))
                {
                    slot6Card = gm.GetComponent<ItemCell>();
                }
            }

            imgIconSlot6.gameObject.SetActive(true);

            imgIconSlot6.sprite = slot6Card.sprIcon;

            imgSlot6.sprite = slot6Card.imgCard.sprite;
            slot6Card.gameObject.SetActive(false);
        }
    }

    void SaveItem()
    {
        int cellCount = 0;

        //Оружие
        foreach (GameObject _item in itemGunInst)
        {
            PlayerPrefs.SetInt("itemGunID" + cellCount, _item.GetComponent<ItemCell>().itemID);
            PlayerPrefs.SetInt("itemGunLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
            PlayerPrefs.SetString("itemGunRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
            PlayerPrefs.SetString("itemGunType" + cellCount, _item.GetComponent<ItemCell>().itemType);
            cellCount++;
        }

        foreach (GameObject gm in itemGunInst)
        {
            Destroy(gm);
        }

        itemGunInst.Clear();

        //Engine
        cellCount = 0;

        foreach (GameObject _item in itemEngineInst)
        {
            PlayerPrefs.SetInt("itemEngineID" + cellCount, _item.GetComponent<ItemCell>().itemID);
            PlayerPrefs.SetInt("itemEngineLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
            PlayerPrefs.SetString("itemEngineRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
            PlayerPrefs.SetString("itemEngineType" + cellCount, _item.GetComponent<ItemCell>().itemType);
            cellCount++;
        }

        foreach (GameObject gm in itemEngineInst)
        {
            Destroy(gm);
        }

        itemEngineInst.Clear();

        //Brakes
        cellCount = 0;

        foreach (GameObject _item in itemBrakesInst)
        {
            PlayerPrefs.SetInt("itemBrakesID" + cellCount, _item.GetComponent<ItemCell>().itemID);
            PlayerPrefs.SetInt("itemBrakesLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
            PlayerPrefs.SetString("itemBrakesRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
            PlayerPrefs.SetString("itemBrakesType" + cellCount, _item.GetComponent<ItemCell>().itemType);
            cellCount++;
        }

        foreach (GameObject gm in itemBrakesInst)
        {
            Destroy(gm);
        }

        itemBrakesInst.Clear();

        //FuelSystem
        cellCount = 0;

        foreach (GameObject _item in itemFuelSystemInst)
        {
            PlayerPrefs.SetInt("itemFuelSystemID" + cellCount, _item.GetComponent<ItemCell>().itemID);
            PlayerPrefs.SetInt("itemFuelSystemLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
            PlayerPrefs.SetString("itemFuelSystemRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
            PlayerPrefs.SetString("itemFuelSystemType" + cellCount, _item.GetComponent<ItemCell>().itemType);
            cellCount++;
        }

        foreach (GameObject gm in itemFuelSystemInst)
        {
            Destroy(gm);
        }

        itemFuelSystemInst.Clear();

        //Suspension
        cellCount = 0;

        foreach (GameObject _item in itemSuspensionInst)
        {
            PlayerPrefs.SetInt("itemSuspensionID" + cellCount, _item.GetComponent<ItemCell>().itemID);
            PlayerPrefs.SetInt("itemSuspensionLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
            PlayerPrefs.SetString("itemSuspensionRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
            PlayerPrefs.SetString("itemSuspensionType" + cellCount, _item.GetComponent<ItemCell>().itemType);
            cellCount++;
        }

        foreach (GameObject gm in itemSuspensionInst)
        {
            Destroy(gm);
        }

        itemSuspensionInst.Clear();

        //Transmission
        cellCount = 0;

        foreach (GameObject _item in itemTransmissionInst)
        {
            PlayerPrefs.SetInt("itemTransmissionID" + cellCount, _item.GetComponent<ItemCell>().itemID);
            PlayerPrefs.SetInt("itemTransmissionLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
            PlayerPrefs.SetString("itemTransmissionRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
            PlayerPrefs.SetString("itemTransmissionType" + cellCount, _item.GetComponent<ItemCell>().itemType);
            cellCount++;
        }

        foreach (GameObject gm in itemTransmissionInst)
        {
            Destroy(gm);
        }

        itemTransmissionInst.Clear();
    }

    public void ButChangeCar()
    {
        canvasChangeCar.GetComponent<PopUpController>().OpenPopUp();
        StartCoroutine(OffGarage());
    }

    IEnumerator OffGarage()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }

    public void ItemSelect()
    {
        switch (activeItem.slotNum)
        {
            case 1:
                imgIconSlot1.gameObject.SetActive(true);
                imgSlot1.sprite = activeItem.imgCard.sprite;
                imgIconSlot1.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("GunIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("GunRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("GunLevelSelect", activeItem.currentLevel);
                break;

            case 2:
                imgIconSlot2.gameObject.SetActive(true);
                imgSlot2.sprite = activeItem.imgCard.sprite;
                imgIconSlot2.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("EngineIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("EngineRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("EngineLevelSelect", activeItem.currentLevel);
                break;

            case 3:
                imgIconSlot3.gameObject.SetActive(true);
                imgSlot3.sprite = activeItem.imgCard.sprite;
                imgIconSlot3.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("BrakesIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("BrakesRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("BrakesLevelSelect", activeItem.currentLevel);
                break;

            case 4:
                imgIconSlot4.gameObject.SetActive(true);
                imgSlot4.sprite = activeItem.imgCard.sprite;
                imgIconSlot4.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("FuelSystemIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("FuelSystemRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("FuelSystemLevelSelect", activeItem.currentLevel);
                break;

            case 5:
                imgIconSlot5.gameObject.SetActive(true);
                imgSlot5.sprite = activeItem.imgCard.sprite;
                imgIconSlot5.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("SuspensionIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("SuspensionRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("SuspensionLevelSelect", activeItem.currentLevel);
                break;

            case 6:
                imgIconSlot6.gameObject.SetActive(true);
                imgSlot6.sprite = activeItem.imgCard.sprite;
                imgIconSlot6.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("TransmissionIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("TransmissionRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("TransmissionLevelSelect", activeItem.currentLevel);
                break;
        }
    }

    public void ButItemSlotOpen(int slotNum)
    {
        if (slotNum == 1)
        {
            if (PlayerPrefs.GetInt("slot1_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot1_itemNumInInventory"))
            {
                slot1Card.gameObject.SetActive(true);

                activeItem = slot1Card;
                popUpDetail.sprRarity = slot1Card.imgCard.sprite;
                popUpDetail.itemIcon = slot1Card.sprIcon;
                popUpDetail.itemName = slot1Card.itemName;
                popUpDetail.itemRarity = slot1Card.itemRarity;
                popUpDetail.isSlotItem = true;

                slot1Card.gameObject.SetActive(false);

                popUpDetail.ButOpen();
            }
        }

        if (slotNum == 2)
        {
            if (PlayerPrefs.GetInt("slot2_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot2_itemNumInInventory"))
            {
                slot2Card.gameObject.SetActive(true);

                activeItem = slot2Card;
                popUpDetail.sprRarity = slot2Card.imgCard.sprite;
                popUpDetail.itemIcon = slot2Card.sprIcon;
                popUpDetail.itemName = slot2Card.itemName;
                popUpDetail.itemRarity = slot2Card.itemRarity;
                popUpDetail.isSlotItem = true;

                slot2Card.gameObject.SetActive(false);

                popUpDetail.ButOpen();
            }
        }

        if (slotNum == 3)
        {
            if (PlayerPrefs.GetInt("slot3_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot3_itemNumInInventory"))
            {
                slot3Card.gameObject.SetActive(true);

                activeItem = slot3Card;
                popUpDetail.sprRarity = slot3Card.imgCard.sprite;
                popUpDetail.itemIcon = slot3Card.sprIcon;
                popUpDetail.itemName = slot3Card.itemName;
                popUpDetail.itemRarity = slot3Card.itemRarity;
                popUpDetail.isSlotItem = true;

                slot3Card.gameObject.SetActive(false);

                popUpDetail.ButOpen();
            }
        }

        if (slotNum == 4)
        {
            if (PlayerPrefs.GetInt("slot4_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot4_itemNumInInventory"))
            {
                slot4Card.gameObject.SetActive(true);

                activeItem = slot4Card;
                popUpDetail.sprRarity = slot4Card.imgCard.sprite;
                popUpDetail.itemIcon = slot4Card.sprIcon;
                popUpDetail.itemName = slot4Card.itemName;
                popUpDetail.itemRarity = slot4Card.itemRarity;
                popUpDetail.isSlotItem = true;

                slot4Card.gameObject.SetActive(false);

                popUpDetail.ButOpen();
            }
        }

        if (slotNum == 5)
        {
            if (PlayerPrefs.GetInt("slot5_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot5_itemNumInInventory"))
            {
                slot5Card.gameObject.SetActive(true);

                activeItem = slot5Card;
                popUpDetail.sprRarity = slot5Card.imgCard.sprite;
                popUpDetail.itemIcon = slot5Card.sprIcon;
                popUpDetail.itemName = slot5Card.itemName;
                popUpDetail.itemRarity = slot5Card.itemRarity;
                popUpDetail.isSlotItem = true;

                slot5Card.gameObject.SetActive(false);

                popUpDetail.ButOpen();
            }
        }

        if (slotNum == 6)
        {
            if (PlayerPrefs.GetInt("slot6_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot6_itemNumInInventory"))
            {
                slot6Card.gameObject.SetActive(true);

                activeItem = slot6Card;
                popUpDetail.sprRarity = slot6Card.imgCard.sprite;
                popUpDetail.itemIcon = slot6Card.sprIcon;
                popUpDetail.itemName = slot6Card.itemName;
                popUpDetail.itemRarity = slot6Card.itemRarity;
                popUpDetail.isSlotItem = true;

                slot6Card.gameObject.SetActive(false);

                popUpDetail.ButOpen();
            }
        }
    }

    public void ItemUnselect()
    {
        string s = "";

        switch (activeItem.slotNum)
        {
            case 1:
                slot1Card.gameObject.SetActive(true);
                slot1Card = null;

                PlayerPrefs.SetInt("slot1_itemNumInInventory", -1);

                PlayerPrefs.SetInt("gunIDSelect", 0);

                imgSlot1.sprite = sprSlotDefault;
                imgIconSlot1.gameObject.SetActive(false);

                #region ClearSave
                s = "Gun";

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
                #endregion
                break;

            case 2:
                slot2Card.gameObject.SetActive(true);
                slot2Card = null;

                PlayerPrefs.SetInt("slot2_itemNumInInventory", -1);

                imgSlot2.sprite = sprSlotDefault;
                imgIconSlot2.gameObject.SetActive(false);

                #region ClearSave
                s = "Engine";

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
                #endregion
                break;

            case 3:
                slot3Card.gameObject.SetActive(true);
                slot3Card = null;

                PlayerPrefs.SetInt("slot3_itemNumInInventory", -1);

                imgSlot3.sprite = sprSlotDefault;
                imgIconSlot3.gameObject.SetActive(false);

                #region ClearSave
                s = "Brakes";

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
                #endregion
                break;

            case 4:
                slot4Card.gameObject.SetActive(true);
                slot4Card = null;

                PlayerPrefs.SetInt("slot4_itemNumInInventory", -1);

                imgSlot4.sprite = sprSlotDefault;
                imgIconSlot4.gameObject.SetActive(false);

                #region ClearSave
                s = "FuelSystem";

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
                #endregion
                break;

            case 5:
                slot5Card.gameObject.SetActive(true);
                slot5Card = null;

                PlayerPrefs.SetInt("slot5_itemNumInInventory", -1);

                imgSlot5.sprite = sprSlotDefault;
                imgIconSlot5.gameObject.SetActive(false);

                #region ClearSave
                s = "Suspension";

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
                #endregion
                break;

            case 6:
                slot6Card.gameObject.SetActive(true);
                slot6Card = null;

                PlayerPrefs.SetInt("slot6_itemNumInInventory", -1);

                imgSlot6.sprite = sprSlotDefault;
                imgIconSlot6.gameObject.SetActive(false);

                #region ClearSave
                s = "Transmission";

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
                #endregion
                break;
        }

        CalculateViewStats();
    }

    public void CalculateViewStats()
    {
        float _damage = PlayerPrefs.GetFloat(activeCarObj.carName + "carDamage") 
                        + PlayerPrefs.GetFloat("carGlobalCoeffdamage") 
                        + PlayerPrefs.GetFloat("GunSelectDamage") 
                        + PlayerPrefs.GetFloat("EngineSelectDamage") 
                        + PlayerPrefs.GetFloat("BrakesSelectDamage")
                        + PlayerPrefs.GetFloat("FuelSystemSelectDamage") 
                        + PlayerPrefs.GetFloat("SuspensionSelectDamage")
                        + PlayerPrefs.GetFloat("TransmissionSelectDamage");
        float _health = PlayerPrefs.GetFloat(activeCarObj.carName + "carHealth") 
                        + PlayerPrefs.GetFloat("carGlobalCoeffhealth")
                        + PlayerPrefs.GetFloat("GunSelectHealth")
                        + PlayerPrefs.GetFloat("EngineSelectHealth")
                        + PlayerPrefs.GetFloat("BrakesSelectHealth")
                        + PlayerPrefs.GetFloat("FuelSystemSelectHealth")
                        + PlayerPrefs.GetFloat("SuspensionSelectHealth")
                        + PlayerPrefs.GetFloat("TransmissionSelectHealth");

        tDamage.text = "+" + _damage + "%";
        tHealth.text = _health + "";
    }
}
