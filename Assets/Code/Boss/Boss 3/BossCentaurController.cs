using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCentaurController : MonoBehaviour
{
    bool isStop;

    [Header("Attack 1")]
    public GameObject spawnPos;
    public List<GameObject> spawnPosObj;

    public int attack1BulletCount;
    public GameObject objBullet1;
    public float attack1ShotInterval;
    public float attack1ShotPause;

    [Header("Attack 2")]
    public int attack2BulletCount;
    public float attack2ShotInterval;
    public float attack2ShotPause;
    public int lazerCount;

    public Transform shot2Pos1, shot2Pos2;

    EnemyController _enemyController;

    bool isLocalMove;
    public float damage;

    private Vector3 localMoveDirection;
    public float maxX, minX;
    public float localMoveSpeed;

    public GameObject mesh1, mesh2, mesh3;


    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();

        localMoveDirection = Vector3.left;

        mesh1.SetActive(true);
        mesh2.SetActive(false);
        mesh3.SetActive(false);
    }

    private void Update()
    {
        if (!isStop)
        {
            if (transform.position.z > 58)
            {
                BaseMovement();
            }
            else
            {
                isStop = true;
                StartCoroutine(Shot1());
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
            GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave = 11;
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
        spawnPos.transform.eulerAngles = new Vector3(0, 0, 0);

        for (int i = 0; i < attack1BulletCount; i++)
        {
            foreach (GameObject pos in spawnPosObj)
            {
                GameObject gm = Instantiate(objBullet1, pos.transform.position, pos.transform.rotation);

                gm.GetComponent<BossTankBullet1>().damage = damage;
                gm.GetComponent<BossTankBullet1>()._controller = GetComponent<EnemyController>();
            }

            spawnPos.transform.eulerAngles = new Vector3(0, spawnPos.transform.eulerAngles.y + 10, 0);
            yield return new WaitForSeconds(attack1ShotInterval);
        }

        isLocalMove = true;

        yield return new WaitForSeconds(attack1ShotPause);
        isLocalMove = false;

        if (_enemyController.hp >= _enemyController.maxHp / 2)
        {
            StartCoroutine(Shot1());
        }
        else
        {
            StartCoroutine(StartPhase2());
        }        
    }

    IEnumerator StartPhase2()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);

        mesh1.SetActive(false);
        mesh2.SetActive(true);
        mesh3.SetActive(true);

        mesh2.transform.DOMoveX(-9.2f, 1f);
        mesh3.transform.DOMoveX(9.2f, 1f);

        yield return new WaitForSeconds(1);

        StartCoroutine(Shot2());
    }

    IEnumerator Shot2()
    {
        List<float> rotYPos = new List<float>();

        for (int i = 0; i < lazerCount; i++)
        {
            float rand = Random.Range(140f, 220f);

            rotYPos.Add(rand);
        }        

        for (int i = 0; i < attack2BulletCount; i++)
        {           
            for (int t = 0; t < lazerCount; t++)
            {
                shot2Pos1.transform.eulerAngles = new Vector3(0, rotYPos[t], 0);
                shot2Pos2.transform.eulerAngles = new Vector3(0, -rotYPos[t], 0);

                GameObject gm1 = Instantiate(objBullet1, shot2Pos1.transform.position, shot2Pos1.transform.rotation);
                GameObject gm2 = Instantiate(objBullet1, shot2Pos2.transform.position, shot2Pos2.transform.rotation);

                gm1.GetComponent<BossTankBullet1>().damage = damage;
                gm1.GetComponent<BossTankBullet1>()._controller = GetComponent<EnemyController>();

                gm2.GetComponent<BossTankBullet1>().damage = damage;
                gm2.GetComponent<BossTankBullet1>()._controller = GetComponent<EnemyController>();
            }            

            yield return new WaitForSeconds(attack2ShotInterval);
        }

        yield return new WaitForSeconds(attack2ShotPause);

        StartCoroutine(Shot2());
    }
}
