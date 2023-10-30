using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarageController : MonoBehaviour
{
    public GameObject canvasChangeCar;

    [Header("Car Settings")]
    public ChangeCarController changeCarController;
    public TMP_Text tCarName;
    public Image imgCar;
    public TMP_Text tDamage;
    public TMP_Text tHealth;

    [Header("Car Level")]
    public TMP_Text tCarLevel;
    public Image fillCarLevel;
    public Image fillEndCarLevel;

    public void Initialize()
    {
        tCarName.text = changeCarController._activeCarObj.carName;
        imgCar = changeCarController._activeCarObj.imgCar;

        #region Car Level
        tCarLevel.text = PlayerPrefs.GetInt(changeCarController._activeCarObj.carName + "carLevel") + "/40";
        fillCarLevel.DOFillAmount((float)PlayerPrefs.GetInt(changeCarController._activeCarObj.carName + "carLevel") / 40, 0.5f);
        fillEndCarLevel.GetComponent<RectTransform>().DOAnchorPos(new Vector2((float)PlayerPrefs.GetInt(changeCarController._activeCarObj.carName + "carLevel") / 40 * 132f, 0), 0.5f);
        #endregion

        tDamage.text = "+" + PlayerPrefs.GetFloat(changeCarController._activeCarObj.carName + "carDamage") + "%";
        tHealth.text = PlayerPrefs.GetFloat(changeCarController._activeCarObj.carName + "carHealth") + "";
    }

    public void ButChangeCar()
    {
        canvasChangeCar.GetComponent<PopUpController>().OpenPopUp();
        StartCoroutine(OffGarage());
    }

    IEnumerator OffGarage()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
