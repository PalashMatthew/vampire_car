using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ASyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject loadingBar;

    [SerializeField] private Image loadingProgressBarFill;

    public void LoadLevel(string levelToLoad)
    {
        loadingScreen.SetActive(true);
        loadingBar.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        
        while (!loadOperation.isDone)
        {
            float progressValuse = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingProgressBarFill.fillAmount = progressValuse;
            yield return null;
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        ScreenCapture.CaptureScreenshot(Random.Range(1, 999999) + "_image.png");
    //    }
    //}
}
