using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassiveController : MonoBehaviour
{
    [Header("Passive")]
    public bool isPassiveHealthRecovery;

    PlayerController _playerController;


    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (isPassiveHealthRecovery)
        {
            PassiveHealthRecovery();
        }
    }

    public void PassiveHealthRecovery()
    {
        if (isPassiveHealthRecovery)
        {
            //_playerController.currentHp = 
        }
    }
}
