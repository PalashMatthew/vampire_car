using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodGun : MonoBehaviour
{
    public GameObject bulletObj;

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
            List<GameObject> _enemyDrone = new List<GameObject>();

            foreach (GameObject gm in _gameplayController.activeEnemy)
            {
                if (gm != null)
                {
                    if (gm.GetComponent<EnemyController>().carType == EnemyController.CarType.Static)
                        _enemyDrone.Add(gm);
                }
            }

            if (_enemyDrone.Count > 0)
            {
                _target = _gameplayController.activeEnemy[Random.Range(0, _enemyDrone.Count)];
            } 
            else
            {
                _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];
            }            

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

            GameObject _inst = Instantiate(bulletObj, _target.transform.position, transform.rotation);
            _inst.GetComponent<GodGunObj>().target = _target;
            _inst.GetComponent<GodGunObj>()._gunController = _gunController;

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
            List<GameObject> _enemyDrone = new List<GameObject>();

            foreach (GameObject gm in _gameplayController.activeEnemy)
            {
                if (gm != null)
                {
                    if (gm.GetComponent<EnemyController>().carType == EnemyController.CarType.Static)
                        _enemyDrone.Add(gm);
                }
            }

            if (_enemyDrone.Count > 0)
            {
                _target = _gameplayController.activeEnemy[Random.Range(0, _enemyDrone.Count)];
            }
            else
            {
                _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];
            }

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

            GameObject _inst = Instantiate(bulletObj, _target.transform.position, transform.rotation);
            _inst.GetComponent<GodGunObj>().target = _target;
            _inst.GetComponent<GodGunObj>()._gunController = _gunController;
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
                List<GameObject> _enemyDrone = new List<GameObject>();

                foreach (GameObject gm in _gameplayController.activeEnemy)
                {
                    if (gm != null)
                    {
                        if (gm.GetComponent<EnemyController>().carType == EnemyController.CarType.Static)
                            _enemyDrone.Add(gm);
                    }
                }

                if (_enemyDrone.Count > 0)
                {
                    _target = _gameplayController.activeEnemy[Random.Range(0, _enemyDrone.Count)];
                }
                else
                {
                    _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];
                }

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

                GameObject _inst = Instantiate(bulletObj, _target.transform.position, transform.rotation);
                _inst.GetComponent<GodGunObj>().target = _target;
                _inst.GetComponent<GodGunObj>()._gunController = _gunController;
            }
            else
            {
                yield break;
            }
        }
    }
}
