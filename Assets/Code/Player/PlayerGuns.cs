using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    public List<Gun> guns;

    [Header("Gun")]
    public bool _isDefaultGun;
    public bool _isRocketLauncher;
    public bool _isBoomerang;
    public bool _isPartner;
    public bool _isDron;
    public bool _isIce;
    public bool _isLazer;
    public bool _isGrowingShot;
    public bool _isFanGun;
    public bool _isTornado;
    public bool _isMines;
    public bool _isGrenade;
    public bool _isGodGun;
    public bool _isRicochet;
    public bool _isBow;
    public bool _isPinPong;

    [Header("Gun Object")]
    public GameObject DefaultGunObj;
    public GameObject RocketLauncherObj;
    public GameObject BoomerangObj;
    public GameObject PartnerObj;
    public GameObject DronObj;
    public GameObject IceObj;
    public GameObject LazerObj;
    public GameObject GrowingShotObj;
    public GameObject FanGunObj;
    public GameObject TornadoObj;
    public GameObject MinesObj;
    public GameObject GrenadeObj;
    public GameObject GodGunObj;
    public GameObject RicochetGunObj;
    public GameObject BowObj;
    public GameObject PinPongObj;


    private void Start()
    {
        GunActivate();
    }

    public void GunActivate()
    {
        if (_isDefaultGun)
            DefaultGunObj.SetActive(true);

        if (_isRocketLauncher)
            RocketLauncherObj.SetActive(true);

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

        if (_isGrowingShot)
            GrowingShotObj.SetActive(true);

        if (_isFanGun)
            FanGunObj.SetActive(true);

        if (_isTornado)
            TornadoObj.SetActive(true);

        if (_isMines)
            MinesObj.SetActive(true);

        if (_isGrenade)
            GrenadeObj.SetActive(true);

        if (_isGodGun)
            GodGunObj.SetActive(true);

        if (_isRicochet)
            RicochetGunObj.SetActive(true);

        if (_isBow)
            BowObj.SetActive(true);

        if (_isPinPong)
            PinPongObj.SetActive(true);
    }
}
