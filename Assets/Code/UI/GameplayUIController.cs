using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    public TMP_Text tScrew;
    public TMP_Text tMoney;

    [Header("Wave")]
    public TMP_Text tCurrentWave;
    public TMP_Text tNextWave;
    public Image imgFillWaveBar;

    public WaveController waveController;


    private void Start()
    {
        UpdateScrewText();        
    }

    private void Update()
    {
        UpdateWave();
    }

    #region ScrewText
    public void UpdateScrewText()
    {
        Sequence _textAnim = DOTween.Sequence();

        _textAnim.Append(tScrew.gameObject.GetComponent<RectTransform>().DOScale(1.25f, 0.15f));
        _textAnim.AppendCallback(_UpdateScrewText);
        _textAnim.Append(tScrew.gameObject.GetComponent<RectTransform>().DOScale(1f, 0.15f));
    }

    private void _UpdateScrewText()
    {
        tScrew.text = GlobalStats.screwCount.ToString();
    }
    #endregion

    #region Wave
    public void UpdateWave()
    {
        tCurrentWave.text = waveController.currentWave.ToString();
        tNextWave.text = waveController.currentWave + 1 + "";

        imgFillWaveBar.fillAmount = (float)waveController.enemyDestroy / waveController.waveList[waveController.currentWave - 1].enemyKillCount;
    }
    #endregion
}
