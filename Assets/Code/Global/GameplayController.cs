using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [Header("Enemy")]
    public List<GameObject> activeEnemy;

    [Header("Initialize")]
    public PlayerUIController _playerUIController;
    public PlayerController _playerController;

    [Header("Wave")]
    public int currentWaveNum;

    private void Start()
    {
        Application.targetFrameRate = 60;

        InitializeGame();
    }

    void InitializeGame()
    {
        _playerController.Initialize();
        _playerUIController.Initialize();

        currentWaveNum = 1;
    }
}
