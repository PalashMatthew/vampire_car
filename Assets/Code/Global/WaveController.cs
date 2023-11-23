using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] public List<Wave> waveList;

    public int currentWave;
    public int enemyDestroy;

    public int lastWave;

    public GameObject enemyM1Obj;
    public GameObject enemyM2Obj;
    public GameObject enemyM3Obj;
    public GameObject enemyM4Obj;
    public GameObject enemyM5Obj;
    public GameObject enemyM6Obj;

    public GameObject enemyS1Obj;
    public GameObject enemyS2Obj;
    public GameObject enemyS3Obj;
    public GameObject enemyS4Obj;

    private GameplayController _gameplayController;
    private Generate _generate;
    public GameplayUIController gameplayUIController;
    GenerateObstacles _generateObstacles;

    public static bool isWaveEnd;

    private void Awake()
    {
        _generate = GameObject.Find("Generate Controller").GetComponent<Generate>();
        _generateObstacles = GameObject.Find("Generate Controller").GetComponent<GenerateObstacles>();
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();

        StartWave();        
    }

    public void StartWave()
    {
        isWaveEnd = false;
        GameObject.Find("Player").GetComponent<PlayerStats>().currentExp = 0;

        if (currentWave < lastWave)
        {
            enemyDestroy = 0;
            currentWave++;
            gameplayUIController.StartWave(waveList[currentWave - 1].waveTime, currentWave);
            _generate.moveSpeedCoeff = waveList[currentWave - 1].waveSpeedCoeff;
            _generate.spawnTime = waveList[currentWave - 1].enemySpawnTime;

            for (int i = 0; i < waveList[currentWave - 1].patternsSpawnTime.Count; i++)
            {
                if (waveList[currentWave - 1].patternsSpawnTime[i] > 0)
                {
                    StartCoroutine(PatternPrioritySpawn(waveList[currentWave - 1].patterns[i], waveList[currentWave - 1].patternsSpawnTime[i]));
                }
            }

            StartCoroutine(PatternSpawn());

            _generate.StartSpawn();
            _generateObstacles.NewWave();
        }
        else
        {
            _generate.BossFight();
        }        
    }

    public void WaveEnd()
    {
        isWaveEnd = true;

        StopAllCoroutines();
        _generate.StopAllCoroutines();

        for (int i = 0; i < _gameplayController.activeEnemy.Count; i++)
        {
            if (_gameplayController.activeEnemy[i] != null)
            {
                _gameplayController.activeEnemy[i].GetComponent<EnemyController>().DeleteEnemy();
            }
        }

        for (int i = 0; i < _generateObstacles.instObstacles.Count; i++)
        {
            if (_generateObstacles.instObstacles[i] != null)
            {
                Destroy(_generateObstacles.instObstacles[i]);
            }
        }

        _gameplayController.activeEnemy.Clear();
        gameplayUIController.StartCoroutine(gameplayUIController.WaveCompliteAnim());

        StartCoroutine(ShowUpgrade());
    }

    public void StopGame()
    {
        isWaveEnd = true;

        StopAllCoroutines();
        _generate.StopAllCoroutines();
        _generateObstacles.StopAllCoroutines();
        GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().StopAllCoroutines();

        for (int i = 0; i < _gameplayController.activeEnemy.Count; i++)
        {
            if (_gameplayController.activeEnemy[i] != null)
            {
                _gameplayController.activeEnemy[i].GetComponent<EnemyController>().DeleteEnemy();
            }
        }

        _gameplayController.activeEnemy.Clear();

        GameObject.Find("Enviroments").GetComponent<PlaceGenerate>().StopGame();
        //Destroy(GameObject.Find("Player"));
        Camera.main.GetComponent<CameraController>().follow = false;      
    }

    IEnumerator ShowUpgrade()
    {
        yield return new WaitForSeconds(2.5f);
        GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().isWaveUpgrade = true;
        GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().ButOpen();
    }

    public GameObject ChoiseEnemy()
    {
        List <GameObject> _potencialEnemy = new List<GameObject> ();

        #region AddEnemy
        if (waveList[currentWave - 1].enemyM1Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyM1Weight; i++)
            {
                _potencialEnemy.Add(enemyM1Obj);
            }
        }

        if (waveList[currentWave - 1].enemyM2Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyM2Weight; i++)
            {
                _potencialEnemy.Add(enemyM2Obj);
            }
        }

        if (waveList[currentWave - 1].enemyM3Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyM3Weight; i++)
            {
                _potencialEnemy.Add(enemyM3Obj);
            }
        }

        if (waveList[currentWave - 1].enemyM4Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyM4Weight; i++)
            {
                _potencialEnemy.Add(enemyM4Obj);
            }
        }

        if (waveList[currentWave - 1].enemyM5Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyM5Weight; i++)
            {
                _potencialEnemy.Add(enemyM5Obj);
            }
        }

        if (waveList[currentWave - 1].enemyM6Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyM6Weight; i++)
            {
                _potencialEnemy.Add(enemyM6Obj);
            }
        }

        if (waveList[currentWave - 1].enemyS1Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyS1Weight; i++)
            {
                _potencialEnemy.Add(enemyS1Obj);
            }
        }

        if (waveList[currentWave - 1].enemyS2Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyS2Weight; i++)
            {
                _potencialEnemy.Add(enemyS2Obj);
            }
        }

        if (waveList[currentWave - 1].enemyS3Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyS3Weight; i++)
            {
                _potencialEnemy.Add(enemyS3Obj);
            }
        }

        if (waveList[currentWave - 1].enemyS4Weight > 0)
        {
            for (int i = 0; i < waveList[currentWave - 1].enemyS4Weight; i++)
            {
                _potencialEnemy.Add(enemyS4Obj);
            }
        }
        #endregion

        GameObject _newEnemy = _potencialEnemy[UnityEngine.Random.Range(0, _potencialEnemy.Count)];
        return _newEnemy;
    }

    IEnumerator PatternSpawn()
    {
        yield return new WaitForSeconds(waveList[currentWave - 1].patternSpawnTime);

        if (waveList[currentWave - 1].patterns.Count > 0)
        {

            List<GameObject> _potencialPattern = new List<GameObject>();

            for (int i = 0; i < waveList[currentWave - 1].patterns.Count; i++)
            {
                if (waveList[currentWave - 1].patternsWeight[i] > 0)
                {
                    for (int w = 0; w < waveList[currentWave - 1].patternsWeight[i]; w++)
                    {
                        _potencialPattern.Add(waveList[currentWave - 1].patterns[i]);
                    }
                }
            }

            GameObject _newPattern = _potencialPattern[UnityEngine.Random.Range(0, _potencialPattern.Count)];

            _generate.SpawnPattern(_newPattern, _newPattern.GetComponent<Pattern>().patternX);
        }

        StartCoroutine(PatternSpawn());
    }

    IEnumerator PatternPrioritySpawn(GameObject _pattern, float _time)
    {
        yield return new WaitForSeconds(_time);

        _generate.SpawnPattern(_pattern, _pattern.GetComponent<Pattern>().patternX);

        //StartCoroutine(PatternPrioritySpawn(_pattern, _time));
    }
}

