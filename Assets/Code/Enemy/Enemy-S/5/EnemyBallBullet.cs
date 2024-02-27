using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallBullet : MonoBehaviour
{
    public float bulletMoveSpeed;
    public float rotateSpeed;

    public GameObject objBall1;
    public GameObject objBall2;
    public GameObject cylinder;

    public GameObject projectilesParent;

    private void Start()
    {
        StartAnimation();

        EnemyDefaultBullet enemyDefaulBullet = GetComponent<EnemyDefaultBullet>();
        objBall1.GetComponent<EnemyBallBulletCollider>()._gunController = enemyDefaulBullet._gunController;
        objBall2.GetComponent<EnemyBallBulletCollider>()._gunController = enemyDefaulBullet._gunController;

        objBall1.GetComponent<EnemyBallBulletCollider>()._controller = enemyDefaulBullet._controller;
        objBall2.GetComponent<EnemyBallBulletCollider>()._controller = enemyDefaulBullet._controller;

        objBall1.GetComponent<EnemyBallBulletCollider>().brain = gameObject;
        objBall2.GetComponent<EnemyBallBulletCollider>().brain = gameObject;
    }


    private void Update()
    {
        //transform.Translate(-transform.forward * bulletMoveSpeed * Time.deltaTime); 

        projectilesParent.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }

    void StartAnimation()
    {
        objBall1.transform.DOLocalMoveX(-1f, 0);
        objBall2.transform.DOLocalMoveX(1f, 0);
        cylinder.transform.DOScale(new Vector3(0.23f, 0.52f, 0.23f), 0);

        objBall1.transform.DOLocalMoveX(-2.2f, 3);
        objBall2.transform.DOLocalMoveX(2.2f, 3);
        cylinder.transform.DOScale(new Vector3(0.23f, 2.08f, 0.23f), 3);
    }
}
