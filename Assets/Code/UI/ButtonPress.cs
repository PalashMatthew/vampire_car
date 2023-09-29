using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : EventTrigger
{
    public override void OnPointerDown(PointerEventData data)
    {
        transform.DOScale(0.93f, 0.15f);
    }

    public override void OnPointerUp(PointerEventData data)
    {
        Sequence buttomUnpress = DOTween.Sequence();
        buttomUnpress.Append(transform.DOScale(1.05f, 0.06f));
        buttomUnpress.Append(transform.DOScale(0.97f, 0.06f));
        buttomUnpress.Append(transform.DOScale(1f, 0.06f));
    }
}
