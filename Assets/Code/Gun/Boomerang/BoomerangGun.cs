using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangGun : MonoBehaviour
{
    public GameObject bulletObj;
    public Transform bulletSpawnPoint;

    Gun _gunController;
    GameplayController _gameplayController;

    void Initialize()
    {
        _gunController = GetComponent<Gun>();
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();

        StartCoroutine(Shot());
    }

    private void Start()
    {
        Initialize();
    }

    IEnumerator Shot()
    {
        if (_gameplayController.activeEnemy.Count > 0)
        {
            for (int i = 1; i <= _gunController.projectileValue; i++)
            {
                GameObject _target;

                int _rand = Random.Range(0, _gameplayController.activeEnemy.Count);
                _target = _gameplayController.activeEnemy[_rand];

                GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
                _inst.transform.LookAt(_target.transform.position);
                _inst.transform.eulerAngles = new Vector3(0, _inst.transform.eulerAngles.y, 0);
                _inst.GetComponent<BoomerangBullet>()._gunController = _gunController;
            }
        }
        yield return new WaitForSeconds(_gunController.shotSpeed);
        StartCoroutine(Shot());
    }

    IEnumerator NotFind()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Shot());
    }
}
