using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    public List<Gun> guns;

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


    private void Start()
    {
        GunActivate();
    }

    public void GunDamageUpgrade(float _procent)
    {
        foreach (Gun gun in guns)
        {
            gun.damage = gun.damage / 100 * _procent;
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
}
