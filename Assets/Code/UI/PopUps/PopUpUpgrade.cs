using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PopUpUpgrade : MonoBehaviour
{
    public Image card1, card2, card3;

    public GameObject imgFade;
    public GameObject objPopUp;

    public float animSpeed;

    PopUpController controller;
    public UpgradeController upgradeController;

    public TMP_Text tChoiceUpgrade;
    public TMP_Text tScrewValue;

    public GameObject butRerollScrew;

    public Image imgFAQ1, imgFAQ2, imgFAQ3;

    private void Start()
    {
        controller = new PopUpController();
        controller.imgFade = imgFade;
        controller.objPopUp = objPopUp;
        controller.animTime = animSpeed;

        controller.Initialize();
    }

    private void Update()
    {
        if (GlobalStats.screwCount > 12)
        {
            butRerollScrew.GetComponent<ButtonPress>().NegativeAnimation = false;
        }
        else
        {
            butRerollScrew.GetComponent<ButtonPress>().NegativeAnimation = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ButOpen();
        }
    }

    void CardAnimation()
    {
        tChoiceUpgrade.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;

        imgFAQ1.GetComponent<RectTransform>().localScale = Vector3.zero;
        imgFAQ2.GetComponent<RectTransform>().localScale = Vector3.zero;
        imgFAQ3.GetComponent<RectTransform>().localScale = Vector3.zero;

        card1.GetComponent<RectTransform>().DOAnchorPosX(-348, 0f).SetUpdate(true);
        card2.GetComponent<RectTransform>().DOAnchorPosX(0f, 0f).SetUpdate(true);
        card3.GetComponent<RectTransform>().DOAnchorPosX(348, 0f).SetUpdate(true);

        Sequence cardAnim = DOTween.Sequence();

        card1.GetComponent<RectTransform>().localScale = Vector3.zero;
        card2.GetComponent<RectTransform>().localScale = Vector3.zero;
        card3.GetComponent<RectTransform>().localScale = Vector3.zero;

        cardAnim.Insert(0.3f, card1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardAnim.Insert(0.4f, card2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardAnim.Insert(0.5f, card3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);

        cardAnim.Insert(0.3f, imgFAQ1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardAnim.Insert(0.4f, imgFAQ2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardAnim.Insert(0.5f, imgFAQ3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
    }

    public void ChoiceCard(int _cardNum)
    {
        tChoiceUpgrade.gameObject.GetComponent<RectTransform>().DOScale(0, 0.3f).SetEase(Ease.InBack).SetUpdate(true);

        if (_cardNum == 1)
        {
            card1.GetComponent<RectTransform>().DOAnchorPosX(0, 0.4f).SetUpdate(true);
            card1.GetComponent<RectTransform>().DOScale(1.3f, 0.4f).SetUpdate(true);

            card2.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
            card3.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
        }

        if (_cardNum == 2)
        {
            card2.GetComponent<RectTransform>().DOAnchorPosX(0, 0.4f).SetUpdate(true);
            card2.GetComponent<RectTransform>().DOScale(1.3f, 0.4f).SetUpdate(true);

            card1.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
            card3.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
        }

        if (_cardNum == 3)
        {
            card3.GetComponent<RectTransform>().DOAnchorPosX(0, 0.4f).SetUpdate(true);
            card3.GetComponent<RectTransform>().DOScale(1.3f, 0.4f).SetUpdate(true);

            card1.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
            card2.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
        }

        imgFAQ1.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
        imgFAQ2.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);
        imgFAQ3.GetComponent<RectTransform>().DOScale(0, 0.2f).SetUpdate(true);

        StartCoroutine(EndUpgrade());
    }

    IEnumerator EndUpgrade()
    {
        yield return new WaitForSecondsRealtime(0.8f);        
        ButClosed();
    }

    public void ButOpen()
    {
        Time.timeScale = 0;
        CardAnimation();        
        upgradeController.GenerateUpgrades();
        controller.OpenPopUp();

        card1.gameObject.GetComponent<UpgradeCardController>().Initialize();
        card2.gameObject.GetComponent<UpgradeCardController>().Initialize();
        card3.gameObject.GetComponent<UpgradeCardController>().Initialize();

        tScrewValue.text = GlobalStats.screwCount.ToString();
    }

    public void ButClosed()
    {
        Time.timeScale = 1;
        controller.ClosedPopUp();
    }

    public void ButRerollAds()
    {
        StartCoroutine(Reroll());
    }

    public void ButRerollScrew()
    {
        if (GlobalStats.screwCount > 12)
        {
            butRerollScrew.GetComponent<ButtonPress>().NegativeAnimation = false;
            GlobalStats.screwCount -= 12;
            tScrewValue.text = GlobalStats.screwCount.ToString();
            StartCoroutine(Reroll());
        }        
    }

    IEnumerator Reroll()
    {
        card1.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);
        card2.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);
        card3.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true);

        yield return new WaitForSecondsRealtime(0.5f);

        upgradeController.GenerateUpgrades();
        card1.gameObject.GetComponent<UpgradeCardController>().Initialize();
        card2.gameObject.GetComponent<UpgradeCardController>().Initialize();
        card3.gameObject.GetComponent<UpgradeCardController>().Initialize();

        Sequence cardAnim = DOTween.Sequence();

        cardAnim.Insert(0f, card1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardAnim.Insert(0.1f, card2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
        cardAnim.Insert(0.2f, card3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack)).SetUpdate(true);
    }
}
