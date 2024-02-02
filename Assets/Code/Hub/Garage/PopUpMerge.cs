using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DetailCard;
using static PanelCharacteristics;

public class PopUpMerge : MonoBehaviour
{
    public GarageController garageController;
    public GameObject canvasGarage;
    PopUpController _popUpController;
    public GameObject panelMerge;
    public GameObject panelRepair;
    public string panelActive;
    PopUpRepair _popUpRepair;

    [Header("Merge Panel")]
    public Image imgSlot1;
    public Image imgSlot2;
    public Image imgSlot3;
    public Image imgSlotFinal;

    public Image iconSlot1;
    public Image iconSlot2;
    public Image iconSlot3;
    public Image iconSlotFinal;

    public bool isSlot1full;
    public bool isSlot2full;
    public bool isSlot3full;

    public Sprite sprItemRare;
    public Sprite sprItemEpic;
    public Sprite sprItemLegendary;

    public ItemCell itemCellGeneral;
    public ItemCell itemCell1;
    public ItemCell itemCell2;
    public ItemCell itemCell3;

    public TMP_Text tLevelCell1;
    public TMP_Text tLevelCell2;
    public TMP_Text tLevelCell3;
    public TMP_Text tLevelCellFinal;
    public GameObject levelCell1TextPanel;
    public GameObject levelCell2TextPanel;
    public GameObject levelCell3TextPanel;
    public GameObject levelCellFinalTextPanel;
    private int maxLevel;  //Содержит самый высокий уровень среди выбранных итемов

    [Header("Buttons Panel")]
    public Sprite sprActiveButton;
    public Sprite sprPassiveButton;
    public Image imgButtonMerge;
    public Image imgButtonRepair;

    [Header("Scroll Gun")]
    public Transform globalContentPanel;
    public List<GameObject> itemGunInst;
    public List<GameObject> itemEngineInst;
    public List<GameObject> itemBrakesInst;
    public List<GameObject> itemFuelSystemInst;
    public List<GameObject> itemSuspensionInst;
    public List<GameObject> itemTransmissionInst;
    public List<GameObject> itemMass;
    public List<GameObject> instItemMass;

    [Header("Cell")]
    public GameObject cellPrefab;

    [Header("Detail Obj")]
    public List<DetailCard> gunItem;
    public List<DetailCard> engineItem;
    public List<DetailCard> brakesItem;
    public List<DetailCard> fuelSystemItem;
    public List<DetailCard> suspensionItem;
    public List<DetailCard> transmissionItem;

    [Header("Panel New Stats")]
    public GameObject panelNewStats;
    public TMP_Text tCurrentMaxLevel;
    public TMP_Text tNextMaxLevel;
    public TMP_Text tCurrentPanelValue1;
    public TMP_Text tNextPanelValue1;
    public TMP_Text tCurrentPanelValue2;
    public TMP_Text tNextPanelValue2;

    public GameObject objPanel1, objPanel2;
    public Image imgIcon1, imgIcon2;
    public Sprite sprIconDamage, sprIconHealth;

    public GameObject butMerge;
    public GameObject butRepair;

    [Header("PopUp Merge Final")]
    public PopUpMergeFinal popUpMergeFinal;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
        _popUpRepair = GetComponent<PopUpRepair>();

