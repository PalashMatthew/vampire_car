using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLocationController : MonoBehaviour
{
    public int currentLocNum;
    public int maxLocNum;

    public List<GameObject> locationsObj;
    public Camera mainCamera;

    public GameObject butNext, butPrev;

    public List<string> levelName;

    public ASyncLoader loader;

    private void Start()
    {
        for (int i = 1; i <= maxLocNum; i++)
        {
            if (currentLocNum == i)
                locationsObj[i - 1].SetActive(true);
            else
                locationsObj[i - 1].SetActive(false);
        }

        if (currentLocNum == 1)
        {
            butNext.SetActive(true);
            butPrev.SetActive(false);
        }

        if (currentLocNum == maxLocNum)
        {
            butNext.SetActive(false);
            butPrev.SetActive(true);
        }
    }

    void ChangeLocation()
    {
        for (int i = 1; i <= maxLocNum; i++)
        {
            if (currentLocNum == i)
                locationsObj[i - 1].SetActive(true);
            else
                locationsObj[i - 1].SetActive(false);
        }

        if (currentLocNum == 1)
        {
            butNext.SetActive(true);
            butPrev.SetActive(false);
        }

        if (currentLocNum == maxLocNum)
        {
            butNext.SetActive(false);
            butPrev.SetActive(true);
        }

        #region Camera Color Settings
        if (currentLocNum == 1)
        {
            mainCamera.backgroundColor = new Color(0.2311321f, 1f, 0.9111943f);
        }

        if (currentLocNum == 2)
        {
            mainCamera.backgroundColor = new Color(0.8313726f, 0.7568628f, 0.5294118f);
        }
        #endregion
    }

    public void ButNextLocation()
    {
        currentLocNum++;
        ChangeLocation();
    }

    public void ButPrevLocation()
    {
        currentLocNum--;
        ChangeLocation();
    }

    public void ButPlay()
    {
        loader.LoadLevel(levelName[currentLocNum-1]);
    }
}
