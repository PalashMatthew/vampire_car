using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    public ASyncLoader loader;

    public ReadGoogleSheet readData;
    public GameCloud cloudSave;
    public FirebaseSetup firebaseSetup;
    public AdsController adsController;
    public AuthManager authManager;

    public static int initCount;

    bool init;

    private void Start()
    {
        StartCoroutine(InitGame());
    }

    private void Update()
    {
        if (initCount == 5)
        {
            loader.LoadLevel("Hub");
            initCount = 0;
        }
    }

    IEnumerator InitGame()
    {
        if (PlayerPrefs.GetInt("internet_access") == 1)
        {
            readData.ReadDataInitialize();
            cloudSave.CloudSaveInitialize();
            firebaseSetup.FirebaseInit();
            adsController.Initialize();
            authManager.AuthInitialize();
            init = true;
        }

        yield return new WaitForSeconds(2);

        if (PlayerPrefs.GetInt("internet_access") == 0 || !init)
        {
            StartCoroutine(InitGame());
        }          
    }
}
