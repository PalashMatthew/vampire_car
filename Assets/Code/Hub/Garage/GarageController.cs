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
    public List<DetailCard> ngineItem;
    public List<DetailCard> brakesItem;
    public List<DetailCard> fuelSystemItem;
    public List<DetailCard> suspensionItem;
    public List<DetailCard> transmissionItem;

    [Header("Cell")]
    public GameObject cellPrefab;    

    [Header("Scroll Gun")]
    public Transform scrollGun;
    public Transform scrollEngine;
    public Transform scrollBrakes;
    public Transform scrollFuelSystem;
    public Transform scrollSuspension;
    public Transform scrollTransmission;
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


    private void OnEnable()
    {
        PlayerPrefs.SetInt("itemCountGun", 4);

        PlayerPrefs.SetInt("itemGunID0", 1001);
        PlayerPrefs.SetInt("itemGunID1", 1002);
        PlayerPrefs.SetInt("itemGunID2", 1003);
        PlayerPrefs.SetInt("itemGunID3", 1004);

        PlayerPrefs.SetString("itemGunRarity0", "common");
        PlayerPrefs.SetString("itemGunRarity1", "legendary");
        PlayerPrefs.SetString("itemGunRarity2", "epic");
        PlayerPrefs.SetString("itemGunRarity3", "rare");

        //PlayerPrefs.SetInt("itemCountEngine", 4);
        //PlayerPrefs.SetInt("itemCountBrakes", 4);
        //PlayerPrefs.SetInt("itemCountFuelSystem", 4);
        //PlayerPrefs.SetInt("itemCountSuspension", 4);
        //PlayerPrefs.SetInt("itemCountTransmission", 4);
        Initialize();
    }

    private void OnDisable()
    {
        SaveItem();
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

        tDamage.text = "+" + PlayerPrefs.GetFloat(activeCarObj.carName + "carDamage") + "%";
        tHealth.text = PlayerPrefs.GetFloat(activeCarObj.carName + "carHealth") + "";

        LoadItem();
        LoadSlots();
    }

    void LoadItem()
    {
        int cellCount;

        // Оружие
        cellCount = PlayerPrefs.GetInt("itemCountGun");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);
            _cell.transform.parent = scrollGun.transform;
            _cell.transform.localScale = Vector3.one;

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemGunID" + i);
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
        }

        // Engine
        cellCount = PlayerPrefs.GetInt("itemCountEngine");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);
            _cell.transform.parent = scrollEngine.transform;
            _cell.transform.localScale = Vector3.one;

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemEngineID" + i);
            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemEngineLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemEngineRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemEngineType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            _cell.GetComponent<ItemCell>().Initialize();

            itemGunInst.Add(_cell);
        }

        // Brakes
        cellCount = PlayerPrefs.GetInt("itemCountBrakes");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);
            _cell.transform.parent = scrollBrakes.transform;
            _cell.transform.localScale = Vector3.one;

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemBrakesID" + i);
            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemBrakesLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemBrakesRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemBrakesType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            _cell.GetComponent<ItemCell>().Initialize();

            itemGunInst.Add(_cell);
        }

        // Fuel System
        cellCount = PlayerPrefs.GetInt("itemCountFuelSystem");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);
            _cell.transform.parent = scrollFuelSystem.transform;
            _cell.transform.localScale = Vector3.one;

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemFuelSystemID" + i);
            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemFuelSystemLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemFuelSystemRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemFuelSystemType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            _cell.GetComponent<ItemCell>().Initialize();

            itemGunInst.Add(_cell);
        }

        // Suspension
        cellCount = PlayerPrefs.GetInt("itemCountSuspension");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);
            _cell.transform.parent = scrollSuspension.transform;
            _cell.transform.localScale = Vector3.one;

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemSuspensionID" + i);
            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemSuspensionLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemSuspensionRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemSuspensionType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            _cell.GetComponent<ItemCell>().Initialize();

            itemGunInst.Add(_cell);
        }

        // Transmission
        cellCount = PlayerPrefs.GetInt("itemCountTransmission");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);
            _cell.transform.parent = scrollTransmission.transform;
            _cell.transform.localScale = Vector3.one;

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemTransmissionID" + i);
            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemTransmissionLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemTransmissionRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemTransmissionType" + i);
            _cell.GetComponent<ItemCell>().garageController = gameObject.GetComponent<GarageController>();
            _cell.GetComponent<ItemCell>().itemNumInInventory = i;

            _cell.GetComponent<ItemCell>().Initialize();

            itemGunInst.Add(_cell);
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

            string itemSpriteName = "item_" + slot1Card.itemID;
            imgIconSlot1.sprite = Resources.Load<Sprite>("ItemSprite/" + itemSpriteName);

            imgSlot1.sprite = slot1Card.imgCard.sprite;
            slot1Card.gameObject.SetActive(false);
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
        imgIconSlot1.gameObject.SetActive(true);

        switch (activeItem.slotNum)
        {
            case 1:
                imgSlot1.sprite = activeItem.imgCard.sprite;
                imgIconSlot1.sprite = activeItem.imgIcon.sprite;
                break;

            case 2:
                imgSlot2.sprite = activeItem.imgCard.sprite;
                imgIconSlot2.sprite = activeItem.imgIcon.sprite;
                break;

            case 3:
                imgSlot3.sprite = activeItem.imgCard.sprite;
                imgIconSlot3.sprite = activeItem.imgIcon.sprite;
                break;

            case 4:
                imgSlot4.sprite = activeItem.imgCard.sprite;
                imgIconSlot4.sprite = activeItem.imgIcon.sprite;
                break;

            case 5:
                imgSlot5.sprite = activeItem.imgCard.sprite;
                imgIconSlot5.sprite = activeItem.imgIcon.sprite;
                break;

            case 6:
                imgSlot6.sprite = activeItem.imgCard.sprite;
                imgIconSlot6.sprite = activeItem.imgIcon.sprite;
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
            if (PlayerPrefs.GetInt("slot4_itemNumInInventory") != -1 && PlayerPrefs.HasKey("slot4_itemNumInInventory"))
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
        switch (activeItem.slotNum)
        {
            case 1:
                slot1Card.gameObject.SetActive(true);
                slot1Card = null;

                PlayerPrefs.SetInt("slot1_itemNumInInventory", -1);

                imgSlot1.sprite = sprSlotDefault;
                imgIconSlot1.gameObject.SetActive(false);
                break;

            case 2:
                slot2Card.gameObject.SetActive(true);
                slot2Card = null;

                PlayerPrefs.SetInt("slot2_itemNumInInventory", -1);

                imgSlot2.sprite = sprSlotDefault;
                imgIconSlot2.gameObject.SetActive(false);
                break;

            case 3:
                slot3Card.gameObject.SetActive(true);
                slot3Card = null;

                PlayerPrefs.SetInt("slot3_itemNumInInventory", -1);

                imgSlot3.sprite = sprSlotDefault;
                imgIconSlot3.gameObject.SetActive(false);
                break;

            case 4:
                slot4Card.gameObject.SetActive(true);
                slot4Card = null;

                PlayerPrefs.SetInt("slot4_itemNumInInventory", -1);

                imgSlot4.sprite = sprSlotDefault;
                imgIconSlot4.gameObject.SetActive(false);
                break;

            case 5:
                slot5Card.gameObject.SetActive(true);
                slot5Card = null;

                PlayerPrefs.SetInt("slot5_itemNumInInventory", -1);

                imgSlot5.sprite = sprSlotDefault;
                imgIconSlot5.gameObject.SetActive(false);
                break;

            case 6:
                slot6Card.gameObject.SetActive(true);
                slot6Card = null;

                PlayerPrefs.SetInt("slot6_itemNumInInventory", -1);

                imgSlot6.sprite = sprSlotDefault;
                imgIconSlot6.gameObject.SetActive(false);
                break;
        }        
    }
}
