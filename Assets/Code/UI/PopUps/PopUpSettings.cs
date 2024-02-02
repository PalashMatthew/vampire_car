using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Firebase.Analytics;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PopUpSettings : MonoBehaviour
{
    private PopUpController _popUpController;

    [Header("Sound")]
    public GameObject imgSoundOn;
    public GameObject imgSoundOff;
    public GameObject imgSoundToggleOn;
    public GameObject imgSoundToggleOff;
    private bool _isSoundOn = true;

    [Header("Sound")]
    public GameObject imgMusicOn;
    public GameObject imgMusicOff;
    public GameObject imgMusicToggleOn;
    public GameObject imgMusicToggleOff;
    private bool _isMusicOn = true;

    [Header("Localization")]
    public GameObject toggleRu;
    public GameObject toggleEn;

    public static Action onLocalization;

    public TMP_Text tUserID;

    [Header("Delete Account")]
    public PopUpController _popUpDeleteAccount;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
        tUserID.text = "UserID - " + PlayerPrefs.GetString("userID");
    }

    #region Sound and Music
    public void ButSoundSettings()
    {
        _isSoundOn = !_isSoundOn;

        if (_isSoundOn)
        {
            imgSoundOn.SetActive(true);
            imgSoundOff.SetActive(false);

            imgSoundToggleOn.SetActive(true);
            imgSoundToggleOff.SetActive(false);

            PlayerPrefs.SetInt("soundSettings", 1);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Sound("On");
        } 
        else
        {
            imgSoundOn.SetActive(false);
            imgSoundOff.SetActive(true);

            imgSoundToggleOn.SetActive(false);
            imgSoundToggleOff.SetActive(true);

            PlayerPrefs.SetInt("soundSettings", 0);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Sound("Off");
        }
    }

    public void ButMusicSettings()
    {
        _isMusicOn = !_isMusicOn;

        if (_isMusicOn)
        {
            imgMusicOn.SetActive(true);
            imgMusicOff.SetActive(false);

            imgMusicToggleOn.SetActive(true);
            imgMusicToggleOff.SetActive(false);

            PlayerPrefs.SetInt("musicSettings", 1);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Music("On");
        }
        else
        {
            imgMusicOn.SetActive(false);
            imgMusicOff.SetActive(true);

            imgMusicToggleOn.SetActive(false);
            imgMusicToggleOff.SetActive(true);

            PlayerPrefs.SetInt("musicSettings", 0);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Music("Off");
        }
    }
    #endregion

    void Initialize()
    {       
        if (PlayerPrefs.GetInt("soundSettings") == 1)
            _isSoundOn = true;
        else _isSoundOn = false;

        if (PlayerPrefs.GetInt("musicSettings") == 1)
            _isMusicOn = true;
        else _isMusicOn = false;

        if (PlayerPrefs.GetString("activeLang") == "en")
        {
            toggleEn.SetActive(true);
            toggleRu.SetActive(false);
        } 
        else
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(true);
        }
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("Settings");

        Initialize();
    }

    public void ButClosed()
    {
        if (_isSoundOn)
            PlayerPrefs.SetInt("soundSettings", 1);
        else
            PlayerPrefs.SetInt("soundSettings", 2);

        if (_isMusicOn)
            PlayerPrefs.SetInt("musicSettings", 1);
        else
            PlayerPrefs.SetInt("musicSettings", 2);

        _popUpController.ClosedPopUp();
    }

    public void ButLocalization(string _lang)
    {
        PlayerPrefs.SetString("activeLang", _lang);
        FirebaseAnalytics.SetUserProperty("language", PlayerPrefs.GetString("activeLang"));
        onLocalization?.Invoke();

        if (PlayerPrefs.GetString("activeLang") == "en")
        {
            toggleEn.SetActive(true);
            toggleRu.SetActive(false);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("EN");
        }
        else
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(true);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("RU");
        }
    }

    public void ButOpenPopUpDeleteAccount()
    {
        _popUpDeleteAccount.OpenPopUp();
    }

    public void ButClosedPopUpDeleteAccount()
    {
        _popUpDeleteAccount.ClosedPopUp();
    }

    public async void ButDeleteAccount()
    {
        Destroy(GameObject.Find("MusicController"));

        await UnityServices.InitializeAsync();

        await AuthenticationService.Instance.DeleteAccountAsync();

        PlayerPrefs.DeleteAll();

        Application.LoadLevel("InitScene");
    }

    public void TermOfUse()
    {
        Application.OpenURL("https://docs.google.com/document/d/1hIP0K0tIf-ZTM3jAEeXYW5ttRWKtSZ8hAlRqvSr_vlU/edit?usp=sharing");
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://docs.google.com/document/d/1laNGSxcNIK3RrWfe8c7DAWKFUGrqSjK6LH7QQh9SJsQ/edit?usp=sharing");
    }

    public void ButLoginGooglePlayGames()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_LoginGooglePlay();

        LinkWithGooglePlayGamesAsync(PlayerPrefs.GetString("userID"));
    }

    async Task LinkWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.LinkWithGooglePlayGamesAsync(authCode);
            Debug.Log("Link is successful.");
        }
        catch (AuthenticationException ex) when (ex.ErrorCode == AuthenticationErrorCodes.AccountAlreadyLinked)
        {
            // Prompt the player with an error message.
            Debug.LogError("This user is already linked with another account. Log in instead.");
        }

        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
}
