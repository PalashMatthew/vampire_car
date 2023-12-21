using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using Unity.Services.CloudSave.Models;

public class GameCloud : MonoBehaviour
{
    private const string PLAYER_CLOUD_KEY = "PLAYER_DATA";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public async void CloudSaveInitialize()
    {
        await UnityServices.InitializeAsync();

        InitScene.initCount++;
    }

    public async void SaveData()
    {
        #region ItemGunLevel
        List<int> _itemGunLevel = new List<int>();
        List<string> _itemGunRarity = new List<string>();
        List<string> _itemGunType = new List<string>();
        List<int> _itemGunID = new List<int>();

        for (int i = 0; i < PlayerPrefs.GetInt("itemCountGun"); i++)
        {
            _itemGunLevel.Add(PlayerPrefs.GetInt("itemGunLevel" + i));
            _itemGunRarity.Add(PlayerPrefs.GetString("itemGunRarity" + i));
            _itemGunType.Add(PlayerPrefs.GetString("itemGunType" + i));
            _itemGunID.Add(PlayerPrefs.GetInt("itemGunID" + i));
        }
        #endregion

        #region ItemEngineLevel
        List<int> _itemEngineLevel = new List<int>();
        List<string> _itemEngineRarity = new List<string>();
        List<string> _itemEngineType = new List<string>();
        List<int> _itemEngineID = new List<int>();

        for (int i = 0; i < PlayerPrefs.GetInt("itemCountEngine"); i++)
        {
            _itemEngineLevel.Add(PlayerPrefs.GetInt("itemEngineLevel" + i));
            _itemEngineRarity.Add(PlayerPrefs.GetString("itemEngineRarity" + i));
            _itemEngineType.Add(PlayerPrefs.GetString("itemEngineType" + i));
            _itemEngineID.Add(PlayerPrefs.GetInt("itemEngineID" + i));
        }
        #endregion

        #region ItemBrakesLevel
        List<int> _itemBrakesLevel = new List<int>();
        List<string> _itemBrakesRarity = new List<string>();
        List<string> _itemBrakesType = new List<string>();
        List<int> _itemBrakesID = new List<int>();

        for (int i = 0; i < PlayerPrefs.GetInt("itemCountBrakes"); i++)
        {
            _itemBrakesLevel.Add(PlayerPrefs.GetInt("itemBrakesLevel" + i));
            _itemBrakesRarity.Add(PlayerPrefs.GetString("itemBrakesRarity" + i));
            _itemBrakesType.Add(PlayerPrefs.GetString("itemBrakesType" + i));
            _itemBrakesID.Add(PlayerPrefs.GetInt("itemBrakesID" + i));
        }
        #endregion

        #region ItemFuelSystemLevel
        List<int> _itemFuelSystemLevel = new List<int>();
        List<string> _itemFuelSystemRarity = new List<string>();
        List<string> _itemFuelSystemType = new List<string>();
        List<int> _itemFuelSystemID = new List<int>();

        for (int i = 0; i < PlayerPrefs.GetInt("itemCountFuelSystem"); i++)
        {
            _itemFuelSystemLevel.Add(PlayerPrefs.GetInt("itemFuelSystemLevel" + i));
            _itemFuelSystemRarity.Add(PlayerPrefs.GetString("itemFuelSystemRarity" + i));
            _itemFuelSystemType.Add(PlayerPrefs.GetString("itemFuelSystemType" + i));
            _itemFuelSystemID.Add(PlayerPrefs.GetInt("itemFuelSystemID" + i));
        }
        #endregion

        #region ItemSuspensionLevel
        List<int> _itemSuspensionLevel = new List<int>();
        List<string> _itemSuspensionRarity = new List<string>();
        List<string> _itemSuspensionType = new List<string>();
        List<int> _itemSuspensionID = new List<int>();

        for (int i = 0; i < PlayerPrefs.GetInt("itemCountSuspension"); i++)
        {
            _itemSuspensionLevel.Add(PlayerPrefs.GetInt("itemSuspensionLevel" + i));
            _itemSuspensionRarity.Add(PlayerPrefs.GetString("itemSuspensionRarity" + i));
            _itemSuspensionType.Add(PlayerPrefs.GetString("itemSuspensionType" + i));
            _itemSuspensionID.Add(PlayerPrefs.GetInt("itemSuspensionID" + i));
        }
        #endregion

        #region ItemTransmissionLevel
        List<int> _itemTransmissionLevel = new List<int>();
        List<string> _itemTransmissionRarity = new List<string>();
        List<string> _itemTransmissionType = new List<string>();
        List<int> _itemTransmissionID = new List<int>();

        for (int i = 0; i < PlayerPrefs.GetInt("itemCountTransmission"); i++)
        {
            _itemTransmissionLevel.Add(PlayerPrefs.GetInt("itemTransmissionLevel" + i));
            _itemTransmissionRarity.Add(PlayerPrefs.GetString("itemTransmissionRarity" + i));
            _itemTransmissionType.Add(PlayerPrefs.GetString("itemTransmissionType" + i));
            _itemTransmissionID.Add(PlayerPrefs.GetInt("itemTransmissionID" + i));
        }
        #endregion

        PlayerData playerData = new()
        {
            playerMoney = PlayerPrefs.GetInt("playerMoney"),
            playerHard = PlayerPrefs.GetInt("playerHard"),
            playerExp = PlayerPrefs.GetInt("playerExp"),
            playerLevel = PlayerPrefs.GetInt("playerLevel"),
            playerFuelCurrent = PlayerPrefs.GetInt("playerFuelCurrent"),

            drawingGunCount = PlayerPrefs.GetInt("drawingEngineCount"),
            drawingEngineCount = PlayerPrefs.GetInt("drawingEngineCount"),
            drawingBrakesCount = PlayerPrefs.GetInt("drawingEngineCount"),
            drawingFuelSystemCount = PlayerPrefs.GetInt("drawingEngineCount"),
            drawingSuspensionCount = PlayerPrefs.GetInt("drawingEngineCount"),
            drawingTransmissionCount = PlayerPrefs.GetInt("drawingEngineCount"),

            dionysusCarPurchased = PlayerPrefs.GetInt("DionysuscarPurchased"),
            taiowaCarPurchased = PlayerPrefs.GetInt("TaiowacarPurchased"),
            prunCarPurchased = PlayerPrefs.GetInt("P-RuncarPurchased"),
            lyssaCarPurchased = PlayerPrefs.GetInt("LyssacarPurchased"),
            aeolusCarPurchased = PlayerPrefs.GetInt("AeoluscarPurchased"),
            hyasCarPurchased = PlayerPrefs.GetInt("HyascarPurchased"),
            hemeraCarPurchased = PlayerPrefs.GetInt("HemeracarPurchased"),
            eosCarPurchased = PlayerPrefs.GetInt("EoscarPurchased"),

            activeCarID = PlayerPrefs.GetString("activeCarID"),

            dionysusCarLevel = PlayerPrefs.GetInt("DionysuscarLevel"),
            taiowaCarLevel = PlayerPrefs.GetInt("TaiowacarLevel"),
            prunCarLevel = PlayerPrefs.GetInt("P-RuncarLevel"),
            lyssaCarLevel = PlayerPrefs.GetInt("LyssacarLevel"),
            aeolusCarLevel = PlayerPrefs.GetInt("AeoluscarLevel"),
            hyasCarLevel = PlayerPrefs.GetInt("HyascarLevel"),
            hemeraCarLevel = PlayerPrefs.GetInt("HemeracarLevel"),
            eosCarLevel = PlayerPrefs.GetInt("EoscarLevel"),

            selectedCarId = PlayerPrefs.GetString("selectedCarID"),

            itemCountGun = PlayerPrefs.GetInt("itemCountGun"),
            itemCountEngine = PlayerPrefs.GetInt("itemCountEngine"),
            itemCountBrakes = PlayerPrefs.GetInt("itemCountBrakes"),
            itemCountFuelSystem = PlayerPrefs.GetInt("itemCountFuelSystem"),
            itemCountSuspension = PlayerPrefs.GetInt("itemCountSuspension"),
            itemCountTransmission = PlayerPrefs.GetInt("itemCountTransmission"),

            itemGunLevel = _itemGunLevel,
            itemEngineLevel = _itemEngineLevel,
            itemBrakesLevel = _itemBrakesLevel,
            itemFuelSystemLevel = _itemFuelSystemLevel,
            itemSuspensionLevel = _itemSuspensionLevel,
            itemTransmissionLevel = _itemTransmissionLevel,

            itemGunID = _itemGunID,
            itemEngineID = _itemEngineID,
            itemBrakesID = _itemBrakesID,
            itemFuelSystemID = _itemFuelSystemID,
            itemSuspensionID = _itemSuspensionID,
            itemTransmissionID = _itemTransmissionID,

            itemGunRarity = _itemGunRarity,
            itemBrakesRarity = _itemBrakesRarity,
            itemEngineRarity = _itemEngineRarity,
            itemFuelSystemRarity = _itemFuelSystemRarity,
            itemSuspensionRarity = _itemSuspensionRarity,
            itemTransmissionRarity = _itemTransmissionRarity,

            itemGunType = _itemGunType,
            itemEngineType = _itemEngineType,
            itemBrakesType = _itemBrakesType,
            itemFuelSystemType = _itemFuelSystemType,
            itemSuspensionType = _itemSuspensionType,
            itemTransmissionType = _itemTransmissionType,

            talentGlobalLevel = PlayerPrefs.GetInt("talentGlobalLevel"),
            talentDamageLevel = PlayerPrefs.GetInt("talentDamageLevel"),
            talentBlockLevel = PlayerPrefs.GetInt("talentBlockLevel"),
            talentCarImprovementLevel = PlayerPrefs.GetInt("talentCarImprovementLevel"),
            talentEquipmentImprovementLevel = PlayerPrefs.GetInt("talentEquipmentImprovementLevel"),
            talentGunSlotLevel = PlayerPrefs.GetInt("talentGunSlotLevel"),
            talentHealthLevel = PlayerPrefs.GetInt("talentHealthLevel"),
            talentIronLevel = PlayerPrefs.GetInt("talentIronLevel"),
            talentRecoveryHpInFirstAidKitLevel = PlayerPrefs.GetInt("talentRecoveryHpInFirstAidKitLevel"),
            talentShotSpeedLevel = PlayerPrefs.GetInt("talentShotSpeedLevel")            
        };

        Dictionary<string, object> data = new Dictionary<string, object>() { { PLAYER_CLOUD_KEY, playerData} };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);

