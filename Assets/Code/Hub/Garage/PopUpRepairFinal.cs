using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpRepairFinal : MonoBehaviour
{
    PopUpController _popUpController;

    public TMP_Text tMoneyCount;
    public TMP_Text tDrawingCount;

    public int moneyCount;
    public int drawingCount;

    public TMP_Text tName;

    public GameObject objCell1, objCell2;
    public GameObject butOk;

    public GameObject particleImage1, particleImage2;

    [Header("Sounds")]
    public AudioClip clipNewItem;

    [Header("Recipe")]
    public Image recipeImg;
    public Sprite sprGunDrawing;
    public Sprite sprEngineDrawing;
    public Sprite sprBrakesDrawing;
    public Sprite sprFuelSystemDrawing;
    public Sprite sprSuspensionDrawing;
    public Sprite sprTransmissionDrawing;

    public string itemType;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void Open()
    {
        _popUpController.OpenPopUp();

        switch (itemType)
        {
            case "Gun":
                recipeImg.sprite = sprGunDrawing;
                break;

            case "Engine":
                recipeImg.sprite = sprEngineDrawing;
                break;

            case "Brakes":
                recipeImg.sprite = sprBrakesDrawing;
                break;

            case "FuelSystem":
                recipeImg.sprite = sprFuelSystemDrawing;
                break;

            case "Suspension":
                recipeImg.sprite = sprSuspensionDrawing;
                break;

            case "Transmission":
                recipeImg.sprite = sprTransmissionDrawing;
                break;
        }

        tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_received");
        tMoneyCount.text = "x" + moneyCount;
        tDrawingCount.text = "x" + drawingCount;

        StartCoroutine(Animation());

        SoundController _soundController = GameObject.Find("SoundsController").GetComponent<SoundController>();

        if (_soundController != null)
        {
            _soundController.PlaySound(clipNewItem);
        }

        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();
    }

    public void ButContinue()
    {
        _popUpController.ClosedPopUp();
    }

    IEnumerator Animation()
    {
        tName.gameObject.SetActive(false);
        objCell1.SetActive(false);
        objCell2.SetActive(false);
        butOk.SetActive(false);
        particleImage1.SetActive(false);
        particleImage2.SetActive(false);

        tName.transform.DOScale(0, 0);
        objCell1.transform.DOScale(0, 0);
        objCell2.transform.DOScale(0, 0);
        butOk.transform.DOScale(0, 0);
        particleImage1.transform.DOScale(0, 0);
        particleImage2.transform.DOScale(0, 0);

        yield return new WaitForSeconds(0.5f);

        tName.gameObject.SetActive(true);
        objCell1.SetActive(true);
        objCell2.SetActive(true);

        particleImage1.SetActive(true);
        particleImage2.SetActive(true);

        tName.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        objCell1.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        objCell2.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        particleImage1.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        particleImage2.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.5f);

        butOk.SetActive(true);

        butOk.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
    }
}
