using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarageController : MonoBehaviour
{
    public GameObject canvasChangeCar;
    [HideInInspector] public CarButtonController activeCarObj;
    public List<GameObject> carsButtons;

    public ItemCell activeItem;

    [Header("Cell")]
    public GameObject cellPrefab;    

    [Header("Scroll Gun")]
    public Transform scrollGun;
    public List<GameObject> itemGunInst;

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


    private void OnEnable()
    {
        PlayerPrefs.SetInt("itemCountGun", 4);
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

        #region Slot Settings
        //if (PlayerPrefs.Set)
        #endregion
    }

    void LoadItem()
    {
        // Оружие
        int cellCount = PlayerPrefs.GetInt("itemCountGun");

        for (int i = 0; i < cellCount; i++)
        {
            GameObject _cell = Instantiate(cellPrefab, transform.position, transform.rotation);
            _cell.transform.parent = scrollGun.transform;
            _cell.transform.localScale = Vector3.one;

            _cell.GetComponent<ItemCell>().itemID = PlayerPrefs.GetInt("itemGunID" + i);
            _cell.GetComponent<ItemCell>().currentLevel = PlayerPrefs.GetInt("itemGunLevel" + i);
            _cell.GetComponent<ItemCell>().itemRarity = PlayerPrefs.GetString("itemGunRarity" + i);
            _cell.GetComponent<ItemCell>().itemType = PlayerPrefs.GetString("itemGunType" + i);

            itemGunInst.Add(_cell);
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
}
