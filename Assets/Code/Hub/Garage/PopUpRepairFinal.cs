using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpRepairFinal : MonoBehaviour
{
    PopUpController _popUpController;

    public TMP_Text tMoneyCount;
    public TMP_Text tDrawingCount;

    public int moneyCount;
    public int drawingCount;


    private void Start()
    {
        _popUpController = GetComponent<PopUpController>();
    }

    public void Open()
    {
        _popUpController.OpenPopUp();

        tMoneyCount.text = "x" + moneyCount;
        tDrawingCount.text = "x" + drawingCount;
    }

    public void ButContinue()
    {
        _popUpController.ClosedPopUp();
    }
}
