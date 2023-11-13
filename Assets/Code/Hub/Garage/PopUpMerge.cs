using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMerge : MonoBehaviour
{
    PopUpController _popUpController;
    public GameObject panelMerge;
    public GameObject panelRepair;

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

    [Header("Cell")]
    public GameObject cellPrefab;

    [Header("Detail Obj")]
    public List<DetailCard> gunItem;
    public List<DetailCard> engineItem;
    public List<DetailCard> brakesItem;
    public List<DetailCard> fuelSystemItem;
    public List<DetailCard> suspensionItem;
    public List<DetailCard> transmissionItem;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        ButPanelMerge();

        LoadItem();
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

        for (int i = 0; i < itemMass.Count; i++)
        {
            if (itemMass[i] != null)
            {
                bool isFindPair = false;

                GameObject _cell = itemMass[i];
                List<GameObject> instItemMass = new List<GameObject>();

                for (int g = i; g < itemMass.Count; g++)
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

        ////Legendary
        //for (int i = 0; i < itemMass.Count; i++)
        //{
        //    GameObject _cell = itemMass[i];

        //    if (_cell.GetComponent<ItemCell>().itemRarity == "legendary")
        //    {
        //        _cell.transform.parent = globalContentPanel.transform;
        //        _cell.transform.localScale = Vector3.one;
        //    }
        //}

        ////Epic
        //for (int i = 0; i < itemMass.Count; i++)
        //{
        //    GameObject _cell = itemMass[i];

        //    if (_cell.GetComponent<ItemCell>().itemRarity == "epic")
        //    {
        //        _cell.transform.parent = globalContentPanel.transform;
        //        _cell.transform.localScale = Vector3.one;
        //    }
        //}

        ////Rare
        //for (int i = 0; i < itemMass.Count; i++)
        //{
        //    GameObject _cell = itemMass[i];

        //    if (_cell.GetComponent<ItemCell>().itemRarity == "rare")
        //    {
        //        _cell.transform.parent = globalContentPanel.transform;
        //        _cell.transform.localScale = Vector3.one;
        //    }
        //}

        ////Common
        //for (int i = 0; i < itemMass.Count; i++)
        //{
        //    GameObject _cell = itemMass[i];

        //    if (_cell.GetComponent<ItemCell>().itemRarity == "common")
        //    {
        //        _cell.transform.parent = globalContentPanel.transform;
        //        _cell.transform.localScale = Vector3.one;
        //    }
        //}
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

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        Initialize();
    }

    public void ButClosed()
    {
        StartCoroutine(EnumSaveItem());

        _popUpController.ClosedPopUp();
    }

    IEnumerator EnumSaveItem()
    {
        yield return new WaitForSeconds(0.3f);
        SaveItem();
    }

    public void ButPanelMerge()
    {
        panelMerge.SetActive(true);
        panelRepair.SetActive(false);

        imgButtonMerge.sprite = sprActiveButton;
        imgButtonRepair.sprite = sprPassiveButton;
    }

    public void ButPanelRepair()
    {
        panelMerge.SetActive(false);
        panelRepair.SetActive(true);

        imgButtonMerge.sprite = sprPassiveButton;
        imgButtonRepair.sprite = sprActiveButton;
    }
}
