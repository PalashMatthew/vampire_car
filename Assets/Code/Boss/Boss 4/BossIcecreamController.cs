using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIcecreamController : MonoBehaviour
{
    bool isStop;

    GameObject player;
    public GameObject bulletObj;
    public Transform spawnPos;

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
            if (transform.position.z > 47)
            {
                BaseMovement();
            }
            else
            {
                isStop = true;
                isLocalMove = true;
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
            GameObject.Find("GameplayController").GetComponent<GameplayController>().Win();
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
        isLocalMove = false;
        transform.DOLookAt(player.transform.position, 0.5f);

        yield return new WaitForSeconds(1f);

        GameObject gm1 = Instantiate(bulletObj, spawnPos.position, transform.rotation);
        GameObject gm2 = Instantiate(bulletObj, spawnPos.position, transform.rotation);
        GameObject gm3 = Instantiate(bulletObj, spawnPos.position, transform.rotation);

        gm2.transform.eulerAngles = new Vector3(0, gm2.transform.eulerAngles.y - 30, 0);
        gm3.transform.eulerAngles = new Vector3(0, gm3.transform.eulerAngles.y + 30, 0);

        gm1.GetComponent<Boss4Bullet>()._controller = _enemyController;
        gm2.GetComponent<Boss4Bullet>()._controller = _enemyController;
        gm3.GetComponent<Boss4Bullet>()._controller = _enemyController;

        gm1.GetComponent<Boss4Bullet>().damage = damage;
        gm2.GetComponent<Boss4Bullet>().damage = damage;
        gm3.GetComponent<Boss4Bullet>().damage = damage;

        yield return new WaitForSeconds(0.5f);
        transform.DORotate(new Vector3(0, 180, 0), 0.5f);

        yield return new WaitForSeconds(0.5f);
        isLocalMove = true;

        yield return new WaitForSeconds(attack1ShotPause);

        StartCoroutine(Shot2());
    }

    IEnumerator Shot2()
    {
        isLocalMove = false;
        transform.DOLookAt(player.transform.position, 0.5f);

        yield return new WaitForSeconds(1f);

        GameObject gm1 = Instantiate(bulletObj, spawnPos.position, transform.rotation);
        GameObject gm2 = Instantiate(bulletObj, spawnPos.position, transform.rotation);
        GameObject gm3 = Instantiate(bulletObj, spawnPos.position, transform.rotation);
        GameObject gm4 = Instantiate(bulletObj, spawnPos.position, transform.rotation);
        GameObject gm5 = Instantiate(bulletObj, spawnPos.position, transform.rotation);

        gm2.transform.eulerAngles = new Vector3(0, gm2.transform.eulerAngles.y - 30, 0);
        gm3.transform.eulerAngles = new Vector3(0, gm3.transform.eulerAngles.y + 30, 0);
        gm4.transform.eulerAngles = new Vector3(0, gm2.transform.eulerAngles.y - 50, 0);
        gm5.transform.eulerAngles = new Vector3(0, gm3.transform.eulerAngles.y + 50, 0);

        gm1.GetComponent<Boss4Bullet>()._controller = _enemyController;
        gm2.GetComponent<Boss4Bullet>()._controller = _enemyController;
        gm3.GetComponent<Boss4Bullet>()._controller = _enemyController;
        gm4.GetComponent<Boss4Bullet>()._controller = _enemyController;
        gm5.GetComponent<Boss4Bullet>()._controller = _enemyController;

        gm1.GetComponent<Boss4Bullet>().damage = damage;
        gm2.GetComponent<Boss4Bullet>().damage = damage;
        gm3.GetComponent<Boss4Bullet>().damage = damage;
        gm4.GetComponent<Boss4Bullet>().damage = damage;
        gm5.GetComponent<Boss4Bullet>().damage = damage;

        yield return new WaitForSeconds(0.5f);
        transform.DORotate(new Vector3(0, 180, 0), 0.5f);

        yield return new WaitForSeconds(0.5f);
        isLocalMove = true;

        yield return new WaitForSeconds(attack2ShotPause);

        StartCoroutine(Shot3());
    }

    IEnumerator Shot3()
    {
        isLocalMove = false;
        transform.DOLookAt(player.transform.position, 0.5f);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 4; i++)
        {
            GameObject gm = Instantiate(bulletObj, spawnPos.position, transform.rotation);

            gm.GetComponent<Boss4Bullet>()._controller = _enemyController;
            gm.GetComponent<Boss4Bullet>().damage = damage;

            transform.DOLookAt(player.transform.position, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.5f);
        transform.DORotate(new Vector3(0, 180, 0), 0.5f);

        yield return new WaitForSeconds(0.5f);
        isLocalMove = true;

        yield return new WaitForSeconds(attack3ShotPause);

        StartCoroutine(Shot1());
    }
}
