using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static bool isPause;

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

        currentWaveNum = 1;
    }

    public void Win()
    {
        GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().isWin = true;
        GameObject.Find("PopUp Win").GetComponent<PopUpWin>().ButOpen();
    }
}
