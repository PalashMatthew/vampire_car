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
        GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst.GetComponent<EnemyDefaultBullet>()._gunController = _gunController;
        _inst.transform.LookAt(GameObject.Find("Player").transform);
        _inst.transform.eulerAngles = new Vector3(0, _inst.transform.eulerAngles.y, 0);

        GameObject _instMuzzle = Instantiate(muzzleObj, bulletSpawnPoint.position, transform.rotation);
        _instMuzzle.transform.parent = bulletSpawnPoint;
        Destroy(_instMuzzle, 2);

        yield return new WaitForSeconds(_gunController.shotSpeed);

        StartCoroutine(Shot());
    }
}
