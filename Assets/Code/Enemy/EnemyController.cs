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
    //public float damage;
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


    private void Start()
    {
        if (isTest || isPattern)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);

        if (isPattern)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Add(gameObject);
        }
    }

    private void Update()
    {
        if (transform.position.z < -40)
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
    }

    #region Hit
    public void Hit(float _damage)
    {
        hp -= _damage;
        GetComponentInChildren<EnemyUI>().ViewDamage((int)_damage);
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
        GameObject.Find("GameplayController").GetComponent<PlayerLevelController>().enemyCountInThisLevel++;

        Instantiate(screwObj, transform.position, transform.rotation);
        GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Remove(gameObject);
        GameObject _fx = Instantiate(fxExplosion, transform.position, transform.rotation);
        Destroy(_fx, 3);
        Destroy(gameObject);
    }

    #region Freeze
    public void Freeze(float _freezeTime)
    {
        StartCoroutine(FreezeEnum(_freezeTime));
    }

    IEnumerator FreezeEnum(float _freezeTime)
    {
        float _saveMoveSpeed = moveSpeed;
        moveSpeed = 0;
        yield return new WaitForSeconds(_freezeTime);
        moveSpeed = _saveMoveSpeed;
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
