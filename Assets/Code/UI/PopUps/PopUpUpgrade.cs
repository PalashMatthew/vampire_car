using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PopUpUpgrade : MonoBehaviour
{
    public Image cardGun1, cardGun2, cardGun3;
    public Image cardPassive1, cardPassive2, cardPassive3;

    private PopUpController _popUpController;
    public UpgradeController upgradeController;
    private WaveController _waveController;

    public TMP_Text tScrewValue;

    public GameObject pauseBut;

    public GameObject butRerollScrew;

    public bool isWaveUpgrade = false;
    public bool isOpen;

    public bool isDefferenUpgrade;

    [Header("Reroll")]
    public TMP_Text tRerollPrice;
    float rerollPrice;

    public GameObject panelPassiveUpgrade;
    public GameObject panelGunUpgrade;

    public TMP_Text tHeader;

    public GameObject butRerollAds1, butRerollAds2;
    public GameObject butRerollHard1, butRerollHard2;

    public static bool buttonTapAccess;

    public TMP_Text tPassiveInfo1, tPassiveInfo2, tPassiveInfo3;

    public TMP_Text tHardValue;


    private void Start()
    {
        _waveController = GameObject.Find("GameplayController").GetComponent<WaveController>();
        _popUpController = GetComponent<PopUpController>();

        panelPassiveUpgrade.SetActive(true);
        panelGunUpgrade.SetActive(false);
    }

    private void Update()
    {
        tHardValue.text = "" + PlayerPrefs.GetInt("playerHard");
    }

    void CardGunAnimation()
    {
        tHeader.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_gunUpgrade") + " " + upgradeController.upgradeLevelCount + "/" + 1;

        cardGun1.GetComponent<RectTransform>().DOAnchorPosX(-350, 0f).SetUpdate(true);
        cardGun2.GetComponent<RectTransform>().DOAnchorPosX(0f, 0f).SetUpdate(true);
        cardGun3.GetComponent<RectTransform>().DOAnchorPosX(350, 0f).SetUpdate(true);

        Sequence cardGunAnim = DOTween.Sequence();

        cardGun1.GetComponent<RectTransform>().localScale = Vector3.zero;
        cardGun2.GetComponent<RectTransform>().localScale = Vector3.zero;
        cardGun3.GetComponent<RectTransform>().localScale = Vector3.zero;

        cardGunAnim.Insert(0.3f, cardGun1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardGunAnim.Insert(0.4f, cardGun2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardGunAnim.Insert(0.5f, cardGun3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
    }

    public void CardPassiveAnim()
    {
        tHeader.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_passiveUpgrade");

        tPassiveInfo1.gameObject.SetActive(true);
        tPassiveInfo2.gameObject.SetActive(true);
        tPassiveInfo3.gameObject.SetActive(true);

        cardPassive1.GetComponent<RectTransform>().DOAnchorPosX(-350, 0f).SetUpdate(true);
        cardPassive2.GetComponent<RectTransform>().DOAnchorPosX(0f, 0f).SetUpdate(true);
        cardPassive3.GetComponent<RectTransform>().DOAnchorPosX(350, 0f).SetUpdate(true);

        Sequence cardPassiveAnim = DOTween.Sequence();

        cardPassive1.GetComponent<RectTransform>().localScale = Vector3.zero;
        cardPassive2.GetComponent<RectTransform>().localScale = Vector3.zero;
        cardPassive3.GetComponent<RectTransform>().localScale = Vector3.zero;

        tPassiveInfo1.gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        tPassiveInfo2.gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        tPassiveInfo3.gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;

        cardPassiveAnim.Insert(0.3f, cardPassive1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardPassiveAnim.Join(tPassiveInfo1.gameObject.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);

        cardPassiveAnim.Insert(0.4f, cardPassive2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardPassiveAnim.Join(tPassiveInfo2.gameObject.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);

        cardPassiveAnim.Insert(0.5f, cardPassive3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardPassiveAnim.Join(tPassiveInfo3.gameObject.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);

    }

    public IEnumerator ChoiceGunCard()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        if (upgradeController.cardGunAccept != null)
        {
            foreach (UpgradeCardController _card in upgradeController.cardsGunController)
            {
                if (upgradeController.cardGunAccept == _card)
                {
                    _card.GetComponent<RectTransform>().DOScale(0, 0.3f).SetUpdate(true).SetEase(Ease.InBack);

                    _card.ChoiceCard();
                }
            }
        }

        upgradeController.cardGunAccept = null;

        //CardGunAnimation();

        yield return new WaitForSecondsRealtime(0.3f);

        if (upgradeController.upgradeLevelCount > 0)
        {
            InitializeGunUpgrade();
            buttonTapAccess = true;
        }
        else
        {
            //StartCoroutine(EndUpgrade());
            ButClosed();
            _waveController.StartWave();
        }
    }

    public void ActivateCardObj(int _count)
    {
        if (_count == 3)
        {
            cardGun1.gameObject.SetActive(true);
            cardGun2.gameObject.SetActive(true);
            cardGun3.gameObject.SetActive(true);
        }

        if (_count == 2)
        {
            cardGun1.gameObject.SetActive(true);
            cardGun2.gameObject.SetActive(true);
            cardGun3.gameObject.SetActive(false);
        }

        if (_count == 1)
        {
            cardGun1.gameObject.SetActive(true);
            cardGun2.gameObject.SetActive(false);
            cardGun3.gameObject.SetActive(false);
        }
    }

    public IEnumerator ChoicePassiveCard()
    {
        tPassiveInfo1.gameObject.GetComponent<RectTransform>().DOScale(0, 0.3f).SetUpdate(true).SetEase(Ease.InBack);
        tPassiveInfo2.gameObject.GetComponent<RectTransform>().DOScale(0, 0.3f).SetUpdate(true).SetEase(Ease.InBack);
        tPassiveInfo3.gameObject.GetComponent<RectTransform>().DOScale(0, 0.3f).SetUpdate(true).SetEase(Ease.InBack);

        yield return new WaitForSecondsRealtime(0.5f);      

        foreach (UpgradeCardController _card in upgradeController.cardsPassiveController)
        {
            if (upgradeController.cardPassiveAccept == _card)
            {
                _card.GetComponent<RectTransform>().DOScale(0, 0.3f).SetUpdate(true).SetEase(Ease.InBack);

                _card.ChoiceCard();
            }
        }

        upgradeController.cardPassiveAccept = null;
        
        yield return new WaitForSecondsRealtime(0.3f);        

        if (upgradeController.upgradeLevelCount > 0)
        {
            panelPassiveUpgrade.SetActive(false);
            panelGunUpgrade.SetActive(true);
            InitializeGunUpgrade();

            buttonTapAccess = true;
        } 
        else
        {
            _waveController.StartWave();
            ButClosed();
            //StartCoroutine(EndUpgrade());
        }        
    }

    IEnumerator EndUpgrade()
    {
        yield return new WaitForSecondsRealtime(0.8f);        
        ButClosed();
    }

    public void ButOpen()
    {
        pauseBut.SetActive(false);

        GameplayController.isPause = true;
        isOpen = true;
        Time.timeScale = 0;

        buttonTapAccess = true;

        panelPassiveUpgrade.SetActive(true);
        panelGunUpgrade.SetActive(false);

        butRerollAds1.SetActive(true);
        butRerollAds2.SetActive(true);
        butRerollHard1.SetActive(true);
        butRerollHard2.SetActive(true);        

        //CardAnimation();
        CardPassiveAnim();
        //upgradeController.GenerateUpgrades();

        InitializePassiveUpgrade();
    }

    void InitializePassiveUpgrade()
    {
        //CardAnimation();
        //CardPassiveAnim();

        upgradeController.GeneratePassiveCards();
        _popUpController.OpenPopUp();

        cardPassive1.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardPassive2.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardPassive3.gameObject.GetComponent<UpgradeCardController>().Initialize();

        tScrewValue.text = GlobalStats.screwCount.ToString();
    }

    void InitializeGunUpgrade()
    {
        CardGunAnimation();

        upgradeController.GenerateGunCards();

        //_popUpController.OpenPopUp();

        cardGun1.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardGun2.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardGun3.gameObject.GetComponent<UpgradeCardController>().Initialize();

        tScrewValue.text = GlobalStats.screwCount.ToString();

        upgradeController.SlotInitialize();
        upgradeController.UpdateTextLevels();

        upgradeController.upgradeLevelCount--;

        //RerollInitialize();
    }

    public void ButClosed()
    {
        pauseBut.SetActive(true);

        GameplayController.isPause = false;
        isOpen = false;
        Time.timeScale = 1;
        _popUpController.ClosedPopUp();
        isWaveUpgrade = false;
    }

    public void ButRerollAds(string _type)
    {
        if (_type == "gun")
        {
            StartCoroutine(ReturnButtonTapAccept());
            GameObject.Find("AdsManager").GetComponent<AdsController>().ShowAds("gunReroll");
            butRerollAds1.SetActive(false);
            butRerollAds2.SetActive(false);
            //buttonTapAccess = false;
        }

        if (_type == "passive")
        {
            StartCoroutine(ReturnButtonTapAccept());
            GameObject.Find("AdsManager").GetComponent<AdsController>().ShowAds("passiveReroll");
            butRerollAds1.SetActive(false);
            butRerollAds2.SetActive(false);
            //buttonTapAccess = false;
        }

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Reroll("ads");
    }

    public void CallBackRerollAds(string _type)
    {
        StartCoroutine(StartRerollAds(_type));
    }

    IEnumerator ReturnButtonTapAccept()
    {
        yield return new WaitForSeconds(1);
        buttonTapAccess = true;
    }

    IEnumerator StartRerollAds(string _type)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        buttonTapAccess = true;

        if (_type == "gun")
        {
            StartCoroutine(RerollGun());
        }

        if (_type == "passive")
        {
            StartCoroutine(RerollPassive());
        }        
    }

    public void ButRerollHard(string _type)
    {
        if (buttonTapAccess)
        {            
            if (_type == "gun" && PlayerPrefs.GetInt("playerHard") >= 10)
            {
                PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 10);
                StartCoroutine(RerollGun());

                buttonTapAccess = false;

                butRerollHard1.SetActive(false);
                butRerollHard2.SetActive(false);

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Reroll("hard");
            }

            if (_type == "passive" && PlayerPrefs.GetInt("playerHard") >= 10)
            {
                PlayerPrefs.SetInt("playerHard", PlayerPrefs.GetInt("playerHard") - 10);
                StartCoroutine(RerollPassive());

                buttonTapAccess = false;

                butRerollHard1.SetActive(false);
                butRerollHard2.SetActive(false);

                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Reroll("hard");
            }

            if (PlayerPrefs.GetInt("playerHard") < 10)
            {
                if (GameObject.Find("Firebase") != null)
                    GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_NotEnoughHard();
            }              

            
        }
    }

    IEnumerator RerollGun()
    {
        cardGun1.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);
        cardGun2.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);
        cardGun3.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);

        cardGun1.gameObject.GetComponent<UpgradeCardController>().Reroll();
        cardGun2.gameObject.GetComponent<UpgradeCardController>().Reroll();
        cardGun3.gameObject.GetComponent<UpgradeCardController>().Reroll();

        yield return new WaitForSecondsRealtime(0.5f);

        upgradeController.GenerateGunCards();
        cardGun1.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardGun2.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardGun3.gameObject.GetComponent<UpgradeCardController>().Initialize();

        Sequence cardGunAnim = DOTween.Sequence();

        cardGunAnim.Insert(0f, cardGun1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardGunAnim.Insert(0.1f, cardGun2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardGunAnim.Insert(0.2f, cardGun3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);

        buttonTapAccess = true;
    }

    IEnumerator RerollPassive()
    {
        cardPassive1.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);
        cardPassive2.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);
        cardPassive3.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);

        cardPassive1.gameObject.GetComponent<UpgradeCardController>().Reroll();
        cardPassive2.gameObject.GetComponent<UpgradeCardController>().Reroll();
        cardPassive3.gameObject.GetComponent<UpgradeCardController>().Reroll();

        yield return new WaitForSecondsRealtime(0.5f);

        upgradeController.GeneratePassiveCards();

        cardPassive1.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardPassive2.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardPassive3.gameObject.GetComponent<UpgradeCardController>().Initialize();

        Sequence cardPassiveAnim = DOTween.Sequence();

        cardPassiveAnim.Insert(0f, cardPassive1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardPassiveAnim.Insert(0.1f, cardPassive2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardPassiveAnim.Insert(0.2f, cardPassive3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);

        buttonTapAccess = true;
    }
}
