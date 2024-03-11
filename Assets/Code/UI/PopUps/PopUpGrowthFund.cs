using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpGrowthFund : MonoBehaviour
{
    private PopUpController _popUpController;

    public List<string> freePassRewardName;
    public List<string> rarePassRewardName;
    public List<string> epicPassRewardName;

    public List<int> freePassRewardValue;
    public List<int> rarePassRewardValue;
    public List<int> epicPassRewardValue;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        Initialize();
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }

    void Initialize()
    {

    }

    public void BuyRarePass()
    {

    }

    public void BuyEpicPass()
    {

    }

    public void ButCellFree(int num)
    {
        int playerLevel = PlayerPrefs.GetInt("playerLevel");

        if (num <= playerLevel)
        {
            if (freePassRewardName[num - 1] == "hard")
            {
                PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + freePassRewardValue[num - 1]);
            }
        }
    }

    public void ButCellRare(int num)
    {

    }

    public void ButCellEpic(int num)
    {

    }
}