        Debug.Log("SAVING SUCCESS");
    }

    public async void LoadData()
    {
        if (!PlayerPrefs.HasKey("playerMoney"))
        {
            PlayerPrefs.SetInt("playerMoney", 1000);
            PlayerPrefs.SetInt("playerHard", 20);
            PlayerPrefs.SetInt("playerFuelCurrent", 20);
            PlayerPrefs.SetInt("playerFuelMax", 20);
            PlayerPrefs.SetInt("playerLevel", 1);
        }

        Dictionary<string, string> data = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { PLAYER_CLOUD_KEY });

        List<ItemKey> list = await CloudSaveService.Instance.Data.Player.ListAllKeysAsync();

        bool isFind = false;

        foreach (ItemKey key in list)
        {
            Debug.Log("KEY = " + key.Key);
            if (key.Key == PLAYER_CLOUD_KEY)
            {
                isFind = true;
            }
        }

        if (isFind)
        {
            PlayerData player = JsonUtility.FromJson<PlayerData>(data[PLAYER_CLOUD_KEY]);

            #region LoadData
            PlayerPrefs.SetInt("playerMoney", player.playerMoney);
            PlayerPrefs.SetInt("playerHard", player.playerHard);
            PlayerPrefs.SetInt("playerExp", player.playerExp);
            PlayerPrefs.SetInt("playerLevel", player.playerLevel);
            PlayerPrefs.SetInt("playerFuelCurrent", player.playerFuelCurrent);

            PlayerPrefs.SetInt("drawingEngineCount", player.drawingEngineCount);
            PlayerPrefs.SetInt("drawingBrakesCount", player.drawingBrakesCount);
            PlayerPrefs.SetInt("drawingFuelSystemCount", player.drawingFuelSystemCount);
            PlayerPrefs.SetInt("drawingGunCount", player.drawingGunCount);
            PlayerPrefs.SetInt("drawingSuspensionCount", player.drawingSuspensionCount);
            PlayerPrefs.SetInt("drawingTransmissionCount", player.drawingTransmissionCount);

            PlayerPrefs.SetInt("DionysuscarPurchased", player.dionysusCarPurchased);
            PlayerPrefs.SetInt("TaiowacarPurchased", player.taiowaCarPurchased);
            PlayerPrefs.SetInt("P-RuncarPurchased", player.prunCarPurchased);
            PlayerPrefs.SetInt("LyssacarPurchased", player.lyssaCarPurchased);
            PlayerPrefs.SetInt("AeoluscarPurchased", player.aeolusCarPurchased);
            PlayerPrefs.SetInt("HyascarPurchased", player.hyasCarPurchased);
            PlayerPrefs.SetInt("HemeracarPurchased", player.hemeraCarPurchased);
            PlayerPrefs.SetInt("EoscarPurchased", player.eosCarPurchased);

            PlayerPrefs.SetString("activeCarID", player.activeCarID);

            PlayerPrefs.SetInt("DionysuscarLevel", player.dionysusCarLevel);
            PlayerPrefs.SetInt("TaiowacarLevel", player.taiowaCarLevel);
            PlayerPrefs.SetInt("P-RuncarLevel", player.prunCarLevel);
            PlayerPrefs.SetInt("LyssacarLevel", player.lyssaCarLevel);
            PlayerPrefs.SetInt("AeoluscarLevel", player.aeolusCarLevel);
            PlayerPrefs.SetInt("HyascarLevel", player.hyasCarLevel);
            PlayerPrefs.SetInt("HemeracarLevel", player.hemeraCarLevel);
            PlayerPrefs.SetInt("EoscarLevel", player.eosCarLevel);

            PlayerPrefs.SetString("selectedCarID", player.selectedCarId);

            PlayerPrefs.SetInt("itemCountGun", player.itemCountGun);
            PlayerPrefs.SetInt("itemCountEngine", player.itemCountEngine);
            PlayerPrefs.SetInt("itemCountBrakes", player.itemCountBrakes);
            PlayerPrefs.SetInt("itemCountFuelSystem", player.itemCountFuelSystem);            
            PlayerPrefs.SetInt("itemCountSuspension", player.itemCountSuspension);
            PlayerPrefs.SetInt("itemCountTransmission", player.itemCountTransmission);

            for (int i = 0; i < player.itemCountGun; i++)
            {
                PlayerPrefs.SetInt("itemGunID" + i, player.itemGunID[i]);
                PlayerPrefs.SetInt("itemGunLevel" + i, player.itemGunLevel[i]);
                PlayerPrefs.SetString("itemGunRarity" + i, player.itemGunRarity[i]);
                PlayerPrefs.SetString("itemGunType" + i, player.itemGunType[i]);
            }

            for (int i = 0; i < player.itemCountEngine; i++)
            {
                PlayerPrefs.SetInt("itemEngineID" + i, player.itemEngineID[i]);
                PlayerPrefs.SetInt("itemEngineLevel" + i, player.itemEngineLevel[i]);
                PlayerPrefs.SetString("itemEngineRarity" + i, player.itemEngineRarity[i]);
                PlayerPrefs.SetString("itemEngineType" + i, player.itemEngineType[i]);
            }

            for (int i = 0; i < player.itemCountBrakes; i++)
            {
                PlayerPrefs.SetInt("itemBrakesID" + i, player.itemBrakesID[i]);
                PlayerPrefs.SetInt("itemBrakesLevel" + i, player.itemBrakesLevel[i]);
                PlayerPrefs.SetString("itemBrakesRarity" + i, player.itemBrakesRarity[i]);
                PlayerPrefs.SetString("itemBrakesType" + i, player.itemBrakesType[i]);
            }

            for (int i = 0; i < player.itemCountFuelSystem; i++)
            {
                PlayerPrefs.SetInt("itemFuelSystemID" + i, player.itemFuelSystemID[i]);
                PlayerPrefs.SetInt("itemFuelSystemLevel" + i, player.itemFuelSystemLevel[i]);
                PlayerPrefs.SetString("itemFuelSystemRarity" + i, player.itemFuelSystemRarity[i]);
                PlayerPrefs.SetString("itemFuelSystemType" + i, player.itemFuelSystemType[i]);
            }            

            for (int i = 0; i < player.itemCountSuspension; i++)
            {
                PlayerPrefs.SetInt("itemSuspensionID" + i, player.itemSuspensionID[i]);
                PlayerPrefs.SetInt("itemSuspensionLevel" + i, player.itemSuspensionLevel[i]);
                PlayerPrefs.SetString("itemSuspensionRarity" + i, player.itemSuspensionRarity[i]);
                PlayerPrefs.SetString("itemSuspensionType" + i, player.itemSuspensionType[i]);
            }

            for (int i = 0; i < player.itemCountTransmission; i++)
            {
                PlayerPrefs.SetInt("itemTransmissionID" + i, player.itemTransmissionID[i]);
                PlayerPrefs.SetInt("itemTransmissionLevel" + i, player.itemTransmissionLevel[i]);
                PlayerPrefs.SetString("itemTransmissionRarity" + i, player.itemTransmissionRarity[i]);
                PlayerPrefs.SetString("itemTransmissionType" + i, player.itemTransmissionType[i]);
            }

            PlayerPrefs.SetInt("talentGlobalLevel", player.talentGlobalLevel);
            PlayerPrefs.SetInt("talentDamageLevel", player.talentDamageLevel);
            PlayerPrefs.SetInt("talentHealthLevel", player.talentHealthLevel);
            PlayerPrefs.SetInt("talentIronLevel", player.talentIronLevel);
            PlayerPrefs.SetInt("talentRecoveryHpInFirstAidKitLevel", player.talentRecoveryHpInFirstAidKitLevel);
            PlayerPrefs.SetInt("talentShotSpeedLevel", player.talentShotSpeedLevel);
            PlayerPrefs.SetInt("talentBlockLevel", player.talentBlockLevel);
            PlayerPrefs.SetInt("talentEquipmentImprovementLevel", player.talentEquipmentImprovementLevel);
            PlayerPrefs.SetInt("talentCarImprovementLevel", player.talentCarImprovementLevel);
            PlayerPrefs.SetInt("talentGunSlotLevel", player.talentGunSlotLevel);
            #endregion
        }

        Debug.Log("Data Load");
    }

    private void OnApplicationPause()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (UnityServices.State == ServicesInitializationState.Initializing && AuthenticationService.Instance.IsAuthorized)
        {
            SaveData();
        }             
