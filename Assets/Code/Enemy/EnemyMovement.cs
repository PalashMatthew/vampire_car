using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyController _enemyController;

    public bool localMove;  //Будет ли статичный враг двигаться вправо влево после того как выедет на экран?
    public bool isLazer;
    public float moveSpeed;
    

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();        
    }

    private void Update()
    {
        moveSpeed = _enemyController.moveSpeed;

        if (_enemyController.carType == EnemyController.CarType.Movement)
        {
            BaseMovement();
        }

        if (_enemyController.carType == EnemyController.CarType.Static)
        {
            if (transform.position.z > 45)
                BaseMovement();
            else if (!_enemyController.isCarStop)
                _enemyController.isCarStop = true;

            if (_enemyController.isCarStop && localMove)
            {
                StartCoroutine(LocalMove());
                localMove = false;
            }

            if (_enemyController.isCarStop && isLazer)
            {
                gameObject.GetComponent<EnemyLazer>().StartCoroutine(gameObject.GetComponent<EnemyLazer>().AttackEnum());
                isLazer = false;
            }
        }
    }

    void BaseMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    public IEnumerator LocalMove()
    {
        transform.DOMoveX(transform.position.x + 4, 2);
        yield return new WaitForSeconds(2);
        transform.DOMoveX(transform.position.x - 4, 2);
        yield return new WaitForSeconds(2);
        StartCoroutine(LocalMove());
    }
}
