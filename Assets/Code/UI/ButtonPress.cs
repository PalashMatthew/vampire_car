using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool NegativeAnimation;

    private bool negativeAnimationPlayed;

    private void OnEnable()
    {
        //transform.DOScale(1f, 0f).SetUpdate(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!NegativeAnimation)
        {
            transform.DOScale(0.93f, 0.15f).SetUpdate(true);
        } 
        else
        {
            if (!negativeAnimationPlayed)
            {
                StartCoroutine(NegativeAnim());
                Sequence buttomNegative = DOTween.Sequence();

                float _x = transform.GetComponent<RectTransform>().anchoredPosition.x;

                buttomNegative.Append(transform.GetComponent<RectTransform>().DOAnchorPosX(_x + 10, 0.08f)).SetUpdate(true);
                buttomNegative.Append(transform.GetComponent<RectTransform>().DOAnchorPosX(_x - 10, 0.08f)).SetUpdate(true);
                buttomNegative.Append(transform.GetComponent<RectTransform>().DOAnchorPosX(_x, 0.08f)).SetUpdate(true);
            }
        }
    }

    IEnumerator NegativeAnim()
    {
        negativeAnimationPlayed = true;
        yield return new WaitForSecondsRealtime(0.24f);
        negativeAnimationPlayed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!NegativeAnimation)
        {
            Sequence buttomUnpress = DOTween.Sequence();
            buttomUnpress.Append(transform.DOScale(1.05f, 0.06f)).SetUpdate(true);
            buttomUnpress.Append(transform.DOScale(0.97f, 0.06f)).SetUpdate(true);
            buttomUnpress.Append(transform.DOScale(1f, 0.06f)).SetUpdate(true);
        } 
    }
}
