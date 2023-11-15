using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class PopUpRepair : MonoBehaviour
{
    public PopUpDetail popUpDetail;

    [Header("Repair Panel")]
    public Image imgSlot1;
    public Image imgSlot2;
    public Image imgSlot3;

    public Image iconSlot1;

    public bool isSlot1full;

    public Sprite sprItemCommon;
    public Sprite sprItemRare;
    public Sprite sprItemEpic;
    public Sprite sprItemLegendary;

    public ItemCell itemCellGeneral;

    public TMP_Text tLevelCell1;

    public GameObject butRepair;

    [Header("Cell")]
    public GameObject cellPrefab;
    public List<GameObject> itemMass;

    [Header("Detail Obj")]
    public List<DetailCard> gunItem;
    public List<DetailCard> engineItem;
    public List<DetailCard> brakesItem;
    public List<DetailCard> fuelSystemItem;
    public List<DetailCard> suspensionItem;
    public List<DetailCard> transmissionItem;

    [Header("Scroll")]
    public Transform globalContentPanel;
    public List<GameObject> itemGunInst;
    public List<GameObject> itemEngineInst;
    public List<GameObject> itemBrakesInst;
    public List<GameObject> itemFuelSystemInst;
    public List<GameObject> itemSuspensionInst;
    public List<GameObject> itemTransmissionInst;

    [Header("Return Consumables")]
    public int returnLevelValue;

    public int returnMoneyValue;
    public int returnDrawingValue;

    public TMP_Text tMoneyCount;
    public TMP_Text tDrawingCount;

    [Header("PopUp Repair Final")]
    public PopUpRepairFinal popUpRepairFinal;


    public void Initialize()
    {
        LoadItem();

        imgSlot1.gameObject.SetActive(false);
        imgSlot2.gameObject.SetActive(false);
        imgSlot3.gameObject.SetActive(false);
        butRepair.SetActive(false);

        tLevelCell1.text = "";

        itemCellGeneral = null;

        isSlot1full = false;
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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Repair;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Repair;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Repair;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Repair;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Repair;

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
            _cell.GetComponent<ItemCell>().cellType = ItemCell.CellType.Repair;

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

    public void SaveItem()
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

    public IEnumerator EnumSaveItem()
    {
        yield return new WaitForSeconds(0.3f);
        SaveItem();
    }

    public void ButChooseItem(ItemCell _itemCell)
    {
        if (!isSlot1full)
        {
            #region Choose Item
            imgSlot1.gameObject.SetActive(true);
            imgSlot2.gameObject.SetActive(true);
            imgSlot3.gameObject.SetActive(true);

            itemCellGeneral = _itemCell;

            imgSlot1.sprite = _itemCell.imgCard.sprite;
            iconSlot1.sprite = _itemCell.imgIcon.sprite;

            if (_itemCell.itemRarity == "common")
            {
                imgSlot1.sprite = sprItemCommon;
            }

            if (_itemCell.itemRarity == "rare")
            {
                imgSlot1.sprite = sprItemRare;
            }

            if (_itemCell.itemRarity == "epic")
            {
                imgSlot1.sprite = sprItemEpic;
            }

            if (_itemCell.itemRarity == "legendary")
            {
                imgSlot1.sprite = sprItemLegendary;
            }

            isSlot1full = true;

            itemCellGeneral = _itemCell;

            foreach (GameObject gm in itemMass)
            {
                if (gm != itemCellGeneral.gameObject)
                {
                    gm.GetComponent<ItemCell>().MergeDeactivate();
                }
            }

            itemCellGeneral.MergeSelect();

            tLevelCell1.text = "Lv " + _itemCell.currentLevel;
            #endregion

            #region Return Level 1
            returnMoneyValue = 0;
            returnDrawingValue = 0;

            returnLevelValue = _itemCell.currentLevel;

            if (returnLevelValue > 0)
            {
                for (int i = 1; i < returnLevelValue; i++)
                {
                    returnMoneyValue += popUpDetail.upgradePrice[i];
                    returnDrawingValue += popUpDetail.drawingCount[i];
                }
            }

            switch (_itemCell.itemRarity)
            {
                case "common":
                    returnMoneyValue += 500;
                    returnDrawingValue += 2;
                    break;

                case "rare":
                    returnMoneyValue += 1500;
                    returnDrawingValue += 6;
                    break;

                case "epic":
                    returnMoneyValue += 4500;
                    returnDrawingValue += 18;
                    break;

                case "legendary":
                    returnMoneyValue += 13500;
                    returnDrawingValue += 54;
                    break;
            }

            tMoneyCount.text = "x" + returnMoneyValue;
            tDrawingCount.text = "x" + returnDrawingValue;
            #endregion

            butRepair.SetActive(true);
        }
    }

    public void ButItemUnselect()
    {
        foreach (GameObject gm in itemMass)
        {
            gm.GetComponent<ItemCell>().MergeDefault();
        }

        imgSlot1.gameObject.SetActive(false);
        imgSlot2.gameObject.SetActive(false);
        imgSlot3.gameObject.SetActive(false);

        isSlot1full = false;        

        tLevelCell1.text = "";
        itemCellGeneral = null;

        butRepair.SetActive(false);
    }

    public void ButRepair()
    {       
        PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + returnMoneyValue);

        popUpRepairFinal.moneyCount = returnMoneyValue;

        switch (itemCellGeneral.itemObj.itemType)
        {
            case DetailCard.ItemType.Gun:
                PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + returnDrawingValue);
                break;

            case DetailCard.ItemType.Engine:
                PlayerPrefs.SetInt("drawingEngineCount", PlayerPrefs.GetInt("drawingEngineCount") + returnDrawingValue);
                break;

            case DetailCard.ItemType.Brakes:
                PlayerPrefs.SetInt("drawingBrakesCount", PlayerPrefs.GetInt("drawingBrakesCount") + returnDrawingValue);
                break;

            case DetailCard.ItemType.FuelSystem:
                PlayerPrefs.SetInt("drawingFuelSystemCount", PlayerPrefs.GetInt("drawingFuelSystemCount") + returnDrawingValue);
                break;

            case DetailCard.ItemType.Suspension:
                PlayerPrefs.SetInt("drawingSuspensionCount", PlayerPrefs.GetInt("drawingSuspensionCount") + returnDrawingValue);
                break;

            case DetailCard.ItemType.Transmission:
                PlayerPrefs.SetInt("drawingTransmissionCount", PlayerPrefs.GetInt("drawingTransmissionCount") + returnDrawingValue);
                break;
        }

        popUpRepairFinal.drawingCount = returnDrawingValue;

        foreach (GameObject gm in itemMass)
        {
            gm.GetComponent<ItemCell>().MergeDefault();
        }

        RemoveItemInInventory(itemCellGeneral.itemNumInInventory, itemCellGeneral.itemType, itemCellGeneral.gameObject);

        imgSlot1.gameObject.SetActive(false);
        imgSlot2.gameObject.SetActive(false);
        imgSlot3.gameObject.SetActive(false);

        isSlot1full = false;

        tLevelCell1.text = "";
        itemCellGeneral = null;

        butRepair.SetActive(false);

        SaveItem();
        LoadItem();

        popUpRepairFinal.Open();
    }

    void RemoveItemInInventory(int _itemNum, string _itemType, GameObject _gm)
    {
        int itemCount = PlayerPrefs.GetInt("itemCount" + _itemType);

        if (itemCount > 1)
        {
            for (int i = _itemNum; i < itemCount - 1; i++)
            {
                PlayerPrefs.SetInt("item" + _itemType + "ID" + i, PlayerPrefs.GetInt("item" + _itemType + "ID" + (i + 1)));
                PlayerPrefs.SetInt("item" + _itemType + "Level" + i, PlayerPrefs.GetInt("item" + _itemType + "Level" + (i + 1)));
                PlayerPrefs.SetString("item" + _itemType + "Rarity" + i, PlayerPrefs.GetString("item" + _itemType + "Rarity" + (i + 1)));
                PlayerPrefs.SetString("item" + _itemType + "Type" + i, PlayerPrefs.GetString("item" + _itemType + "Type" + (i + 1)));

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

        //if (_gm.GetComponent<ItemCell>().itemType == "Gun")
        //    itemGunInst.Remove(_gm);

        //if (_gm.GetComponent<ItemCell>().itemType == "Engine")
        //    itemEngineInst.Remove(_gm);

        //if (_gm.GetComponent<ItemCell>().itemType == "Brakes")
        //    itemBrakesInst.Remove(_gm);

        //if (_gm.GetComponent<ItemCell>().itemType == "FuelSystem")
        //    itemFuelSystemInst.Remove(_gm);

        //if (_gm.GetComponent<ItemCell>().itemType == "Suspension")
        //    itemSuspensionInst.Remove(_gm);

        //if (_gm.GetComponent<ItemCell>().itemType == "Transmission")
        //    itemTransmissionInst.Remove(_gm);
    }
}
