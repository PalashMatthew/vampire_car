using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
using UnityEngine.Purchasing;

public class ShopController : MonoBehaviour, IStoreListener
{
    [Header("Chest")]
    public PopUpOpenChest popUpOpenChest;
    public GameObject butBuyChest1;
    public GameObject butAdsChest1;
    public GameObject butKeyChest1;

    public List<DetailCard> items;
    [SerializeField] public List<ConsumableItem> consumableItems;

    IStoreController m_StoreController;


    private void Awake()
    {
        Initialize();
        SetupBuilder();
    }

    public void Initialize()
    {
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

    public void ButBuyMoney(int _value)
    {
        PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + _value);
    }

    public void ButBuyHard(int _value)
    {
        PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + _value);
    }

    public void AddHard(int value)
    {
        PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + value);
    }

    public void AddMoney(int value)
    {
        PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + value);
    }

    public void ButBuyConsumable(int id)
    {
        m_StoreController.InitiatePurchase(consumableItems[id].Id);
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

        foreach (ConsumableItem cItem in consumableItems)
        {
            builder.AddProduct(cItem.Id, ProductType.Consumable);
        }

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("Initialize IAP Error!" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("Initialize IAP Error!" + error + message);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        print("Purchase Complite " + product.definition.id);

        if (product.definition.id == consumableItems[0].Id)
        {
            AddHard(80);
        }

        if (product.definition.id == consumableItems[1].Id)
        {
            AddHard(500);
        }

        if (product.definition.id == consumableItems[2].Id)
        {
            AddHard(1200);
        }

        if (product.definition.id == consumableItems[3].Id)
        {
            AddHard(2500);
        }

        if (product.definition.id == consumableItems[4].Id)
        {
            AddHard(6500);
        }

        if (product.definition.id == consumableItems[5].Id)
        {
            AddHard(14000);
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchased Failed!");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("IAP Initialized");
        m_StoreController = controller;
    }
}

[System.Serializable]
public class ConsumableItem
{
    public string Name;
    public string Id;
    public string desk;
    public string price;
}
