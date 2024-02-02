using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.Purchasing;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using static DetailCard;

public class ShopController : MonoBehaviour, IStoreListener
{   
    [Header("Chest")]
    public PopUpOpenChest popUpOpenChest;
    public GameObject butBuyChest1, butBuyChest2;
    public GameObject butAdsChest1;
    public GameObject butKeyChest1, butKeyChest2;
    public bool isChest1Ads;

    public TMP_Text tKeyCountChest1, tKeyCountChest2;

    public List<DetailCard> items;

    IStoreController m_StoreController;

    public List<ConsumableItem> cItems;
    public List<ConsumableItem> nonCItems;
    public List<TMP_Text> tPrices;
    public List<string> prices;

    public bool openChestAccess;


    private void Awake()
    {
        Initialize();

        openChestAccess = true;

        if (!PlayerPrefs.HasKey("caseSpecialDropEngine"))
        {
            PlayerPrefs.SetString("caseSpecialDropEngine", "true");
            PlayerPrefs.SetString("caseSpecialDropBrakes", "true");
            PlayerPrefs.SetString("caseSpecialDropFuelSystem", "true");
            PlayerPrefs.SetString("caseSpecialDropSuspension", "true");
            PlayerPrefs.SetString("caseSpecialDropTransmission", "true");
        }

        Debug.Log("Test Chest = " + PlayerPrefs.GetString("caseSpecialDropEngine"));
    }

    public void Initialize()
    {
        SetupBuilder();        

        #region Chest 1       
        if (PlayerPrefs.HasKey("OfflineTimeLast"))
        {
            int seconds = OfflineTimeCheck.totalSeconds;

            Debug.Log("Seconds = " + seconds);

            PlayerPrefs.SetInt("AdsChestTimerSaveTime", PlayerPrefs.GetInt("AdsChestTimerSaveTime") + seconds);

            Debug.Log("All Time Spend = " + PlayerPrefs.GetInt("AdsChestTimerSaveTime"));            

            if (PlayerPrefs.GetInt("AdsChestTimerSaveTime") > 86400)
            {
                isChest1Ads = true;
            }
            else
            {
                isChest1Ads = false;
                StartCoroutine(AdsChestTimer());
            }
        }
        #endregion        

        ChestSettings();

        GameObject.Find("HubController").GetComponent<RedPushController>().CheckRedPush();
    }

    IEnumerator AdsChestTimer()
    {
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetInt("AdsChestTimerSaveTime", PlayerPrefs.GetInt("AdsChestTimerSaveTime") + 1);

        if (PlayerPrefs.GetInt("AdsChestTimerSaveTime") < 86400)
        {
            StartCoroutine(AdsChestTimer());
        } 
        else
        {
            isChest1Ads = true;
        }
    }

    public void ChestSettings()
    {
        #region Chest1
        if (PlayerPrefs.GetInt("playerKey1") > 0)
        {
            butKeyChest1.SetActive(true);
            butAdsChest1.SetActive(false);
            butBuyChest1.SetActive(false);

            tKeyCountChest1.text = PlayerPrefs.GetInt("playerKey1") + "/1";
        } 
        else if (isChest1Ads)
        {
            butKeyChest1.SetActive(false);
            butAdsChest1.SetActive(true);
            butBuyChest1.SetActive(false);
        } 
        else
        {
            butKeyChest1.SetActive(false);
            butAdsChest1.SetActive(false);
            butBuyChest1.SetActive(true);
        }
        #endregion

        #region Chest2
        if (PlayerPrefs.GetInt("playerKey2") > 0)
        {
            butKeyChest2.SetActive(true);
            butBuyChest2.SetActive(false);

            tKeyCountChest2.text = PlayerPrefs.GetInt("playerKey2") + "/1";
        }
        else
        {
            butKeyChest2.SetActive(false);
            butBuyChest2.SetActive(true);
        }
        #endregion
    }