#endif
    }

    private void OnApplicationQuit()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (UnityServices.State == ServicesInitializationState.Initializing && AuthenticationService.Instance.IsAuthorized)
        {
            SaveData();
        }             
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveData();
        }
    }
}

public class PlayerData
{
    //Base
    public int playerMoney;
    public int playerHard;
    public int playerExp;
    public int playerLevel;
    public int playerFuelCurrent;
    public int drawingGunCount;
    public int drawingBrakesCount;
    public int drawingEngineCount;
    public int drawingFuelSystemCount;
    public int drawingSuspensionCount;
    public int drawingTransmissionCount;

    //Cars
    public int dionysusCarPurchased;
    public int taiowaCarPurchased;
    public int prunCarPurchased;
    public int lyssaCarPurchased;
    public int aeolusCarPurchased;
    public int hyasCarPurchased;
    public int hemeraCarPurchased;
    public int eosCarPurchased;

    public string activeCarID;

    public int dionysusCarLevel;
    public int taiowaCarLevel;
    public int prunCarLevel;
    public int lyssaCarLevel;
    public int aeolusCarLevel;
    public int hyasCarLevel;
    public int hemeraCarLevel;
    public int eosCarLevel;

    public string selectedCarId;

    //Items
    public int itemCountGun;
    public int itemCountBrakes;
    public int itemCountEngine;
    public int itemCountFuelSystem;
    public int itemCountSuspension;
    public int itemCountTransmission;

