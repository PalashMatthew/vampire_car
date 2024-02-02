using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun : MonoBehaviour
{
    Gun _gunController;
    GameplayController _gameplayController;

    private List<GameObject> _enemyInRadius = new List<GameObject>();

    void Initialize()
    {
        _gunController = GetComponent<Gun>();
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();

        StartCoroutine(Attack());
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        transform.localScale = new Vector3(_gunController.areaValue, _gunController.areaValue, _gunController.areaValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (!_enemyInRadius.Contains(other.gameObject))
            {
                _enemyInRadius.Add(other.gameObject);
            }
        }

        if (other.tag == "boss")
        {
            if (!_enemyInRadius.Contains(other.gameObject))
            {
                _enemyInRadius.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (_enemyInRadius.Contains(other.gameObject))
            {
                _enemyInRadius.Remove(other.gameObject);
            }
        }

        if (other.tag == "boss")
        {
            if (_enemyInRadius.Contains(other.gameObject))
            {
                _enemyInRadius.Remove(other.gameObject);
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(_gunController.shotSpeed);

        foreach (GameObject gm in _enemyInRadius)
        {
            if (gm != null)
            {
                if (!gm.GetComponent<EnemyController>().isBoss)
                    _gunController.DamageEnemy(gm, gameObject);
                else
                    _gunController.DamageBoss(gm, gameObject);
            }            
        }

        StartCoroutine(Attack());
    }
}
