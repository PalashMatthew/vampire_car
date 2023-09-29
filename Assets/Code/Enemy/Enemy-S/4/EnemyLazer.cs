using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazer : MonoBehaviour
{
    GameObject _player;

    private bool _isLookAt;

    public LayerMask layer;
    EnemyGun _gunController;

    public GameObject objLazer;
    public Material matDefault, matAttack;

    EnemyMovement _enemyMovement;


    private void Start()
    {
        _gunController = GetComponent<EnemyGun>();
        _player = GameObject.Find("Player");
        _enemyMovement = GetComponent<EnemyMovement>();

        objLazer.SetActive(false);
    }

    private void Update()
    {
        if (_isLookAt)
        {
            LookAt();
        }
    }

    public IEnumerator AttackEnum()
    {
        yield return new WaitForSeconds(2);
        objLazer.SetActive(true);
        objLazer.GetComponent<MeshRenderer>().material = matDefault;
        _enemyMovement.StopAllCoroutines();
        _isLookAt = true;
        yield return new WaitForSeconds(2);
        _isLookAt = false;
        yield return new WaitForSeconds(1);
        objLazer.GetComponent<MeshRenderer>().material = matAttack;
        objLazer.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        objLazer.GetComponent<BoxCollider>().enabled = false;
        objLazer.SetActive(false);
        yield return new WaitForSeconds(_gunController.pauseTime - 0.5f);
        _enemyMovement.StartCoroutine(_enemyMovement.LocalMove());
        StartCoroutine(AttackEnum());
    }

    void LookAt()
    {
        transform.LookAt(_player.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Shot()
    {
        objLazer.GetComponent<BoxCollider>().enabled = true;
    }
}
