using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class ShopController : MonoBehaviour
{
    [Header("Chest")]
    int chest1SecondPassed;

    public PopUpOpenChest popUpOpenChest;
    public GameObject butBuyChest1;
    public GameObject butAdsChest1;
    public GameObject butKeyChest1;

    public List<DetailCard> items;

    private void Awake()
    {
        Initialize();
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

    void SetDateTime(string key, DateTime value)
    {
        string convertedToString = value.ToString("u", CultureInfo.InvariantCulture);

        PlayerPrefs.SetString(key, convertedToString);
    }

    DateTime GetDateTime(string key, DateTime defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string stored = PlayerPrefs.GetString(key);
            DateTime result = DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
        {
            return defaultValue;
        }
    }

    void CheckOffline()
    {

    }

    private void OnApplicationPause()
    {
        
    }
}
