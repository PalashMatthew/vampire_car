using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum CarType
    {
        Movement,
        Static
    }

    public CarType carType;

    public bool isCarStop;

    [HideInInspector]
    public float moveSpeed;
    public float hp;
    public float maxHp;
    public float brakeDamage;

    public float moveSpeedMin;
    public float moveSpeedMax;

    public MeshRenderer meshRenderer;

    public GameObject screwObj;

    public bool isVisible;
    public bool isPattern;

    //AFK
    private float _timerAfk = 5;
    public float zInvisibleDestroy;

    public GameObject fxExplosion;

    public bool isTest;

    public bool _isFreeze;
    public bool isBoss;

    EnemyMovement _enemyMovement;


    private void Start()
    {       
        if (isTest || isPattern)
        {
            Initialize();
        }

        CoeffSettings();

        if (isBoss)
        {
            hp = maxHp;
        }        
    }

    void CoeffSettings()
    {
        int _currentWave = GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1;
        float _coeff = GameObject.Find("Generate Controller").GetComponent<Generate>().enemyCoeffList[_currentWave].healthCoeff;
        hp *= _coeff;
    }

    public void Initialize()
    {
        _enemyMovement = GetComponent<EnemyMovement>();

        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        _enemyMovement._saveMoveSpeed = moveSpeed;

        if (isPattern)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Add(gameObject);
        }
    }

    private void Update()
    {
        if (transform.position.z < -20 && !isBoss)
        {
            GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Remove(gameObject);
            Destroy(gameObject);
        }

        CheckVisible();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(brakeDamage);
        }

        if (other.tag == "enemy" && transform.position.z > 75)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
    }

    #region Hit
    public void Hit(float _damage, bool _isKrit)
    {
        hp -= _damage;
        GetComponentInChildren<EnemyUI>().ViewDamage((int)_damage, _isKrit);
        StartCoroutine(HitAnim());

        if (hp <= 0)
        {
            Dead();
        }
    }

    IEnumerator HitAnim()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.DisableKeyword("_EMISSION");
    }
    #endregion

    void Dead()
    {
        if (!isBoss)
        {
            GameObject.Find("GameplayController").GetComponent<PlayerLevelController>().enemyCountInThisLevel++;
            GameObject.Find("Player").GetComponent<PlayerPassiveController>().PassiveHealthRecovery();

            Instantiate(screwObj, transform.position, transform.rotation);
            GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Remove(gameObject);
            GameObject _fx = Instantiate(fxExplosion, transform.position, transform.rotation);
            Destroy(_fx, 3);
            Destroy(gameObject);
        }
    }

    #region Freeze
    public void Freeze(float _freezeTime)
    {
        if (!_isFreeze)
        {
            StartCoroutine(FreezeEnum(_freezeTime));
            _isFreeze = true;
        }
    }

    IEnumerator FreezeEnum(float _freezeTime)
    {
        float _saveMoveSpeed = moveSpeed;
        float _saveLocalMoveSpeed = _enemyMovement.localMoveSpeed;
        moveSpeed = 0;
        _enemyMovement.localMoveSpeed = 0;
        yield return new WaitForSeconds(_freezeTime);
        _isFreeze = false;
        moveSpeed = _saveMoveSpeed;
        _enemyMovement.localMoveSpeed = _saveLocalMoveSpeed;
    }
    #endregion

    public void DestroyAfk()
    {
        _timerAfk = 5;
    }

    void CheckVisible()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            isVisible = true;
        }
        else
        {
            if (isVisible)
            {
                isVisible = false;
            }
        }
    }
}
