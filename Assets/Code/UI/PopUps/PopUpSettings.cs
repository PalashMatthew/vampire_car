using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
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
        } 
        else
        {
            imgSoundOn.SetActive(false);
            imgSoundOff.SetActive(true);

            imgSoundToggleOn.SetActive(false);
            imgSoundToggleOff.SetActive(true);
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
        }
        else
        {
            imgMusicOn.SetActive(false);
            imgMusicOff.SetActive(true);

            imgMusicToggleOn.SetActive(false);
            imgMusicToggleOff.SetActive(true);
        }
    }
    #endregion

    void Initialize()
    {
        if (!PlayerPrefs.HasKey("soundSettings"))
            PlayerPrefs.SetInt("soundSettings", 1);

        if (!PlayerPrefs.HasKey("musicSettings"))
            PlayerPrefs.SetInt("musicSettings", 1);

        if (PlayerPrefs.GetInt("soundSettings") == 1)
            _isSoundOn = true;
        else _isSoundOn = false;

        if (PlayerPrefs.GetInt("musicSettings") == 1)
            _isMusicOn = true;
        else _isMusicOn = false;
    }

    public void ButOpen()
    {
        _popUpController.OpenPopUp();

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
}
