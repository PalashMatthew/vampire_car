using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGun : MonoBehaviour
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
        yield return new WaitForSeconds(_gunController.shotSpeed);

        GameObject _target;

        if (_gameplayController.activeEnemy.Count > 0)
        {
            _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];

            int i = 10;

            while (!_target.GetComponent<EnemyController>().isVisible)
            {
                _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];
                i--;

                if (i <= 0)
                {
                    StartCoroutine(Shot());
                    yield break;
                }
            }

            GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
            _inst.GetComponent<IceBullet>().target = _target;
            _inst.GetComponent<IceBullet>()._gunController = _gunController;

            if (_gunController.projectileValue > 1)
            {
                StartCoroutine(AnotherShot());
            }

            StartCoroutine(Shot());
        }
        else
        {
            StartCoroutine(Shot());
            yield break;
        }        
    }

    IEnumerator AnotherShot()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject _target;

        if (_gameplayController.activeEnemy.Count > 0)
        {
            _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];

            int i = 10;

            while (!_target.GetComponent<EnemyController>().isVisible)
            {
                _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];
                i--;

                if (i <= 0)
                {
                    yield break;
                }
            }

            GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
            _inst.GetComponent<IceBullet>().target = _target;
            _inst.GetComponent<IceBullet>()._gunController = _gunController;
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(0.1f);

        if (_gunController.projectileValue == 3)
        {
            if (_gameplayController.activeEnemy.Count > 0)
            {
                _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];

                int i = 10;

                while (!_target.GetComponent<EnemyController>().isVisible)
                {
                    _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];
                    i--;

                    if (i <= 0)
                    {
                        yield break;
                    }
                }

                GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
                _inst.GetComponent<IceBullet>().target = _target;
                _inst.GetComponent<IceBullet>()._gunController = _gunController;
            }
            else
            {
                yield break;
            }
        }
    }
}
