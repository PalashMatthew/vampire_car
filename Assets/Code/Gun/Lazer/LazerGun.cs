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
                StartCoroutine(Attack(other.gameObject));
            }
        }

        if (other.tag == "boss")
        {
            if (!_enemyInRadius.Contains(other.gameObject))
            {
                _enemyInRadius.Add(other.gameObject);
                StartCoroutine(AttackBoss(other.gameObject));
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

    IEnumerator Attack(GameObject obj)
    {
        if (obj != null)
        {
            _gunController.DamageEnemy(obj.gameObject, gameObject);
        }

        yield return new WaitForSeconds(_gunController.shotSpeed);

        if (_enemyInRadius.Contains(obj))
            StartCoroutine(Attack(obj));      
    }

    IEnumerator AttackBoss(GameObject obj)
    {
        if (obj != null)
        {
            _gunController.DamageBoss(obj.gameObject);
        }

        yield return new WaitForSeconds(_gunController.shotSpeed);

        if (_enemyInRadius.Contains(obj))
            StartCoroutine(Attack(obj));
    }
}
