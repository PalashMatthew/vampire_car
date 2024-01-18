using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTalentsFinal : MonoBehaviour
{
    PopUpController _popUpController;

    public TMP_Text tName;
    public TMP_Text tDescription;
    public Image imgCell;
    public Sprite sprCommon;
    public Sprite sprRare;
    public Sprite sprEpic;

    public TMP_Text tValue;

    public int value;
    public string talentName;

    public GameObject butOk;
    public GameObject particleImage;

    public Image imgIcon;

    public Sprite sprTalent1, sprTalent2, sprTalent3, sprTalent4, sprTalent5, sprTalent6, sprTalent7, sprTalent8, sprTalent9;

    [Header("Sounds")]
    public AudioClip clipNewItem;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void Open()
    {
        _popUpController.OpenPopUp();

        string procent = "";

        SoundController _soundController = GameObject.Find("SoundsController").GetComponent<SoundController>();

        if (_soundController != null)
        {
            _soundController.PlaySound(clipNewItem);
        }

        switch (talentName)
        {
            case "GunSlot":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameGunSlot");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskGunSlot");

                imgIcon.sprite = sprTalent7;
                break;

            case "Health":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameHealth");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskHealth");

                imgIcon.sprite = sprTalent1;
                break;

            case "Damage":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameDamage");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskDamage");
                procent = "%";

                imgIcon.sprite = sprTalent2;
                break;

            case "Iron":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameIron");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskIron");

                imgIcon.sprite = sprTalent5;
                break;

            case "RecoveryHpInFirstAidKit":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameFirstAidKit");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskFirstAidKit");
                procent = "%";

                imgIcon.sprite = sprTalent3;
                break;

            case "ShotSpeed":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameShotSpeed");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskShotSpeed");
                procent = "%";

                imgIcon.sprite = sprTalent6;
                break;

            case "Block":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameBlock");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskBlock");
                procent = "%";

                imgIcon.sprite = sprTalent4;
                break;

            case "EquipmentImprovement":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameEquipments");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskEquipments");
                procent = "%";

                imgIcon.sprite = sprTalent8;
                break;

            case "CarImprovement":
                tName.text = "<wave>" + PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsNameCar");
                tDescription.text = PlayerPrefs.GetString(PlayerPrefs.GetString("activeLang") + "LOC_talentsDeskCar");
                procent = "%";

                imgIcon.sprite = sprTalent9;
                break;
        }

        tValue.text = "+" + value + procent;

        GameObject.Find("GameCloud").GetComponent<GameCloud>().SaveData();

        StartCoroutine(Animation());
    }

    public void ButContinue()
    {
        _popUpController.ClosedPopUp();
    }

    IEnumerator Animation()
    {
        tName.gameObject.SetActive(false);
        tDescription.gameObject.SetActive(false);
        imgCell.gameObject.SetActive(false);
        tValue.gameObject.SetActive(false);
        butOk.gameObject.SetActive(false);
        particleImage.gameObject.SetActive(false);

        tName.transform.DOScale(0, 0);
        tDescription.transform.DOScale(0, 0);
        imgCell.transform.DOScale(0, 0);
        tValue.transform.DOScale(0, 0);
        butOk.transform.DOScale(0, 0);
        particleImage.transform.DOScale(0, 0);

        yield return new WaitForSeconds(0.5f);

        tName.gameObject.SetActive(true);        

        tName.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);        

        yield return new WaitForSeconds(0.5f);

        imgCell.gameObject.SetActive(true);
        tDescription.gameObject.SetActive(true);
        tValue.gameObject.SetActive(true);
        particleImage.gameObject.SetActive(true);

        imgCell.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        tDescription.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        tValue.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
        particleImage.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSeconds(0.5f);

        butOk.gameObject.SetActive(true);

        butOk.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
    }
}
