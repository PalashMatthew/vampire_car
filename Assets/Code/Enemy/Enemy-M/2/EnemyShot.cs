using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject bulletObj;
    public GameObject muzzleObj;
    public Transform bulletSpawnPoint;

    EnemyGun _gunController;
    EnemyMovement _enemyMovement;
    EnemyController _enemyController;

    private int _enemyShotCount = 1;

    public bool isShotStart;


    void Initialize()
    {
        _gunController = GetComponent<EnemyGun>();
        _enemyController = GetComponent<EnemyController>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        Initialize();

        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        if (isShotStart)
        {
            GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
            _inst.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
            _inst.GetComponent<EnemyDefaultBullet>()._controller = gameObject.GetComponent<EnemyController>();
        }

        yield return new WaitForSeconds(_gunController.shotSpeed * GameObject.Find("GameplayController").GetComponent<WaveController>().waveList[GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1].attackSpeedCoeff);

        if (!isShotStart)
        {
            GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
            _inst.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
            _inst.GetComponent<EnemyDefaultBullet>()._controller = gameObject.GetComponent<EnemyController>();
        }

        if (_enemyController.carType == EnemyController.CarType.Static)
        {
            if (_enemyShotCount >= 3)
            {
                _enemyMovement.StartCoroutine(_enemyMovement.MoveInside());
            }
            else
            {
                StartCoroutine(Shot());
                _enemyShotCount++;
            }
        } else
        {
            StartCoroutine(Shot());
        }
    }
}
