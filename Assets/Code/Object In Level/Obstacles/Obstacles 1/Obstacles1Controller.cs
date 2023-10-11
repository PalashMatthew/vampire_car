using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles1Controller : MonoBehaviour
{
    public float timeActive;
    public GameObject shieldObj;
    Obstacle _obstacle;

    private bool _isJumping = false;

    private void Start()
    {
        shieldObj.SetActive(false);
        StartCoroutine(ShowShield());
        _obstacle = GetComponent<Obstacle>();
    }

    IEnumerator ShowShield()
    {
        yield return new WaitForSeconds(timeActive);
        shieldObj.SetActive(true);
        yield return new WaitForSeconds(timeActive);
        shieldObj.SetActive(false);
        StartCoroutine(ShowShield());
    }

    IEnumerator JumpAnim()
    {
        transform.DOMoveZ(transform.position.z - 7.5f, 0.3f).SetEase(Ease.Linear);
        transform.DORotate(new Vector3(-26f, 0, 0), 0.1f).SetEase(Ease.Linear);
        transform.DOMoveY(2.88f, 0.3f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.1f);
        transform.DORotate(new Vector3(0f, 0, 0), 0.1f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.2f);
        transform.DOMoveZ(transform.position.z - 3, 0.2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.1f);

        transform.DORotate(new Vector3(26f, 0, 0), 0.1f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.1f);

        transform.DOMoveZ(transform.position.z - 2, 0.4f).SetEase(Ease.Linear);
        transform.DOMoveY(0f, 0.3f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.3f);

        transform.DORotate(new Vector3(0f, 0, 0), 0.3f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.3f);
        _obstacle.isMove = true;
        _isJumping = false;
        shieldObj.SetActive(true);
        StartCoroutine(ShowShield());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "jump" && !_isJumping)
        {
            StopAllCoroutines();
            shieldObj.SetActive(false);
            _obstacle.isMove = false;
            StartCoroutine(JumpAnim());
        }
    }
}
