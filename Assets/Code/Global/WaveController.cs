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
        if (currentWave != 0)
        {
            if (!GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().isOpen)
            {
                GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().isWaveUpgrade = true;
                GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().ButOpen();
            }
            else
            {
                GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().isDefferenUpgrade = true;
            }
        }

        enemyDestroy = 0;
        currentWave++;
        gameplayUIController.StartWave(waveList[currentWave - 1].waveTime, currentWave);
        _generate.moveSpeedCoeff = waveList[currentWave - 1].waveSpeedCoeff;
        _generate.spawnTime = waveList[currentWave - 1].enemySpawnTime;

        _generate.StartSpawn();
    }

    IEnumerator DefferedUpgrade()
    {
        yield return new WaitForSeconds(1);

        if (!GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().isOpen)
        {
            GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().isWaveUpgrade = true;
            GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().ButOpen();
        } else
        {
            StartCoroutine(DefferedUpgrade());
        }
    }

    public void WaveEnd()
    {
        if (currentWave < lastWave)
        {
            StartWave();
        } 
        else
        {
            StopAllCoroutines();
            
            _generate.BossFight();
        }        
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

    [Header("Enemy Settings")]
    public float waveSpeedCoeff;  //Скорость объектов на этой волне
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

    public List<int> patternsSpawnTime;  //Если 0 то похуй
}
