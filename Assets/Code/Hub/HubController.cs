using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HubController : MonoBehaviour
{
    [Header("Selection Menu")]
    public Image imgSelect;    
    public Image imgShop;
    public Image imgGarage;
    public Image imgFight;
    public Image imgUpgrade;
    public Image imgLock;

    [Header("Screen")]
    public GameObject screenShop;
    public GameObject screenGarage;
    public GameObject screenUpgrade;
    public GameObject screenLock;
    private string _prevScreen, _nextScreen;
    public GameObject imgBackground;

    [Header("Pass-Through Interface")]
    public Image imgHeader;
    public TMP_Text tFuelValue;
    public TMP_Text tMoneyValue;
    public TMP_Text tHardValue;    


    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        ShowResouces();
    }

    void Initialize()
    {
        Application.targetFrameRate = 60;

        screenGarage.SetActive(false);
        screenLock.SetActive(false);
        screenShop.SetActive(false);
        screenUpgrade.SetActive(false);

        imgHeader.DOFade(0.27f, 0);

        imgBackground.SetActive(false);

        _nextScreen = "fight";

        if (!PlayerPrefs.HasKey("playerMoney"))
        {
            PlayerPrefs.SetInt("playerMoney", 1000);
            PlayerPrefs.SetInt("playerHard", 20);
            PlayerPrefs.SetInt("playerFuelCurrent", 30);
            PlayerPrefs.SetInt("playerFuelMax", 30);
        }
    }

    public void ButPlay()
    {
        Application.LoadLevel(1);
    }

    #region Selection Menu
    public void ButFight()
    {
        if (_nextScreen != "fight")
        {
            _prevScreen = _nextScreen;
            _nextScreen = "fight";

            imgSelect.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.2f);

            imgFight.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 24f), 0.2f);
            imgFight.GetComponent<RectTransform>().DOScale(1.4f, 0.2f);

            imgShop.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgGarage.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgLock.GetComponent<RectTransform>().DOScale(1f, 0.2f);

            imgShop.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-435f, 0f), 0.2f);
            imgGarage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-243f, 0f), 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOAnchorPos(new Vector2(243f, 0f), 0.2f);
            imgLock.GetComponent<RectTransform>().DOAnchorPos(new Vector2(435f, 0f), 0.2f);

            imgHeader.DOFade(0.27f, 0.2f);

            StartCoroutine(ScreenSwap());
        }
    }

    public void ButShop()
    {
        if (_nextScreen != "shop")
        {
            _prevScreen = _nextScreen;
            _nextScreen = "shop";

            imgSelect.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-400.5f, 0), 0.2f);

            imgShop.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-400.5f, 24f), 0.2f);
            imgShop.GetComponent<RectTransform>().DOScale(1.4f, 0.2f);

            imgFight.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgGarage.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgLock.GetComponent<RectTransform>().DOScale(1f, 0.2f);

            imgFight.GetComponent<RectTransform>().DOAnchorPos(new Vector2(44f, 0f), 0.2f);
            imgGarage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-146f, 0f), 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOAnchorPos(new Vector2(243f, 0f), 0.2f);
            imgLock.GetComponent<RectTransform>().DOAnchorPos(new Vector2(435f, 0f), 0.2f);

            imgHeader.DOFade(0f, 0.2f);

            StartCoroutine(ScreenSwap());
        }
    }

    public void ButGarage()
    {
        if (_nextScreen != "garage")
        {
            _prevScreen = _nextScreen;
            _nextScreen = "garage";

            imgSelect.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-200f, 0), 0.2f);

            imgGarage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-200f, 24f), 0.2f);
            imgGarage.GetComponent<RectTransform>().DOScale(1.4f, 0.2f);

            imgFight.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgShop.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgLock.GetComponent<RectTransform>().DOScale(1f, 0.2f);

            imgFight.GetComponent<RectTransform>().DOAnchorPos(new Vector2(44f, 0f), 0.2f);
            imgShop.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-435f, 0f), 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOAnchorPos(new Vector2(243f, 0f), 0.2f);
            imgLock.GetComponent<RectTransform>().DOAnchorPos(new Vector2(435f, 0f), 0.2f);

            imgHeader.DOFade(0f, 0.2f);

            StartCoroutine(ScreenSwap());
        }
    }

    public void ButUpgrade()
    {
        if (_nextScreen != "upgrade")
        {
            _prevScreen = _nextScreen;
            _nextScreen = "upgrade";

            imgSelect.GetComponent<RectTransform>().DOAnchorPos(new Vector2(200f, 0), 0.2f);

            imgUpgrade.GetComponent<RectTransform>().DOAnchorPos(new Vector2(200f, 24f), 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOScale(1.4f, 0.2f);

            imgFight.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgShop.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgGarage.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgLock.GetComponent<RectTransform>().DOScale(1f, 0.2f);

            imgFight.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-44f, 0f), 0.2f);
            imgShop.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-435f, 0f), 0.2f);
            imgGarage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-243f, 0f), 0.2f);
            imgLock.GetComponent<RectTransform>().DOAnchorPos(new Vector2(435f, 0f), 0.2f);

            imgHeader.DOFade(0f, 0.2f);

            StartCoroutine(ScreenSwap());
        }
    }

    public void ButLock()
    {
        if (_nextScreen != "lock")
        {
            _prevScreen = _nextScreen;
            _nextScreen = "lock";

            imgSelect.GetComponent<RectTransform>().DOAnchorPos(new Vector2(400.5f, 0), 0.2f);

            imgLock.GetComponent<RectTransform>().DOAnchorPos(new Vector2(400.5f, 24f), 0.2f);
            imgLock.GetComponent<RectTransform>().DOScale(1.4f, 0.2f);

            imgFight.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgGarage.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOScale(1f, 0.2f);
            imgShop.GetComponent<RectTransform>().DOScale(1f, 0.2f);

            imgFight.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-44f, 0f), 0.2f);
            imgGarage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-243f, 0f), 0.2f);
            imgUpgrade.GetComponent<RectTransform>().DOAnchorPos(new Vector2(146f, 0f), 0.2f);
            imgShop.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-435f, 0f), 0.2f);

            imgHeader.DOFade(0f, 0.2f);

            StartCoroutine(ScreenSwap());
        }
    }
    #endregion

    IEnumerator ScreenSwap()
    {
        if (_prevScreen != "fight" && _nextScreen != "fight")
            imgBackground.SetActive(true);
        else 
            imgBackground.SetActive(false);

        #region NextScreen
        //Shop
        if (_nextScreen == "shop")
        {
            screenShop.SetActive(true);
            screenShop.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width*2, 0);
            screenShop.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

            if (_prevScreen == "garage")
            {
                screenGarage.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "upgrade")
            {
                screenUpgrade.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "lock")
            {
                screenLock.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 2, 0.2f);
            }
        }

        //Garage
        if (_nextScreen == "garage")
        {
            if (_prevScreen == "shop")
            {
                screenGarage.SetActive(true);
                screenGarage.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 2, 0);
                screenGarage.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenShop.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "fight")
            {
                screenGarage.SetActive(true);
                screenGarage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width * 2, 0);
                screenGarage.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);
            }

            if (_prevScreen == "upgrade")
            {
                screenGarage.SetActive(true);
                screenGarage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width * 2, 0);
                screenGarage.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenUpgrade.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "lock")
            {
                screenGarage.SetActive(true);
                screenGarage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width * 2, 0);
                screenGarage.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenLock.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 2, 0.2f);
            }
        }

        //Fight
        if (_nextScreen == "fight")
        {
            if (_prevScreen == "shop")
            {
                screenShop.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "garage")
            {
                screenGarage.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "upgrade")
            {
                screenUpgrade.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "lock")
            {
                screenLock.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 2, 0.2f);
            }
        }

        //Upgrade
        if (_nextScreen == "upgrade")
        {
            if (_prevScreen == "shop")
            {
                screenUpgrade.SetActive(true);
                screenUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 2, 0);
                screenUpgrade.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenShop.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "fight")
            {
                screenUpgrade.SetActive(true);
                screenUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 2, 0);
                screenUpgrade.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);
            }

            if (_prevScreen == "garage")
            {
                screenUpgrade.SetActive(true);
                screenUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 2, 0);
                screenUpgrade.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenGarage.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "lock")
            {
                screenUpgrade.SetActive(true);
                screenUpgrade.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width * 2, 0);
                screenUpgrade.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenLock.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 2, 0.2f);
            }
        }

        //Lock
        if (_nextScreen == "lock")
        {
            if (_prevScreen == "shop")
            {
                screenLock.SetActive(true);
                screenLock.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 2 * 2, 0);
                screenLock.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenShop.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "fight")
            {
                screenLock.SetActive(true);
                screenLock.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 2, 0);
                screenLock.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);
            }

            if (_prevScreen == "garage")
            {
                screenLock.SetActive(true);
                screenLock.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 2, 0);
                screenLock.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenGarage.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width * 2, 0.2f);
            }

            if (_prevScreen == "upgrade")
            {
                screenLock.SetActive(true);
                screenLock.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 2, 0);
                screenLock.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f);

                screenUpgrade.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width * 2, 0.2f);
            }
        }
        #endregion

        yield return new WaitForSeconds(0.2f);

        imgBackground.SetActive(false);

        if (_prevScreen == "shop")
        {
            screenShop.SetActive(false);
        }

        if (_prevScreen == "garage")
        {
            screenGarage.SetActive(false);
        }

        if (_prevScreen == "upgrade")
        {
            screenUpgrade.SetActive(false);
        }

        if (_prevScreen == "lock")
        {
            screenLock.SetActive(false);
        }
    }

    void ShowResouces()
    {
        tFuelValue.text = PlayerPrefs.GetInt("playerFuelCurrent") + "/" + PlayerPrefs.GetInt("playerFuelMax");
        tMoneyValue.text = PlayerPrefs.GetInt("playerMoney").ToString();
        tHardValue.text = PlayerPrefs.GetInt("playerHard").ToString();
    }    
}
