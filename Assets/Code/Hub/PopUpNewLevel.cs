using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpNewLevel : MonoBehaviour
{
    public HubController hubController;
    private PopUpController _popUpController;

    public TMP_Text tLevel;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        CheckPlayerExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("playerExp", PlayerPrefs.GetInt("playerExp") + 40);
            CheckPlayerExp();
        }
    }

    void Initialize()
    {
        tLevel.text = PlayerPrefs.GetInt("playerLevel").ToString();
    }

    public void CheckPlayerExp()
    {
        if (PlayerPrefs.GetInt("playerExp") >= hubController.playerExpNeed[PlayerPrefs.GetInt("playerLevel") - 1])
        {
            int newExp = PlayerPrefs.GetInt("playerExp") - hubController.playerExpNeed[PlayerPrefs.GetInt("playerLevel") - 1];

            PlayerPrefs.SetInt("playerLevel", PlayerPrefs.GetInt("playerLevel") + 1);
            PlayerPrefs.SetInt("playerExp", newExp);

            ShowPopUp();
        }
    }

    public void ShowPopUp()
    {
        _popUpController.OpenPopUp();
        Initialize();
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }
}
