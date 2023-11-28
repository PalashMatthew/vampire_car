using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckController : MonoBehaviour
{
    public int currentPhase = 1;
    public int currentAttack = 1;

    public float damage;

    public enum BossActions
    {
        FirstAppearance,
        FindPlayer,
        Pause,
        FireAttack1,
        FireAttack2,
        ShotRicochet
    }
    public BossActions action;

    EnemyController _enemyController;
        
    public float moveSpeedStart;
    public float moveSpeedFindPlayer;

    //bool _isFirstAppearance = true;
    GameObject _player;

    public ParticleSystem fxPrewarmShot;
    public ParticleSystem fxShot1_1, fxShot1_2, fxShot1_3;
    public ParticleSystem fxShot2;

    public float lookMove;

    public GameObject fxBoom;
    public GameObject mesh;
    public GameObject meshPanel;

    public GameObject objFireball;
    public float meshMoveSpeed;

    [Header("Ricochet")]
    public Transform spawnRicochetPos;
    public GameObject objRicochetBullet;
    public float shotRicochetTimePause;
    public int shotRicochetCount;


    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        #region Phase 1
        if (action == BossActions.FirstAppearance)
        {
            if (transform.position.z > 47)
                BaseMovement();
            else
            {
                //action = BossActions.FindPlayer;
                action = BossActions.ShotRicochet;
                StartCoroutine(ShotRicochet());
            }
        }

        if (action == BossActions.FindPlayer)
        {
            FindPlayer();
        }

        if (action == BossActions.Pause)
        {
            fxPrewarmShot.Play();
        }

        //if (currentPhase == 2)
        //{
        //    FindPlayer();
        //}
        #endregion

        #region Phase 2
        CheckKillBoss();
        #endregion
    }

    #region Phase 1
    //Следит за игроком по X пока не поймает его
    void FindPlayer()
    {
        transform.DOMoveX(_player.transform.position.x, moveSpeedFindPlayer);
    }

    IEnumerator ShotRicochet()
    {
        for (int i = 0; i < shotRicochetCount; i++)
        {
            GameObject gm = Instantiate(objRicochetBullet, spawnRicochetPos.position, transform.rotation);
            gm.transform.localEulerAngles = new Vector3(0, Random.Range(130f, 230f), 0);
            gm.GetComponent<BossFireTruckRicochetBullet>().damage = damage;
        }

        yield return new WaitForSeconds(shotRicochetTimePause);

        if (_enemyController.hp >= _enemyController.maxHp / 2)
        {
            StartCoroutine(ShotRicochet());
        } 
        else
        {
            currentPhase = 2;
            currentAttack = 1;
            action = BossActions.FindPlayer;
            StartCoroutine(Pause());
        }
    }

    IEnumerator FindPlayerTimer()
    {
        yield return new WaitForSeconds(2);
        DOTween.KillAll();
        action = BossActions.Pause;
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(2);

        //if (_enemyController.hp <= _enemyController.maxHp / 2 && currentPhase == 1)
        //{
        //    currentPhase = 2;
        //    StartCoroutine(StartPhase2());
        //    yield break;
        //}

        if (currentAttack == 1)
        {
            action = BossActions.FireAttack1;
            StartCoroutine(FireAttack1());
        }

        if (currentAttack == 2)
        {
            action = BossActions.FireAttack2;
            StartCoroutine(FireAttack2());
        }
    }

    IEnumerator FireAttack1()
    {
        fxShot1_1.gameObject.transform.LookAt(_player.transform.position, Vector3.up);
        fxShot1_1.gameObject.transform.eulerAngles = new Vector3(0, fxShot1_1.gameObject.transform.eulerAngles.y, 0);
        fxShot1_1.Play();

        yield return new WaitForSeconds(2);

        fxShot1_2.gameObject.transform.LookAt(_player.transform.position, Vector3.up);
        fxShot1_2.gameObject.transform.eulerAngles = new Vector3(0, fxShot1_2.gameObject.transform.eulerAngles.y, 0);
        fxShot1_2.Play();

        yield return new WaitForSeconds(2);

        fxShot1_3.gameObject.transform.LookAt(_player.transform.position, Vector3.up);
        fxShot1_3.gameObject.transform.eulerAngles = new Vector3(0, fxShot1_3.gameObject.transform.eulerAngles.y, 0);
        fxShot1_3.Play();

        yield return new WaitForSeconds(2);

        currentAttack = 2;

        action = BossActions.FindPlayer;
        StartCoroutine(FindPlayerTimer());
    }

    IEnumerator FireAttack2()
    {
        int _rot1 = 0;
        int _rot2 = 0;
        int _rand = Random.Range(1, 3);

        if (_rand == 1)
        {
            _rot1 = -90;
            _rot2 = 90;
        }

        if (_rand == 2)
        {
            _rot1 = 90;
            _rot2 = -90;
        }

        fxShot2.gameObject.transform.eulerAngles = new Vector3(0, _rot1, 0);
        fxShot2.gameObject.transform.eulerAngles = new Vector3(0, fxShot2.gameObject.transform.eulerAngles.y, 0);
        fxShot2.Play();

        transform.DOMoveZ(-20f, 1.5f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, 90);

        transform.DOMoveZ(47f, 2).SetEase(Ease.Linear);
        yield return new WaitForSeconds(2);

        fxShot2.gameObject.transform.eulerAngles = new Vector3(0, _rot2, 0);
        fxShot2.gameObject.transform.eulerAngles = new Vector3(0, fxShot2.gameObject.transform.eulerAngles.y, 0);
        fxShot2.Play();

        transform.DOMoveZ(-20f, 1.5f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(1.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, 90);

        transform.DOMoveZ(47f, 2).SetEase(Ease.Linear);
        yield return new WaitForSeconds(2);

        currentAttack = 1;

        action = BossActions.FindPlayer;
        StartCoroutine(FindPlayerTimer());
    }

    void BaseMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeedStart);
    }
    #endregion

    #region Phase 2
    IEnumerator StartPhase2()
    {
        GameObject _boom = Instantiate(fxBoom, transform.position, transform.rotation);
        Destroy(_boom, 3);

        yield return new WaitForSeconds(0.1f);
        mesh.SetActive(false);

        meshPanel.SetActive(true);

        StartCoroutine(FireballAttack());
    }

    IEnumerator FireballAttack()
    {
        yield return new WaitForSeconds(2);

        GameObject _inst = Instantiate(objFireball, transform.position, transform.rotation);
        _inst.transform.LookAt(_player.transform.position);
        _inst.transform.eulerAngles = new Vector3(0, _inst.transform.eulerAngles.y, 0);
        _inst.transform.position = new Vector3(_inst.transform.position.x, 2, _inst.transform.position.z);
        _inst.GetComponent<BossFireTruckFireball>()._brain = gameObject.GetComponent<BossFireTruckController>();
        Destroy(_inst, 5);

        StartCoroutine(FireballAttack());
    }

    void CheckKillBoss()
    {
        if (_enemyController.hp <= 0)
        {
            GameObject.Find("GameplayController").GetComponent<GameplayController>().Win();
            Destroy(gameObject);
        }
    }
    #endregion
}
