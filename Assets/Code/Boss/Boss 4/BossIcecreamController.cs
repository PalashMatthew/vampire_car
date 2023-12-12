using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIcecreamController : MonoBehaviour
{
    bool isStop;

    GameObject player;

    [Header("Attack Rocket")]
    public float attackRocketTime;
    public GameObject rocketObj;
    public Transform spawnRocketPos;

    [Header("Attack 1")]
    public GameObject truckObj;

    public float attack1ShotPause;

    [Header("Attack 2")]
    public float attack2ShotPause;

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
                StartCoroutine(ShotRocket());
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

    IEnumerator ShotRocket()
    {
        yield return new WaitForSeconds(attackRocketTime);

        GameObject gm = Instantiate(rocketObj, spawnRocketPos.position, transform.rotation);

        gm.transform.LookAt(player.transform.position);
        gm.transform.eulerAngles = new Vector3(0, gm.transform.eulerAngles.y, 0);

        gm.GetComponent<Boss4Rocket>()._controller = _enemyController;
        gm.GetComponent<Boss4Rocket>().damage = damage;
        gm.GetComponent<Boss4Rocket>().target = player.transform.position;

        StartCoroutine(ShotRocket());
    }

    IEnumerator Shot1()
    {
        GameObject gm1 = Instantiate(truckObj, new Vector3(-26.5f, 0, 38), transform.rotation);
        GameObject gm2 = Instantiate(truckObj, new Vector3(-26.5f, 0, 20), transform.rotation);
        //GameObject gm3 = Instantiate(truckObj, new Vector3(-26.5f, 0, 2), transform.rotation);

        GameObject gm4 = Instantiate(truckObj, new Vector3(26.5f, 0, 28.8f), transform.rotation);
        GameObject gm5 = Instantiate(truckObj, new Vector3(26.5f, 0, 10.8f), transform.rotation);
        //GameObject gm6 = Instantiate(truckObj, new Vector3(26.5f, 0, -7.1f), transform.rotation);

        gm1.GetComponent<Boss4Truck>().isTruckUpDown = false;
        gm2.GetComponent<Boss4Truck>().isTruckUpDown = false;
        //gm3.GetComponent<Boss4Truck>().isTruckUpDown = false;
        gm4.GetComponent<Boss4Truck>().isTruckUpDown = false;
        gm5.GetComponent<Boss4Truck>().isTruckUpDown = false;
        //gm6.GetComponent<Boss4Truck>().isTruckUpDown = false;

        gm1.GetComponent<Boss4Truck>().isMoveRight = true;
        gm2.GetComponent<Boss4Truck>().isMoveRight = true;
        //gm3.GetComponent<Boss4Truck>().isMoveRight = true;

        gm4.GetComponent<Boss4Truck>().isMoveRight = false;
        gm5.GetComponent<Boss4Truck>().isMoveRight = false;
        //gm6.GetComponent<Boss4Truck>().isMoveRight = false;

        gm1.transform.eulerAngles = new Vector3(0, 90, 0);
        gm2.transform.eulerAngles = new Vector3(0, 90, 0);
        //gm3.transform.eulerAngles = new Vector3(0, 90, 0);

        gm4.transform.eulerAngles = new Vector3(0, -90, 0);
        gm5.transform.eulerAngles = new Vector3(0, -90, 0);
        //gm6.transform.eulerAngles = new Vector3(0, -90, 0);

        gm1.GetComponent<Boss4Truck>().damage = damage;
        gm2.GetComponent<Boss4Truck>().damage = damage;
        //gm3.GetComponent<Boss4Truck>().damage = damage;
        gm4.GetComponent<Boss4Truck>().damage = damage;
        gm5.GetComponent<Boss4Truck>().damage = damage;
        //gm6.GetComponent<Boss4Truck>().damage = damage;

        gm1.GetComponent<Boss4Truck>()._controller = _enemyController;
        gm2.GetComponent<Boss4Truck>()._controller = _enemyController;
        //gm3.GetComponent<Boss4Truck>()._controller = _enemyController;
        gm4.GetComponent<Boss4Truck>()._controller = _enemyController;
        gm5.GetComponent<Boss4Truck>()._controller = _enemyController;
        //gm6.GetComponent<Boss4Truck>()._controller = _enemyController;

        yield return new WaitForSeconds(attack1ShotPause);

        StartCoroutine(Shot2());
    }

    IEnumerator Shot2()
    {
        GameObject gm1 = Instantiate(truckObj, new Vector3(-13.3f, 0, 77), transform.rotation);
        GameObject gm2 = Instantiate(truckObj, new Vector3(-7, 0, 77), transform.rotation);
        GameObject gm3 = Instantiate(truckObj, new Vector3(7, 0, 77), transform.rotation);
        GameObject gm4 = Instantiate(truckObj, new Vector3(13.3f, 0, 77), transform.rotation);

        gm1.GetComponent<Boss4Truck>().isTruckUpDown = true;
        gm2.GetComponent<Boss4Truck>().isTruckUpDown = true;
        gm3.GetComponent<Boss4Truck>().isTruckUpDown = true;
        gm4.GetComponent<Boss4Truck>().isTruckUpDown = true;

        gm1.transform.eulerAngles = new Vector3(0, 180, 0);
        gm2.transform.eulerAngles = new Vector3(0, 180, 0);
        gm3.transform.eulerAngles = new Vector3(0, 180, 0);
        gm4.transform.eulerAngles = new Vector3(0, 180, 0);

        gm1.GetComponent<Boss4Truck>().damage = damage;
        gm2.GetComponent<Boss4Truck>().damage = damage;
        gm3.GetComponent<Boss4Truck>().damage = damage;
        gm4.GetComponent<Boss4Truck>().damage = damage;

        gm1.GetComponent<Boss4Truck>()._controller = _enemyController;
        gm2.GetComponent<Boss4Truck>()._controller = _enemyController;
        gm3.GetComponent<Boss4Truck>()._controller = _enemyController;
        gm4.GetComponent<Boss4Truck>()._controller = _enemyController;

        yield return new WaitForSeconds(attack2ShotPause);

        StartCoroutine(Shot1());
    }
}
