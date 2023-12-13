using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.Purchasing;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class ShopController : MonoBehaviour, IStoreListener
{
    [Header("Chest")]
    public PopUpOpenChest popUpOpenChest;
    public GameObject butBuyChest1;
    public GameObject butAdsChest1;
    public GameObject butKeyChest1;

    public List<DetailCard> items;

    IStoreController m_StoreController;

    public List<ConsumableItem> cItems;
    public List<TMP_Text> tPrices;


    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetupBuilder();

        //#region Chest 1       
        //TimeSpan timePassed = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
        //int secondsPassed = (int)timePassed.TotalSeconds;

        //PlayerPrefs.SetInt("Chest1GeneralTimePassed", PlayerPrefs.GetInt("Chest1GeneralTimePassed") + secondsPassed);
        //chest1SecondPassed = PlayerPrefs.GetInt("Chest1GeneralTimePassed");

        //if (chest1SecondPassed > 60)
        //{
        //    butAdsChest1.SetActive(true);
        //    butBuyChest1.SetActive(false);
        //    butKeyChest1.SetActive(false);
        //}
        //else if (PlayerPrefs.GetInt("playerKey1") > 0)
        //{
        //    butAdsChest1.SetActive(false);
        //    butBuyChest1.SetActive(false);
        //    butKeyChest1.SetActive(true);
        //}
        //else
        //{
        //    butAdsChest1.SetActive(false);
        //    butBuyChest1.SetActive(true);
        //    butKeyChest1.SetActive(false);
        //}

        //StartCoroutine(CheckTimeChest1());
        //#endregion        
    }

    IEnumerator CheckTimeChest1()
    {
        yield return new WaitForSeconds(1);

        StartCoroutine(CheckTimeChest1());
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
            }
            
        }
        else if (num == 3)
        {
            if (PlayerPrefs.GetInt("playerHard") >= 200)
            {
                PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 200);
                PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + 17280);
            }
        }        
    }

    public void ButBuyHard(int itemNum)
    {
        m_StoreController.InitiatePurchase(cItems[itemNum].Id);
    }

    public void AddHard(int value)
    {
        PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + value);
    }

    public void ButChest1()
    {
        if (PlayerPrefs.GetInt("playerHard") >= 60)
        {
            DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

            int rand = UnityEngine.Random.Range(1, 101);
            string rarity;

            if (rand <= 80)
                rarity = "common";
            else
                rarity = "rare";            

            popUpOpenChest.rarity = rarity;
            popUpOpenChest.card = card;

            popUpOpenChest.Open();

            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 60);
        }
    }

    public void ButChest2()
    {
        if (PlayerPrefs.GetInt("playerHard") >= 300)
        {
            DetailCard card = items[UnityEngine.Random.Range(0, items.Count)];

            int rand = UnityEngine.Random.Range(1, 101);
            string rarity = "";

            if (rand <= 45)
                rarity = "rare";
            
            if (rand > 45 && rand <= 95)
                rarity = "epic";

            if (rand > 95)
                rarity = "legendary";

            popUpOpenChest.rarity = rarity;
            popUpOpenChest.card = card;

            popUpOpenChest.Open();

            PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 300);
        }
    }

    void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        for (int i = 0; i < cItems.Count; i++)
        {
            builder.AddProduct(cItems[i].Id, ProductType.Consumable);
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

            tPrices[i].text = product.metadata.localizedPriceString;
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
