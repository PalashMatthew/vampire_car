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

    private bool _startShot = false;

    void Initialize()
    {
        _gunController = GetComponent<EnemyGun>();
        _enemyController = GetComponent<EnemyController>();
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

        GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst2.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
        _inst2.transform.eulerAngles = new Vector3(0, 195, 0);

        GameObject _inst3 = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst3.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
        _inst3.transform.eulerAngles = new Vector3(0, 165, 0);

        GameObject _instMuzzle = Instantiate(muzzleObj, bulletSpawnPoint.position, transform.rotation);
        _instMuzzle.transform.parent = bulletSpawnPoint;
        Destroy(_instMuzzle, 2);

        yield return new WaitForSeconds(_gunController.shotSpeed);

        StartCoroutine(Shot());
    }
}
