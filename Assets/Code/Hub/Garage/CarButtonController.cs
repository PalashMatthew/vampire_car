using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Висит на кнопке покупки машины
public class CarButtonController : MonoBehaviour
{
    [Header("Car Settings")]
    public string carName;
    public int carID;
    public bool isCarPurchased;

    [Header("Text")]
    public TMP_Text tCarName;
    public TMP_Text tCarLevel;
    public TMP_Text tCarPriceSoft;
    public TMP_Text tCarPriceHard;
    public TMP_Text tCarPriceReal;

    [Header("Visualization")]
    public GameObject imgOutline;
    public Image imgButton;
    public Sprite sprButtonPurchased;
    public Sprite sprButtonNonPurchased;

    [Header("Purchase Settings")]
    public float price;
    public enum PriceType
    {
        Soft,
        Hard,
        Real
    }
    public PriceType priceType;
    public GameObject objSoft;
    public GameObject objHard;
    public GameObject objReal;

    [Header("CarImage")]
    public Image imgCar;
    public Sprite sprCar1, sprCar2, sprCar3;

    [Header("Other")]
    public GameObject panelLevel;
    public GameObject panelPrice;

    public ChangeCarController changeCarController;
    public PopUpCarUpgrade popUpCarUpgrade;

    public int carShopID;

    public GameObject arrowObj;


    public void Initialize()
    {
        if (PlayerPrefs.GetInt(carName + "carPurchased") == 1)
        {
            isCarPurchased = true;
        }

        if (PlayerPrefs.GetInt(carName + "carPurchased") == 2)
        {
            isCarPurchased = false;
        }

        if (isCarPurchased)
        {
            imgButton.sprite = sprButtonPurchased;
            panelLevel.SetActive(true);
            panelPrice.SetActive(false);

            tCarLevel.text = "Lv " + PlayerPrefs.GetInt(carName + "carLevel");
        }

        if (!isCarPurchased)
        {
            imgButton.sprite = sprButtonNonPurchased;
            panelLevel.SetActive(false);
            panelPrice.SetActive(true);

            if (priceType == PriceType.Soft)
            {
                objSoft.SetActive(true);
                objHard.SetActive(false);
                objReal.SetActive(false);

                tCarPriceSoft.text = price + "";
            }

            if (priceType == PriceType.Hard)
            {
                objSoft.SetActive(false);
                objHard.SetActive(true);
                objReal.SetActive(false);

                tCarPriceHard.text = price + "";
            }

            if (priceType == PriceType.Real)
            {
                objSoft.SetActive(false);
                objHard.SetActive(false);
                objReal.SetActive(true);

                tCarPriceReal.text = GameObject.Find("HubController").GetComponent<ShopController>().prices[carShopID];
            }
        }

        CheckArrow();
    }

    public void ButSelect()
    {
        imgOutline.SetActive(true);
        PlayerPrefs.SetString("activeCarID", carName);
        changeCarController.SelectCar(gameObject);
    }

    public void ButUnselect()
    {
        imgOutline.SetActive(false);
    }

    public void CheckArrow()
    {
        if (PlayerPrefs.GetInt(carName + "carPurchased") == 1)
        {
            if (PlayerPrefs.GetInt(carName + "carLevel") < 40 &&
                PlayerPrefs.GetInt("playerTitan") >= popUpCarUpgrade.titanCount[PlayerPrefs.GetInt(carName + "carLevel")] &&
                PlayerPrefs.GetInt("playerLevel") > PlayerPrefs.GetInt(carName + "carLevel") &&
                PlayerPrefs.GetInt("playerMoney") >= popUpCarUpgrade.upgradePrice[PlayerPrefs.GetInt(carName + "carLevel")])
            {
                arrowObj.SetActive(true);
            }
            else
            {
                arrowObj.SetActive(false);
            }
        }
        else
        {
            arrowObj.SetActive(false);
        }
    }
}
