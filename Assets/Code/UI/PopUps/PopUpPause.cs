using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopUpPause : MonoBehaviour
{
    public GameObject imgFade;
    public GameObject objPopUp;

    public float animSpeed;

    private PopUpController _popUpController;

    public Transform passivePanel;
    public GameObject pauseCell;

    [Header("Input Settings")]
    public Sprite sprButInputActive;
    public Sprite sprButInputInactive;
    public Image imgButInput1, imgButInput2;

    public UpgradeController upgController;
    public List<Image> slotImage;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
        GunSlotInitialize();
    }

    public void ButOpen()
    {
        _popUpController = GetComponent<PopUpController>();

        GameplayController.isPause = true;
        Time.timeScale = 0;
        _popUpController.OpenPopUp();

        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Pause();

        GameInputCheck();
    }

    void GameInputCheck()
    {
        if (!PlayerPrefs.HasKey("GameInput"))
        {
            ButInput1();
            return;
        }

        if (PlayerPrefs.GetInt("GameInput") == 1)
        {
            ButInput1();
            return;
        }

        if (PlayerPrefs.GetInt("GameInput") == 2)
        {
            ButInput2();
            return;
        }
    }

    public void ButClosed()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_Continue();

        GameplayController.isPause = false;
        Time.timeScale = 1;
        _popUpController.ClosedPopUp();
    }

    public void ButInput1()
    {
        imgButInput2.sprite = sprButInputActive;
        imgButInput1.sprite = sprButInputInactive;

        PlayerPrefs.SetInt("GameInput", 1);

        GameObject.Find("GameplayController").GetComponent<GameplayController>().inputSettings = GameplayController.InputSettings.RelativeToTheFinger;
    }

    public void ButInput2()
    {
        imgButInput1.sprite = sprButInputActive;
        imgButInput2.sprite = sprButInputInactive;

        PlayerPrefs.SetInt("GameInput", 2);

        GameObject.Find("GameplayController").GetComponent<GameplayController>().inputSettings = GameplayController.InputSettings.FingerTracking;
    }

    public void ButExit()
    {
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_GoToHub();

        GameplayController.isPause = false;
        Time.timeScale = 1;
        GameObject.Find("LoadingCanvas").GetComponent<ASyncLoader>().LoadLevel("Hub");
    }

    public void InstPassive(Sprite _spr)
    {
        GameObject gm = Instantiate(pauseCell, transform.position, transform.rotation);
        
        gm.GetComponent<PauseCell>().spr = _spr;
        gm.GetComponent<PauseCell>().Initialize();

        gm.transform.parent = passivePanel;
        gm.transform.localScale = Vector3.one;
    }

    void GunSlotInitialize()
    {
        for (int i = 0; i < slotImage.Count; i++)
        {
            slotImage[i].DOFade(0f, 0f).SetUpdate(true);
        }

        if (upgController.activeGunCard != null)
        {
            for (int i = 0; i < upgController.activeGunCard.Count; i++)
            {
                slotImage[i].sprite = upgController.activeGunCard[i].imageItem;
                slotImage[i].DOFade(1f, 0f).SetUpdate(true);
            }
        }
    }

    public void AddCardToSlot()
    {
        for (int i = 0; i < upgController.activeGunCard.Count; i++)
        {
            slotImage[i].sprite = upgController.activeGunCard[i].imageItem;
            slotImage[i].DOFade(1f, 0f).SetUpdate(true);
        }
    }

    #if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause()
    {
        if (!PopUpWin.isEndGame && !WaveController.isWaveEnd)
            ButOpen();
    }

    private void OnApplicationQuit()
    {
        if (!PopUpWin.isEndGame && !WaveController.isWaveEnd)
            ButOpen();
    }
    #endif
}
