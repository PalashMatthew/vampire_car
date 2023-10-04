using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpUpgrade : MonoBehaviour
{
    public Image card1, card2, card3;

    public GameObject imgFade;
    public GameObject objPopUp;

    public float animSpeed;

    PopUpController controller;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            ButOpen();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ButClosed();
        }
    }

    void CardAnimation()
    {
        Sequence cardAnim = DOTween.Sequence();

        cardAnim.Append(card1.GetComponent<RectTransform>().DOScale(0, 0));
        cardAnim.Join(card2.GetComponent<RectTransform>().DOScale(0, 0));
        cardAnim.Join(card3.GetComponent<RectTransform>().DOScale(0, 0));

        cardAnim.Insert(0.3f, card1.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack));
        cardAnim.Insert(0.4f, card2.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack));
        cardAnim.Insert(0.5f, card3.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack));
    }

    public void ButOpen()
    {
        controller.OpenPopUp();
        CardAnimation();
    }

    public void ButClosed()
    {
        controller.ClosedPopUp();
    }

    public void ButSettings()
    {

    }

    public void ButExit()
    {

    }
}
