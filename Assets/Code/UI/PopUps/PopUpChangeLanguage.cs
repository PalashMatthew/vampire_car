using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpChangeLanguage : MonoBehaviour
{
    private PopUpController _popUpController;

    [Header("Localization")]
    public GameObject toggleRu;
    public GameObject toggleEn;
    public GameObject togglePt;
    public GameObject toggleKo;
    public GameObject toggleEs;
    public GameObject toggleFr;


    public void ButOpen()
    {
        _popUpController.OpenPopUp();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("ChangeLanguage");

        Initialize();
    }

    void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {
        if (PlayerPrefs.GetString("activeLang") == "en")
        {
            toggleEn.SetActive(true);
            toggleRu.SetActive(false);
            togglePt.SetActive(false);
            toggleKo.SetActive(false);
            toggleEs.SetActive(false);
            toggleFr.SetActive(false);
        }
        else if (PlayerPrefs.GetString("activeLang") == "ru")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(true);
            togglePt.SetActive(false);
            toggleKo.SetActive(false);
            toggleEs.SetActive(false);
            toggleFr.SetActive(false);
        }
        else if (PlayerPrefs.GetString("activeLang") == "pt")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(false);
            togglePt.SetActive(true);
            toggleKo.SetActive(false);
            toggleEs.SetActive(false);
            toggleFr.SetActive(false);
        }
        else if (PlayerPrefs.GetString("activeLang") == "ko")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(false);
            togglePt.SetActive(false);
            toggleKo.SetActive(true);
            toggleEs.SetActive(false);
            toggleFr.SetActive(false);
        }
        else if (PlayerPrefs.GetString("activeLang") == "es")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(false);
            togglePt.SetActive(false);
            toggleKo.SetActive(false);
            toggleEs.SetActive(true);
            toggleFr.SetActive(false);
        }
        else if (PlayerPrefs.GetString("activeLang") == "fr")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(false);
            togglePt.SetActive(false);
            toggleKo.SetActive(false);
            toggleEs.SetActive(false);
            toggleFr.SetActive(true);
        }
    }

    public void ButLocalization(string _lang)
    {
        PlayerPrefs.SetString("activeLang", _lang);
        FirebaseAnalytics.SetUserProperty("language", PlayerPrefs.GetString("activeLang"));
        PopUpSettings.onLocalization?.Invoke();

        if (PlayerPrefs.GetString("activeLang") == "en")
        {
            toggleEn.SetActive(true);
            toggleRu.SetActive(false);
            togglePt.SetActive(false);
            toggleKo.SetActive(false);
            toggleEs.SetActive(false);
            toggleFr.SetActive(false);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("EN");
        }
        else if (PlayerPrefs.GetString("activeLang") == "ru")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(true);
            togglePt.SetActive(false);
            toggleKo.SetActive(false);
            toggleEs.SetActive(false);
            toggleFr.SetActive(false);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("RU");
        }
        else if (PlayerPrefs.GetString("activeLang") == "pt")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(false);
            togglePt.SetActive(true);
            toggleKo.SetActive(false);
            toggleEs.SetActive(false);
            toggleFr.SetActive(false);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("PT");
        }
        else if (PlayerPrefs.GetString("activeLang") == "ko")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(false);
            togglePt.SetActive(false);
            toggleKo.SetActive(true);
            toggleEs.SetActive(false);
            toggleFr.SetActive(false);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("KO");
        }
        else if (PlayerPrefs.GetString("activeLang") == "es")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(false);
            togglePt.SetActive(false);
            toggleKo.SetActive(false);
            toggleEs.SetActive(true);
            toggleFr.SetActive(false);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("ES");
        }
        else if (PlayerPrefs.GetString("activeLang") == "fr")
        {
            toggleEn.SetActive(false);
            toggleRu.SetActive(false);
            togglePt.SetActive(false);
            toggleKo.SetActive(false);
            toggleEs.SetActive(false);
            toggleFr.SetActive(true);

            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_OpenScreen("FR");
        }
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }
}
