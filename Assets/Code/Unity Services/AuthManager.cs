using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class AuthManager : MonoBehaviour
{
    public TMP_Text tUserID;
    public GameCloud gameCloudManaer;


    public async void AuthInitialize()
    {
        await UnityServices.InitializeAsync();

        tUserID.text = "";

        SignIn();

        InitScene.initCount++;
    }

    async void SignIn()
    {
        await signInAnonymous();
        gameCloudManaer.LoadData();
    }

    async Task signInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            print("Sign in Success! UserID - " + AuthenticationService.Instance.PlayerId);
            //tUserID.text = "UserID - " + AuthenticationService.Instance.PlayerId;
            PlayerPrefs.SetString("userID", AuthenticationService.Instance.PlayerId);
        }
        catch (AuthenticationException ex)
        {
            print("Sign in failed!");
            Debug.LogException(ex);
        }
    }
}
