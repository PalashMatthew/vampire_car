using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTripleShot : MonoBehaviour
{
    public GameObject bulletObj;
    public GameObject muzzleObj;
    public Transform bulletSpawnPoint;

    EnemyGun _gunController;
    EnemyController _enemyController;
    EnemyMovement _enemyMovement;

    private bool _startShot = false;
    private int _enemyShotCount = 1;

    void Initialize()
    {
        _gunController = GetComponent<EnemyGun>();
        _enemyController = GetComponent<EnemyController>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (!_startShot)
        {
            if (_enemyController.isCarStop)
            {
                StartCoroutine(Shot());
                _startShot = true;
            }
        }
    }

    IEnumerator Shot()
    {
        GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst1.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
        _inst1.transform.eulerAngles = new Vector3(0, 180, 0);
        _inst1.GetComponent<EnemyDefaultBullet>()._controller = gameObject.GetComponent<EnemyController>();

        GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst2.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
        _inst2.transform.eulerAngles = new Vector3(0, 195, 0);
        _inst2.GetComponent<EnemyDefaultBullet>()._controller = gameObject.GetComponent<EnemyController>();

        GameObject _inst3 = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst3.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
        _inst3.transform.eulerAngles = new Vector3(0, 165, 0);
        _inst3.GetComponent<EnemyDefaultBullet>()._controller = gameObject.GetComponent<EnemyController>();

        yield return new WaitForSeconds(_gunController.shotSpeed * GameObject.Find("GameplayController").GetComponent<WaveController>().waveList[GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1].attackSpeedCoeff);

        if (_enemyShotCount >= 3 && _enemyController.carType == EnemyController.CarType.Static)
        {
            _enemyMovement.StartCoroutine(_enemyMovement.MoveInside());
        }
        else
        {
            StartCoroutine(Shot());
            _enemyShotCount++;
        }
    }
}
