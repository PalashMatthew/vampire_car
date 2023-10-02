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
    public TMP_Text tWaveTimer;
    public WaveController waveController;
    private int _currentWaveTime;

    [Header("Level")]
    public TMP_Text tCurrentLevel;
    public TMP_Text tNextLevel;
    public Image imgFillLevelBar;
    public PlayerLevelController playerLevelController;


    private void Start()
    {
        UpdateScrewText();        
    }

    private void Update()
    {
        UpdateLevel();
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

    #region Level
    void UpdateLevel()
    {
        tCurrentLevel.text = playerLevelController.currentLevel.ToString();
        tNextLevel.text = playerLevelController.currentLevel + 1 + "";

        imgFillLevelBar.fillAmount = (float)playerLevelController.screwCountInThisLevel / playerLevelController.screwCountFromNewLevel[playerLevelController.currentLevel - 1];
    }
    #endregion

    #region Wave
    public void StartWave(int _waveTime)
    {
        _currentWaveTime = _waveTime;

        if (_currentWaveTime > 9)
        {
            tWaveTimer.text = "00:" + _currentWaveTime;
        }
        else
        {
            tWaveTimer.text = "00:0" + _currentWaveTime;
        }

        StartCoroutine(WaveTimer());        
    }

    IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(1);
        _currentWaveTime -= 1;

        if (_currentWaveTime <= 0)
        {
            tWaveTimer.text = "00:00";
            waveController.WaveEnd();
        } 
        else
        {
            if (_currentWaveTime > 9)
            {
                tWaveTimer.text = "00:" + _currentWaveTime;
            }
            else
            {
                tWaveTimer.text = "00:0" + _currentWaveTime;
            }
            StartCoroutine(WaveTimer());
        }
    }
    #endregion
}
