using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Net.Mail;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class PopUpRate : MonoBehaviour
{
    private PopUpController _popUpController;

    public GameObject panelStart, panelRate, panelEmail;

    public TMP_InputField tInputField;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();

        if (!PlayerPrefs.HasKey("rateLater"))
        {
            PlayerPrefs.SetInt("rateLater", 0);
        }

        if (!PlayerPrefs.HasKey("rateComplite"))
        {
            PlayerPrefs.SetInt("rateComplite", 0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ButOpen();
        }
    }

    public void ButOpen()
    {
        if (PlayerPrefs.GetInt("rateLater") == 0 && PlayerPrefs.GetInt("rateComplite") == 0)
        {
            _popUpController.OpenPopUp();

            Initialize();

            PlayerPrefs.SetInt("rateActivate", 0);
        }        
    }

    public void ButClosed()
    {
        _popUpController.ClosedPopUp();
    }

    void Initialize()
    {
        panelStart.SetActive(true);
        panelRate.SetActive(false);
        panelEmail.SetActive(false);
    }

    public void ButYes()
    {
        panelStart.SetActive(false);
        panelRate.SetActive(true);
    }

    public void ButNo()
    {
        panelStart.SetActive(false);
        panelEmail.SetActive(true);

        PlayerPrefs.SetInt("rateLater", 1);
    }

    public void ButRate()
    {
        PlayerPrefs.SetInt("rateComplite", 1);

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.SplashGames.NoBrakes");
        ButClosed();
    }

    public void ButDontAsk()
    {
        ButClosed();
    }

    public void ButSendMessage()
    {        
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);

            mail.From = new MailAddress("looneygameshelp@gmail.com");
            mail.To.Add("info@splashgames.app");

            mail.Subject = "NoBrakes Review";
            mail.Body = tInputField.text;

            SmtpServer.Credentials = (ICredentialsByHost)new NetworkCredential("looneygameshelp@gmail.com", "jscw kbut cyzx mpch");

            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };


            SmtpServer.Send(mail);

            Debug.Log("mail Sent");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        ButClosed();
    }
}