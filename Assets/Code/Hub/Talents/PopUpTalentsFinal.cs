using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTalentsFinal : MonoBehaviour
{
    PopUpController _popUpController;

    public TMP_Text tName;
    public Image imgCell;
    public Sprite sprCommon;
    public Sprite sprRare;
    public Sprite sprEpic;

    public TMP_Text tValue;

    public int value;
    public string talentName;

    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void Open()
    {
        _popUpController.OpenPopUp();

        tValue.text = "+" + value;
        tName.text = talentName;
    }

    public void ButContinue()
    {
        _popUpController.ClosedPopUp();
    }
}
