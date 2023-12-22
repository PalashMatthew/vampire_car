using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public enum CarType
    {
        Movement,
        Static
    }

    public CarType carType;

    public bool isCarStop;

    public string enemyKind;

    [HideInInspector]
    public float moveSpeed;
    public float hp;
    public float maxHp;
    public float brakeDamage;

    public float moveSpeedMin;
    public float moveSpeedMax;

    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

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

    public List<Material> materialsCar;

    bool isWeakening;

    [Header("HP Progress Bar")]
    public Image imgHpProgressBar;

    public bool isSpeedUp;
    public float newSpeed;

    [Header("Audio")]
    public AudioClip clipHit;
    public AudioClip clipDeath;

    public GameObject roadPit;


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
        } else
        {
            maxHp = hp;
        }

        isWeakening = false;

        //if (materialsCar.Count > 0)
        //meshRenderer.material = materialsCar[Random.Range(0, materialsCar.Count)];

        //if (materialsCar)
        //meshRenderer.material = materialsCar[0];
    }

    void CoeffSettings()
    {
        int _currentWave = GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1;
        float _coeff = GameObject.Find("Generate Controller").GetComponent<Generate>().enemyCoeffList[_currentWave].healthCoeff;
        hp *= _coeff;
    }

    public void Initialize()
    {        
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        moveSpeed *= GameObject.Find("Generate Controller").GetComponent<Generate>().moveSpeedCoeff;

        if (!isBoss)
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyMovement._saveMoveSpeed = moveSpeed;
        }        

        if (isPattern && !_enemyMovement.isStartRotate)
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

        if (!isBoss && imgHpProgressBar != null)
        {
            if (hp == maxHp)
            {
                imgHpProgressBar.gameObject.SetActive(false);
            }
            else
            {
                imgHpProgressBar.gameObject.SetActive(true);
                imgHpProgressBar.fillAmount = hp / maxHp;
            }
        }

        if (isSpeedUp)
        {
            if (moveSpeed < newSpeed)
            {
                moveSpeed += Time.deltaTime * 2;
            }

            if (moveSpeed > newSpeed)
            {
                moveSpeed = newSpeed;
                isSpeedUp = false;
            }
        }

        CheckVisible();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s01")  //Если у нас сет Таран активен
            {
                int rand = Random.Range(1, 101);

                if (rand <= PlayerPrefs.GetFloat("setValue"))
                {
                    return;
                } 
                else
                {
                    other.gameObject.GetComponent<PlayerController>().isBrakeDamage = true;
                    other.gameObject.GetComponent<PlayerController>().Hit(brakeDamage);
                    BackDamage(brakeDamage);
                }
            } 
            else
            {
                other.gameObject.GetComponent<PlayerController>().isBrakeDamage = true;
                other.gameObject.GetComponent<PlayerController>().Hit(brakeDamage);
                BackDamage(brakeDamage);
            }
        }

        if (other.tag == "enemy" && transform.position.z > 75)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
    }

    public void BackDamage(float damage)
    {
        if (GameObject.Find("Player").GetComponent<PlayerPassiveController>().isBackDamage)
        {
            Hit(damage / 100 * GameObject.Find("Player").GetComponent<PlayerStats>().backDamageProcent, false);
        }
    }

    #region Hit
    public void Hit(float _damage, bool _isKrit)
    {
        if (isVisible)
        {
            SoundController _soundController = GameObject.Find("SoundsController").GetComponent<SoundController>();

            if (_soundController != null)
            {
                _soundController.PlaySound(clipHit);
            }

            if (isWeakening)
            {
                _damage *= 2;
                isWeakening = false;
            }

            hp -= _damage;
            GetComponentInChildren<EnemyUI>().ViewDamage((int)_damage, _isKrit);
            StartCoroutine(HitAnim());

            if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s09")  //Если у нас сет Таран активен
            {
                int rand = Random.Range(1, 101);

                if (rand <= PlayerPrefs.GetFloat("setValue"))
                {
                    isWeakening = true;
                }
            }

            if (GameObject.Find("Player").GetComponent<PlayerPassiveController>().isHeadshot)
            {
                int rand = Random.Range(1, 101);

                if (rand <= GameObject.Find("Player").GetComponent<PlayerStats>().headshotProcent)
                {
                    Dead();
                    return;
                }
            }

            if (hp <= 0)
            {
                Dead();
            }
        }
    }

    public void Headshot()
    {
        GetComponentInChildren<EnemyUI>().ViewHeadshot();
        StartCoroutine(HitAnim());
        Dead();
    }

    IEnumerator HitAnim()
    {
        if (meshRenderer != null)
            meshRenderer.material.EnableKeyword("_EMISSION");

        if (skinnedMeshRenderer != null)
            skinnedMeshRenderer.material.EnableKeyword("_EMISSION");

        yield return new WaitForSeconds(0.1f);

        if (meshRenderer != null)
            meshRenderer.material.DisableKeyword("_EMISSION");

        if (skinnedMeshRenderer != null)
            skinnedMeshRenderer.material.DisableKeyword("_EMISSION");
    }
    #endregion

    public void Dead()
    {
        if (!isBoss)
        {
            if (carType == CarType.Static)
            {
                GameObject.Find("Generate Controller").GetComponent<Generate>().dronCountInScreen--;
            }

            float exp = 1 * GameObject.Find("GameplayController").GetComponent<WaveController>().waveList[GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1].expCoeff;
            GameObject.Find("Player").GetComponent<PlayerStats>().currentExp += exp;
            GameObject.Find("Player").GetComponent<PlayerStats>().CheckLevel();

            GameObject.Find("GameplayController").GetComponent<GameplayController>().activeEnemy.Remove(gameObject);
            GameObject _fx = Instantiate(fxExplosion, transform.position, transform.rotation);
            Destroy(_fx, 3);
            Destroy(gameObject);

            SoundController _soundController = GameObject.Find("SoundsController").GetComponent<SoundController>();

            if (_soundController != null)
            {
                _soundController.PlaySound(clipDeath);
            }

            //if (roadPit != null)
            //{
            //    Instantiate(roadPit, transform.position, transform.rotation);
            //}            
        }
    }

    public void DeleteEnemy()
    {
        GameObject _fx = Instantiate(fxExplosion, transform.position, transform.rotation);
        Destroy(_fx, 3);
        Destroy(gameObject);
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
