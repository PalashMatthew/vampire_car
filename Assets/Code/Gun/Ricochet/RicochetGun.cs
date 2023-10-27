using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetGun : MonoBehaviour
{
    public GameObject bulletObj;
    public Transform bulletSpawnPoint;

    Gun _gunController;
    GameplayController _gameplayController;
    GameObject _player;

    private List<GameObject> _activeEnemy = new List<GameObject>();


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

        if (_gameplayController.activeEnemy.Count > 0)
        {
            List<GameObject> _usedEnemy = new List<GameObject>();
            _activeEnemy.Clear();
            _activeEnemy.AddRange(_gameplayController.activeEnemy);

            for (int i = 1; i <= _gunController.projectileValue; i++)
            {
                GameObject _target = null;
                float _minDistance = 9999;

                foreach (GameObject gm in _gameplayController.activeEnemy)
                {
                    if (Vector3.Distance(_player.transform.position, gm.transform.position) < _minDistance && !_usedEnemy.Contains(gm) && gm.transform.position.z > _player.transform.position.z)
                    {
                        _target = gm;
                        _minDistance = Vector3.Distance(_player.transform.position, gm.transform.position);
                    }
                }

                if (_target == null)
                {
                    GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
                    _inst.transform.localEulerAngles = new Vector3(0, Random.Range(-50f, 50f), 0);
                    _inst.GetComponent<RicochetBullet>()._gunController = _gunController;
                } 
                else
                {
                    _usedEnemy.Add(_target);

                    GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
                    _inst.GetComponent<RicochetBullet>().target = _target;
                    _inst.GetComponent<RicochetBullet>()._gunController = _gunController;
                }                
            }
        }
        else
        {
            StartCoroutine(Shot());
            yield break;
        }



        StartCoroutine(Shot());
    }
}
