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
    public PlayerStats playerStats;
    public Image imgLevelEndFill;

    [Header("WaveComplite")]
    public GameObject waveCompliteObj;
    public GameObject waveHeaderObj;

    [Header("Boss")]
    public TMP_Text tBossName;
    public TMP_Text tBossHP;
    public Image imgFillBossBar;
    public Image imgBossEndFill;
    public GameObject bossPanel;

    public bool isShowPanelWave;
    public bool _isBossFight;
    public bool isWin;

    public Image imgPlayerHit;


    private void Start()
    {
        UpdateScrewText();
        imgPlayerHit.gameObject.SetActive(false);
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

    #region HP
    void UpdateLevel()
    {
        tCurrentLevel.text = "Level: " + playerStats.currentLevel;

        imgFillLevelBar.fillAmount = (float)playerStats.currentExp / playerStats.levelExpNeed[playerStats.currentLevel - 1];

        if (imgFillLevelBar.fillAmount > 0 && imgFillLevelBar.fillAmount < 1)
        {
            imgLevelEndFill.gameObject.SetActive(true);
            imgLevelEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgFillLevelBar.fillAmount * 281f, 0);
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
            tWaveTimer.text = "Волна " + _waveNum + " - 00:" + _currentWaveTime;
        }
        else
        {
            tWaveTimer.text = "Волна " + _waveNum + " - 00:0" + _currentWaveTime;
        }

        imgWaveFill.fillAmount = 0;

        imgWaveEndFill.gameObject.SetActive(false);

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
            tWaveTimer.text = "Волна " + _waveNum + " - 00:00";
            waveController.WaveEnd();
        } 
        else
        {
            if (_currentWaveTime > 9)
            {
                tWaveTimer.text = "Волна " + _waveNum + " - 00:" + _currentWaveTime;
            }
            else
            {
                tWaveTimer.text = "Волна " + _waveNum + " - 00:0" + _currentWaveTime;
            }

            if (!_isBossFight)
                StartCoroutine(WaveTimer(_saveWaveTime, _waveNum));
        }
    }
    #endregion

    #region Boss
    public void Boss()
    {
        tBossName.text = "БОСС - Fire Truck";

        if (GameObject.Find("BOSS").GetComponent<EnemyController>().hp > 0)
            tBossHP.text = (int)GameObject.Find("BOSS").GetComponent<EnemyController>().hp + "";
        else tBossHP.text = "0";

        imgFillBossBar.fillAmount = (float)GameObject.Find("BOSS").GetComponent<EnemyController>().hp / GameObject.Find("BOSS").GetComponent<EnemyController>().maxHp;

        imgWaveEndFill.gameObject.SetActive(true);
        imgWaveEndFill.GetComponent<RectTransform>().anchoredPosition = new Vector2(imgWaveFill.fillAmount * 1030f, 0);
    }
    #endregion

    public IEnumerator WaveCompliteAnim()
    {
        waveCompliteObj.SetActive(true);
        waveHeaderObj.transform.localScale = Vector3.zero;
        waveHeaderObj.transform.DOScale(1, 0.3f);

        yield return new WaitForSeconds(2f);

        waveHeaderObj.transform.DOScale(0, 0.3f);

        yield return new WaitForSeconds(0.3f);

        waveCompliteObj.SetActive(false);
    }

    public IEnumerator PlayerHit()
    {
        imgPlayerHit.gameObject.SetActive(true);
        imgPlayerHit.DOFade(1, 0.05f);
        yield return new WaitForSeconds(0.05f);
        imgPlayerHit.DOFade(0, 0.05f);
        imgPlayerHit.gameObject.SetActive(false);
    }
}
