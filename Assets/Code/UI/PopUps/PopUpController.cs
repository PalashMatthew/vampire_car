using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour 
{
    public GameObject imgFade;
    public GameObject objPopUp;

    public float animTime = 0.3f;

    public float fadeValue = 0.7f;

    public void Initialize()
    {
        if (imgFade != null)
            imgFade.SetActive(false);

        objPopUp.SetActive(false);
    }

    private void Awake()
    {
        Initialize();
    }

    public void OpenPopUp()
    {
        if (imgFade != null)
        {
            imgFade.SetActive(true);
            imgFade.GetComponent<Image>().color = new Vector4(0, 0, 0, 0);
            imgFade.GetComponent<Image>().DOFade(fadeValue, animTime).SetUpdate(true);
        }

        objPopUp.SetActive(true);        

        objPopUp.transform.localScale = Vector3.zero;
        objPopUp.transform.DOScale(1, animTime).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void ClosedPopUp()
    {
        if (imgFade != null)
            imgFade.GetComponent<Image>().DOFade(0, animTime).SetUpdate(true);

        objPopUp.transform.DOScale(0, animTime).SetEase(Ease.InBack).OnComplete(PopUpComponentOff).SetUpdate(true);        
    }

    void PopUpComponentOff()
    {
        if (imgFade != null)
            imgFade.SetActive(false);

        objPopUp.SetActive(false);
    }
}
