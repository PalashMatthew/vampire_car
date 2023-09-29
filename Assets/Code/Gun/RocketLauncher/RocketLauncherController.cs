using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RocketLauncherController : MonoBehaviour
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
            _inst.GetComponent<RocketLauncherBullet>().target = _target;
            _inst.GetComponent<RocketLauncherBullet>()._gunController = _gunController;
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