        for (int i = 1; i < 3; i++)
        {
            Debug.Log("IIIIIII");
        }
    }

    void Initialize()
    {
        panelMerge.SetActive(true);
        panelRepair.SetActive(false);

        imgButtonMerge.sprite = sprActiveButton;
        imgButtonRepair.sprite = sprPassiveButton;

        panelActive = "merge";

        LoadItem();

        imgSlot1.gameObject.SetActive(false);
        imgSlot2.gameObject.SetActive(false);
        imgSlot3.gameObject.SetActive(false);
        imgSlotFinal.gameObject.SetActive(false);
        panelNewStats.SetActive(false);
        butMerge.SetActive(false);
        butRepair.SetActive(false);

        tLevelCell1.text = "";
        tLevelCell2.text = "";
        tLevelCell3.text = "";
        tLevelCellFinal.text = "";
        maxLevel = 0;

        itemCell1 = null;
        itemCell2 = null;
        itemCell3 = null;
        itemCellGeneral = null;

        isSlot1full = false;
        isSlot2full = false;
        isSlot3full = false;
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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Merge;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Merge;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Merge;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Merge;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Merge;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Merge;

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

        foreach (GameObject gm in itemMass)
        {
            if (gm.GetComponent<ItemCell>().itemRarity == "legendary")
            {
                gm.SetActive(false);
            }
        }

        for (int i = 0; i < itemMass.Count; i++)
        {
            if (itemMass[i] != null)
            {
                bool isFindPair = false;

                GameObject _cell = itemMass[i];
                instItemMass.Clear();

                for (int g = i; g < itemMass.Count; g++)
                {
                    if (itemMass[g] != null)
                    {
                        GameObject _nextCell = itemMass[g];

                        if (_nextCell.GetComponent<ItemCell>().itemID == _cell.GetComponent<ItemCell>().itemID &&
                            _nextCell.GetComponent<ItemCell>().itemRarity == _cell.GetComponent<ItemCell>().itemRarity
                            && _cell != _nextCell)
                        {
                            instItemMass.Add(_nextCell);
                            itemMass[g] = null;

                            isFindPair = true;

                            if (!instItemMass.Contains(_cell))
                            {
                                instItemMass.Add(_cell);
                            }
                        }
                    }                    
                }

                if (isFindPair)
                    itemMass[i] = null;

                foreach (GameObject gm in instItemMass)
                {
                    gm.transform.parent = globalContentPanel.transform;
                    gm.transform.localScale = Vector3.one;
                }
            }
        }

        foreach (GameObject gm in itemMass)
        {
            if (gm != null)
            {
                gm.transform.parent = globalContentPanel.transform;
                gm.transform.localScale = Vector3.one;
            }            
        }        
    }

    void SaveItem()
    {
        int cellCount = 0;

        //Оружие
        //foreach (GameObject _item in itemGunInst)
        //{
        //    PlayerPrefs.SetInt("itemGunID" + cellCount, _item.GetComponent<ItemCell>().itemID);
        //    PlayerPrefs.SetInt("itemGunLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
        //    PlayerPrefs.SetString("itemGunRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
        //    PlayerPrefs.SetString("itemGunType" + cellCount, _item.GetComponent<ItemCell>().itemType);
        //    cellCount++;
        //}

        foreach (GameObject gm in itemGunInst)
        {
            Destroy(gm);
        }

        itemGunInst.Clear();

        //Engine
        cellCount = 0;

        //foreach (GameObject _item in itemEngineInst)
        //{
        //    PlayerPrefs.SetInt("itemEngineID" + cellCount, _item.GetComponent<ItemCell>().itemID);
        //    PlayerPrefs.SetInt("itemEngineLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
        //    PlayerPrefs.SetString("itemEngineRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
        //    PlayerPrefs.SetString("itemEngineType" + cellCount, _item.GetComponent<ItemCell>().itemType);
        //    cellCount++;
        //}

        foreach (GameObject gm in itemEngineInst)
        {
            Destroy(gm);
        }

        itemEngineInst.Clear();

        //Brakes
        cellCount = 0;

        //foreach (GameObject _item in itemBrakesInst)
        //{
        //    PlayerPrefs.SetInt("itemBrakesID" + cellCount, _item.GetComponent<ItemCell>().itemID);
        //    PlayerPrefs.SetInt("itemBrakesLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
        //    PlayerPrefs.SetString("itemBrakesRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
        //    PlayerPrefs.SetString("itemBrakesType" + cellCount, _item.GetComponent<ItemCell>().itemType);
        //    cellCount++;
        //}

        foreach (GameObject gm in itemBrakesInst)
        {
            Destroy(gm);
        }

        itemBrakesInst.Clear();

        //FuelSystem
        cellCount = 0;

        //foreach (GameObject _item in itemFuelSystemInst)
        //{
        //    PlayerPrefs.SetInt("itemFuelSystemID" + cellCount, _item.GetComponent<ItemCell>().itemID);
        //    PlayerPrefs.SetInt("itemFuelSystemLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
        //    PlayerPrefs.SetString("itemFuelSystemRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
        //    PlayerPrefs.SetString("itemFuelSystemType" + cellCount, _item.GetComponent<ItemCell>().itemType);
        //    cellCount++;
        //}

        foreach (GameObject gm in itemFuelSystemInst)
        {
            Destroy(gm);
        }

        itemFuelSystemInst.Clear();

        //Suspension
        cellCount = 0;

        //foreach (GameObject _item in itemSuspensionInst)
        //{
        //    PlayerPrefs.SetInt("itemSuspensionID" + cellCount, _item.GetComponent<ItemCell>().itemID);
        //    PlayerPrefs.SetInt("itemSuspensionLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
        //    PlayerPrefs.SetString("itemSuspensionRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
        //    PlayerPrefs.SetString("itemSuspensionType" + cellCount, _item.GetComponent<ItemCell>().itemType);
        //    cellCount++;
        //}

        foreach (GameObject gm in itemSuspensionInst)
        {
            Destroy(gm);
        }

        itemSuspensionInst.Clear();

        //Transmission
        cellCount = 0;

        //foreach (GameObject _item in itemTransmissionInst)
        //{
        //    PlayerPrefs.SetInt("itemTransmissionID" + cellCount, _item.GetComponent<ItemCell>().itemID);
        //    PlayerPrefs.SetInt("itemTransmissionLevel" + cellCount, _item.GetComponent<ItemCell>().currentLevel);
        //    PlayerPrefs.SetString("itemTransmissionRarity" + cellCount, _item.GetComponent<ItemCell>().itemRarity);
        //    PlayerPrefs.SetString("itemTransmissionType" + cellCount, _item.GetComponent<ItemCell>().itemType);
        //    cellCount++;
        //}

        foreach (GameObject gm in itemTransmissionInst)
        {
            Destroy(gm);
        }

        itemTransmissionInst.Clear();
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();
        garageController.StartCoroutine(garageController.OffGarage());

        Initialize();
    }

    public void ButClosed()
    {
        //canvasGarage.SetActive(false);

        if (panelActive == "merge")
        {
            StartCoroutine(EnumSaveItem());
        }

        if (panelActive == "repair")
        {
            _popUpRepair.StartCoroutine(_popUpRepair.EnumSaveItem());
        }

        butMerge.SetActive(false);
        butRepair.SetActive(false);
        
        canvasGarage.SetActive(true);
        canvasGarage.GetComponent<GarageController>().Initialize();

        _popUpController.ClosedPopUp();
    }

    IEnumerator EnumSaveItem()
    {
        yield return new WaitForSeconds(0.3f);
        SaveItem();
    }

    public void ButPanelMerge()
    {
        if (panelActive != "merge")
        { 
            panelActive = "merge";

            _popUpRepair.SaveItem();
            Initialize();

            panelMerge.SetActive(true);
            panelRepair.SetActive(false);

            imgButtonMerge.sprite = sprActiveButton;
            imgButtonRepair.sprite = sprPassiveButton;

            butRepair.SetActive(false);
        }
    }

    public void ButPanelRepair()
    {
        if (panelActive != "repair")
        {
            panelActive = "repair";

            SaveItem();
            _popUpRepair.Initialize();

            panelMerge.SetActive(false);
            panelRepair.SetActive(true);

            imgButtonMerge.sprite = sprPassiveButton;
            imgButtonRepair.sprite = sprActiveButton;

            butMerge.SetActive(false);
        }
    }

    public void ButChooseItem(ItemCell _itemCell)
    {
        if (!isSlot1full)
        {
            imgSlot1.gameObject.SetActive(true);
            imgSlot2.gameObject.SetActive(true);
            imgSlot3.gameObject.SetActive(true);
            imgSlotFinal.gameObject.SetActive(true);

            levelCell1TextPanel.SetActive(true);
            levelCell2TextPanel.SetActive(false);
            levelCell3TextPanel.SetActive(false);
            levelCellFinalTextPanel.SetActive(true);

            itemCell1 = _itemCell;

            imgSlot1.sprite = _itemCell.imgCard.sprite;
            iconSlot1.sprite = _itemCell.imgIcon.sprite;

            imgSlot2.sprite = _itemCell.imgCard.sprite;
            iconSlot2.sprite = _itemCell.imgIcon.sprite;
            imgSlot2.color = new Color(0.65f, 0.65f, 0.65f, 1);
            iconSlot2.color = new Color(0.65f, 0.65f, 0.65f, 1);

            imgSlot3.sprite = _itemCell.imgCard.sprite;
            iconSlot3.sprite = _itemCell.imgIcon.sprite;
            imgSlot3.color = new Color(0.65f, 0.65f, 0.65f, 1);
            iconSlot3.color = new Color(0.65f, 0.65f, 0.65f, 1);

            if (_itemCell.itemRarity == "common")
            {
                imgSlotFinal.sprite = sprItemRare;
            }

            if (_itemCell.itemRarity == "rare")
            {
                imgSlotFinal.sprite = sprItemEpic;
            }

            if (_itemCell.itemRarity == "epic")
            {
                imgSlotFinal.sprite = sprItemLegendary;
            }

            iconSlotFinal.sprite = _itemCell.imgIcon.sprite;
            isSlot1full = true;

            panelNewStats.SetActive(true);

            #region Deactivate Items
            foreach (GameObject gm in itemGunInst)
            {
                if (gm.GetComponent<ItemCell>().itemID != _itemCell.itemID ||
                    gm.GetComponent<ItemCell>().itemRarity != _itemCell.itemRarity)
                {
                    gm.GetComponent<ItemCell>().MergeDeactivate();
                }
            }

            foreach (GameObject gm in itemEngineInst)
            {
                if (gm.GetComponent<ItemCell>().itemID != _itemCell.itemID ||
                    gm.GetComponent<ItemCell>().itemRarity != _itemCell.itemRarity)
                {
                    gm.GetComponent<ItemCell>().MergeDeactivate();
                }
            }

            foreach (GameObject gm in itemBrakesInst)
            {
                if (gm.GetComponent<ItemCell>().itemID != _itemCell.itemID ||
                    gm.GetComponent<ItemCell>().itemRarity != _itemCell.itemRarity)
                {
                    gm.GetComponent<ItemCell>().MergeDeactivate();
                }
            }

            foreach (GameObject gm in itemFuelSystemInst)
            {
                if (gm.GetComponent<ItemCell>().itemID != _itemCell.itemID ||
                    gm.GetComponent<ItemCell>().itemRarity != _itemCell.itemRarity)
                {
                    gm.GetComponent<ItemCell>().MergeDeactivate();
                }
            }

            foreach (GameObject gm in itemSuspensionInst)
            {
                if (gm.GetComponent<ItemCell>().itemID != _itemCell.itemID ||
                    gm.GetComponent<ItemCell>().itemRarity != _itemCell.itemRarity)
                {
                    gm.GetComponent<ItemCell>().MergeDeactivate();
                }
            }

            foreach (GameObject gm in itemTransmissionInst)
            {
                if (gm.GetComponent<ItemCell>().itemID != _itemCell.itemID ||
                    gm.GetComponent<ItemCell>().itemRarity != _itemCell.itemRarity)
                {
                    gm.GetComponent<ItemCell>().MergeDeactivate();
                }
            }
            #endregion

            itemCell1.MergeSelect();

            tLevelCell1.text = "Lv " + _itemCell.currentLevel;
            tLevelCellFinal.text = "Lv " + _itemCell.currentLevel;

            maxLevel = _itemCell.currentLevel;
            itemCellGeneral = _itemCell;

            PanelNewStatsSettings();

            return;
        }

        if (!isSlot2full)
        {
            itemCell2 = _itemCell;

            imgSlot2.sprite = _itemCell.imgCard.sprite;
            iconSlot2.sprite = _itemCell.imgIcon.sprite;
            imgSlot2.color = new Color(1f, 1f, 1f, 1);
            iconSlot2.color = new Color(1f, 1f, 1f, 1);

            isSlot2full = true;

            itemCell2.MergeSelect();

            if (isSlot1full && isSlot2full && isSlot3full)
            {
                butMerge.SetActive(true);

                foreach (GameObject gm in instItemMass)
                {
                    if (gm != itemCell1.gameObject && gm != itemCell2.gameObject && gm != itemCell3.gameObject)
                    {
                        gm.GetComponent<ItemCell>().MergeDeactivate();
                    }
                }
            }

            tLevelCell2.text = "Lv " + _itemCell.currentLevel;
            levelCell2TextPanel.SetActive(true);

            if (_itemCell.currentLevel > maxLevel)
            {
                maxLevel = _itemCell.currentLevel;
                tLevelCellFinal.text = "Lv " + maxLevel;
                itemCellGeneral = _itemCell;
            }

            PanelNewStatsSettings();

            return;
        }

        if (!isSlot3full)
        {
            itemCell3 = _itemCell;

            imgSlot3.sprite = _itemCell.imgCard.sprite;
            iconSlot3.sprite = _itemCell.imgIcon.sprite;
            imgSlot3.color = new Color(1f, 1f, 1f, 1);
            iconSlot3.color = new Color(1f, 1f, 1f, 1);

            isSlot3full = true;

            itemCell3.MergeSelect();

            if (isSlot1full && isSlot2full && isSlot3full)
            {
                butMerge.SetActive(true);

                foreach (GameObject gm in instItemMass)
                {
                    if (gm != itemCell1.gameObject && gm != itemCell2.gameObject && gm != itemCell3.gameObject)
                    {
                        gm.GetComponent<ItemCell>().MergeDeactivate();
                    }
                }
            }

            tLevelCell3.text = "Lv " + _itemCell.currentLevel;
            levelCell3TextPanel.SetActive(true);

            if (_itemCell.currentLevel > maxLevel)
            {
                maxLevel = _itemCell.currentLevel;
                tLevelCellFinal.text = "Lv " + maxLevel;
                itemCellGeneral = _itemCell;
            }

            PanelNewStatsSettings();

            return;
        }        
    }

    public void ButItemUnselect(int slotID)
    {
        if (isSlot1full && isSlot2full && isSlot3full)
        {
            foreach (GameObject gm in instItemMass)
            {
                if (gm != itemCell1.gameObject && gm != itemCell2.gameObject && gm != itemCell3.gameObject)
                {
                    gm.GetComponent<ItemCell>().MergeDefault();
                }
            }
        }

        if (slotID == 1)
        {
            if (itemCell1 != null)
            {
                imgSlot1.gameObject.SetActive(false);
                imgSlot2.gameObject.SetActive(false);
                imgSlot3.gameObject.SetActive(false);
                imgSlotFinal.gameObject.SetActive(false);

                levelCell1TextPanel.SetActive(false);
                levelCell2TextPanel.SetActive(false);
                levelCell3TextPanel.SetActive(false);
                levelCellFinalTextPanel.SetActive(false);

                isSlot1full = false;
                isSlot2full = false;
                isSlot3full = false;

                panelNewStats.SetActive(false);

                #region Deactivate Items
                foreach (GameObject gm in itemGunInst)
                {
                    gm.GetComponent<ItemCell>().MergeDefault();
                }

                foreach (GameObject gm in itemEngineInst)
                {
                    gm.GetComponent<ItemCell>().MergeDefault();
                }

                foreach (GameObject gm in itemBrakesInst)
                {
                    gm.GetComponent<ItemCell>().MergeDefault();
                }

                foreach (GameObject gm in itemFuelSystemInst)
                {
                    gm.GetComponent<ItemCell>().MergeDefault();
                }

                foreach (GameObject gm in itemSuspensionInst)
                {
                    gm.GetComponent<ItemCell>().MergeDefault();
                }

                foreach (GameObject gm in itemTransmissionInst)
                {
                    gm.GetComponent<ItemCell>().MergeDefault();
                }
                #endregion

                itemCell1 = null;
                itemCell2 = null;
                itemCell3 = null;

                tLevelCell1.text = "";
                tLevelCell2.text = "";
                tLevelCell3.text = "";
                tLevelCellFinal.text = "";
                maxLevel = 0;
                itemCellGeneral = null;
            }
        }

        if (slotID == 2)
        {
            if (itemCell2 != null)
            {
                imgSlot2.color = new Color(0.65f, 0.65f, 0.65f, 1);
                iconSlot2.color = new Color(0.65f, 0.65f, 0.65f, 1);

                if (itemCellGeneral == itemCell2)
                {
                    if (itemCell3 != null)
                    {
                        if (itemCell1.currentLevel > itemCell3.currentLevel)
                        {
                            itemCellGeneral = itemCell1;
                        }
                        else
                        {
                            itemCellGeneral = itemCell3;
                        }
                    }
                    else
                    {
                        itemCellGeneral = itemCell1;
                    }
                }

                levelCell2TextPanel.SetActive(false);

                itemCell2.MergeDefault();

                itemCell2 = null;

                isSlot2full = false;                
            }
        }

        if (slotID == 3)
        {
            if (itemCell3 != null)
            {
                imgSlot3.color = new Color(0.65f, 0.65f, 0.65f, 1);
                iconSlot3.color = new Color(0.65f, 0.65f, 0.65f, 1);

                if (itemCellGeneral == itemCell3)
                {
                    if (itemCell2 != null)
                    {
                        if (itemCell1.currentLevel > itemCell2.currentLevel)
                        {
                            itemCellGeneral = itemCell1;
                        }
                        else
                        {
                            itemCellGeneral = itemCell2;
                        }
                    }
                    else
                    {
                        itemCellGeneral = itemCell1;
                    }
                }

                levelCell3TextPanel.SetActive(false);

                itemCell3.MergeDefault();

                itemCell3 = null;

                isSlot3full = false;
            }
        }              

        butMerge.SetActive(false);
    }

    void RemoveItemInInventory(int _itemNum, string _itemType, GameObject _gm)
    {
        int itemCount = PlayerPrefs.GetInt("itemCount" + _itemType);

        //#region DeleteSlot       
        //if (_itemType == "Gun")
        //{
        //    if (PlayerPrefs.GetInt("slot1_itemNumInInventory") > _itemNum)
        //    {
        //        PlayerPrefs.SetInt("slot1_itemNumInInventory", PlayerPrefs.GetInt("slot1_itemNumInInventory") - 1);
        //    }
        //}

        //if (_itemType == "Engine")
        //{
        //    if (PlayerPrefs.GetInt("slot2_itemNumInInventory") > _itemNum)
        //    {
        //        PlayerPrefs.SetInt("slot2_itemNumInInventory", PlayerPrefs.GetInt("slot2_itemNumInInventory") - 1);
        //    }
        //}

        //if (_itemType == "Brakes")
        //{
        //    if (PlayerPrefs.GetInt("slot3_itemNumInInventory") > _itemNum)
        //    {
        //        PlayerPrefs.SetInt("slot3_itemNumInInventory", PlayerPrefs.GetInt("slot3_itemNumInInventory") - 1);
        //    }
        //}

        //if (_itemType == "FuelSystem")
        //{
        //    if (PlayerPrefs.GetInt("slot4_itemNumInInventory") > _itemNum)
        //    {
        //        PlayerPrefs.SetInt("slot4_itemNumInInventory", PlayerPrefs.GetInt("slot4_itemNumInInventory") - 1);
        //    }
        //}

        //if (_itemType == "Suspension")
        //{
        //    if (PlayerPrefs.GetInt("slot5_itemNumInInventory") > _itemNum)
        //    {
        //        PlayerPrefs.SetInt("slot5_itemNumInInventory", PlayerPrefs.GetInt("slot5_itemNumInInventory") - 1);
        //    }
        //}

        //if (_itemType == "Transmission")
        //{
        //    if (PlayerPrefs.GetInt("slot6_itemNumInInventory") > _itemNum)
        //    {
        //        PlayerPrefs.SetInt("slot6_itemNumInInventory", PlayerPrefs.GetInt("slot6_itemNumInInventory") - 1);
        //    }
        //}
        //#endregion

        if (itemCount > 1)
        {
            for (int i = _itemNum; i < itemCount - 1; i++)
            {
                PlayerPrefs.SetInt("item" + _itemType + "ID" + i, PlayerPrefs.GetInt("item" + _itemType + "ID" + (i + 1)));
                PlayerPrefs.SetInt("item" + _itemType + "Level" + i, PlayerPrefs.GetInt("item" + _itemType + "Level" + (i + 1)));
                PlayerPrefs.SetString("item" + _itemType + "Rarity" + i, PlayerPrefs.GetString("item" + _itemType + "Rarity" + (i + 1)));
                PlayerPrefs.SetString("item" + _itemType + "Type" + i, PlayerPrefs.GetString("item" + _itemType + "Type" + (i + 1)));
                PlayerPrefs.SetInt("item" + _itemType + "New" + i, PlayerPrefs.GetInt("item" + _itemType + "New" + (i + 1)));

                if (PlayerPrefs.HasKey("item" + _itemType + "baseCharacterCommon1Value" + i + 1))
                {
                    PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterCommon1Value" + i, PlayerPrefs.GetFloat("item" + _itemType + "baseCharacterCommon1Value" + (i + 1)));
                    PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterCommon2Value" + i, PlayerPrefs.GetFloat("item" + _itemType + "baseCharacterCommon2Value" + (i + 1)));

                    PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterRare1Value" + i, PlayerPrefs.GetFloat("item" + _itemType + "baseCharacterRare1Value" + (i + 1)));
                    PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterRare2Value" + i, PlayerPrefs.GetFloat("item" + _itemType + "baseCharacterRare2Value" + (i + 1)));

                    PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterEpic1Value" + i, PlayerPrefs.GetFloat("item" + _itemType + "baseCharacterEpic1Value" + (i + 1)));
                    PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterEpic2Value" + i, PlayerPrefs.GetFloat("item" + _itemType + "baseCharacterEpic2Value" + (i + 1)));

                    PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterLegendary1Value" + i, PlayerPrefs.GetFloat("item" + _itemType + "baseCharacterLegendary1Value" + (i + 1)));
                    PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterLegendary2Value" + i, PlayerPrefs.GetFloat("item" + _itemType + "baseCharacterLegendary2Value" + (i + 1)));
                }
            }

            #region Удаляем инфу про удаленный итем
            itemCount -= 1;

            PlayerPrefs.DeleteKey("item" + _itemType + "ID" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "Level" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "Rarity" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "Type" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "New" + itemCount);

            PlayerPrefs.DeleteKey("item" + _itemType + "baseCharacterCommon1Value" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "baseCharacterCommon2Value" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "baseCharacterRare1Value" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "baseCharacterRare2Value" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "baseCharacterEpic1Value" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "baseCharacterEpic2Value" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "baseCharacterLegendary1Value" + itemCount);
            PlayerPrefs.DeleteKey("item" + _itemType + "baseCharacterLegendary2Value" + itemCount);
            #endregion

            PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) - 1);
        } 
        else
        {
            PlayerPrefs.SetInt("itemCount" + _itemType, 0);
        }

        if (_gm.GetComponent<ItemCell>().itemType == "Gun")
            itemGunInst.Remove(_gm);

        if (_gm.GetComponent<ItemCell>().itemType == "Engine")
            itemEngineInst.Remove(_gm);

        if (_gm.GetComponent<ItemCell>().itemType == "Brakes")
            itemBrakesInst.Remove(_gm);

        if (_gm.GetComponent<ItemCell>().itemType == "FuelSystem")
            itemFuelSystemInst.Remove(_gm);

        if (_gm.GetComponent<ItemCell>().itemType == "Suspension")
            itemSuspensionInst.Remove(_gm);

        if (_gm.GetComponent<ItemCell>().itemType == "Transmission")
            itemTransmissionInst.Remove(_gm);
    }

    public void ButMerge()
    {
        int _numInInv1 = itemCell1.itemNumInInventory;

        int _numInInv2 = itemCell2.itemNumInInventory;

        int _numInInv3 = itemCell3.itemNumInInventory;

        if (_numInInv2 > _numInInv1)
        {
            _numInInv2 -= 1;
        }

        if (_numInInv3 > _numInInv1)
        {
            _numInInv3 -= 1;
        }

        if (_numInInv3 > _numInInv2)
        {
            _numInInv3 -= 1;
        }


        RemoveItemInInventory(_numInInv1, itemCell1.itemType, itemCell1.gameObject);
        RemoveItemInInventory(_numInInv2, itemCell2.itemType, itemCell2.gameObject);
        RemoveItemInInventory(_numInInv3, itemCell3.itemType, itemCell3.gameObject);        

        #region AddNewItem
        string _itemType;
        string _itemRarity = "";

        _itemType = itemCellGeneral.itemType;
        Debug.Log("itemCellGeneral.itemID = " + itemCellGeneral.itemID);

        PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);

        int _cellCount = PlayerPrefs.GetInt("itemCount" + _itemType) - 1;

        if (itemCellGeneral.itemRarity == "common")
        {
            _itemRarity = "rare";

            float _value1 = itemCellGeneral.itemObj.baseItemCharactersRare1Value + itemCellGeneral.itemObj.baseItemCharactersRare1StepValue * (maxLevel - 1);
            float _value2 = itemCellGeneral.itemObj.baseItemCharactersRare2Value + itemCellGeneral.itemObj.baseItemCharactersRare2StepValue * (maxLevel - 1);

            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterRare1Value" + _cellCount, _value1);
            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterRare2Value" + _cellCount, _value2);
        }

        if (itemCellGeneral.itemRarity == "rare")
        {
            _itemRarity = "epic";

            float _value1 = itemCellGeneral.itemObj.baseItemCharactersEpic1Value + itemCellGeneral.itemObj.baseItemCharactersEpic1StepValue * (maxLevel - 1);
            float _value2 = itemCellGeneral.itemObj.baseItemCharactersEpic2Value + itemCellGeneral.itemObj.baseItemCharactersEpic2StepValue * (maxLevel - 1);

            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterEpic1Value" + _cellCount, _value1);
            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterEpic2Value" + _cellCount, _value2);
        }

        if (itemCellGeneral.itemRarity == "epic")
        {
            _itemRarity = "legendary";

            float _value1 = itemCellGeneral.itemObj.baseItemCharactersLegendary1Value + itemCellGeneral.itemObj.baseItemCharactersLegendary1StepValue * (maxLevel - 1);
            float _value2 = itemCellGeneral.itemObj.baseItemCharactersLegendary2Value + itemCellGeneral.itemObj.baseItemCharactersLegendary2StepValue * (maxLevel - 1);

            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterLegendary1Value" + _cellCount, _value1);
            PlayerPrefs.SetFloat("item" + _itemType + "baseCharacterLegendary2Value" + _cellCount, _value2);
        }        

        PlayerPrefs.SetInt("item" + _itemType + "ID" + _cellCount, itemCellGeneral.itemID);
        PlayerPrefs.SetInt("item" + _itemType + "Level" + _cellCount, maxLevel);
        PlayerPrefs.SetString("item" + _itemType + "Rarity" + _cellCount, _itemRarity);
        PlayerPrefs.SetString("item" + _itemType + "Type" + _cellCount, itemCellGeneral.itemType);
        PlayerPrefs.SetInt("item" + _itemType + "New" + _cellCount, 1);
        #endregion

        #region RemoveActiveSlot
        if (itemCell1.itemType == "Gun")
        {
            if (PlayerPrefs.GetInt("slot1_itemNumInInventory") == itemCell1.itemNumInInventory
                || PlayerPrefs.GetInt("slot1_itemNumInInventory") == itemCell2.itemNumInInventory
                || PlayerPrefs.GetInt("slot1_itemNumInInventory") == itemCell3.itemNumInInventory)
            {
                PlayerPrefs.SetInt("slot1_itemNumInInventory", _cellCount);
            }
        }

        if (itemCell1.itemType == "Engine")
        {
            if (PlayerPrefs.GetInt("slot2_itemNumInInventory") == itemCell1.itemNumInInventory
                || PlayerPrefs.GetInt("slot2_itemNumInInventory") == itemCell2.itemNumInInventory
                || PlayerPrefs.GetInt("slot2_itemNumInInventory") == itemCell3.itemNumInInventory)
            {
                PlayerPrefs.SetInt("slot2_itemNumInInventory", _cellCount);
            }
        }

        if (itemCell1.itemType == "Brakes")
        {
            if (PlayerPrefs.GetInt("slot3_itemNumInInventory") == itemCell1.itemNumInInventory
                || PlayerPrefs.GetInt("slot3_itemNumInInventory") == itemCell2.itemNumInInventory
                || PlayerPrefs.GetInt("slot3_itemNumInInventory") == itemCell3.itemNumInInventory)
            {
                PlayerPrefs.SetInt("slot3_itemNumInInventory", _cellCount);
            }
        }

        if (itemCell1.itemType == "FuelSystem")
        {
            if (PlayerPrefs.GetInt("slot4_itemNumInInventory") == itemCell1.itemNumInInventory
                || PlayerPrefs.GetInt("slot4_itemNumInInventory") == itemCell2.itemNumInInventory
                || PlayerPrefs.GetInt("slot4_itemNumInInventory") == itemCell3.itemNumInInventory)
            {
                PlayerPrefs.SetInt("slot4_itemNumInInventory", _cellCount);
            }
        }

        if (itemCell1.itemType == "Suspension")
        {
            if (PlayerPrefs.GetInt("slot5_itemNumInInventory") == itemCell1.itemNumInInventory
                || PlayerPrefs.GetInt("slot5_itemNumInInventory") == itemCell2.itemNumInInventory
                || PlayerPrefs.GetInt("slot5_itemNumInInventory") == itemCell3.itemNumInInventory)
            {
                PlayerPrefs.SetInt("slot5_itemNumInInventory", _cellCount);
            }
        }

        if (itemCell1.itemType == "Transmission")
        {
            if (PlayerPrefs.GetInt("slot6_itemNumInInventory") == itemCell1.itemNumInInventory
                || PlayerPrefs.GetInt("slot6_itemNumInInventory") == itemCell2.itemNumInInventory
                || PlayerPrefs.GetInt("slot6_itemNumInInventory") == itemCell3.itemNumInInventory)
            {
                PlayerPrefs.SetInt("slot6_itemNumInInventory", _cellCount);
            }
        }
        #endregion

        SaveDetailStats(_itemType, _itemRarity, _cellCount);

        popUpMergeFinal.card = itemCellGeneral.itemObj;
        popUpMergeFinal.sprItemCell = imgSlotFinal.sprite;
        popUpMergeFinal.sprItemIcon = iconSlotFinal.sprite;
        //popUpMergeFinal.itemName = itemCellGeneral.itemName;
        popUpMergeFinal.itemName = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + itemCellGeneral.itemID + "Name");

        if (itemCell1.currentLevel == maxLevel)
        {
            popUpMergeFinal.returnLevelValue1 = itemCell2.currentLevel;
            popUpMergeFinal.returnLevelValue2 = itemCell3.currentLevel;
        }

        if (itemCell2.currentLevel == maxLevel)
        {
            popUpMergeFinal.returnLevelValue1 = itemCell1.currentLevel;
            popUpMergeFinal.returnLevelValue2 = itemCell3.currentLevel;
        }

        if (itemCell3.currentLevel == maxLevel)
        {
            popUpMergeFinal.returnLevelValue1 = itemCell2.currentLevel;
            popUpMergeFinal.returnLevelValue2 = itemCell1.currentLevel;
        }

        popUpMergeFinal.level = maxLevel;

        #region Event
        if (GameObject.Find("Firebase") != null)
        {
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Merge(_itemType, itemCellGeneral.itemID, maxLevel, _itemRarity);
        }        
        #endregion

        imgSlot1.gameObject.SetActive(false);
        imgSlot2.gameObject.SetActive(false);
        imgSlot3.gameObject.SetActive(false);
        imgSlotFinal.gameObject.SetActive(false);

        isSlot1full = false;
        isSlot2full = false;
        isSlot3full = false;

        panelNewStats.SetActive(false);

        #region Deactivate Items
        foreach (GameObject gm in itemGunInst)
        {
            gm.GetComponent<ItemCell>().MergeDefault();
        }

        foreach (GameObject gm in itemEngineInst)
        {
            gm.GetComponent<ItemCell>().MergeDefault();
        }

        foreach (GameObject gm in itemBrakesInst)
        {
            gm.GetComponent<ItemCell>().MergeDefault();
        }

        foreach (GameObject gm in itemFuelSystemInst)
        {
            gm.GetComponent<ItemCell>().MergeDefault();
        }

        foreach (GameObject gm in itemSuspensionInst)
        {
            gm.GetComponent<ItemCell>().MergeDefault();
        }

        foreach (GameObject gm in itemTransmissionInst)
        {
            gm.GetComponent<ItemCell>().MergeDefault();
        }
        #endregion

        Destroy(itemCell1.gameObject);
        Destroy(itemCell2.gameObject);
        Destroy(itemCell3.gameObject);

        itemCell1 = null;
        itemCell2 = null;
        itemCell3 = null;

        tLevelCell1.text = "";
        tLevelCell2.text = "";
        tLevelCell3.text = "";
        tLevelCellFinal.text = "";
        maxLevel = 0;

        butMerge.SetActive(false);

        SaveItem();
        LoadItem();

        GameObject.Find("HubController").GetComponent<RedPushController>().CheckRedPush();

        popUpMergeFinal.rarity = _itemRarity;        
        popUpMergeFinal.ButOpen();        
    }

    void PanelNewStatsSettings()
    {
        DetailCard _card = itemCellGeneral.itemObj;

        if (itemCellGeneral.itemRarity == "common")
        {
            tCurrentMaxLevel.text = "10";
            tNextMaxLevel.text = "20";

            if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.HpUp)
            {
                imgIcon1.sprite = sprIconHealth;
            }

            if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.DamageUp)
            {
                imgIcon1.sprite = sprIconDamage;
            }

            tCurrentPanelValue1.text = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterCommon1Value" + itemCellGeneral.itemNumInInventory) + "%";
            float _value1 = itemCellGeneral.itemObj.baseItemCharactersRare1Value + itemCellGeneral.itemObj.baseItemCharactersRare1StepValue * (maxLevel - 1);
            tNextPanelValue1.text = _value1 + "";

            popUpMergeFinal.currentPanelValue1 = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterCommon1Value" + itemCellGeneral.itemNumInInventory);
            popUpMergeFinal.nextPanelValue1 = _value1;

            if (_card.baseItemCharactersCommon2 != DetailCard.ItemCharacters.none)
            {
                tCurrentPanelValue2.text = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterCommon2Value" + itemCellGeneral.itemNumInInventory) + "%";

                float _value2 = itemCellGeneral.itemObj.baseItemCharactersRare2Value + itemCellGeneral.itemObj.baseItemCharactersRare2StepValue * (maxLevel - 1);
                tNextPanelValue2.text = _value2 + "";

                popUpMergeFinal.currentPanelValue2 = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterCommon2Value" + itemCellGeneral.itemNumInInventory);
                popUpMergeFinal.nextPanelValue2 = _value2;

                if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.HpUp)
                {
                    objPanel2.SetActive(true);
                    imgIcon2.sprite = sprIconHealth;
                }

                if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.DamageUp)
                {
                    objPanel2.SetActive(true);
                    imgIcon2.sprite = sprIconDamage;
                }
            } 
            else
            {
                objPanel2.SetActive(false);
            }
        }

        if (itemCellGeneral.itemRarity == "rare")
        {
            tCurrentMaxLevel.text = "20";
            tNextMaxLevel.text = "30";

            if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.HpUp)
            {
                imgIcon1.sprite = sprIconHealth;
            }

            if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.DamageUp)
            {
                imgIcon1.sprite = sprIconDamage;
            }

            tCurrentPanelValue1.text = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterRare1Value" + itemCellGeneral.itemNumInInventory) + "%";

            float _value1 = itemCellGeneral.itemObj.baseItemCharactersEpic1Value + itemCellGeneral.itemObj.baseItemCharactersEpic1StepValue * (maxLevel - 1);
            tNextPanelValue1.text = _value1 + "";

            popUpMergeFinal.currentPanelValue1 = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterRare1Value" + itemCellGeneral.itemNumInInventory);
            popUpMergeFinal.nextPanelValue1 = _value1;

            if (_card.baseItemCharactersCommon2 != DetailCard.ItemCharacters.none)
            {
                tCurrentPanelValue2.text = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterRare2Value" + itemCellGeneral.itemNumInInventory) + "%";

                float _value2 = itemCellGeneral.itemObj.baseItemCharactersEpic2Value + itemCellGeneral.itemObj.baseItemCharactersEpic2StepValue * (maxLevel - 1);
                tNextPanelValue2.text = _value2 + "";

                popUpMergeFinal.currentPanelValue2 = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterRare2Value" + itemCellGeneral.itemNumInInventory);
                popUpMergeFinal.nextPanelValue2 = _value2;

                if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.HpUp)
                {
                    objPanel2.SetActive(true);
                    imgIcon2.sprite = sprIconHealth;
                }

                if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.DamageUp)
                {
                    objPanel2.SetActive(true);
                    imgIcon2.sprite = sprIconDamage;
                }
            }
            else
            {
                objPanel2.SetActive(false);
            }
        }

        if (itemCellGeneral.itemRarity == "epic")
        {
            tCurrentMaxLevel.text = "30";
            tNextMaxLevel.text = "40";

            if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.HpUp)
            {
                imgIcon1.sprite = sprIconHealth;
            }

            if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.DamageUp)
            {
                imgIcon1.sprite = sprIconDamage;
            }

            tCurrentPanelValue1.text = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterEpic1Value" + itemCellGeneral.itemNumInInventory) + "%";

            float _value1 = itemCellGeneral.itemObj.baseItemCharactersLegendary1Value + itemCellGeneral.itemObj.baseItemCharactersLegendary1StepValue * (maxLevel - 1);
            tNextPanelValue1.text = _value1 + "";

            popUpMergeFinal.currentPanelValue1 = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterEpic1Value" + itemCellGeneral.itemNumInInventory);
            popUpMergeFinal.nextPanelValue1 = _value1;

            if (_card.baseItemCharactersCommon2 != DetailCard.ItemCharacters.none)
            {
                tCurrentPanelValue2.text = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterEpic2Value" + itemCellGeneral.itemNumInInventory) + "%";

                float _value2 = itemCellGeneral.itemObj.baseItemCharactersLegendary2Value + itemCellGeneral.itemObj.baseItemCharactersLegendary2StepValue * (maxLevel - 1);
                tNextPanelValue2.text = _value2 + "";

                popUpMergeFinal.currentPanelValue2 = PlayerPrefs.GetFloat("item" + itemCellGeneral.itemType + "baseCharacterEpic2Value" + itemCellGeneral.itemNumInInventory);
                popUpMergeFinal.nextPanelValue2 = _value2;

                if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.HpUp)
                {
                    objPanel2.SetActive(true);
                    imgIcon2.sprite = sprIconHealth;
                }

                if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.DamageUp)
                {
                    objPanel2.SetActive(true);
                    imgIcon2.sprite = sprIconDamage;
                }
            }
            else
            {
                objPanel2.SetActive(false);
            }
        }
    }

    public void SaveDetailStats(string _type, string itemRarity, int numInv)
    {
        DetailCard _card = itemCell1.itemObj;

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
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon1Value" + numInv));
                }

                if (_card.baseItemCharactersCommon1 == DetailCard.ItemCharacters.DamageUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon1Value" + numInv));
                }

                if (_card.baseItemCharactersCommon2 != DetailCard.ItemCharacters.none)
                {
                    if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.HpUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon2Value" + numInv));
                    }

                    if (_card.baseItemCharactersCommon2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterCommon2Value" + numInv));
                    }
                }

                break;

            case "rare":

                if (_card.baseItemCharactersRare1 == DetailCard.ItemCharacters.HpUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare1Value" + numInv));
                }

                if (_card.baseItemCharactersRare1 == DetailCard.ItemCharacters.DamageUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare1Value" + numInv));
                }

                if (_card.baseItemCharactersRare2 != DetailCard.ItemCharacters.none)
                {
                    if (_card.baseItemCharactersRare2 == DetailCard.ItemCharacters.HpUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare2Value" + numInv));
                    }

                    if (_card.baseItemCharactersRare2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterRare2Value" + numInv));
                    }
                }

                break;

            case "epic":

                if (_card.baseItemCharactersEpic1 == DetailCard.ItemCharacters.HpUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic1Value" + numInv));
                }

                if (_card.baseItemCharactersEpic1 == DetailCard.ItemCharacters.DamageUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic1Value" + numInv));
                }

                if (_card.baseItemCharactersEpic2 != DetailCard.ItemCharacters.none)
                {
                    if (_card.baseItemCharactersEpic2 == DetailCard.ItemCharacters.HpUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic2Value" + numInv));
                    }

                    if (_card.baseItemCharactersEpic2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterEpic2Value" + numInv));
                    }
                }

                break;

            case "legendary":

                if (_card.baseItemCharactersLegendary1 == DetailCard.ItemCharacters.HpUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary1Value" + numInv));
                }

                if (_card.baseItemCharactersLegendary1 == DetailCard.ItemCharacters.DamageUp)
                {
                    PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary1Value" + numInv));
                }

                if (_card.baseItemCharactersLegendary2 != DetailCard.ItemCharacters.none)
                {
                    if (_card.baseItemCharactersLegendary2 == DetailCard.ItemCharacters.HpUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectHealth", PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary2Value" + numInv));
                    }

                    if (_card.baseItemCharactersLegendary2 == DetailCard.ItemCharacters.DamageUp)
                    {
                        PlayerPrefs.SetFloat(_type + "SelectDamage", PlayerPrefs.GetFloat("item" + _type + "baseCharacterLegendary2Value" + numInv));
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
