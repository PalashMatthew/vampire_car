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
        GameObject _target = null;

        if (_gameplayController.activeEnemy != null)
        {
            bool _visibleCount = false;
            foreach (GameObject gm in _gameplayController.activeEnemy)
            {
                if (gm.GetComponent<EnemyController>().isVisible)
                    _visibleCount = true;
            }

            if (_visibleCount)
            {
                while (_target == null)
                {
                    _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];

                    if (!_target.GetComponent<EnemyController>().isVisible)
                    {
                        _target = null;
                    }
                }
            } else
            {
                StartCoroutine(NotFind());
                yield break;
            }

            GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
            _inst.transform.LookAt(_target.transform.position);
            _inst.transform.eulerAngles = new Vector3(0, _inst.transform.eulerAngles.y, 0);
            _inst.GetComponent<BoomerangBullet>()._gunController = _gunController;
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
