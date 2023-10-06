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
        GameObject _target;

        if (_gameplayController.activeEnemy.Count > 0)
        {
            for (int i = 1; i <= _gunController.projectileValue; i++)
            {
                _target = _gameplayController.activeEnemy[Random.Range(0, _gameplayController.activeEnemy.Count)];

                GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
                _inst.GetComponent<IceBullet>().target = _target;
                _inst.GetComponent<IceBullet>()._gunController = _gunController;
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
