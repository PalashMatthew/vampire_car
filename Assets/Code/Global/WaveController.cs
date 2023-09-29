using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        StartWave();
    }

    public void StartWave()
    {
        enemyDestroy = 0;
        currentWave++;
    }

    public void EnemyKillPlus()
    {
        enemyDestroy++;

        if (enemyDestroy >= waveList[currentWave - 1].enemyKillCount)
        {
            StartWave();
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

        GameObject _newEnemy = _potencialEnemy[Random.Range(0, _potencialEnemy.Count)];
        return _newEnemy;
    }
}

[System.Serializable]
public class Wave
{
    public int waveNum;

    public int enemyKillCount;  //Сколько нужно убить для конца волны

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
}
