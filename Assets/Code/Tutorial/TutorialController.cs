using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject Message1;
    public GameObject Message2;
    public GameObject message2Hand;
    public GameObject Message3;
    public GameObject objAidFirstKit;
    public GameObject Message4;
    public GameObject Message5;

    public GameObject progressPanel;
    public GameObject upgradeMultiplierBar;

    public GameObject message5_text1, message5_text2, message5_text3;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("tutorialLoc1Complite") && Application.loadedLevelName == "Loc alpha 1")
        {
            PlayerPrefs.SetString("tutorialLoc1Complite", "true");
            PlayerPrefs.SetString("tutorialCards", "false");
            WaveController.isTutorialActive = true;
            StartCoroutine(ShowMessage1());
        }    
    }

    public IEnumerator ShowMessage1()
    {
        GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().isTutorialActive = true;
        progressPanel.SetActive(false);
        upgradeMultiplierBar.SetActive(false);

        yield return new WaitForSeconds(2f);

        Message1.GetComponent<PopUpController>().OpenPopUp();

        yield return new WaitForSeconds(2f);

        Message1.GetComponent<PopUpController>().ClosedPopUp();

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ShowMessage2());
    }

    IEnumerator ShowMessage2()
    {
        Message2.GetComponent<PopUpController>().OpenPopUp();
        StartCoroutine(Message2HandAnim());

        yield return new WaitForSeconds(4);

        Message2.GetComponent<PopUpController>().ClosedPopUp();

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ShowMessage3());
    }

    IEnumerator ShowMessage3()
    {
        Message3.GetComponent<PopUpController>().OpenPopUp();

        GameObject.Find("Player").GetComponent<PlayerStats>().currentHp -= 20;

        GameObject aidKitObj = Instantiate(objAidFirstKit, new Vector3(0, 1, 82f), transform.rotation);

        yield return new WaitForSeconds(4f);

        if (aidKitObj != null)
        {
            aidKitObj.GetComponent<FirstAidKitController>().isStopMove = true;
        }        
    }

    public IEnumerator ShowMessage4()
    {
        Message3.GetComponent<PopUpController>().ClosedPopUp();

        yield return new WaitForSeconds(0.5f);

        Message4.GetComponent<PopUpController>().OpenPopUp();

        yield return new WaitForSeconds(2f);

        Message4.GetComponent<PopUpController>().ClosedPopUp();

        yield return new WaitForSeconds(0.5f);

        upgradeMultiplierBar.SetActive(true);

        GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().isTutorialActive = false;
        GameObject.Find("GameplayController").GetComponent<WaveController>().StartTutorialWave();
        GameObject.Find("Generate Controller").GetComponent<Generate>().StartCoroutine(GameObject.Find("Generate Controller").GetComponent<Generate>().TutorialEnemyGen(1));
    }

    public void Message5Start()
    {
        GameplayController.isPause = true;
        Time.timeScale = 0;
        PlayerPrefs.SetString("tutorialCards", "true");

        Message5.GetComponent<PopUpController>().OpenPopUp();

        message5_text1.SetActive(true);
        message5_text2.SetActive(false);
        message5_text3.SetActive(false);
    }

    public void ButNextText()
    {
        if (message5_text1.active)
        {
            message5_text1.SetActive(false);
            message5_text2.SetActive(true);
            message5_text3.SetActive(false);

            return;
        }

        if (message5_text2.active)
        {
            message5_text1.SetActive(false);
            message5_text2.SetActive(false);
            message5_text3.SetActive(true);

            return;
        }

        if (message5_text3.active)
        {
            Message5.GetComponent<PopUpController>().ClosedPopUp();

            GameplayController.isPause = false;

            Time.timeScale = 1;

            if (WaveController.isTutorialActive)
            {
                Debug.Log("FALSE");
                GameObject.Find("Generate Controller").GetComponent<Generate>().StopAllCoroutines();
                GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave = 0;
                GameObject.Find("GameplayController").GetComponent<WaveController>().StartWave();

                WaveController.isTutorialActive = false;
            }
            

            PlayerPrefs.SetString("tutorialComplite", "true");
        }
    }

    IEnumerator Message2HandAnim()
    {
        message2Hand.GetComponent<RectTransform>().DOAnchorPosX(-291f, 0.5f);

        yield return new WaitForSeconds(0.8f);

        message2Hand.GetComponent<RectTransform>().DOAnchorPosX(291f, 0.5f);

        yield return new WaitForSeconds(0.8f);

        if (WaveController.isTutorialActive)
            StartCoroutine(Message2HandAnim());
    }
}
