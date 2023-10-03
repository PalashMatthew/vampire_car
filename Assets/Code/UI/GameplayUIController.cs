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
    public TMP_Text tCurrentWave;
    public TMP_Text tNextWave;
    public Image imgWaveFill;
    public Image imgWaveEndFill;

    [Header("Level")]
    public TMP_Text tCurrentLevel;
    public Image imgFillLevelBar;
    public PlayerLevelController playerLevelController;
    public Image imgLevelEndFill;


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
        tCurrentLevel.text = "Level " + playerLevelController.currentLevel.ToString();

        imgFillLevelBar.fillAmount = (float)playerLevelController.enemyCountInThisLevel / playerLevelController.enemyCountFromNewLevel[playerLevelController.currentLevel - 1];
        imgLevelEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgFillLevelBar.fillAmount * 280f, 0);
    }
    #endregion

    #region Wave
    public void StartWave(int _waveTime, int _waveNum)
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

        imgWaveFill.fillAmount = 0;
        imgWaveEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgWaveFill.fillAmount * 230f, 0);

        tCurrentWave.text = _waveNum.ToString();
        tNextWave.text = _waveNum + 1 + "";

        StartCoroutine(WaveTimer(_waveTime));        
    }

    IEnumerator WaveTimer(int _saveWaveTime)
    {
        yield return new WaitForSeconds(1);
        _currentWaveTime -= 1;

        imgWaveFill.fillAmount = (float)(_saveWaveTime - _currentWaveTime) / _saveWaveTime;
        imgWaveEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgWaveFill.fillAmount * 230f, 0);

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
            StartCoroutine(WaveTimer(_saveWaveTime));
        }
    }
    #endregion
}
