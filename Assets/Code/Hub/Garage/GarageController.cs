using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PanelCharacteristics;

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
    public GameObject slot1Silhouette;
    public GameObject slot2Silhouette;
    public GameObject slot3Silhouette;
    public GameObject slot4Silhouette;
    public GameObject slot5Silhouette;
    public GameObject slot6Silhouette;

    [Header("Slots Card")]
    public ItemCell slot1Card;
    public ItemCell slot2Card;
    public ItemCell slot3Card;
    public ItemCell slot4Card;
    public ItemCell slot5Card;
    public ItemCell slot6Card;

    [Header("Slots Level")]
    public GameObject slot1LevelPanel;
    public GameObject slot2LevelPanel;
    public GameObject slot3LevelPanel;
    public GameObject slot4LevelPanel;
    public GameObject slot5LevelPanel;
    public GameObject slot6LevelPanel;
    public TMP_Text tSlot1Level;
    public TMP_Text tSlot2Level;
    public TMP_Text tSlot3Level;
    public TMP_Text tSlot4Level;
    public TMP_Text tSlot5Level;
    public TMP_Text tSlot6Level;

    public List<GameObject> itemMass;

    [Header("Arrow")]
    public GameObject objArrow1;
    public GameObject objArrow2;
    public GameObject objArrow3;
    public GameObject objArrow4;
    public GameObject objArrow5;
    public GameObject objArrow6;
    public GameObject objArrowCar;

    public PopUpCarUpgrade popUpCarUpgrade;


    private void OnEnable()
    {
        //Initialize();
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
        {
            slot1LevelPanel.SetActive(true);
            tSlot1Level.text = "Lv. " + slot1Card.currentLevel;
            slot1Silhouette.SetActive(false);
        }
        else
        {
            slot1LevelPanel.SetActive(false);
            tSlot1Level.text = "";
            slot1Silhouette.SetActive(true);
        }

        //2
        if (slot2Card != null)
        {
            slot2LevelPanel.SetActive(true);
            tSlot2Level.text = "Lv. " + slot2Card.currentLevel;
            slot2Silhouette.SetActive(false);
        }            
        else
        {
            slot2LevelPanel.SetActive(false);
            tSlot2Level.text = "";
            slot2Silhouette.SetActive(true);
        }            

        //3
        if (slot3Card != null)
        {
            slot3LevelPanel.SetActive(true);
            tSlot3Level.text = "Lv. " + slot3Card.currentLevel;
            slot3Silhouette.SetActive(false);
        }            
        else
        {
            slot3LevelPanel.SetActive(false);
            tSlot3Level.text = "";
            slot3Silhouette.SetActive(true);
        }            

        //4
        if (slot4Card != null)
        {
            slot4LevelPanel.SetActive(true);
            tSlot4Level.text = "Lv. " + slot4Card.currentLevel;
            slot4Silhouette.SetActive(false);
        }            
        else
        {
            slot4LevelPanel.SetActive(false);
            tSlot4Level.text = "";
            slot4Silhouette.SetActive(true);
        }            

        //5
        if (slot5Card != null)
        {
            slot5LevelPanel.SetActive(true);
            tSlot5Level.text = "Lv. " + slot5Card.currentLevel;
            slot5Silhouette.SetActive(false);
        }            
        else
        {
            slot5LevelPanel.SetActive(false);
            tSlot5Level.text = "";
            slot5Silhouette.SetActive(true);
        }            

        //6
        if (slot6Card != null)
        {
            slot6LevelPanel.SetActive(true);
            tSlot6Level.text = "Lv. " + slot6Card.currentLevel;
            slot6Silhouette.SetActive(false);
        }            
        else
        {
            slot6LevelPanel.SetActive(false);
            tSlot6Level.text = "";
            slot6Silhouette.SetActive(true);
        }            
        #endregion
    }

    public void Initialize()
    {
        Debug.Log(PlayerPrefs.GetInt("itemCountGun"));

        if (activeCarObj == null)
        {
            //for (int i = 0; i < carsButtons.Count; i++)
            //{
            //    carsButtons[i].GetComponent<CarButtonController>().Initialize();
            //}

            for (int i = 0; i < carsButtons.Count; i++)
            {
                carsButtons[i].GetComponent<CarButtonController>().Initialize();

                if (carsButtons[i].GetComponent<CarButtonController>().carName == PlayerPrefs.GetString("selectedCarID"))
                {
                    carsButtons[i].GetComponent<CarButtonController>().ButSelect();
                    activeCarObj = carsButtons[i].GetComponent<CarButtonController>();
                }
            }

            //foreach (GameObject _car in carsButtons)
            //{
            //    Debug.Log("Car Name = " + _car.name);

            //    _car.GetComponent<CarButtonController>().Initialize();

            //    if (_car.GetComponent<CarButtonController>().carName == PlayerPrefs.GetString("selectedCarID"))
            //    {
            //        _car.GetComponent<CarButtonController>().ButSelect();
            //        activeCarObj = _car.GetComponent<CarButtonController>();
            //    }
            //}
        } 
        else
        {
            for (int i = 0; i < carsButtons.Count; i++)
            {
                if (carsButtons[i].GetComponent<CarButtonController>().carName == PlayerPrefs.GetString("selectedCarID"))
                {
                    activeCarObj = carsButtons[i].GetComponent<CarButtonController>();
                }
            }

            //foreach (GameObject _car in carsButtons)
            //{
            //    Debug.Log("Car Name = " + _car.name);

            //    if (_car.GetComponent<CarButtonController>().carName == PlayerPrefs.GetString("selectedCarID"))
            //    {
            //        activeCarObj = _car.GetComponent<CarButtonController>();
            //    }
            //}
        }

        tCarName.text = activeCarObj.carName;
        imgCar.sprite = activeCarObj.imgCar.sprite;

        #region Car Level
        tCarLevel.text = PlayerPrefs.GetInt(activeCarObj.carName + "carLevel") + "/40";
        fillCarLevel.DOFillAmount((float)PlayerPrefs.GetInt(activeCarObj.carName + "carLevel") / 40, 0.5f);
        fillEndCarLevel.GetComponent<RectTransform>().DOAnchorPos(new Vector2((float)PlayerPrefs.GetInt(activeCarObj.carName + "carLevel") / 40 * 233f, 0), 0.5f);
        #endregion

        //popUpDetail.SaveDetailStats("Gun");

        CalculateViewStats();

        LoadItem();
        LoadSlots();

        CheckArrowUpgrade();
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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Inventory;

            foreach (DetailCard _item in gunItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemGunID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            popUpDetail.CreateStatsSave(_cell.GetComponent<ItemCell>().itemObj, _cell.GetComponent<ItemCell>());

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Inventory;

            foreach (DetailCard _item in engineItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemEngineID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            popUpDetail.CreateStatsSave(_cell.GetComponent<ItemCell>().itemObj, _cell.GetComponent<ItemCell>());

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Inventory;

            foreach (DetailCard _item in brakesItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemBrakesID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            popUpDetail.CreateStatsSave(_cell.GetComponent<ItemCell>().itemObj, _cell.GetComponent<ItemCell>());

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Inventory;

            foreach (DetailCard _item in fuelSystemItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemFuelSystemID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            popUpDetail.CreateStatsSave(_cell.GetComponent<ItemCell>().itemObj, _cell.GetComponent<ItemCell>());

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Inventory;

            foreach (DetailCard _item in suspensionItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemSuspensionID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            popUpDetail.CreateStatsSave(_cell.GetComponent<ItemCell>().itemObj, _cell.GetComponent<ItemCell>());

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Inventory;

            foreach (DetailCard _item in transmissionItem)
            {
                if (_item.itemID == PlayerPrefs.GetInt("itemTransmissionID" + i))
                {
                    _cell.GetComponent<ItemCell>().itemObj = _item;
                }
            }

            _cell.GetComponent<ItemCell>().Initialize();

            popUpDetail.CreateStatsSave(_cell.GetComponent<ItemCell>().itemObj, _cell.GetComponent<ItemCell>());

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

    public IEnumerator OffGarage()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }

    public void ItemSelect()
    {
        string _detailType = "";
        int _detailID = activeItem.itemID;
        int _detailLevel = activeItem.currentLevel;
        string _detailRare = activeItem.itemRarity;

        switch (activeItem.slotNum)
        {
            case 1:
                imgIconSlot1.gameObject.SetActive(true);
                imgSlot1.sprite = activeItem.imgCard.sprite;
                imgIconSlot1.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("GunIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("GunRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("GunLevelSelect", activeItem.currentLevel);

                _detailType = "Gun";
                break;

            case 2:
                imgIconSlot2.gameObject.SetActive(true);
                imgSlot2.sprite = activeItem.imgCard.sprite;
                imgIconSlot2.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("EngineIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("EngineRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("EngineLevelSelect", activeItem.currentLevel);

                _detailType = "Engine";
                break;

            case 3:
                imgIconSlot3.gameObject.SetActive(true);
                imgSlot3.sprite = activeItem.imgCard.sprite;
                imgIconSlot3.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("BrakesIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("BrakesRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("BrakesLevelSelect", activeItem.currentLevel);

                _detailType = "Brakes";
                break;

            case 4:
                imgIconSlot4.gameObject.SetActive(true);
                imgSlot4.sprite = activeItem.imgCard.sprite;
                imgIconSlot4.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("FuelSystemIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("FuelSystemRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("FuelSystemLevelSelect", activeItem.currentLevel);

                _detailType = "FuelSystem";
                break;

            case 5:
                imgIconSlot5.gameObject.SetActive(true);
                imgSlot5.sprite = activeItem.imgCard.sprite;
                imgIconSlot5.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("SuspensionIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("SuspensionRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("SuspensionLevelSelect", activeItem.currentLevel);

                _detailType = "Suspension";
                break;

            case 6:
                imgIconSlot6.gameObject.SetActive(true);
                imgSlot6.sprite = activeItem.imgCard.sprite;
                imgIconSlot6.sprite = activeItem.imgIcon.sprite;

                PlayerPrefs.SetInt("TransmissionIDSelect", activeItem.itemID);
                PlayerPrefs.SetString("TransmissionRaritySelect", activeItem.itemRarity);
                PlayerPrefs.SetInt("TransmissionLevelSelect", activeItem.currentLevel);

                _detailType = "Transmission";
                break;
        }

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_DetailApply(_detailType, _detailID, _detailLevel, _detailRare);
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
        string _detailType = "";
        int _detailID = activeItem.itemID;
        int _detailLevel = activeItem.currentLevel;
        string _detailRare = activeItem.itemRarity;

        string s = "";

        switch (activeItem.slotNum)
        {
            case 1:
                if (slot1Card != null)
                {
                    slot1Card.gameObject.SetActive(true);
                    slot1Card = null;
                }                

                PlayerPrefs.SetInt("slot1_itemNumInInventory", -1);
                PlayerPrefs.SetInt("GunIDSelect", 0);

                imgSlot1.sprite = sprSlotDefault;
                imgIconSlot1.gameObject.SetActive(false);

                _detailType = "Gun";

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
                if (slot2Card != null)
                {
                    slot2Card.gameObject.SetActive(true);
                    slot2Card = null;
                }                    

                PlayerPrefs.SetInt("slot2_itemNumInInventory", -1);
                PlayerPrefs.SetInt("slot2_itemID", 0);

                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().setID = activeItem.itemObj.setID;
                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().CheckSet();

                imgSlot2.sprite = sprSlotDefault;
                imgIconSlot2.gameObject.SetActive(false);

                _detailType = "Engine";

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
                if (slot3Card != null)
                {
                    slot3Card.gameObject.SetActive(true);
                    slot3Card = null;
                }                    

                PlayerPrefs.SetInt("slot3_itemNumInInventory", -1);
                PlayerPrefs.SetInt("slot3_itemID", 0);

                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().setID = activeItem.itemObj.setID;
                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().CheckSet();

                imgSlot3.sprite = sprSlotDefault;
                imgIconSlot3.gameObject.SetActive(false);

                _detailType = "Brakes";

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
                if (slot4Card != null)
                {
                    slot4Card.gameObject.SetActive(true);
                    slot4Card = null;
                }                    

                PlayerPrefs.SetInt("slot4_itemNumInInventory", -1);
                PlayerPrefs.SetInt("slot4_itemID", 0);

                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().setID = activeItem.itemObj.setID;
                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().CheckSet();

                imgSlot4.sprite = sprSlotDefault;
                imgIconSlot4.gameObject.SetActive(false);

                _detailType = "FuelSystem";

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
                if (slot5Card != null)
                {
                    slot5Card.gameObject.SetActive(true);
                    slot5Card = null;
                }                    

                PlayerPrefs.SetInt("slot5_itemNumInInventory", -1);
                PlayerPrefs.SetInt("slot5_itemID", 0);

                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().setID = activeItem.itemObj.setID;
                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().CheckSet();

                imgSlot5.sprite = sprSlotDefault;
                imgIconSlot5.gameObject.SetActive(false);

                _detailType = "Suspension";

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
                if (slot6Card != null)
                {
                    slot6Card.gameObject.SetActive(true);
                    slot6Card = null;
                }                    

                PlayerPrefs.SetInt("slot6_itemNumInInventory", -1);
                PlayerPrefs.SetInt("slot6_itemID", 0);

                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().setID = activeItem.itemObj.setID;
                GameObject.Find("PopUp Set").GetComponent<PopUpSet>().CheckSet();

                imgSlot6.sprite = sprSlotDefault;
                imgIconSlot6.gameObject.SetActive(false);

                _detailType = "Transmission";

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

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_DetailTakeOff(_detailType, _detailID, _detailLevel, _detailRare);

        CalculateViewStats();

        CheckArrowUpgrade();
    }

    public void CalculateViewStats()
    {
        Debug.Log("Calculate Stats");

        float _damage = PlayerPrefs.GetFloat(activeCarObj.carName + "carDamage") 
                        + PlayerPrefs.GetFloat("carGlobalCoeffdamage") 
                        + PlayerPrefs.GetFloat("GunSelectDamage") 
                        + PlayerPrefs.GetFloat("EngineSelectDamage") 
                        + PlayerPrefs.GetFloat("BrakesSelectDamage")
                        + PlayerPrefs.GetFloat("FuelSystemSelectDamage") 
                        + PlayerPrefs.GetFloat("SuspensionSelectDamage")
                        + PlayerPrefs.GetFloat("TransmissionSelectDamage")
                        + PlayerPrefs.GetInt("talentDamageCurrentValue");

        PlayerPrefs.SetFloat("CurrentAllDamage", _damage);

        float _health = PlayerPrefs.GetFloat(activeCarObj.carName + "carHealth") 
                        + PlayerPrefs.GetFloat("carGlobalCoeffhealth")
                        + PlayerPrefs.GetFloat("GunSelectHealth")
                        + PlayerPrefs.GetFloat("EngineSelectHealth")
                        + PlayerPrefs.GetFloat("BrakesSelectHealth")
                        + PlayerPrefs.GetFloat("FuelSystemSelectHealth")
                        + PlayerPrefs.GetFloat("SuspensionSelectHealth")
                        + PlayerPrefs.GetFloat("TransmissionSelectHealth")
                        + PlayerPrefs.GetInt("talentHealthCurrentValue");

        PlayerPrefs.SetFloat("CurrentAllHealth", _health);

        tDamage.text = "+" + _damage + "%";
        tHealth.text = _health + "";
    }

    public void CheckArrowUpgrade()
    {
        #region Slot 1
        if (slot1Card != null)
        {
            int maxLevel = 10;

            switch (slot1Card.itemRarity)
            {
                case "common":
                    maxLevel = 10;
                    break;

                case "rare":
                    maxLevel = 20;
                    break;

                case "epic":
                    maxLevel = 30;
                    break;

                case "legendary":
                    maxLevel = 40;
                    break;
            }

            int currentDrawing = PlayerPrefs.GetInt("drawingGunCount");

            if (PlayerPrefs.GetInt("item" + "Gun" + "Level" + slot1Card.itemNumInInventory) < maxLevel)
            {
                if (PlayerPrefs.GetInt("playerMoney") >= popUpDetail.upgradePrice[slot1Card.currentLevel] &&
                    currentDrawing >= popUpDetail.drawingCount[slot1Card.currentLevel] && 
                    PlayerPrefs.GetInt("item" + "Gun" + "Level" + slot1Card.itemNumInInventory) < PlayerPrefs.GetInt("playerLevel"))
                {
                    objArrow1.SetActive(true);
                }
                else
                {
                    objArrow1.SetActive(false);
                }
            }
            else
            {
                objArrow1.SetActive(false);
            }
        }
        else
        {
            objArrow1.SetActive(false);
        }
        #endregion

        #region Slot 2
        if (slot2Card != null)
        {
            int maxLevel = 10;

            switch (slot2Card.itemRarity)
            {
                case "common":
                    maxLevel = 10;
                    break;

                case "rare":
                    maxLevel = 20;
                    break;

                case "epic":
                    maxLevel = 30;
                    break;

                case "legendary":
                    maxLevel = 40;
                    break;
            }

            int currentDrawing = PlayerPrefs.GetInt("drawingEngineCount");

            if (PlayerPrefs.GetInt("item" + "Engine" + "Level" + slot2Card.itemNumInInventory) < maxLevel)
            {
                if (PlayerPrefs.GetInt("playerMoney") >= popUpDetail.upgradePrice[slot2Card.currentLevel] &&
                    currentDrawing >= popUpDetail.drawingCount[slot2Card.currentLevel] &&
                    PlayerPrefs.GetInt("item" + "Engine" + "Level" + slot2Card.itemNumInInventory) < PlayerPrefs.GetInt("playerLevel"))
                {
                    objArrow2.SetActive(true);
                }
                else
                {
                    objArrow2.SetActive(false);
                }
            }
            else
            {
                objArrow2.SetActive(false);
            }
        }
        else
        {
            objArrow2.SetActive(false);
        }
        #endregion

        #region Slot 3
        if (slot3Card != null)
        {
            int maxLevel = 10;

            switch (slot3Card.itemRarity)
            {
                case "common":
                    maxLevel = 10;
                    break;

                case "rare":
                    maxLevel = 20;
                    break;

                case "epic":
                    maxLevel = 30;
                    break;

                case "legendary":
                    maxLevel = 40;
                    break;
            }

            int currentDrawing = PlayerPrefs.GetInt("drawingBrakesCount");

            if (PlayerPrefs.GetInt("item" + "Brakes" + "Level" + slot3Card.itemNumInInventory) < maxLevel)
            {
                if (PlayerPrefs.GetInt("playerMoney") >= popUpDetail.upgradePrice[slot3Card.currentLevel] &&
                    currentDrawing >= popUpDetail.drawingCount[slot3Card.currentLevel] &&
                    PlayerPrefs.GetInt("item" + "Brakes" + "Level" + slot3Card.itemNumInInventory) < PlayerPrefs.GetInt("playerLevel"))
                {
                    objArrow3.SetActive(true);
                }
                else
                {
                    objArrow3.SetActive(false);
                }
            }
            else
            {
                objArrow3.SetActive(false);
            }
        }
        else
        {
            objArrow3.SetActive(false);
        }
        #endregion

        #region Slot 4
        if (slot4Card != null)
        {
            int maxLevel = 10;

            switch (slot4Card.itemRarity)
            {
                case "common":
                    maxLevel = 10;
                    break;

                case "rare":
                    maxLevel = 20;
                    break;

                case "epic":
                    maxLevel = 30;
                    break;

                case "legendary":
                    maxLevel = 40;
                    break;
            }

            int currentDrawing = PlayerPrefs.GetInt("drawingFuelSystemCount");

            if (PlayerPrefs.GetInt("item" + "FuelSystem" + "Level" + slot4Card.itemNumInInventory) < maxLevel)
            {
                if (PlayerPrefs.GetInt("playerMoney") >= popUpDetail.upgradePrice[slot4Card.currentLevel] &&
                    currentDrawing >= popUpDetail.drawingCount[slot4Card.currentLevel] &&
                    PlayerPrefs.GetInt("item" + "FuelSystem" + "Level" + slot4Card.itemNumInInventory) < PlayerPrefs.GetInt("playerLevel"))
                {
                    objArrow4.SetActive(true);
                }
                else
                {
                    objArrow4.SetActive(false);
                }
            }
            else
            {
                objArrow4.SetActive(false);
            }
        }
        else
        {
            objArrow4.SetActive(false);
        }
        #endregion

        #region Slot 5
        if (slot5Card != null)
        {
            int maxLevel = 10;

            switch (slot5Card.itemRarity)
            {
                case "common":
                    maxLevel = 10;
                    break;

                case "rare":
                    maxLevel = 20;
                    break;

                case "epic":
                    maxLevel = 30;
                    break;

                case "legendary":
                    maxLevel = 40;
                    break;
            }

            int currentDrawing = PlayerPrefs.GetInt("drawingSuspensionCount");

            if (PlayerPrefs.GetInt("item" + "Suspension" + "Level" + slot5Card.itemNumInInventory) < maxLevel)
            {
                if (PlayerPrefs.GetInt("playerMoney") >= popUpDetail.upgradePrice[slot5Card.currentLevel] &&
                    currentDrawing >= popUpDetail.drawingCount[slot5Card.currentLevel] &&
                    PlayerPrefs.GetInt("item" + "Suspension" + "Level" + slot5Card.itemNumInInventory) < PlayerPrefs.GetInt("playerLevel"))
                {
                    objArrow5.SetActive(true);
                }
                else
                {
                    objArrow5.SetActive(false);
                }
            }
            else
            {
                objArrow5.SetActive(false);
            }
        }
        else
        {
            objArrow5.SetActive(false);
        }
        #endregion

        #region Slot 6
        if (slot6Card != null)
        {
            int maxLevel = 10;

            switch (slot6Card.itemRarity)
            {
                case "common":
                    maxLevel = 10;
                    break;

                case "rare":
                    maxLevel = 20;
                    break;

                case "epic":
                    maxLevel = 30;
                    break;

                case "legendary":
                    maxLevel = 40;
                    break;
            }

            int currentDrawing = PlayerPrefs.GetInt("drawingTransmissionCount");

            if (PlayerPrefs.GetInt("item" + "Transmission" + "Level" + slot6Card.itemNumInInventory) < maxLevel)
            {
                if (PlayerPrefs.GetInt("playerMoney") >= popUpDetail.upgradePrice[slot6Card.currentLevel] &&
                    currentDrawing >= popUpDetail.drawingCount[slot6Card.currentLevel] &&
                    PlayerPrefs.GetInt("item" + "Transmission" + "Level" + slot6Card.itemNumInInventory) < PlayerPrefs.GetInt("playerLevel"))
                {
                    objArrow6.SetActive(true);
                }
                else
                {
                    objArrow6.SetActive(false);
                }
            }
            else
            {
                objArrow6.SetActive(false);
            }
        }
        else
        {
            objArrow6.SetActive(false);
        }
        #endregion

        #region Car Upgrade
        bool arrowAccept = false;

        string _carName = "";

        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
                _carName = "Dionysus";

            if (i == 1)
                _carName = "Lyssa";

            if (i == 2)
                _carName = "Taiowa";

            if (i == 3)
                _carName = "P-Run";

            if (i == 4)
                _carName = "Aeolus";

            if (i == 5)
                _carName = "Hyas";

            if (i == 6)
                _carName = "Hemera";

            if (i == 7)
                _carName = "Eos";

            if (PlayerPrefs.GetInt(_carName + "carPurchased") == 1)
            {
                if (PlayerPrefs.GetInt(_carName + "carLevel") < 40 &&
                    PlayerPrefs.GetInt("playerTitan") >= popUpCarUpgrade.titanCount[PlayerPrefs.GetInt(_carName + "carLevel")] &&
                    PlayerPrefs.GetInt("playerLevel") > PlayerPrefs.GetInt(_carName + "carLevel") &&
                    PlayerPrefs.GetInt("playerMoney") >= popUpCarUpgrade.upgradePrice[PlayerPrefs.GetInt(_carName + "carLevel")])
                {
                    arrowAccept = true;
                }
            }

            if (arrowAccept)
            {
                objArrowCar.SetActive(true);
            } 
            else
            {
                objArrowCar.SetActive(false);
            }
        }

        #endregion
    }    
}
