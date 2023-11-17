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

    public GameObject butRerollScrew;

    public bool isWaveUpgrade = false;
    public bool isOpen;

    public bool isDefferenUpgrade;

    [Header("Reroll")]
    public TMP_Text tRerollPrice;
    float rerollPrice;


    private void Start()
    {
        _waveController = GameObject.Find("GameplayController").GetComponent<WaveController>();
        _popUpController = GetComponent<PopUpController>();
    }

    private void Update()
    {
        //if (GlobalStats.screwCount > 12)
        //{
        //    butRerollScrew.GetComponent<ButtonPress>().NegativeAnimation = false;
        //}
        //else
        //{
        //    butRerollScrew.GetComponent<ButtonPress>().NegativeAnimation = true;
        //}

        if (Input.GetKeyDown(KeyCode.T))
        {
            ButOpen();
        }
    }

    void CardAnimation()
    {
        cardGun1.GetComponent<RectTransform>().DOAnchorPosX(-300, 0f).SetUpdate(true);
        cardGun2.GetComponent<RectTransform>().DOAnchorPosX(0f, 0f).SetUpdate(true);
        cardGun3.GetComponent<RectTransform>().DOAnchorPosX(300, 0f).SetUpdate(true);

        cardPassive1.GetComponent<RectTransform>().DOAnchorPosX(-300, 0f).SetUpdate(true);
        cardPassive2.GetComponent<RectTransform>().DOAnchorPosX(0f, 0f).SetUpdate(true);
        cardPassive3.GetComponent<RectTransform>().DOAnchorPosX(300, 0f).SetUpdate(true);

        Sequence cardGunAnim = DOTween.Sequence();
        Sequence cardPassiveAnim = DOTween.Sequence();

        cardGun1.GetComponent<RectTransform>().localScale = Vector3.zero;
        cardGun2.GetComponent<RectTransform>().localScale = Vector3.zero;
        cardGun3.GetComponent<RectTransform>().localScale = Vector3.zero;

        cardPassive1.GetComponent<RectTransform>().localScale = Vector3.zero;
        cardPassive2.GetComponent<RectTransform>().localScale = Vector3.zero;
        cardPassive3.GetComponent<RectTransform>().localScale = Vector3.zero;

        cardGunAnim.Insert(0.3f, cardGun1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardGunAnim.Insert(0.4f, cardGun2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardGunAnim.Insert(0.5f, cardGun3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);

        cardPassiveAnim.Insert(0.3f, cardPassive1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardPassiveAnim.Insert(0.4f, cardPassive2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardPassiveAnim.Insert(0.5f, cardPassive3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
    }

    public void ChoiceCard()
    {
        if (upgradeController.cardGunAccept != null && upgradeController.cardPassiveAccept != null)
        {
            foreach (UpgradeCardController _card in upgradeController.cardsGunController)
            {
                if (upgradeController.cardGunAccept == _card)
                {
                    _card.GetComponent<RectTransform>().DOAnchorPosX(0, 0.4f).SetUpdate(true);
                    //_card.GetComponent<RectTransform>().DOScale(1.3f, 0.4f).SetUpdate(true);

                    _card.ChoiceCard();
                }
                else
                {
                    _card.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
                }
            }

            foreach (UpgradeCardController _card in upgradeController.cardsPassiveController)
            {
                if (upgradeController.cardPassiveAccept == _card)
                {
                    _card.GetComponent<RectTransform>().DOAnchorPosX(0, 0.4f).SetUpdate(true);
                    //_card.GetComponent<RectTransform>().DOScale(1.3f, 0.4f).SetUpdate(true);

                    _card.ChoiceCard();
                }
                else
                {
                    _card.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
                }
            }

            StartCoroutine(EndUpgrade());
            _waveController.StartWave();

            upgradeController.cardGunAccept = null;
            upgradeController.cardPassiveAccept = null;
        }
    }

    IEnumerator EndUpgrade()
    {
        yield return new WaitForSecondsRealtime(0.8f);        
        ButClosed();
    }

    public void ButOpen()
    {
        GameplayController.isPause = true;
        isOpen = true;
        Time.timeScale = 0;
        CardAnimation();
        //upgradeController.GenerateUpgrades();
        upgradeController.GenerateGunCards();
        upgradeController.GeneratePassiveCards();
        _popUpController.OpenPopUp();

        cardGun1.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardGun2.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardGun3.gameObject.GetComponent<UpgradeCardController>().Initialize();

        cardPassive1.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardPassive2.gameObject.GetComponent<UpgradeCardController>().Initialize();
        cardPassive3.gameObject.GetComponent<UpgradeCardController>().Initialize();

        tScrewValue.text = GlobalStats.screwCount.ToString();

        upgradeController.SlotInitialize();
        upgradeController.UpdateTextLevels();
        //RerollInitialize();
    }

    void RerollInitialize()
    {
        rerollPrice = upgradeController.rerollPrice[_waveController.currentWave - 1];
        tRerollPrice.text = rerollPrice.ToString();
    }

    public void ButClosed()
    {
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
            StartCoroutine(RerollGun());
        }

        if (_type == "passive")
        {
            StartCoroutine(RerollPassive());
        }        
    }

    public void ButRerollScrew()
    {
        if (GlobalStats.screwCount >= rerollPrice)
        {
            butRerollScrew.GetComponent<ButtonPress>().NegativeAnimation = false;
            GlobalStats.screwCount -= rerollPrice;
            tScrewValue.text = GlobalStats.screwCount.ToString();
            StartCoroutine(RerollGun());
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
    }
}
