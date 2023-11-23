using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimShot : MonoBehaviour
{
    public GameObject bulletObj;
    public GameObject muzzleObj;
    public Transform bulletSpawnPoint;

    EnemyGun _gunController;
    EnemyController _enemyController;
    EnemyMovement _enemyMovement;

    private bool _startShot = false;
    private int _enemyShotCount = 1;

    public bool shotAlways = false;

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
            if (!shotAlways)
            {
                if (_enemyController.isCarStop)
                {
                    StartCoroutine(Shot());
                    _startShot = true;
                }
            } 
            else
            {
                StartCoroutine(Shot());
                _startShot = true;
            }
            
        }
    }

    IEnumerator Shot()
    {
        GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
        _inst.transform.LookAt(GameObject.Find("Player").transform);
        _inst.transform.eulerAngles = new Vector3(0, _inst.transform.eulerAngles.y, 0);
        _inst.GetComponent<EnemyDefaultBullet>()._controller = gameObject.GetComponent<EnemyController>();

        yield return new WaitForSeconds(_gunController.shotSpeed * GameObject.Find("GameplayController").GetComponent<WaveController>().waveList[GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1].attackSpeedCoeff);

        if (_enemyShotCount >= 3)
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
