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
        Application.targetFrameRate = 60;

        //StartCoroutine(InitGame());

        readData.ReadDataInitialize();
        cloudSave.CloudSaveInitialize();
        firebaseSetup.FirebaseInit();
        adsController.Initialize();
        authManager.GooglePlayGamesInit();
    }

    private void Update()
    {
        if (initCount == 5)
        {
            if (PlayerPrefs.GetString("tutorialComplite") == "false")
            {
                loader.LoadLevel("Loc alpha 1");
                initCount = 0;
            }
            else
            {
                loader.LoadLevel("Hub");
                initCount = 0;
            }            
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
            authManager.GooglePlayGamesInit();
            init = true;
        }

        yield return new WaitForSeconds(2);

        if (PlayerPrefs.GetInt("internet_access") == 0 || !init)
        {
            StartCoroutine(InitGame());
        }          
    }
}