[System.Serializable]
public class Wave
{
    [Header("Base")]
    public int waveNum;
    public int waveTime;  //Сколько нужно убить для конца волны
    public float enemySpawnTime;

    [Header("Exp")]
    public float expCoeff;
    public float expMinus;

    [Header("Enemy Settings")]
    public float waveSpeedCoeff;  //Скорость объектов на этой волне
    public float attackSpeedCoeff;
    public float shotSpeedCoeff;

    [Range (0, 10)]
    public int enemyM1Weight;
    [Range(0, 10)]
    public int enemyM2Weight;
    [Range(0, 10)]
    public int enemyM3Weight;
    [Range(0, 10)]
    public int enemyM4Weight;
    [Range(0, 10)]
    public int enemyM5Weight;
    [Range(0, 10)]
    public int enemyM6Weight;
    [Range(0, 10)]
    public int enemyS1Weight;
    [Range(0, 10)]
    public int enemyS2Weight;
    [Range(0, 10)]
    public int enemyS3Weight;
    [Range(0, 10)]
    public int enemyS4Weight;

    [Header("Pattern Settings")]
    public float patternSpawnTime;
    public List<GameObject> patterns;
    [Range(0, 10)]
    public List<int> patternsWeight;

    public List<int> patternsSpawnTime;  //Если 0 то похуй
}
