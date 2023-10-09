using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float damage;
    public float shotSpeed;
    public float bulletMoveSpeed;
    public float pauseTime;

    private void Start()
    {
        CoeffSettings();
    }

    void CoeffSettings()
    {
        int _currentWave = GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1;
        float _coeff = GameObject.Find("Generate Controller").GetComponent<Generate>().enemyCoeffList[_currentWave].damageCoeff;
        damage *= _coeff;
    }
}
