using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandAnim : MonoBehaviour
{
    public bool isFlip;


    private void OnEnable()
    {
        StartCoroutine(Anim());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Anim()
    {
        if (!isFlip)
        {
            transform.DOScale(0.8f, 0.4f);
        } 
        else
        {
            transform.DOScale(new Vector3(-0.8f, 0.8f, 0.8f), 0.4f);
        }        

        yield return new WaitForSeconds(0.4f);

        if (!isFlip)
        {
            transform.DOScale(1, 0.4f);
        }
        else
        {
            transform.DOScale(new Vector3(-1, 1, 1), 0.4f);
        }

        yield return new WaitForSeconds(0.4f);

        StartCoroutine(Anim());
    }
}