    public void ButBuyMoney(int num)
    {
        if (num == 1)
        {
            GameObject.Find("AdsManager").GetComponent<AdsController>().ShowAds("freeMoney");
        } 
        else if (num == 2)
        {
            if (PlayerPrefs.GetInt("playerHard") >= 80)
            {
                PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 80);
                PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + 5760);

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyMoney(5760, "-", PlayerPrefs.GetInt("playerMoney"));

                GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
            }
            
        }
        else if (num == 3)
        {
            if (PlayerPrefs.GetInt("playerHard") >= 200)
            {
                PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 200);
                PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + 17280);

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyMoney(17280, "-", PlayerPrefs.GetInt("playerMoney"));

                GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
            }
        }        
    }

    public void ButBuyHard(int itemNum)
    {
        m_StoreController.InitiatePurchase(cItems[itemNum].Id);
    }

    public void ButBuyCar(int itemNum)
    {
        m_StoreController.InitiatePurchase(nonCItems[itemNum].Id);
    }

    public void AddHard(int value)
    {
        PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + value);

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_BuyHard(value, "-", PlayerPrefs.GetInt("playerHard"));

        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
    }

    public void ButChest1()
    {
        if (openChestAccess)
        {
            if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
            {
                openChestAccess = false;

                DetailCard card = items[0];

                string rarity;

                rarity = "common";

                popUpOpenChest.rarity = rarity;
                popUpOpenChest.card = card;
                popUpOpenChest.chestType = 1;

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenCase1("Key", rarity, card.itemType.ToString());

                ChestSettings();

                popUpOpenChest.Open(1, "tutorial");
            }
            else if (PlayerPrefs.GetInt("playerKey1") > 0)
            {
                openChestAccess = false;

                l1:
                DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

                if (PlayerPrefs.GetString("caseSpecialDropEngine") == "true")
                {
                    if (card.itemType.ToString() != "Engine")
                    {
                        goto l1;
                    }
                    else
                    {
                        PlayerPrefs.SetString("caseSpecialDropEngine", "false");
                        goto l2;
                    }
                }

                if (PlayerPrefs.GetString("caseSpecialDropBrakes") == "true")
                {
                    if (card.itemType.ToString() != "Brakes")
                    {
                        goto l1;
                    }
                    else
                    {
                        PlayerPrefs.SetString("caseSpecialDropBrakes", "false");
                        goto l2;
                    }
                }

                if (PlayerPrefs.GetString("caseSpecialDropFuelSystem") == "true")
                {
                    if (card.itemType.ToString() != "FuelSystem")
                    {
                        goto l1;
                    }
                    else
                    {
                        PlayerPrefs.SetString("caseSpecialDropFuelSystem", "false");
                        goto l2;
                    }
                }

                if (PlayerPrefs.GetString("caseSpecialDropTransmission") == "true")
                {
                    if (card.itemType.ToString() != "Transmission")
                    {
                        goto l1;
                    }
                    else
                    {
                        PlayerPrefs.SetString("caseSpecialDropTransmission", "false");
                        goto l2;
                    }
                }

                if (PlayerPrefs.GetString("caseSpecialDropSuspension") == "true")
                {
                    if (card.itemType.ToString() != "Suspension")
                    {
                        goto l1;
                    }
                    else
                    {
                        PlayerPrefs.SetString("caseSpecialDropSuspension", "false");
                        goto l2;
                    }
                }

                l2:
                if (card.itemType.ToString() == "Gun")
                {
                    PlayerPrefs.SetInt("unlockGun" + card.itemID, 1);
                }

                int rand = UnityEngine.Random.Range(1, 101);
                string rarity;

                if (rand <= 80)
                    rarity = "common";
                else
                    rarity = "rare";

                popUpOpenChest.rarity = rarity;
                popUpOpenChest.card = card;
                popUpOpenChest.chestType = 1;

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenCase1("Key", rarity, card.itemType.ToString());

                PlayerPrefs.SetInt("playerKey1", PlayerPrefs.GetInt("playerKey1") - 1);
                ChestSettings();

                popUpOpenChest.Open(1, "key");
            }
            else if (isChest1Ads)
            {
                openChestAccess = false;
                GameObject.Find("AdsManager").GetComponent<AdsController>().ShowAds("adsChest");
            }
            else
            {
                if (PlayerPrefs.GetInt("playerHard") >= 60)
                {
                    openChestAccess = false;

                    DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

                    if (card.itemType.ToString() == "Gun")
                    {
                        PlayerPrefs.SetInt("unlockGun" + card.itemID, 1);
                    }

                    int rand = UnityEngine.Random.Range(1, 101);
                    string rarity;

                    if (rand <= 80)
                        rarity = "common";
                    else
                        rarity = "rare";

                    popUpOpenChest.rarity = rarity;
                    popUpOpenChest.card = card;
                    popUpOpenChest.chestType = 1;

                    GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenCase1("Hard", rarity, card.itemType.ToString());

                    ChestSettings();
                    popUpOpenChest.Open(1, "hard");

                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 60);
                }
            }
        }
    }

    public void GiveAdsChest()
    {
        DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

        if (card.itemType.ToString() == "Gun")
        {
            PlayerPrefs.SetInt("unlockGun" + card.itemID, 1);
        }

        int rand = UnityEngine.Random.Range(1, 101);
        string rarity;

        if (rand <= 80)
            rarity = "common";
        else
            rarity = "rare";

        popUpOpenChest.rarity = rarity;
        popUpOpenChest.card = card;
        popUpOpenChest.chestType = 1;

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenCase1("Ads", rarity, card.itemType.ToString());

        isChest1Ads = false;
        PlayerPrefs.SetInt("AdsChestTimerSaveTime", 0);
        OfflineTimeCheck.totalSeconds = 0;
        //StartCoroutine(AdsChestTimer());

        Debug.Log("All Time Spend = " + PlayerPrefs.GetInt("AdsChestTimerSaveTime"));

        ChestSettings();

        popUpOpenChest.Open(1, "ads");
    }

    public void ButChest2()
    {
        if (openChestAccess)
        {
            if (PlayerPrefs.GetInt("playerKey2") > 0)
            {
                openChestAccess = false;

                DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

                if (card.itemType.ToString() == "Gun")
                {
                    PlayerPrefs.SetInt("unlockGun" + card.itemID, 1);
                }

                int rand = UnityEngine.Random.Range(1, 101);
                string rarity = "";

                if (rand <= 65)
                    rarity = "rare";

                if (rand > 65 && rand <= 98)
                    rarity = "epic";

                if (rand > 98)
                    rarity = "legendary";

                popUpOpenChest.rarity = rarity;
                popUpOpenChest.card = card;
                popUpOpenChest.chestType = 2;

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenCase2("Key", rarity, card.itemType.ToString());

                PlayerPrefs.SetInt("playerKey2", PlayerPrefs.GetInt("playerKey2") - 1);

                ChestSettings();
                popUpOpenChest.Open(2, "key");
            }
            else
            {
                if (PlayerPrefs.GetInt("playerHard") >= 300)
                {
                    openChestAccess = false;

                    DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

                    if (card.itemType.ToString() == "Gun")
                    {
                        PlayerPrefs.SetInt("unlockGun" + card.itemID, 1);
                    }

                    int rand = UnityEngine.Random.Range(1, 101);
                    string rarity = "";

                    if (rand <= 65)
                        rarity = "rare";

                    if (rand > 65 && rand <= 98)
                        rarity = "epic";

                    if (rand > 98)
                        rarity = "legendary";

                    popUpOpenChest.rarity = rarity;
                    popUpOpenChest.card = card;
                    popUpOpenChest.chestType = 2;

                    GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenCase2("Hard", rarity, card.itemType.ToString());

                    ChestSettings();
                    popUpOpenChest.Open(2, "hard");

                    PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 300);
                }
            }
        }
    }

    public void ButChest1Again()
    {
        if (openChestAccess)
        {
            if (PlayerPrefs.GetInt("playerHard") >= 50)
            {
                openChestAccess = false;

                StartCoroutine(OpenChest1Again());
            }
        }
    }

    IEnumerator OpenChest1Again()
    {
        popUpOpenChest.ButContinue();

        yield return new WaitForSeconds(0.4f);

        DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

        if (card.itemType.ToString() == "Gun")
        {
            PlayerPrefs.SetInt("unlockGun" + card.itemID, 1);
        }

        int rand = UnityEngine.Random.Range(1, 101);
        string rarity;

        if (rand <= 80)
            rarity = "common";
        else
            rarity = "rare";

        popUpOpenChest.rarity = rarity;
        popUpOpenChest.card = card;
        popUpOpenChest.chestType = 1;

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenCase1("Hard", rarity, card.itemType.ToString());

        ChestSettings();
        popUpOpenChest.Open(1, "hard");

        PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 50);
    }

    public void ButChest2Again()
    {
        if (openChestAccess)
        {
            if (PlayerPrefs.GetInt("playerHard") >= 280)
            {
                openChestAccess = false;

                StartCoroutine(OpenChest2Again());
            }
        }
    }

    IEnumerator OpenChest2Again()
    {
        popUpOpenChest.ButContinue();

        yield return new WaitForSeconds(0.4f);

        DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

        if (card.itemType.ToString() == "Gun")
        {
            PlayerPrefs.SetInt("unlockGun" + card.itemID, 1);
        }

        int rand = UnityEngine.Random.Range(1, 101);
        string rarity = "";

        if (rand <= 65)
            rarity = "rare";

        if (rand > 65 && rand <= 98)
            rarity = "epic";

        if (rand > 98)
            rarity = "legendary";

        popUpOpenChest.rarity = rarity;
        popUpOpenChest.card = card;
        popUpOpenChest.chestType = 2;

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenCase2("Hard", rarity, card.itemType.ToString());

        ChestSettings();
        popUpOpenChest.Open(2, "hard");

        PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 280);
    }

    void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        for (int i = 0; i < cItems.Count; i++)
        {
            builder.AddProduct(cItems[i].Id, ProductType.Consumable);
        }

        for (int i = 0; i < nonCItems.Count; i++)
        {
            builder.AddProduct(nonCItems[i].Id, ProductType.NonConsumable);
        }

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("initialize iap failed" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("initialize iap failed" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        print("Purchase Complite" + product.definition.id);

        if (product.definition.id == cItems[0].Id)
        {
            AddHard(80);
        } 
        else if (product.definition.id == cItems[1].Id)
        {
            AddHard(500);
        }
        else if (product.definition.id == cItems[2].Id)
        {
            AddHard(1200);
        }
        else if (product.definition.id == cItems[3].Id)
        {
            AddHard(2500);
        }
        else if (product.definition.id == cItems[4].Id)
        {
            AddHard(6500);
        }
        else if (product.definition.id == cItems[5].Id)
        {
            AddHard(14000);
        }
        else if (product.definition.id == nonCItems[0].Id)
        {
            GameObject.Find("PopUp Change Car").GetComponent<ChangeCarController>().BuyCarCallBack();
        }
        else if (product.definition.id == nonCItems[1].Id)
        {
            GameObject.Find("PopUp Change Car").GetComponent<ChangeCarController>().BuyCarCallBack();
        }
        else if (product.definition.id == nonCItems[2].Id)
        {
            GameObject.Find("PopUp Change Car").GetComponent<ChangeCarController>().BuyCarCallBack();
        }
        else if (product.definition.id == nonCItems[3].Id)
        {
            GameObject.Find("PopUp Change Car").GetComponent<ChangeCarController>().BuyCarCallBack();
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("purchase iap failed");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Success IAP Initialize");
        m_StoreController = controller;

        for (int i = 0; i < m_StoreController.products.all.Length; i++)
        {
            var product = m_StoreController.products.all[i];

            if (i < 6)
            {
                tPrices[i].text = product.metadata.localizedPriceString;
            }
            else
            {
                prices.Add(product.metadata.localizedPriceString);
            }
        }
    }
}

[Serializable]
public class ConsumableItem
{
    public string Name;
    public string Id;
    public float price;
}
