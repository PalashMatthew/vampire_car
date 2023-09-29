using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    public TMP_Text tHp;
    public Image imgFill;

    private float _currentHp;
    private float _maxHp;

    PlayerController _playerController;

    public void Initialize()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        UpdateHP();
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(55.56f, 0, 0);
    }

    public void UpdateHP()
    {
        _currentHp = _playerController.currentHp;
        _maxHp = _playerController.maxHp;

        imgFill.fillAmount = _currentHp / _maxHp;
        tHp.text = _currentHp.ToString();
    }
}
