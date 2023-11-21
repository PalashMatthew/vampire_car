using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("Chest")]
    public PopUpOpenChest popUpOpenChest;

    public List<DetailCard> items;

    private void OnEnable()
    {
        Initialize();
    }

    void Initialize()
    {

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
            DetailCard card = items[Random.Range(0, items.Count)];

            int rand = Random.Range(1, 101);
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
            DetailCard card = items[Random.Range(0, items.Count)];

            int rand = Random.Range(1, 101);
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
}
