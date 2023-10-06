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

        imgFade.GetComponent<Image>().color = new Vector4(0, 0, 0, 0);
        imgFade.GetComponent<Image>().DOFade(0.5f, animTime).SetUpdate(true);

        objPopUp.transform.localScale = Vector3.zero;
        objPopUp.transform.DOScale(1, animTime).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void ClosedPopUp()
    {
        imgFade.GetComponent<Image>().DOFade(0, animTime).SetUpdate(true);

        objPopUp.transform.DOScale(0, animTime).SetEase(Ease.InBack).OnComplete(PopUpComponentOff).SetUpdate(true);        
    }

    void PopUpComponentOff()
    {
        imgFade.SetActive(false);
        objPopUp.SetActive(false);
    }
}
