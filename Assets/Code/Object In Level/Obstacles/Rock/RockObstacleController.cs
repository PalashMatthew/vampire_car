using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacleController : MonoBehaviour
{
    Obstacle _controller;

    private void Start()
    {
        _controller = GetComponentInParent<Obstacle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            int _currentWave = GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1;
            float _coeff = GameObject.Find("Generate Controller").GetComponent<Generate>().enemyCoeffList[_currentWave].damageCoeff;
            float damage = _controller.damage * _coeff;

            other.gameObject.GetComponent<PlayerController>().Hit(damage);
            _controller.Dead();
        }
    }
}
