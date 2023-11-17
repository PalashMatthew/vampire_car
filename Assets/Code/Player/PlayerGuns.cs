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

    public static int gunCount;


    private void Start()
    {
        GunInitialize();
        GunActivate();
    }

    void GunInitialize()
    {
        if (PlayerPrefs.GetInt("GunIDSelect") != 0)
        {
            switch (PlayerPrefs.GetInt("GunIDSelect"))
            {
                case 1001:
                    _isRocketLauncher = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().RocketLauncher_Level = 1;
                    break;

                case 1002:
                    _isPartner = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Partner_Level = 1;
                    break;

                case 1003:
                    _isLazer = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Lazer_Level = 1;
                    break;

                case 1004:
                    _isBoomerang = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Boomerang_Level = 1;
                    break;

                case 1005:
                    _isDron = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Dron_Level = 1;
                    break;

                case 1006:
                    _isIce = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Ice_Level = 1;
                    break;

                case 1007:
                    _isGrenade = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Grenade_Level = 1;
                    break;

                case 1008:
                    _isRicochet = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Ricochet_Level = 1;
                    break;

                case 1009:
                    _isGodGun = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().GodGun_Level = 1;
                    break;

                case 1010:
                    _isPinPong = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().PinPong_Level = 1;
                    break;

                case 1011:
                    _isMines = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Mines_Level = 1;
                    break;

                case 1012:
                    _isBow = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Bow_Level = 1;
                    break;

                case 1013:
                    _isTornado = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().Tornado_Level = 1;
                    break;

                case 1014:
                    _isFanGun = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().FanGun_Level = 1;
                    break;

                case 1015:
                    _isGrowingShot = true;
                    GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>().GrowingShot_Level = 1;
                    break;
            }
        }
    }

    public void GunActivate()
    {
        gunCount = 0;

        if (_isDefaultGun)
        {
            DefaultGunObj.SetActive(true);
            gunCount++;
        }            

        if (_isRocketLauncher)
        {
            RocketLauncherObj.SetActive(true);
            gunCount++;
        }   

        if (_isBoomerang)
        {
            BoomerangObj.SetActive(true);
            gunCount++;
        }            

        if (_isPartner)
        {
            PartnerObj.SetActive(true);
            gunCount++;
        }            

        if (_isDron)
        {
            DronObj.SetActive(true);
            gunCount++;
        }            

        if (_isIce)
        {
            IceObj.SetActive(true);
            gunCount++;
        }            

        if (_isLazer)
        {
            LazerObj.SetActive(true);
            gunCount++;
        }            

        if (_isGrowingShot)
        {
            GrowingShotObj.SetActive(true);
            gunCount++;
        }            

        if (_isFanGun)
        {
            FanGunObj.SetActive(true);
            gunCount++;
        }            

        if (_isTornado)
        {
            TornadoObj.SetActive(true);
            gunCount++;
        }            

        if (_isMines)
        {
            MinesObj.SetActive(true);
            gunCount++;
        }            

        if (_isGrenade)
        {
            GrenadeObj.SetActive(true);
            gunCount++;
        }            

        if (_isGodGun)
        {
            GodGunObj.SetActive(true);
            gunCount++;
        }            

        if (_isRicochet)
        {
            RicochetGunObj.SetActive(true);
            gunCount++;
        }            

        if (_isBow)
        {
            BowObj.SetActive(true);
            gunCount++;
        }            

        if (_isPinPong)
        {
            PinPongObj.SetActive(true);
            gunCount++;
        }            
    }
}
