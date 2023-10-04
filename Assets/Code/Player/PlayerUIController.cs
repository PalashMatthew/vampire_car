using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    public TMP_Text tHp;
    public Image imgFill;

    PlayerController _playerController;
    PlayerStats _playerStats;

    public void Initialize()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();        
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(55.56f, 0, 0);

        UpdateHP();
    }

    public void UpdateHP()
    {
        imgFill.fillAmount = _playerStats.currentHp / _playerStats.maxHp;
        tHp.text = _playerStats.currentHp.ToString();
    }
}
