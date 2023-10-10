using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CVSLoader : MonoBehaviour
{
    private bool _debug = true;
    private const string url = "";

    public void DownloadTable(string sheetId, Action<string> onSheetLoadedAction)
    {
        string actualUrl = url.Replace("*", sheetId);
        StartCoroutine(DoawnloadRawCvsTable(actualUrl, onSheetLoadedAction));
    }

    IEnumerator DoawnloadRawCvsTable(string actualUrl, Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(actualUrl))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(request.error);
            }
            else
            {
                if (_debug)
                {
                    Debug.Log("Successful download");
                    Debug.Log(request.downloadHandler.text);
                }

                callback(request.downloadHandler.text);
            }
        }
        yield return null;
    }
}
