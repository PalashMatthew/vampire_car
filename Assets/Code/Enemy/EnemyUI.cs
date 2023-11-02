using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public GameObject tHpObj;
    PlayerController _playerController;
    public GameObject headshotUI;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        //transform.eulerAngles = new Vector3(55.56f, 0, 0);
        transform.LookAt(Camera.main.transform.position);
    }

    public void ViewDamage(int _damage, bool _isKrit)
    {
        GameObject _text;
        _text = Instantiate(tHpObj, transform.position, Quaternion.identity);
        _text.transform.parent = gameObject.transform;
        _text.transform.LookAt(Camera.main.transform.position);
        _text.GetComponent<RectTransform>().localEulerAngles = new Vector3(_text.GetComponent<RectTransform>().localEulerAngles.x, 180, _text.GetComponent<RectTransform>().localEulerAngles.z);

        if (_damage < 1) _damage = 1;

        _text.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);

        _text.GetComponent<TMP_Text>().text = _damage.ToString();
        _text.GetComponent<RectTransform>().DOLocalMoveY(tHpObj.GetComponent<RectTransform>().localPosition.y + 2, 1f);
        _text.GetComponent<TMP_Text>().DOFade(0, 1);

        if (_isKrit)
        {
            _text.GetComponent<TMP_Text>().DOColor(new Color(1, 0.3819398f, 0.25f), 0);
        }

        Destroy(_text, 2);
    }

    public void ViewHeadshot()
    {
        GameObject _text;
        _text = Instantiate(headshotUI, transform.position, Quaternion.identity);
        _text.transform.parent = gameObject.transform;
        //_text.transform.LookAt(Camera.main.transform.position);
        _text.GetComponent<RectTransform>().localEulerAngles = new Vector3(_text.GetComponent<RectTransform>().localEulerAngles.x, 180, _text.GetComponent<RectTransform>().localEulerAngles.z);

        _text.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);

        //_text.GetComponent<TMP_Text>().text = "HEADSHOT";
        _text.GetComponent<RectTransform>().DOLocalMoveY(tHpObj.GetComponent<RectTransform>().localPosition.y + 2, 1f);
        _text.GetComponent<TMP_Text>().DOFade(0, 1);
        _text.transform.parent = null;

        //_text.GetComponent<TMP_Text>().DOColor(new Color(1, 0.3819398f, 0.25f), 0);

        Destroy(_text, 0.5f);
    }
}
