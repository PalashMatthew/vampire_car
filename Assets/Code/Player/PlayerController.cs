using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public float currentHp;
    public float maxHp;

    public List<MeshRenderer> meshRenderer;

    PlayerUIController _playerUIController;

    [Header("Gun")]
    public bool _isDefaultGun;
    public bool _isRocketLauncher;
    public bool _isLightning;
    public bool _isOil;
    public bool _isBoomerang;
    public bool _isPartner;
    public bool _isDron;
    public bool _isIce;
    public bool _isLazer;

    [Header("Gun Object")]
    public GameObject DefaultGunObj;
    public GameObject RocketLauncherObj;
    public GameObject LightningObj;
    public GameObject OilObj;
    public GameObject BoomerangObj;
    public GameObject PartnerObj;
    public GameObject DronObj;
    public GameObject IceObj;
    public GameObject LazerObj;

    public bool isDead;

    [Header("Level Up")]
    public ParticleSystem fxLevelUp;
    public TMP_Text tLevelUp;    


    public void Initialize()
    {
        currentHp = maxHp;
        _playerUIController = GetComponentInChildren<PlayerUIController>();
        isDead = false;
        tLevelUp.gameObject.SetActive(false);

        GunActivate();
    }

    public void Hit(float _damage)
    {
        currentHp -= _damage;

        _playerUIController.UpdateHP();

        StartCoroutine(HitAnim());

        if (currentHp <= 0 && !isDead)
        {
            //Смерть
            GameObject.Find("PopUp Dead").GetComponent<PopUpDead>().ButOpen();
            GameObject.Find("PopUp Dead").GetComponent<PopUpDead>().StartCoroutine(GameObject.Find("PopUp Dead").GetComponent<PopUpDead>().SkipTimer());
            isDead = true;
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

    void GunActivate()
    {
        if (_isDefaultGun)
            DefaultGunObj.SetActive(true);

        if (_isRocketLauncher)
            RocketLauncherObj.SetActive(true);

        if (_isLightning)
            LightningObj.SetActive(true);

        if (_isOil)
            OilObj.SetActive(true);

        if (_isBoomerang)
            BoomerangObj.SetActive(true);

        if (_isPartner)
            PartnerObj.SetActive(true);

        if (_isDron)
            DronObj.SetActive(true);

        if (_isIce)
            IceObj.SetActive(true);

        if (_isLazer)
            LazerObj.SetActive(true);
    }

    public void LevelUp()
    {
        fxLevelUp.Play();

        StartCoroutine(LevelUpTextAnimation());
    }    

    IEnumerator LevelUpTextAnimation()
    {
        tLevelUp.gameObject.SetActive(true);
        tLevelUp.transform.DOScale(0, 0);

        tLevelUp.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(2);

        tLevelUp.transform.DOScale(0, 0.3f).SetEase(Ease.InBack);

        yield return new WaitForSeconds(0.3f);
        tLevelUp.gameObject.SetActive(false);
    }
}
