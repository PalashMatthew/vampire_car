using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public int locationNum;

    public static bool isPause;

    WaveController waveController;
    PlayerGuns playerGuns;
    PlayerPassiveController playerPassiveController;
    UpgradeController upgradeController;
    public List<UpgradeCard> upgradeCardsMass;

    public enum InputSettings
    {
        Joy,
        FingerTracking,
        RelativeToTheFinger
    }

    public InputSettings inputSettings;

    [Header("Enemy")]
    public List<GameObject> activeEnemy;
    public static bool isBossActive;

    [Header("Initialize")]
    public PlayerUIController _playerUIController;
    public PlayerController _playerController;

    [Header("Wave")]
    public int currentWaveNum;

    private void Start()
    {
        InitializeGame();
        isBossActive = false;
    }

    void InitializeGame()
    {
        waveController = GetComponent<WaveController>();
        _playerController.Initialize();

        currentWaveNum = 1;

        playerGuns = GameObject.Find("Player").GetComponent<PlayerGuns>();
        playerPassiveController = GameObject.Find("Player").GetComponent<PlayerPassiveController>();
        upgradeController = GameObject.Find("Upgrade Controller").GetComponent<UpgradeController>();

        if (PlayerPrefs.GetInt("locationContinue") == 1)
        {           
            currentWaveNum = PlayerPrefs.GetInt("saveTryCurrentWaveNum");
            waveController.currentWave = currentWaveNum - 1;

            if (currentWaveNum >= 10)
            {
                waveController.StartBossWave();
            }
            else
            {
                waveController.StartWave();
            }            

            if (PlayerPrefs.GetString("saveTryRecovery") == "True")
                GameObject.Find("PopUp Recovery").GetComponent<PopUpRecovery>().isRecovery = true;

            if (GameObject.Find("Firebase") != null)
                GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_GameContinueStatus(Application.loadedLevelName, currentWaveNum);

            #region PlayerGuns
            PlayerGuns.gunCount = PlayerPrefs.GetInt("saveTryGunCount");

            if (PlayerPrefs.GetString("saveTryRocketLauncher") == "True")
            {
                playerGuns._isRocketLauncher = true;
            }

            if (PlayerPrefs.GetString("saveTryBoomerang") == "True")
            {
                playerGuns._isBoomerang = true;
            }

            if (PlayerPrefs.GetString("saveTryPartner") == "True")
            {
                playerGuns._isPartner = true;
            }

            if (PlayerPrefs.GetString("saveTryDron") == "True")
            {
                playerGuns._isDron = true;
            }

            if (PlayerPrefs.GetString("saveTryIce") == "True")
            {
                playerGuns._isIce = true;
            }

            if (PlayerPrefs.GetString("saveTryLazer") == "True")
            {
                playerGuns._isLazer = true;
            }

            if (PlayerPrefs.GetString("saveTryGrowingShot") == "True")
            {
                playerGuns._isGrowingShot = true;
            }

            if (PlayerPrefs.GetString("saveTryFanGun") == "True")
            {
                playerGuns._isFanGun = true;
            }

            if (PlayerPrefs.GetString("saveTryTornado") == "True")
            {
                playerGuns._isTornado = true;
            }

            if (PlayerPrefs.GetString("saveTryMines") == "True")
            {
                playerGuns._isMines = true;
            }

            if (PlayerPrefs.GetString("saveTryGrenade") == "True")
            {
                playerGuns._isGrenade = true;
            }

            if (PlayerPrefs.GetString("saveTryGodGun") == "True")
            {
                playerGuns._isGodGun = true;
            }

            if (PlayerPrefs.GetString("saveTryRicochet") == "True")
            {
                playerGuns._isRicochet = true;
            }

            if (PlayerPrefs.GetString("saveTryBow") == "True")
            {
                playerGuns._isBow = true;
            }

            if (PlayerPrefs.GetString("saveTryPinPong") == "True")
            {
                playerGuns._isPinPong = true;
            }

            playerGuns.GunActivate();
            #endregion

            #region PlayerPassive
            if (PlayerPrefs.GetString("saveTryIsVampirizm") == "True")
            {
                playerPassiveController.isVampirizm = true;
            }

            if (PlayerPrefs.GetString("saveTryIsPassiveRage") == "True")
            {
                playerPassiveController.isPassiveRage = true;
            }

            if (PlayerPrefs.GetString("saveTryIsBackDamage") == "True")
            {
                playerPassiveController.isBackDamage = true;
            }

            if (PlayerPrefs.GetString("saveTryIsDodge") == "True")
            {
                playerPassiveController.isDodge = true;
            }

            if (PlayerPrefs.GetString("saveTryIsPunching") == "True")
            {
                playerPassiveController.isPunching = true;
            }

            if (PlayerPrefs.GetString("saveTryIsHeadshot") == "True")
            {
                playerPassiveController.isHeadshot = true;
            }

            if (PlayerPrefs.GetString("saveTryIsScrewValueUp") == "True")
            {
                playerPassiveController.isScrewValueUp = true;
            }

            if (PlayerPrefs.GetString("saveTryIsDistanceDamage") == "True")
            {
                playerPassiveController.isDistanceDamage = true;
            }

            if (PlayerPrefs.GetString("saveTryIsPassiveHealthRecovery") == "True")
            {
                playerPassiveController.isPassiveHealthRecovery = true;
            }

            playerPassiveController.healthRecoveryProcent = PlayerPrefs.GetFloat("saveTryHealthRecoveryProcent");
            #endregion

            #region Level Cards Guns
            upgradeController.Boomerang_Level = PlayerPrefs.GetInt("saveTryBoomerang_Level");
            upgradeController.Dron_Level = PlayerPrefs.GetInt("saveTryDron_Level");
            upgradeController.Ice_Level = PlayerPrefs.GetInt("saveTryIce_Level");
            upgradeController.Lazer_Level = PlayerPrefs.GetInt("saveTryLazer_Level");
            upgradeController.Lightning_Level = PlayerPrefs.GetInt("saveTryLightning_Level");
            upgradeController.Partner_Level = PlayerPrefs.GetInt("saveTryPartner_Level");
            upgradeController.RocketLauncher_Level = PlayerPrefs.GetInt("saveTryRocketLauncher_Level");
            upgradeController.DefaultGun_Level = PlayerPrefs.GetInt("saveTryDefaultGun_Level");
            upgradeController.GrowingShot_Level = PlayerPrefs.GetInt("saveTryGrowingShot_Level");
            upgradeController.FanGun_Level = PlayerPrefs.GetInt("saveTryFanGun_Level");
            upgradeController.Tornado_Level = PlayerPrefs.GetInt("saveTryTornado_Level");
            upgradeController.Mines_Level = PlayerPrefs.GetInt("saveTryMines_Level");
            upgradeController.Grenade_Level = PlayerPrefs.GetInt("saveTryGrenade_Level");
            upgradeController.GodGun_Level = PlayerPrefs.GetInt("saveTryGodGun_Level");
            upgradeController.RocketLauncher_Level = PlayerPrefs.GetInt("saveTryRicochet_Level");
            upgradeController.Bow_Level = PlayerPrefs.GetInt("saveTryBow_Level");
            upgradeController.PinPong_Level = PlayerPrefs.GetInt("saveTryPinPong_Level");
            #endregion

            #region PassiveCardInPause
            if (PlayerPrefs.GetInt("saveTryPassiveCount") > 0)
            {
                for (int i = 0; i < PlayerPrefs.GetInt("saveTryPassiveCount"); i++)
                {
                    Sprite spr = null;

                    switch (PlayerPrefs.GetString("saveTryPassiveCard" + i))
                    {
                        case "Armor":
                            spr = upgradeCardsMass[0].imageItem;
                            break;

                        case "AttackSpeed":
                            spr = upgradeCardsMass[1].imageItem;
                            break;

                        case "BackDamage":
                            spr = upgradeCardsMass[2].imageItem;
                            break;

                        case "DamageUp":
                            spr = upgradeCardsMass[3].imageItem;
                            break;

                        case "DistanceDamage":
                            spr = upgradeCardsMass[4].imageItem;
                            break;

                        case "Dodge":
                            spr = upgradeCardsMass[5].imageItem;
                            break;

                        case "EffectsDuration":
                            spr = upgradeCardsMass[6].imageItem;
                            break;

                        case "Headshot":
                            spr = upgradeCardsMass[7].imageItem;
                            break;

                        case "HealthRecovery":
                            spr = upgradeCardsMass[8].imageItem;
                            break;

                        case "KritChanceUp":
                            spr = upgradeCardsMass[9].imageItem;
                            break;

                        case "KritDamageUp":
                            spr = upgradeCardsMass[10].imageItem;
                            break;

                        case "Lucky":
                            spr = upgradeCardsMass[11].imageItem;
                            break;

                        case "Magnet":
                            spr = upgradeCardsMass[12].imageItem;
                            break;

                        case "MassEnemyDamage":
                            spr = upgradeCardsMass[13].imageItem;
                            break;

                        case "MaxHpUp":
                            spr = upgradeCardsMass[14].imageItem;
                            break;

                        case "Rage":
                            spr = upgradeCardsMass[15].imageItem;
                            break;

                        case "ScrewValueUp":
                            spr = upgradeCardsMass[16].imageItem;
                            break;

                        case "Vampirizm":
                            spr = upgradeCardsMass[17].imageItem;
                            break;
                    }

                    GameObject.Find("PopUp Pause").GetComponent<PopUpPause>().InstPassive(spr);
                }
            }            
            #endregion

            playerGuns.LoadSaveTrySettings();

            upgradeController.GenerateGunSlotTrySave();

            
        }
        else
        {
            playerGuns.GunInitialize();
            upgradeController.GenerateGunSlotTrySave();
            //PlayerPrefs.SetInt("locationContinue", 1);
        }

        PlayerPrefs.SetInt("locationContinue", 1);
    }

    public void Win()
    {
        GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().isWin = true;
        GameObject.Find("PopUp Win").GetComponent<PopUpWin>().ButOpen();
    }
}
