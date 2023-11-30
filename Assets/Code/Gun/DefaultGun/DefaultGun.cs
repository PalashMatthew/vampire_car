using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : MonoBehaviour
{
    [Header("Level")]
    public int currentLevel;

    [Header("Bullet")]
    public GameObject bulletObj;

    [Header("Bullet Spawn Positions")]
    public Transform bulletSpawnPoint1;
    public Transform bulletSpawnPoint2;
    public Transform bulletSpawnPoint3;
    public Transform bulletSpawnPoint4;
    public Transform bulletSpawnPoint5;

    Gun _gunController;
    PlayerStats _playerStats;

    void Initialize()
    {
        _gunController = GetComponent<Gun>();
        _playerStats = GetComponentInParent<PlayerStats>();
    }

    private void Start()
    {
        Initialize();

        StartCoroutine(Shot());
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.zero;
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(_gunController.shotSpeed);        

        if (_gunController.projectileValue == 1)
        {
            GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint1.position, transform.rotation);
            _inst.GetComponent<DefaultGunBullet>()._gunController = _gunController;
        }

        if (_gunController.projectileValue == 2)
        {
            //1
            GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint2.position, transform.rotation);
            _inst1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //2
            GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint3.position, transform.rotation);
            _inst2.GetComponent<DefaultGunBullet>()._gunController = _gunController;
        }

        if (_gunController.projectileValue == 3)
        {
            //1
            GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint2.position, transform.rotation);
            _inst1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //2
            GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint3.position, transform.rotation);
            _inst2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //3
            GameObject _inst3 = Instantiate(bulletObj, bulletSpawnPoint1.position, transform.rotation);
            _inst3.GetComponent<DefaultGunBullet>()._gunController = _gunController;
        }

        if (_gunController.projectileValue == 4)
        {
            //1
            GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint2.position, transform.rotation);
            _inst1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //2
            GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint3.position, transform.rotation);
            _inst2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //3
            GameObject _inst3 = Instantiate(bulletObj, bulletSpawnPoint1.position, transform.rotation);
            _inst3.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //4
            GameObject _inst4 = Instantiate(bulletObj, bulletSpawnPoint4.position, transform.rotation);
            _inst4.GetComponent<DefaultGunBullet>()._gunController = _gunController;
        }

        if (_gunController.projectileValue == 5)
        {
            //1
            GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint2.position, transform.rotation);
            _inst1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //2
            GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint3.position, transform.rotation);
            _inst2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //3
            GameObject _inst3 = Instantiate(bulletObj, bulletSpawnPoint1.position, transform.rotation);
            _inst3.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //4
            GameObject _inst4 = Instantiate(bulletObj, bulletSpawnPoint4.position, transform.rotation);
            _inst4.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            //5
            GameObject _inst5 = Instantiate(bulletObj, bulletSpawnPoint5.position, transform.rotation);
            _inst5.GetComponent<DefaultGunBullet>()._gunController = _gunController;
        }

        StartCoroutine(Shot());
    }
}
