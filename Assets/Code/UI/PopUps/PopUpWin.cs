using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWin : MonoBehaviour
{
    private PopUpController _popUpController;
    public WaveController waveController;

    int locationNum;

    public GameObject objNewRecord;
    public TMP_Text tWave;
    public TMP_Text tLeader;
    public TMP_Text tAdsReward;
    public GameObject objButAds;

    public List<DetailCard> detailCards;

    public List<float> waveDropProcent;
    public List<float> waveDropItemProcent;

    [Header("Cell Prefab")]
    public GameObject itemCellObj;
    public GameObject resourceCellObj;

    public Transform scrollObj;

    [Header("Sprites")]
    public Sprite sprIconMoney;
    public Sprite sprIconExp;
    public Sprite sprIconDrawingGun;
    public Sprite sprIconDrawingEngine;
    public Sprite sprIconDrawingBrakes;
    public Sprite sprIconDrawingFuelSystem;
    public Sprite sprIconDrawingSuspension;
    public Sprite sprIconDrawingTransmission;
    public Sprite sprIconTitan;

    int moneyGiveValue;

    [Header("Objects")]
    public GameObject objHeader1;
    public GameObject objHeader2;
    public GameObject objWaveCount;
    public GameObject objRewardScroll;
    public GameObject objButContinue;

    public static bool isEndGame;


    private void Start()
    {
        isEndGame = false;
        _popUpController = GetComponent<PopUpController>();
    }

    void Initialize()
    {       
        tWave.text = (waveController.currentWave - 1) + "";

        tLeader.text = "Вы справились лучше чем " + Random.Range(70, 86) + "% игроков";

        locationNum = GameObject.Find("GameplayController").GetComponent<GameplayController>().locationNum;

        if (PlayerPrefs.GetInt("loc_" +  locationNum + "_maxWave") < waveController.currentWave - 1)
        {
            PlayerPrefs.SetInt("loc_" + locationNum + "_maxWave", waveController.currentWave - 1);
        }

        int _moneyReward = 0;
        int _expReward = 0;
        int _drawingGunReward = 0;
        int _drawingDetailReward = 0;
        int _titanReward = 0;
        int _itemRewardCount = 0;
        string _itemRewardID = "";

        objButAds.SetActive(false);

        if (waveController.currentWave >= 2)
        {
            #region Money Drop
            float moneyRand = Random.Range(PlayerPrefs.GetFloat(locationNum + "dropSystemMoneyMin"), PlayerPrefs.GetFloat(locationNum + "dropSystemMoneyMax"));
            float moneyValue = 0;

            for (int i = 1; i < waveController.currentWave; i++)
            {
                moneyValue += moneyRand / 100 * waveDropProcent[i - 1];
            }

            if (PlayerPrefs.GetString("activeCarID") == "Eos")
            {
                moneyValue = moneyValue + (moneyValue / 100 * 20);
            }

            GameObject instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
            instCell.transform.parent = scrollObj;
            instCell.GetComponent<ResourcesCell>().value = (int)moneyValue;
            instCell.GetComponent<ResourcesCell>().sprIcon = sprIconMoney;
            instCell.GetComponent<ResourcesCell>().Initialize();

            PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + (int)moneyValue);

            objButAds.SetActive(true);
            tAdsReward.text = "+" + (int)moneyValue;
            moneyGiveValue = (int)moneyValue;

            _moneyReward = (int)moneyValue;
            #endregion

            #region Exp Drop
            float expRand = Random.Range(PlayerPrefs.GetFloat(locationNum + "dropSystemExpMin"), PlayerPrefs.GetFloat(locationNum + "dropSystemExpMax"));
            float expValue = 0;

            for (int i = 1; i < waveController.currentWave; i++)
            {
                expValue += expRand / 100 * waveDropProcent[i - 1];
            }

            instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
            instCell.transform.parent = scrollObj;
            instCell.GetComponent<ResourcesCell>().value = (int)expValue;
            instCell.GetComponent<ResourcesCell>().sprIcon = sprIconExp;
            instCell.GetComponent<ResourcesCell>().Initialize();

            PlayerPrefs.SetInt("playerExp", PlayerPrefs.GetInt("playerExp") + (int)expValue);

            _expReward = (int)expValue;
            #endregion

            #region Drawing Drop
            float drawingRand = Random.Range(PlayerPrefs.GetFloat(locationNum + "dropSystemDrawingMin"), PlayerPrefs.GetFloat(locationNum + "dropSystemDrawingMax"));
            float drawingValue = 0;

            for (int i = 1; i < waveController.currentWave; i++)
            {
                drawingValue += drawingRand / 100 * waveDropProcent[i - 1];
            }

            int drawingGunCount = 0;
            int drawingEngineCount = 0;
            int drawingBrakesCount = 0;
            int drawingFuelSystemCount = 0;
            int drawingSuspensionCount = 0;
            int drawingTransmissionCount = 0;

            for (int i = 1; i < drawingValue; i++)
            {
                int rand = Random.Range(1, 7);

                if (rand == 1)
                {
                    drawingGunCount++;
                    PlayerPrefs.SetInt("drawingGunCount", PlayerPrefs.GetInt("drawingGunCount") + 1);
                }

                if (rand == 2)
                {
                    drawingEngineCount++;
                    PlayerPrefs.SetInt("drawingEngineCount", PlayerPrefs.GetInt("drawingEngineCount") + 1);
                }

                if (rand == 3)
                {
                    drawingBrakesCount++;
                    PlayerPrefs.SetInt("drawingBrakesCount", PlayerPrefs.GetInt("drawingBrakesCount") + 1);
                }

                if (rand == 4)
                {
                    drawingFuelSystemCount++;
                    PlayerPrefs.SetInt("drawingFuelSystemCount", PlayerPrefs.GetInt("drawingFuelSystemCount") + 1);
                }

                if (rand == 5)
                {
                    drawingSuspensionCount++;
                    PlayerPrefs.SetInt("drawingSuspensionCount", PlayerPrefs.GetInt("drawingSuspensionCount") + 1);
                }

                if (rand == 6)
                {
                    drawingTransmissionCount++;
                    PlayerPrefs.SetInt("drawingTransmissionCount", PlayerPrefs.GetInt("drawingTransmissionCount") + 1);
                }
            }

            if (drawingGunCount > 0)
            {
                instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
                instCell.transform.parent = scrollObj;
                instCell.GetComponent<ResourcesCell>().value = drawingGunCount;
                instCell.GetComponent<ResourcesCell>().sprIcon = sprIconDrawingGun;
                instCell.GetComponent<ResourcesCell>().Initialize();                
            }

            _drawingGunReward = drawingGunCount;

            if (drawingEngineCount > 0)
            {
                instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
                instCell.transform.parent = scrollObj;
                instCell.GetComponent<ResourcesCell>().value = drawingEngineCount;
                instCell.GetComponent<ResourcesCell>().sprIcon = sprIconDrawingEngine;
                instCell.GetComponent<ResourcesCell>().Initialize();
            }

            if (drawingBrakesCount > 0)
            {
                instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
                instCell.transform.parent = scrollObj;
                instCell.GetComponent<ResourcesCell>().value = drawingBrakesCount;
                instCell.GetComponent<ResourcesCell>().sprIcon = sprIconDrawingBrakes;
                instCell.GetComponent<ResourcesCell>().Initialize();
            }

            if (drawingFuelSystemCount > 0)
            {
                instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
                instCell.transform.parent = scrollObj;
                instCell.GetComponent<ResourcesCell>().value = drawingFuelSystemCount;
                instCell.GetComponent<ResourcesCell>().sprIcon = sprIconDrawingFuelSystem;
                instCell.GetComponent<ResourcesCell>().Initialize();
            }

            if (drawingSuspensionCount > 0)
            {
                instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
                instCell.transform.parent = scrollObj;
                instCell.GetComponent<ResourcesCell>().value = drawingSuspensionCount;
                instCell.GetComponent<ResourcesCell>().sprIcon = sprIconDrawingSuspension;
                instCell.GetComponent<ResourcesCell>().Initialize();
            }

            if (drawingTransmissionCount > 0)
            {
                instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
                instCell.transform.parent = scrollObj;
                instCell.GetComponent<ResourcesCell>().value = drawingTransmissionCount;
                instCell.GetComponent<ResourcesCell>().sprIcon = sprIconDrawingTransmission;
                instCell.GetComponent<ResourcesCell>().Initialize();
            }

            _drawingDetailReward = drawingEngineCount + drawingBrakesCount + drawingFuelSystemCount + drawingSuspensionCount + drawingTransmissionCount;
            #endregion

            #region Titan Drop
            float titanRand = Random.Range(PlayerPrefs.GetFloat(locationNum + "dropSystemTitanMin"), PlayerPrefs.GetFloat(locationNum + "dropSystemTitanMax"));
            float titanValue = 0;

            for (int i = 1; i < waveController.currentWave; i++)
            {
                titanValue += titanRand / 100 * waveDropProcent[i - 1];
            }

            instCell = Instantiate(resourceCellObj, transform.position, transform.rotation);
            instCell.transform.parent = scrollObj;
            instCell.GetComponent<ResourcesCell>().value = (int)titanValue;
            instCell.GetComponent<ResourcesCell>().sprIcon = sprIconTitan;
            instCell.GetComponent<ResourcesCell>().Initialize();

            PlayerPrefs.SetInt("playerTitan", PlayerPrefs.GetInt("playerTitan") + (int)titanValue);

            _titanReward = (int)titanValue;
            #endregion

            #region ItemDrop
            int itemValue = 0;
            int plusChance = 0;

            for (int i = 1; i < waveController.currentWave; i++)
            {
                int rand = Random.Range(1, 101);

                if (rand <= waveDropItemProcent[i-1] + plusChance)
                {
                    itemValue++;
                    Debug.Log("Item Spawn");
                } 
                else
                {
                    if (itemValue > 0)
                    {
                        plusChance = 0;
                    } 
                    else
                    {
                        plusChance += 3;
                    }
                }
            }

            for (int i = 0; i < itemValue; i++)
            {
                DetailCard _card = detailCards[Random.Range(0, detailCards.Count)];

                string rarity = "";

                int rand = Random.Range(1, 101);
                if (rand <= 90) rarity = "common";
                if (rand > 90 && rand <= 98) rarity = "rare";
                if (rand > 98) rarity = "epic";

                instCell = Instantiate(itemCellObj, transform.position, transform.rotation);
                instCell.transform.parent = scrollObj;
                instCell.GetComponent<ItemDropCell>().rarity = rarity;
                instCell.GetComponent<ItemDropCell>().sprIcon = _card.sprItem;
                instCell.GetComponent<ItemDropCell>().Initialize();

                _itemRewardCount++;
                _itemRewardID += "_" + _card.itemID;

                #region Add Item
                string _itemType = _card.itemType.ToString();

                PlayerPrefs.SetInt("itemCount" + _itemType, PlayerPrefs.GetInt("itemCount" + _itemType) + 1);

                PlayerPrefs.SetInt("item" + _itemType + "ID" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), _card.itemID);

                PlayerPrefs.SetString("item" + _itemType + "Rarity" + (PlayerPrefs.GetInt("itemCount" + _itemType) - 1), rarity);

                if (_itemType == "Gun")
                {
                    PlayerPrefs.SetInt("unlockGun" + _card.itemID, 1);
                }
                #endregion
            }
            #endregion
        }

        if (waveController.currentWave == 11)
        {
            if (PlayerPrefs.GetInt("maxLocation") <= locationNum)
            {
                PlayerPrefs.SetString("unlockNewItems", "true");
                PlayerPrefs.SetInt("maxLocation", locationNum+1);
            }
        }

        if (GameObject.Find("Firebase") != null)
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_WaveReward(waveController.currentWave - 1, locationNum, _moneyReward, _expReward, _drawingGunReward, _drawingDetailReward, _titanReward, _itemRewardCount, _itemRewardID);

        int _dieCount;

        if (GameObject.Find("PopUp Recovery").GetComponent<PopUpRecovery>().isRecovery)
        {
            _dieCount = 2;
        } 
        else
        {
            _dieCount = 1;
        }

        if (GameObject.Find("Firebase") != null)
            GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_PlayerDie(GameObject.Find("GameplayController").GetComponent<WaveController>().currentWave - 1, Application.loadedLevelName, (int)GameObject.Find("GameplayController").GetComponent<WaveController>().secondsPass, _dieCount);
    }

    public void ButOpen()
    {
        GameplayController.isPause = true;
        isEndGame = true;

        Initialize();

        _popUpController.OpenPopUp();

        StartCoroutine(Animation());
    }

    public void ButClosed()
    {
        GameplayController.isPause = false;
        Time.timeScale = 1;
        GameObject.Find("LoadingCanvas").GetComponent<ASyncLoader>().LoadLevel("Hub");
        _popUpController.ClosedPopUp();        
    }

    public void ButAds()
    {
        GameObject.Find("AdsManager").GetComponent<AdsController>().ShowAds("moneyX2");
    }

    public void Ads_Reward()
    {
        PlayerPrefs.SetInt("playerMoney", PlayerPrefs.GetInt("playerMoney") + moneyGiveValue);
        objButAds.SetActive(false);
    }

    IEnumerator Animation()
    {
        objWaveCount.SetActive(false);
        tLeader.gameObject.SetActive(false);
        objHeader2.SetActive(false);
        objRewardScroll.SetActive(false);
        objButContinue.SetActive(false);
        objButAds.SetActive(false);

        objWaveCount.transform.DOScale(0, 0);
        tLeader.transform.DOScale(0, 0);
        objHeader2.transform.DOScale(0, 0);
        objRewardScroll.transform.DOScale(0, 0);
        objButContinue.transform.DOScale(0, 0);
        objButAds.transform.DOScale(0, 0);

        objHeader1.SetActive(true);
        objHeader1.transform.DOScale(0, 0);

        objHeader1.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSecondsRealtime(0.5f);

        objWaveCount.SetActive(true);
        objWaveCount.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSecondsRealtime(0.3f);

        tLeader.gameObject.SetActive(true);
        tLeader.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSecondsRealtime(0.3f);

        objHeader2.SetActive(true);
        objHeader2.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSecondsRealtime(0.3f);

        objRewardScroll.SetActive(true);
        objRewardScroll.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        yield return new WaitForSecondsRealtime(0.3f);

        objButAds.SetActive(true);
        objButAds.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);

        objButContinue.SetActive(true);
        objButContinue.transform.DOScale(1, 0.3f).SetEase(Ease.InOutBack);
    }
}