    public List<int> itemGunLevel;
    public List<int> itemBrakesLevel;
    public List<int> itemEngineLevel;
    public List<int> itemFuelSystemLevel;
    public List<int> itemSuspensionLevel;
    public List<int> itemTransmissionLevel;

    public List<string> itemGunRarity;
    public List<string> itemBrakesRarity;
    public List<string> itemEngineRarity;
    public List<string> itemFuelSystemRarity;
    public List<string> itemSuspensionRarity;
    public List<string> itemTransmissionRarity;

    public List<int> itemGunID;
    public List<int> itemBrakesID;
    public List<int> itemEngineID;
    public List<int> itemFuelSystemID;
    public List<int> itemSuspensionID;
    public List<int> itemTransmissionID;

    public List<string> itemGunType;
    public List<string> itemBrakesType;
    public List<string> itemEngineType;
    public List<string> itemFuelSystemType;
    public List<string> itemSuspensionType;
    public List<string> itemTransmissionType;

    //Talents
    public int talentGlobalLevel;
    public int talentDamageLevel;
    public int talentHealthLevel;
    public int talentIronLevel;
    public int talentRecoveryHpInFirstAidKitLevel;
    public int talentShotSpeedLevel;
    public int talentBlockLevel;
    public int talentEquipmentImprovementLevel;
    public int talentCarImprovementLevel;
    public int talentGunSlotLevel;
}
