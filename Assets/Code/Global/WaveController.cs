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

    public GameObject enemyM1Obj;
    public GameObject enemyM2Obj;
    public GameObject enemyM3Obj;
    public GameObject enemyM4Obj;

    public GameObject enemyS1Obj;
    public GameObject enemyS2Obj;
    public GameObject enemyS3Obj;
    public GameObject enemyS4Obj;

    private Generate _generate;
    public GameplayUIController gameplayUIController;

    private void Awake()
    {
        _generate = GameObject.Find("Generate Controller").GetComponent<Generate>();

        StartWave();
        StartCoroutine(PatternSpawn());

        foreach (int i in waveList[currentWave - 1].patternsSpawnTime)
        {
            if (i > 0)
            { }
        }

        for (int i = 0; i < waveList[currentWave - 1].patternsSpawnTime.Count; i++)
        {
            if (waveList[currentWave - 1].patternsSpawnTime[i] > 0)
            {
                StartCoroutine(PatternPrioritySpawn(waveList[currentWave - 1].patterns[i], waveList[currentWave - 1].patternsSpawnTime[i]));
            }
        }
    }

    public void StartWave()
    {
        enemyDestroy = 0;
        currentWave++;
        gameplayUIController.StartWave(waveList[currentWave - 1].waveTime);
    }

    public void WaveEnd()
    {
        StartWave();
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

            int _a = System.Int32.Parse(_newPattern.name);

            _generate.SpawnPattern(_newPattern, waveList[currentWave - 1].patternsX[_a - 1]);
        }

        StartCoroutine(PatternSpawn());
    }

    IEnumerator PatternPrioritySpawn(GameObject _pattern, float _time)
    {
        yield return new WaitForSeconds(_time);

        int _a = System.Int32.Parse(_pattern.name);
        _generate.SpawnPattern(_pattern, waveList[currentWave - 1].patternsX[_a - 1]);

        StartCoroutine(PatternPrioritySpawn(_pattern, _time));
    }
}

[System.Serializable]
public class Wave
{
    [Header("Base")]
    public int waveNum;
    public int waveTime;  //������� ����� ����� ��� ����� �����

    [Header("Enemy Settings")]
    [Range (0, 10)]
    public int enemyM1Weight;
    [Range(0, 10)]
    public int enemyM2Weight;
    [Range(0, 10)]
    public int enemyM3Weight;
    [Range(0, 10)]
    public int enemyM4Weight;
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

    public List<int> patternsSpawnTime;  //���� 0 �� �����

    public List<float> patternsX;
}
