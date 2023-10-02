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
        transform.eulerAngles = new Vector3(55.56f, 0, 0);
    }

    public void ViewDamage(int _damage)
    {
        GameObject _text;
        _text = Instantiate(tHpObj, transform.position, Quaternion.identity);
        _text.transform.parent = gameObject.transform;

        if (_damage < 1) _damage = 1;

        _text.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);

        _text.GetComponent<TMP_Text>().text = _damage.ToString();
        _text.GetComponent<RectTransform>().DOLocalMoveY(tHpObj.GetComponent<RectTransform>().localPosition.y + 2, 1f);
        _text.GetComponent<TMP_Text>().DOFade(0, 1);

        Destroy(_text, 2);
    }
}
