using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public int locationNum;

    public static bool isPause;

    public enum InputSettings
    {
        Joy,
        FingerTracking,
        RelativeToTheFinger
    }

    public InputSettings inputSettings;

    [Header("Enemy")]
    public List<GameObject> activeEnemy;
    public static bool isBossActive;

    [Header("Initialize")]
    public PlayerUIController _playerUIController;
    public PlayerController _playerController;

    [Header("Wave")]
    public int currentWaveNum;

    private void Start()
    {
        InitializeGame();
        isBossActive = false;
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
