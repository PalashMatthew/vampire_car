using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilGun : MonoBehaviour
{
    public GameObject bulletObj;
    public Transform bulletSpawnPoint;

    Gun _gunController;
    GameplayController _gameplayController;
    GameObject _player;


    void Initialize()
    {
        _gunController = GetComponent<Gun>();
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
        _player = GameObject.Find("Player");
    }

    private void Start()
    {
        Initialize();

        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(_gunController.shotSpeed);

        GameObject _target = null;
        float _minDistance = 9999;

        foreach (GameObject gm in _gameplayController.activeEnemy)
        {
            if (Vector3.Distance(_player.transform.position, gm.transform.position) < _minDistance)
            {
                _target = gm;
                _minDistance = Vector3.Distance(_player.transform.position, gm.transform.position);
            }
        }

        GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst.GetComponent<OilBullet>().target = _target.transform.position;
        _inst.GetComponent<OilBullet>()._gunController = _gunController;

        StartCoroutine(Shot());
    }
}
