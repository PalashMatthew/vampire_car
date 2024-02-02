using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public List<MeshRenderer> meshRenderer;

    PlayerUIController _playerUIController;
    PlayerStats _playerStats;
    PlayerPassiveController _playerPassiveController;

    public bool isDead;

    public List<GameObject> carsMesh;

    public bool isBrakeDamage;

    public ParticleSystem vfxShield;
    bool isShield = false;

    bool isAvenger = false;
    float avengerTimerCurrent;
    float avengerTimerMax = 3;

    [Header("New Level")]
    public ParticleSystem vfxNewLevel;
    public GameObject canvasNewLevel;

    [Header("First Aid Kit")]
    public ParticleSystem vfxHealing;

    public bool isInvulnerability;

    public AudioSource aud;

    public GameObject hitShieldObj;


    public void Initialize()
    {
        isShield = false;

        _playerUIController = GetComponentInChildren<PlayerUIController>();
        _playerStats = GetComponent<PlayerStats>();
        _playerPassiveController = GetComponent<PlayerPassiveController>();

        isDead = false;

        ChoiseCar();

        if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s06")  //���� � ��� ��� ���� �������
        {
            StartCoroutine(ShieldTimer());
        }

        if (PlayerPrefs.GetInt("musicSettings") == 1)
        {
            aud.Play();
        }
    }

    private void Update()
    {
        if (isAvenger)
        {
            avengerTimerCurrent -= Time.deltaTime;

            if (avengerTimerCurrent <= 0)
            {
                isAvenger = false;
            }
        }
    }

    void ChoiseCar()
    {
        foreach (GameObject gm in carsMesh)
        {
            if (gm.GetComponent<CarPlayerMesh>().carName == PlayerPrefs.GetString("selectedCarID"))
            {
                gm.GetComponent<CarPlayerMesh>().CarChoise();
            } 
            else
            {
                gm.GetComponent<CarPlayerMesh>().mesh.SetActive(false);
            }
        }
    }

    public IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("setValue"));
        isShield = true;
        vfxShield.gameObject.SetActive(true);
        vfxShield.Play();
    }

    public void Hit(float _damage)
    {
        if (!isInvulnerability)
        {
            float _finalDamage;
            _finalDamage = _damage - (_damage / 100 * _playerStats.armorProcent);

            if (!isBrakeDamage)
            {
                _finalDamage = _finalDamage - _playerStats.block;

                if (_finalDamage < 0) _finalDamage = 0;
            }
            else
            {
                _finalDamage = _finalDamage - _playerStats.iron;

                if (_finalDamage < 0) _finalDamage = 0;
            }

            if (_playerPassiveController.isDodge)
            {
                int rand = Random.Range(1, 101);
                if (rand > _playerStats.dodgeProcent)
                {
                    if (!isShield)
                    {
                        _playerStats.currentHp -= _finalDamage;

                        StopAllCoroutines();

                        StartCoroutine(GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().PlayerHit());

                        StartCoroutine(HitAnim());

                        if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s06")  //���� � ��� ��� ���� �������
                        {
                            isShield = false;
                            vfxShield.Stop();
                            vfxShield.gameObject.SetActive(false);
                            StartCoroutine(ShieldTimer());
                        }

                        if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s07")  //���� � ��� ��� ����� �������
                        {
                            isAvenger = true;
                            avengerTimerCurrent = avengerTimerMax;
                        }
                    }
                    else
                    {
                        isShield = false;
                        vfxShield.Stop();
                        vfxShield.gameObject.SetActive(false);

                        if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s06")  //���� � ��� ��� ���� �������
                        {
                            StartCoroutine(ShieldTimer());
                        }
                    }
                }
            }
            else
            {
                if (!isShield)
                {
                    _playerStats.currentHp -= _finalDamage;

                    StopAllCoroutines();

                    StartCoroutine(GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().PlayerHit());

                    StartCoroutine(HitAnim());

                    if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s06")  //���� � ��� ��� ����� �������
                    {
                        isShield = false;
                        vfxShield.Stop();
                        vfxShield.gameObject.SetActive(false);
                        StartCoroutine(ShieldTimer());
                    }

                    if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s07")  //���� � ��� ��� ����� �������
                    {
                        isAvenger = true;
                        avengerTimerCurrent = avengerTimerMax;
                    }
                }
                else
                {
                    isShield = false;
                    vfxShield.Stop();
                    vfxShield.gameObject.SetActive(false);

                    if (PlayerPrefs.GetInt("setActive") == 1 && PlayerPrefs.GetString("setActiveID") == "s06")  //���� � ��� ��� ���� �������
                    {
                        StartCoroutine(ShieldTimer());
                    }
                }
            }

            if (_playerStats.currentHp < 0) _playerStats.currentHp = 0;

            isBrakeDamage = false;

            if (_playerStats.currentHp <= 0 && !isDead)
            {
                //������
                if (!GameObject.Find("PopUp Recovery").GetComponent<PopUpRecovery>().isRecovery)
                {
                    GameObject.Find("PopUp Recovery").GetComponent<PopUpRecovery>().ButOpen();
                }
                else
                {
                    GameObject.Find("GameplayController").GetComponent<WaveController>().StopGame();
                    GameObject.Find("PopUp Win").GetComponent<PopUpWin>().ButOpen();
                }

                isDead = true;
            }

            StartCoroutine(Invulnerability(1));
        }
    }

    IEnumerator HitAnim()
    {
        foreach (MeshRenderer _mesh in meshRenderer)
        {
            _mesh.material.EnableKeyword("_EMISSION");
        }
        
        yield return new WaitForSeconds(0.1f);

        foreach (MeshRenderer _mesh in meshRenderer)
        {
            _mesh.material.DisableKeyword("_EMISSION");
        }
    }    

    public IEnumerator Invulnerability(float _time)
    {
        isInvulnerability = true;
        //hitShieldObj.SetActive(true);
        yield return new WaitForSeconds(_time);
        isInvulnerability = false;
        //hitShieldObj.SetActive(false);
    }

    public IEnumerator NewLevel()
    {
        vfxNewLevel.Play();
        canvasNewLevel.SetActive(true);

        yield return new WaitForSeconds(2);

        canvasNewLevel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "firstAidKit")
        {            
            FirstAidKit(other.gameObject.GetComponent<FirstAidKitController>().value);

            string _hp = "";

            if (_playerStats.currentHp < _playerStats.maxHp)
            {
                _hp = "NotFullHP";
            } 
            else
            {
                _hp = "FullHP";
            }

            if (GameObject.Find("Firebase") != null)
                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_TakeFirstAidKit(GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1, Application.loadedLevelName, _hp);

            if (PlayerPrefs.GetString("tutorialLoc1Complite") == "false" && Application.loadedLevelName == "Loc alpha 1")
            {
                GameObject.Find("TutorialController").GetComponent<TutorialController>().StartCoroutine(GameObject.Find("TutorialController").GetComponent<TutorialController>().ShowMessage4());
                PlayerPrefs.SetString("tutorialLoc1Complite", "true");
            }

            Destroy(other.gameObject);
        }
    }

    void FirstAidKit(float value)
    {
        float _value = value + PlayerPrefs.GetInt("talentRecoveryHpInFirstAidKitCurrentValue") +
                               PlayerPrefs.GetFloat("GunSelectRecoveryHpInFirstAidKit") +
                               PlayerPrefs.GetFloat("EngineSelectRecoveryHpInFirstAidKit") +
                               PlayerPrefs.GetFloat("BrakesSelectRecoveryHpInFirstAidKit") +
                               PlayerPrefs.GetFloat("FuelSystemSelectRecoveryHpInFirstAidKit") +
                               PlayerPrefs.GetFloat("SuspensionSelectRecoveryHpInFirstAidKit") +
                               PlayerPrefs.GetFloat("TransmissionSelectRecoveryHpInFirstAidKit");
        float regen = _playerStats.maxHpBase / 100 * _value;
        _playerStats.currentHp += regen;

        vfxHealing.Play();
    }
}
