using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5TimberController : MonoBehaviour
{
    bool isStop;

    GameObject player;
    public GameObject bulletObj;
    public Transform spawnPos;

    [Header("Attack 1")]
    public Transform attack1SpawnPos;
    public int attack1BulletCount;
    public float attack1BulletMoveSpeed;
    public float attack1IterationTime;

    [Header("Attack 2")]
    public Transform attack2SpawnPos;
    public Transform attack2SpawnPos2;
    public GameObject bulletObjWood;
    public float attack2BulletMoveSpeed;

    public float attack1ShotPause;
    public float attack2ShotPause;
    public float attack3ShotPause;

    EnemyController _enemyController;

    bool isLocalMove;
    public float damage;

    private Vector3 localMoveDirection;
    public float maxX, minX;
    public float localMoveSpeed;


    private void Start()
    {
        player = GameObject.Find("Player");

        _enemyController = GetComponent<EnemyController>();

        localMoveDirection = Vector3.left;
    }

    private void Update()
    {
        if (!isStop)
        {
            if (transform.position.z > 59)
            {
                BaseMovement();
            }
            else
            {
                isStop = true;
                isLocalMove = true;
                StartCoroutine(Shot1());
                StartCoroutine(Shot2());
            }
        }

        CheckKillBoss();

        if (isLocalMove)
        {
            LocalMove();
        }
    }

    void CheckKillBoss()
    {
        if (_enemyController.hp <= 0)
        {
            GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave++;
            GameObject.Find("GameplayController").GetComponent<GameplayController>().Win();

            GameObject _fx = Instantiate(_enemyController.fxExplosion, transform.position, transform.rotation);
            Destroy(_fx, 3);

            Destroy(gameObject);
        }
    }

    void BaseMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 30);
    }

    void LocalMove()
    {
        transform.Translate(localMoveDirection * Time.deltaTime * localMoveSpeed);

        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            localMoveDirection = Vector3.right;
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            localMoveDirection = Vector3.left;
        }
    }

    IEnumerator Shot1()
    {
        float time = attack1ShotPause;

        if (_enemyController.hp >= _enemyController.maxHp / 2)
        {
            time /= 2;
        }

        yield return new WaitForSeconds(time);

        isLocalMove = false;

        //attack1SpawnPos.LookAt(player.transform.position);
        //float _y = attack1SpawnPos.eulerAngles.y;

        for (int i = 0; i < attack1BulletCount; i++)
        {
            attack1SpawnPos.LookAt(player.transform.position);
            float _y = attack1SpawnPos.eulerAngles.y;

            attack1SpawnPos.eulerAngles = new Vector3(0, Random.Range(_y - 45, _y + 45), 0);

            GameObject gm = Instantiate(bulletObj, attack1SpawnPos.position, attack1SpawnPos.rotation);
            gm.GetComponent<BossTankBullet1>()._controller = _enemyController;
            gm.GetComponent<BossTankBullet1>().damage = damage;
            gm.GetComponent<BossTankBullet1>().bulletMoveSpeed = attack1BulletMoveSpeed;

            yield return new WaitForSeconds(attack1IterationTime);
        }
        isLocalMove = true;

        StartCoroutine(Shot1());
    }

    IEnumerator Shot2()
    {
        yield return new WaitForSeconds(attack2ShotPause);

        GameObject gm = Instantiate(bulletObjWood, attack2SpawnPos.position, transform.rotation);
        gm.GetComponent<BossTankBullet1>()._controller = _enemyController;
        gm.GetComponent<BossTankBullet1>().damage = damage;
        gm.GetComponent<BossTankBullet1>().bulletMoveSpeed = attack2BulletMoveSpeed;

        gm.transform.DOMoveX(attack2SpawnPos.position.x - 3.7f, 0.3f);
        gm.transform.DOMoveY(0.48f, 0.3f);

        if (_enemyController.hp >= _enemyController.maxHp / 2)
        {
            GameObject gm2 = Instantiate(bulletObjWood, attack2SpawnPos2.position, transform.rotation);
            gm2.GetComponent<BossTankBullet1>()._controller = _enemyController;
            gm2.GetComponent<BossTankBullet1>().damage = damage;
            gm2.GetComponent<BossTankBullet1>().bulletMoveSpeed = attack2BulletMoveSpeed;

            gm2.transform.DOMoveX(attack2SpawnPos2.position.x + 3.7f, 0.3f);
            gm2.transform.DOMoveY(0.48f, 0.3f);
        }

        StartCoroutine(Shot2());
    }    
}
