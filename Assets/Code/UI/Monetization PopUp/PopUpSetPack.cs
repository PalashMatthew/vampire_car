using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSetPack : MonoBehaviour
{
    private PopUpController _popUpController;

    public GameObject outline1, outline2, outline3, outline4, outline5;

    public Image imgDetail1, imgDetail2, imgDetail3, imgDetail4, imgDetail5;

    int activeDetail;

    public TMP_Text tNewPrice, tOldPrice;

    public GameObject butStarterPackObj;

    public List<Sprite> sprEngine;
    public List<Sprite> sprBrakes;
    public List<Sprite> sprFuelSystem;
    public List<Sprite> sprSuspension;
    public List<Sprite> sprTransmission;

    public TMP_Text tDescription;

    public SetCard setCard1, setCard2, setCard3, setCard4, setCard5;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void ButOpen()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpSetInApp("Open");

        _popUpController.OpenPopUp();

        Initialize();
    }

    public void ButClosed()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpSetInApp("Closed");

        _popUpController.ClosedPopUp();
    }

    void Initialize()
    {
        ButChangeDetail(1);

        string newPrice = GameObject.Find("HubController").GetComponent<ShopController>().prices[6];
        string oldPrice = GameObject.Find("HubController").GetComponent<ShopController>().prices[7];

        tNewPrice.text = newPrice;
        tOldPrice.text = oldPrice;
    }

    public void ButChangeDetail(int _id)
    {
        switch (_id)
        {
            case 1:
                outline1.SetActive(true);
                outline2.SetActive(false);
                outline3.SetActive(false);
                outline4.SetActive(false);
                outline5.SetActive(false);

                imgDetail1.sprite = sprEngine[0];
                imgDetail2.sprite = sprBrakes[0];
                imgDetail3.sprite = sprFuelSystem[0];
                imgDetail4.sprite = sprSuspension[0];
                imgDetail5.sprite = sprTransmission[0];

                activeDetail = 1;

                string s1 = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + setCard1.setID + "Desk");
                var replacement1 = s1.Replace("{value}", setCard1.value3.ToString());
                tDescription.text = replacement1;
                break;

            case 2:
                outline1.SetActive(false);
                outline2.SetActive(true);
                outline3.SetActive(false);
                outline4.SetActive(false);
                outline5.SetActive(false);

                imgDetail1.sprite = sprEngine[1];
                imgDetail2.sprite = sprBrakes[1];
                imgDetail3.sprite = sprFuelSystem[1];
                imgDetail4.sprite = sprSuspension[1];
                imgDetail5.sprite = sprTransmission[1];

                activeDetail = 2;

                string s2 = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + setCard2.setID + "Desk");
                var replacement2 = s2.Replace("{value}", setCard2.value3.ToString());
                tDescription.text = replacement2;
                break;

            case 3:
                outline1.SetActive(false);
                outline2.SetActive(false);
                outline3.SetActive(true);
                outline4.SetActive(false);
                outline5.SetActive(false);

                imgDetail1.sprite = sprEngine[2];
                imgDetail2.sprite = sprBrakes[2];
                imgDetail3.sprite = sprFuelSystem[2];
                imgDetail4.sprite = sprSuspension[2];
                imgDetail5.sprite = sprTransmission[2];

                activeDetail = 3;

                string s3 = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + setCard3.setID + "Desk");
                var replacement3 = s3.Replace("{value}", setCard3.value3.ToString());
                tDescription.text = replacement3;
                break;

            case 4:
                outline1.SetActive(false);
                outline2.SetActive(false);
                outline3.SetActive(false);
                outline4.SetActive(true);
                outline5.SetActive(false);

                imgDetail1.sprite = sprEngine[3];
                imgDetail2.sprite = sprBrakes[3];
                imgDetail3.sprite = sprFuelSystem[3];
                imgDetail4.sprite = sprSuspension[3];
                imgDetail5.sprite = sprTransmission[3];

                activeDetail = 4;

                string s4 = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + setCard4.setID + "Desk");
                var replacement4 = s4.Replace("{value}", setCard4.value3.ToString());
                tDescription.text = replacement4;
                break;

            case 5:
                outline1.SetActive(false);
                outline2.SetActive(false);
                outline3.SetActive(false);
                outline4.SetActive(false);
                outline5.SetActive(true);

                imgDetail1.sprite = sprEngine[4];
                imgDetail2.sprite = sprBrakes[4];
                imgDetail3.sprite = sprFuelSystem[4];
                imgDetail4.sprite = sprSuspension[4];
                imgDetail5.sprite = sprTransmission[4];

                activeDetail = 5;

                string s5 = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_" + setCard5.setID + "Desk");
                var replacement5 = s5.Replace("{value}", setCard5.value3.ToString());
                tDescription.text = replacement5;
                break;
        }
    }

    public void ButBuy()
    {
        GameObject.Find("HubController").GetComponent<ShopController>().ButBuySetPack();
    }

    public void CallBackPurchased()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PopUpSetInAppBuy();

        AddItem(activeDetail);

        butStarterPackObj.SetActive(false);
        ButClosed();
    }

    public void AddItem(int setNum)
    {
        int _idEngine = 0;
        int _idBrakes = 0;
        int _idFuelSystem = 0;
        int _idSuspension = 0;
        int _idTransmission = 0;

        string _itemType;
        string _itemRarity = "rare";

        switch (setNum)
        {
            case 1:
                _idEngine = 2002;
                _idBrakes = 3002;
                _idFuelSystem = 4002;
                _idSuspension = 5002;
                _idTransmission = 6002;               
                break;

            case 2:
                _idEngine = 2003;
                _idBrakes = 3003;
                _idFuelSystem = 4003;
                _idSuspension = 5003;
                _idTransmission = 6003;
                break;

            case 3:
                _idEngine = 2005;
                _idBrakes = 3005;
                _idFuelSystem = 4005;
                _idSuspension = 5005;
                _idTransmission = 6005;
                break;

            case 4:
                _idEngine = 2006;
                _idBrakes = 3006;
                _idFuelSystem = 4006;
                _idSuspension = 5006;
                _idTransmission = 6006;
                break;

            case 5:
                _idEngine = 2001;
                _idBrakes = 3001;
                _idFuelSystem = 4001;
                _idSuspension = 5001;
                _idTransmission = 6001;
                break;
        }

        _itemType = "Engine";

        PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);
        PlayerPrefs.SetInt("item" + _itemType + "ID" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _idEngine);
        PlayerPrefs.SetString("item" + _itemType + "Rarity" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _itemRarity);

        _itemType = "Brakes";

        PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);
        PlayerPrefs.SetInt("item" + _itemType + "ID" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _idBrakes);
        PlayerPrefs.SetString("item" + _itemType + "Rarity" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _itemRarity);

        _itemType = "FuelSystem";

        PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);
        PlayerPrefs.SetInt("item" + _itemType + "ID" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _idFuelSystem);
        PlayerPrefs.SetString("item" + _itemType + "Rarity" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _itemRarity);

        _itemType = "Suspension";

        PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);
        PlayerPrefs.SetInt("item" + _itemType + "ID" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _idSuspension);
        PlayerPrefs.SetString("item" + _itemType + "Rarity" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _itemRarity);

        _itemType = "Transmission";

        PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);
        PlayerPrefs.SetInt("item" + _itemType + "ID" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _idTransmission);
        PlayerPrefs.SetString("item" + _itemType + "Rarity" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _itemRarity);
    }
}
