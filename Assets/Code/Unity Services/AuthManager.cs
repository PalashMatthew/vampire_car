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

    async void Start()
    {
        await UnityServices.InitializeAsync();

        tUserID.text = "UserID - 0";

        SignIn();
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
            tUserID.text = "UserID - " + AuthenticationService.Instance.PlayerId;
        }
        catch (AuthenticationException ex)
        {
            print("Sign in failed!");
            Debug.LogException(ex);
        }
    }
}
