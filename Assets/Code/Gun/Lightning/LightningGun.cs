using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGun : MonoBehaviour
{
    public GameObject bulletObj;
    public Transform bulletSpawnPoint;

    Gun _gunController;
    GameplayController _gameplayController;
    GameObject _player;

    private List<GameObject> _activeEnemy = new List<GameObject>();

    public LineRenderer _lineRenderer;


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

            if (_target != null)
            {
                _usedEnemy.Add(_target);

                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, new Vector3(transform.position.x, 1, transform.position.z));
                _lineRenderer.SetPosition(1, new Vector3(_target.transform.position.x, 1, _target.transform.position.z));
                StartCoroutine(OffLine());
            }
        }
        else
        {
            StartCoroutine(Shot());
            yield break;
        }

        StartCoroutine(Shot());
    }

    IEnumerator OffLine()
    {
        yield return new WaitForSeconds(0.1f);
        _lineRenderer.enabled = false;
    }
}
