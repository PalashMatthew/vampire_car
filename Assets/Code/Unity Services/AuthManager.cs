using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class AuthManager : MonoBehaviour
{
    public TMP_Text tUserID;
    public GameCloud gameCloudManaer;

    private void Start()
    {
        tUserID.text = Application.version;
    }


    public void InitializeAuth()
    {
        tUserID.text = Application.version;

        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public async void AuthInitialize()
    {
        await UnityServices.InitializeAsync();

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

    public string Token;
    public string Error;

    public void GooglePlayGamesInit()
    {
        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                    // This token serves as an example to be used for SignInWithGooglePlayGames

                    PlayerPrefs.SetString("playerGooglePlayGamesCode", code);

                    SignInWithGooglePlayGamesAsync(code);
                });
            }
            else
            {
                Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");

                AuthInitialize();
            }
        });
    }

    async Task SignInWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log("SignIn is successful.");
            PlayerPrefs.SetString("userID", AuthenticationService.Instance.PlayerId);
            gameCloudManaer.LoadData();
            InitScene.initCount++;
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
