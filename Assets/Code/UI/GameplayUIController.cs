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
    public Image imgWaveFill;
    public Image imgWaveEndFill;
    public GameObject wavePanel;

    [Header("Level")]
    public TMP_Text tCurrentLevel;
    public Image imgFillLevelBar;
    public PlayerLevelController playerLevelController;
    public Image imgLevelEndFill;

    [Header("Boss")]
    public TMP_Text tBossName;
    public TMP_Text tBossHP;
    public Image imgFillBossBar;
    public Image imgBossEndFill;
    public GameObject bossPanel;

    public bool isShowPanelWave;
    public bool _isBossFight;
    public bool isWin;


    private void Start()
    {
        UpdateScrewText();        
    }

    private void Update()
    {
        UpdateLevel();

        if (!isWin)
        {
            if (isShowPanelWave)
            {
                wavePanel.SetActive(true);
            } else
            {
                wavePanel.SetActive(false);
            }

            if (_isBossFight)
            {
                bossPanel.SetActive(true);

                Boss();
            } 
            else
            {
                bossPanel.SetActive(false);
            }
        } 
        else
        {
            bossPanel.SetActive(false);
            wavePanel.SetActive(false);
        }        
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

        if (imgFillLevelBar.fillAmount > 0)
        {
            imgLevelEndFill.gameObject.SetActive(true);
            imgLevelEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgFillLevelBar.fillAmount * 280f, 0);
        }
        else
        {
            imgLevelEndFill.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Wave
    public void StartWave(int _waveTime, int _waveNum)
    {
        _currentWaveTime = _waveTime;

        if (_currentWaveTime > 9)
        {
            tWaveTimer.text = "Wave " + _waveNum + " - 00:" + _currentWaveTime;
        }
        else
        {
            tWaveTimer.text = "Wave " + _waveNum + " - 00:0" + _currentWaveTime;
        }

        imgWaveFill.fillAmount = 0;        

        if (imgFillLevelBar.fillAmount > 0)
        {
            imgWaveEndFill.gameObject.SetActive(true);
            imgWaveEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgWaveFill.fillAmount * 1030f, 0);
        }
        else
        {
            imgWaveEndFill.gameObject.SetActive(false);
        }

        StartCoroutine(WaveTimer(_waveTime, _waveNum));        
    }

    IEnumerator WaveTimer(int _saveWaveTime, int _waveNum)
    {
        yield return new WaitForSeconds(1);
        _currentWaveTime -= 1;

        imgWaveFill.fillAmount = (float)(_saveWaveTime - _currentWaveTime) / _saveWaveTime;
        imgWaveEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgWaveFill.fillAmount * 1030f, 0);

        if (_currentWaveTime <= 0)
        {
            tWaveTimer.text = "Wave " + _waveNum + " - 00:00";
            waveController.WaveEnd();
        } 
        else
        {
            if (_currentWaveTime > 9)
            {
                tWaveTimer.text = "Wave " + _waveNum + " - 00:" + _currentWaveTime;
            }
            else
            {
                tWaveTimer.text = "Wave " + _waveNum + " - 00:0" + _currentWaveTime;
            }

            if (!_isBossFight)
                StartCoroutine(WaveTimer(_saveWaveTime, _waveNum));
        }
    }
    #endregion

    #region Boss
    public void Boss()
    {
        tBossName.text = "BOSS - Fire Truck";

        if (GameObject.Find("BOSS").GetComponent<EnemyController>().hp > 0)
            tBossHP.text = GameObject.Find("BOSS").GetComponent<EnemyController>().hp.ToString();
        else tBossHP.text = "0";

        imgFillBossBar.fillAmount = (float)GameObject.Find("BOSS").GetComponent<EnemyController>().hp / GameObject.Find("BOSS").GetComponent<EnemyController>().maxHp;

        imgWaveEndFill.gameObject.SetActive(true);
        imgWaveEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgWaveFill.fillAmount * 1030f, 0);
    }
    #endregion
}
