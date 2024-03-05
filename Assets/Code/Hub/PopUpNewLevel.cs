using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpNewLevel : MonoBehaviour
{
    public HubController hubController;
    private PopUpController _popUpController;

    public TMP_Text tLevel;
    public TMP_Text tReward;

    public List<int> reward;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        //CheckPlayerExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("playerExp", PlayerPrefs.GetInt("playerExp") + 250);
            CheckPlayerExp();
        }
    }

    void Initialize()
    {
        tLevel.text = PlayerPrefs.GetInt("playerLevel").ToString();
        tReward.text = reward[PlayerPrefs.GetInt("playerLevel")].ToString();

        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
    }

    public void CheckPlayerExp()
    {
        if (PlayerPrefs.GetInt("playerExp") >= hubController.playerExpNeed[PlayerPrefs.GetInt("playerLevel") - 1])
        {
            int newExp = PlayerPrefs.GetInt("playerExp") - hubController.playerExpNeed[PlayerPrefs.GetInt("playerLevel") - 1];

            PlayerPrefs.SetInt("playerLevel", PlayerPrefs.GetInt("playerLevel") + 1);
            PlayerPrefs.SetInt("playerExp", newExp);

            ShowPopUp();

            GameObject.Find("HubController").GetComponent<RedPushController>().CheckRedPush();
        }
        else
        {
            if (PlayerPrefs.GetString("tutorialHubComplite") != "true")
            {
                GameObject.Find("TutorialController").GetComponent<TutorialControllerHub>().CheckTutorialPlay();
            }
            else
            {
                if (PlayerPrefs.GetInt("rateActivate") == 1)
                {
                    if (PlayerPrefs.GetInt("maxLocation") == 3 ||
                        PlayerPrefs.GetInt("maxLocation") == 5 ||
                        PlayerPrefs.GetInt("maxLocation") == 7 ||
                        PlayerPrefs.GetInt("maxLocation") == 9)
                    {
                        GameObject.Find("PopUp Rate").GetComponent<PopUpRate>().ButOpen();
                    }
                }
            }
        }
    }

    public void ShowPopUp()
    {
        _popUpController = GetComponent<PopUpController>();
        _popUpController.OpenPopUp();
        Initialize();
    }

    public void ButClosed()
    {
        PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") + reward[PlayerPrefs.GetInt("playerLevel")]);
        _popUpController.ClosedPopUp();

        StartCoroutine(CheckAnother());
    }

    IEnumerator CheckAnother()
    {
        yield return new WaitForSeconds(0.5f);
        CheckPlayerExp();
    }
}
