using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BossFireTruckController;

public class BossTankController : MonoBehaviour
{
    GameObject player;

    public Transform bulletSpawnPos;
    public GameObject cabinObj;

    bool isStop;

    [Header("Shield")]
    public BoxCollider collider;
    public ParticleSystem vfxShield;
    public GameObject shieldObj;

    [Header("Attack 1")]
    public int attack1BulletCount;
    public GameObject objBullet1;
    public float attack1ShotInterval;
    public float attack1ShotPause;

    private int attack1CurrentCount;

    [Header("Attack 2")]
    public int attack2BulletCount;
    public GameObject objBullet2;
    public float attack2ShotInterval;
    public float attack2ShotPause;

    private int attack2CurrentCount;

    EnemyController _enemyController;

    public float damage;


    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        player = GameObject.Find("Player");

        attack1CurrentCount = 0;

        collider.enabled = true;
        shieldObj.SetActive(false);

        StartCoroutine(Shield());
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

        cabinObj.transform.LookAt(player.transform.position);
        cabinObj.transform.localEulerAngles = new Vector3(0, cabinObj.transform.localEulerAngles.y, 0);

        CheckKillBoss();
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

    IEnumerator Shield()
    {
        yield return new WaitForSeconds(6);
        collider.enabled = false;
        vfxShield.Play();
        shieldObj.SetActive(true);

        yield return new WaitForSeconds(2);
        collider.enabled = true;
        vfxShield.Stop();
        shieldObj.SetActive(false);

        StartCoroutine(Shield());
    }

    void BaseMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 30);
    }

    IEnumerator Shot1()
    {
        for (int i = 0; i < attack1BulletCount; i++)
        {
            GameObject gm = Instantiate(objBullet1, bulletSpawnPos.position, cabinObj.transform.rotation);
            gm.GetComponent<BossTankBullet1>().damage = damage;
            gm.GetComponent<BossTankBullet1>()._controller = GetComponent<EnemyController>();

            yield return new WaitForSeconds(attack1ShotInterval);
        }

        yield return new WaitForSeconds(attack1ShotPause);

        attack1CurrentCount++;

        if (attack1CurrentCount < 3)
        {
            StartCoroutine(Shot1());
        } 
        else
        {
            attack1CurrentCount = 0;
            StartCoroutine(Shot2());
        }
    }

    IEnumerator Shot2()
    {
        for (int i = 0; i < attack2BulletCount; i++)
        {
            GameObject gm = Instantiate(objBullet2, bulletSpawnPos.position, cabinObj.transform.rotation);
            gm.GetComponent<BossTankBullet2>().damage = damage;
            gm.GetComponent<BossTankBullet2>()._controller = GetComponent<EnemyController>();
            gm.GetComponent<BossTankBullet2>().target = player.transform.position;

            yield return new WaitForSeconds(attack2ShotInterval);
        }

        yield return new WaitForSeconds(attack2ShotPause);

        attack2CurrentCount++;

        if (attack2CurrentCount < 3)
        {
            StartCoroutine(Shot2());
        }
        else
        {
            attack2CurrentCount = 0;
            StartCoroutine(Shot1());
        }
    }
}
