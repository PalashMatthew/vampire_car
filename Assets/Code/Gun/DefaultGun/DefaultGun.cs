using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : MonoBehaviour
{
    [Header("Level")]
    public int currentLevel;

    [Header("Bullet")]
    public GameObject bulletObj;
    public GameObject muzzleObj;

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

        _gunController.damage = _playerStats.damage;
        //_gunController.shotSpeed = _playerStats.attackSpeedCoeff;
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(_gunController.shotSpeed - _gunController.shotSpeed / 100 * _playerStats.attackSpeedCoeff);

        if (_playerStats.projectileCount == 1)
        {
            GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint1.position, transform.rotation);
            _inst.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle = Instantiate(muzzleObj, bulletSpawnPoint1.position, transform.rotation);
            _instMuzzle.transform.parent = bulletSpawnPoint1;
            Destroy(_instMuzzle, 2);
        }

        if (_playerStats.projectileCount == 2)
        {
            //1
            GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint2.position, transform.rotation);
            _inst1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle1 = Instantiate(muzzleObj, bulletSpawnPoint2.position, transform.rotation);
            _instMuzzle1.transform.parent = bulletSpawnPoint2;
            Destroy(_instMuzzle1, 2);

            //2
            GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint3.position, transform.rotation);
            _inst2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle2 = Instantiate(muzzleObj, bulletSpawnPoint3.position, transform.rotation);
            _instMuzzle2.transform.parent = bulletSpawnPoint2;
            Destroy(_instMuzzle2, 2);
        }

        if (_playerStats.projectileCount == 3)
        {
            //1
            GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint2.position, transform.rotation);
            _inst1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle1 = Instantiate(muzzleObj, bulletSpawnPoint2.position, transform.rotation);
            _instMuzzle1.transform.parent = bulletSpawnPoint2;
            Destroy(_instMuzzle1, 2);

            //2
            GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint3.position, transform.rotation);
            _inst2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle2 = Instantiate(muzzleObj, bulletSpawnPoint3.position, transform.rotation);
            _instMuzzle2.transform.parent = bulletSpawnPoint3;
            Destroy(_instMuzzle2, 2);

            //3
            GameObject _inst3 = Instantiate(bulletObj, bulletSpawnPoint1.position, transform.rotation);
            _inst3.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle3 = Instantiate(muzzleObj, bulletSpawnPoint1.position, transform.rotation);
            _instMuzzle3.transform.parent = bulletSpawnPoint1;
            Destroy(_instMuzzle3, 2);
        }

        if (_playerStats.projectileCount == 4)
        {
            //1
            GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint2.position, transform.rotation);
            _inst1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle1 = Instantiate(muzzleObj, bulletSpawnPoint2.position, transform.rotation);
            _instMuzzle1.transform.parent = bulletSpawnPoint2;
            Destroy(_instMuzzle1, 2);

            //2
            GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint3.position, transform.rotation);
            _inst2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle2 = Instantiate(muzzleObj, bulletSpawnPoint3.position, transform.rotation);
            _instMuzzle2.transform.parent = bulletSpawnPoint3;
            Destroy(_instMuzzle2, 2);

            //3
            GameObject _inst3 = Instantiate(bulletObj, bulletSpawnPoint1.position, transform.rotation);
            _inst3.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle3 = Instantiate(muzzleObj, bulletSpawnPoint1.position, transform.rotation);
            _instMuzzle3.transform.parent = bulletSpawnPoint1;
            Destroy(_instMuzzle3, 2);

            //4
            GameObject _inst4 = Instantiate(bulletObj, bulletSpawnPoint4.position, transform.rotation);
            _inst4.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle4 = Instantiate(muzzleObj, bulletSpawnPoint4.position, transform.rotation);
            _instMuzzle4.transform.parent = bulletSpawnPoint4;
            Destroy(_instMuzzle4, 2);
        }

        if (_playerStats.projectileCount == 5)
        {
            //1
            GameObject _inst1 = Instantiate(bulletObj, bulletSpawnPoint2.position, transform.rotation);
            _inst1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle1 = Instantiate(muzzleObj, bulletSpawnPoint2.position, transform.rotation);
            _instMuzzle1.transform.parent = bulletSpawnPoint2;
            Destroy(_instMuzzle1, 2);

            //2
            GameObject _inst2 = Instantiate(bulletObj, bulletSpawnPoint3.position, transform.rotation);
            _inst2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle2 = Instantiate(muzzleObj, bulletSpawnPoint3.position, transform.rotation);
            _instMuzzle2.transform.parent = bulletSpawnPoint3;
            Destroy(_instMuzzle2, 2);

            //3
            GameObject _inst3 = Instantiate(bulletObj, bulletSpawnPoint1.position, transform.rotation);
            _inst3.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle3 = Instantiate(muzzleObj, bulletSpawnPoint1.position, transform.rotation);
            _instMuzzle3.transform.parent = bulletSpawnPoint1;
            Destroy(_instMuzzle3, 2);

            //4
            GameObject _inst4 = Instantiate(bulletObj, bulletSpawnPoint4.position, transform.rotation);
            _inst4.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle4 = Instantiate(muzzleObj, bulletSpawnPoint4.position, transform.rotation);
            _instMuzzle4.transform.parent = bulletSpawnPoint4;
            Destroy(_instMuzzle4, 2);

            //5
            GameObject _inst5 = Instantiate(bulletObj, bulletSpawnPoint5.position, transform.rotation);
            _inst5.GetComponent<DefaultGunBullet>()._gunController = _gunController;

            GameObject _instMuzzle5 = Instantiate(muzzleObj, bulletSpawnPoint5.position, transform.rotation);
            _instMuzzle5.transform.parent = bulletSpawnPoint5;
            Destroy(_instMuzzle5, 2);
        }

        StartCoroutine(Shot());
    }
}
