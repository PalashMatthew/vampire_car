using DG.Tweening;
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
    EnemyController _enemyController;
    private int _enemyShotCount = 1;


    private void Start()
    {
        _gunController = GetComponent<EnemyGun>();
        _player = GameObject.Find("Player");
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyController = GetComponent<EnemyController>();

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
        yield return new WaitForSeconds(2 * GameObject.Find("GameplayController").GetComponent<WaveController>().waveList[GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1].attackSpeedCoeff);
        objLazer.SetActive(true);
        objLazer.GetComponent<MeshRenderer>().material = matDefault;
        _enemyMovement.StopAllCoroutines();
        _isLookAt = true;
        _enemyMovement._isStartLocalMove = false;
        yield return new WaitForSeconds(0.1f);
        _isLookAt = false;
        yield return new WaitForSeconds(1f);
        objLazer.GetComponent<MeshRenderer>().material = matAttack;
        objLazer.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        objLazer.GetComponent<BoxCollider>().enabled = false;
        objLazer.SetActive(false);
        yield return new WaitForSeconds(_gunController.pauseTime - 0.5f);
        transform.DORotate(new Vector3(0, 180, 0), 0.3f);
        _enemyMovement.StartCoroutine(_enemyMovement.LocalMoveEnum());

        if (_enemyShotCount >= 3 && _enemyController.carType == EnemyController.CarType.Static)
        {
            _enemyMovement.StartCoroutine(_enemyMovement.MoveInside());
        }
        else
        {
            StartCoroutine(AttackEnum());
            _enemyShotCount++;
        }        
    }

    void LookAt()
    {
        //transform.LookAt(_player.transform);
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Vector3 relativePos = _player.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1f);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Shot()
    {
        objLazer.GetComponent<BoxCollider>().enabled = true;
    }
}
