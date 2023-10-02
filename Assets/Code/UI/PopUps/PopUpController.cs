using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopUpController
{
    public GameObject imgFade;
    public GameObject objPopUp;

    public float animTime;

    public void Initialize()
    {
        imgFade.SetActive(false);
        objPopUp.SetActive(false);
    }

    public void OpenPopUp()
    {
        imgFade.SetActive(true);
        objPopUp.SetActive(true);

        imgFade.GetComponent<Image>().DOFade(0, 0);
        imgFade.GetComponent<Image>().DOFade(0.5f, animTime);

        objPopUp.transform.DOScale(0, 0);
        objPopUp.transform.DOScale(1, animTime).SetEase(Ease.OutBack).OnComplete(StopTime);
    }

    void StopTime()
    {
        //Time.timeScale = 0;
    }

    public void ClosedPopUp()
    {
        //Time.timeScale = 1;
        imgFade.GetComponent<Image>().DOFade(0, animTime);

        objPopUp.transform.DOScale(0, animTime).SetEase(Ease.InBack).OnComplete(PopUpComponentOff);        
    }

    void PopUpComponentOff()
    {
        imgFade.SetActive(false);
        objPopUp.SetActive(false);
    }
}
