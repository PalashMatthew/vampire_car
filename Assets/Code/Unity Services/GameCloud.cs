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

            drawingGunCount = PlayerPrefs.GetInt("drawingGunCount"),
            drawingEngineCount = PlayerPrefs.GetInt("drawingEngineCount"),
            drawingBrakesCount = PlayerPrefs.GetInt("drawingBrakesCount"),
            drawingFuelSystemCount = PlayerPrefs.GetInt("drawingFuelSystemCount"),
            drawingSuspensionCount = PlayerPrefs.GetInt("drawingSuspensionCount"),
            drawingTransmissionCount = PlayerPrefs.GetInt("drawingTransmissionCount"),

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

            dionysusCarDamage = PlayerPrefs.GetFloat("DionysuscarDamage"),
            taiowaCarDamage = PlayerPrefs.GetFloat("TaiowacarDamage"),
            prunCarDamage = PlayerPrefs.GetFloat("P-RuncarDamage"),
            lyssaCarDamage = PlayerPrefs.GetFloat("LyssacarDamage"),
            aeolusCarDamage = PlayerPrefs.GetFloat("AeoluscarDamage"),
            hyasCarDamage = PlayerPrefs.GetFloat("HyascarDamage"),
            hemeraCarDamage = PlayerPrefs.GetFloat("HemeracarDamage"),
            eosCarDamage = PlayerPrefs.GetFloat("EoscarDamage"),

            dionysusCarDodge = PlayerPrefs.GetFloat("DionysuscarDodge"),
            taiowaCarDodge = PlayerPrefs.GetFloat("TaiowacarDodge"),
            prunCarDodge = PlayerPrefs.GetFloat("P-RuncarDodge"),
            lyssaCarDodge = PlayerPrefs.GetFloat("LyssacarDodge"),
            aeolusCarDodge = PlayerPrefs.GetFloat("AeoluscarDodge"),
            hyasCarDodge = PlayerPrefs.GetFloat("HyascarDodge"),
            hemeraCarDodge = PlayerPrefs.GetFloat("HemeracarDodge"),
            eosCarDodge = PlayerPrefs.GetFloat("EoscarDodge"),

            dionysusCarHealth = PlayerPrefs.GetFloat("DionysuscarHealth"),
            taiowaCarHealth = PlayerPrefs.GetFloat("TaiowacarHealth"),
            prunCarHealth = PlayerPrefs.GetFloat("P-RuncarHealth"),
            lyssaCarHealth = PlayerPrefs.GetFloat("LyssacarHealth"),
            aeolusCarHealth = PlayerPrefs.GetFloat("AeoluscarHealth"),
            hyasCarHealth = PlayerPrefs.GetFloat("HyascarHealth"),
            hemeraCarHealth = PlayerPrefs.GetFloat("HemeracarHealth"),
            eosCarHealth = PlayerPrefs.GetFloat("EoscarHealth"),

            dionysusCarKritChance = PlayerPrefs.GetFloat("DionysuscarKritChance"),
            taiowaCarKritChance = PlayerPrefs.GetFloat("TaiowacarKritChance"),
            prunCarKritChance = PlayerPrefs.GetFloat("P-RuncarKritChance"),
            lyssaCarKritChance = PlayerPrefs.GetFloat("LyssacarKritChance"),
            aeolusCarKritChance = PlayerPrefs.GetFloat("AeoluscarKritChance"),
            hyasCarKritChance = PlayerPrefs.GetFloat("HyascarKritChance"),
            hemeraCarKritChance = PlayerPrefs.GetFloat("HemeracarKritChance"),
            eosCarKritChance = PlayerPrefs.GetFloat("EoscarKritChance"),

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
            talentShotSpeedLevel = PlayerPrefs.GetInt("talentShotSpeedLevel"),

            unlockGun1000 = PlayerPrefs.GetInt("unlockGun1000"),
            unlockGun1001 = PlayerPrefs.GetInt("unlockGun1001"),
            unlockGun1002 = PlayerPrefs.GetInt("unlockGun1002"),
            unlockGun1003 = PlayerPrefs.GetInt("unlockGun1003"),
            unlockGun1004 = PlayerPrefs.GetInt("unlockGun1004"),
            unlockGun1005 = PlayerPrefs.GetInt("unlockGun1005"),
            unlockGun1006 = PlayerPrefs.GetInt("unlockGun1006"),
            unlockGun1007 = PlayerPrefs.GetInt("unlockGun1007"),
            unlockGun1008 = PlayerPrefs.GetInt("unlockGun1008"),
            unlockGun1009 = PlayerPrefs.GetInt("unlockGun1009"),
            unlockGun1010 = PlayerPrefs.GetInt("unlockGun1010"),
            unlockGun1011 = PlayerPrefs.GetInt("unlockGun1011"),
            unlockGun1012 = PlayerPrefs.GetInt("unlockGun1012"),
            unlockGun1013 = PlayerPrefs.GetInt("unlockGun1013"),
            unlockGun1014 = PlayerPrefs.GetInt("unlockGun1014"),
            unlockGun1015 = PlayerPrefs.GetInt("unlockGun1015"),

            unlockPassiveArmor = PlayerPrefs.GetInt("unlockPassiveArmor"),
            unlockPassiveAttackSpeed = PlayerPrefs.GetInt("unlockPassiveAttackSpeed"),
            unlockPassiveVampirizm = PlayerPrefs.GetInt("unlockPassiveVampirizm"),
            unlockPassiveRage = PlayerPrefs.GetInt("unlockPassiveRage"),
            unlockPassiveMaxHpUp = PlayerPrefs.GetInt("unlockPassiveMaxHpUp"),
            unlockPassiveMassEnemyDamage = PlayerPrefs.GetInt("unlockPassiveMassEnemyDamage"),
            unlockPassiveLucky = PlayerPrefs.GetInt("unlockPassiveLucky"),
            unlockPassiveKritDamageUp = PlayerPrefs.GetInt("unlockPassiveKritDamageUp"),
            unlockPassiveKritChanceUp = PlayerPrefs.GetInt("unlockPassiveKritChanceUp"),
            unlockPassiveBackDamage = PlayerPrefs.GetInt("unlockPassiveBackDamage"),
            unlockPassiveDamageUp = PlayerPrefs.GetInt("unlockPassiveDamageUp"),
            unlockPassiveDistanceDamage = PlayerPrefs.GetInt("unlockPassiveDistanceDamage"),
            unlockPassiveDodge = PlayerPrefs.GetInt("unlockPassiveDodge"),
            unlockPassiveEffectsDuration = PlayerPrefs.GetInt("unlockPassiveEffectsDuration"),
            unlockPassiveHeadshot = PlayerPrefs.GetInt("unlockPassiveHeadshot"),
            unlockPassiveHealthRecovery = PlayerPrefs.GetInt("unlockPassiveHealthRecovery"),

            unlockNewItem = PlayerPrefs.GetString("unlockNewItems"),

            tutorialGameComplite = PlayerPrefs.GetString("tutorialComplite"),
            tutorialHubComplite = PlayerPrefs.GetString("tutorialHubComplite"),
            tutorialLoc1Complite = PlayerPrefs.GetString("tutorialLoc1Complite"),
            tutorialCards = PlayerPrefs.GetString("tutorialCards"),

            maxLocation = PlayerPrefs.GetInt("maxLocation")
    };

        Dictionary<string, object> data = new Dictionary<string, object>() { { PLAYER_CLOUD_KEY, playerData} };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);

        Debug.Log("SAVING SUCCESS");
    }

    public async void LoadData()
    {       
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

            PlayerPrefs.SetFloat("DionysuscarDamage", player.dionysusCarDamage);
            PlayerPrefs.SetFloat("TaiowacarDamage", player.taiowaCarDamage);
            PlayerPrefs.SetFloat("P-RuncarDamage", player.prunCarDamage);
            PlayerPrefs.SetFloat("LyssacarDamage", player.lyssaCarDamage);
            PlayerPrefs.SetFloat("AeoluscarDamage", player.aeolusCarDamage);
            PlayerPrefs.SetFloat("HyascarDamage", player.hyasCarDamage);
            PlayerPrefs.SetFloat("HemeracarDamage", player.hemeraCarDamage);
            PlayerPrefs.SetFloat("EoscarDamage", player.eosCarDamage);

            PlayerPrefs.SetFloat("DionysuscarHealth", player.dionysusCarHealth);
            PlayerPrefs.SetFloat("TaiowacarHealth", player.taiowaCarHealth);
            PlayerPrefs.SetFloat("P-RuncarHealth", player.prunCarHealth);
            PlayerPrefs.SetFloat("LyssacarHealth", player.lyssaCarHealth);
            PlayerPrefs.SetFloat("AeoluscarHealth", player.aeolusCarHealth);
            PlayerPrefs.SetFloat("HyascarHealth", player.hyasCarHealth);
            PlayerPrefs.SetFloat("HemeracarHealth", player.hemeraCarHealth);
            PlayerPrefs.SetFloat("EoscarHealth", player.eosCarHealth);

            PlayerPrefs.SetFloat("DionysuscarKritChance", player.dionysusCarKritChance);
            PlayerPrefs.SetFloat("TaiowacarKritChance", player.taiowaCarKritChance);
            PlayerPrefs.SetFloat("P-RuncarKritChance", player.prunCarKritChance);
            PlayerPrefs.SetFloat("LyssacarKritChance", player.lyssaCarKritChance);
            PlayerPrefs.SetFloat("AeoluscarKritChance", player.aeolusCarKritChance);
            PlayerPrefs.SetFloat("HyascarKritChance", player.hyasCarKritChance);
            PlayerPrefs.SetFloat("HemeracarKritChance", player.hemeraCarKritChance);
            PlayerPrefs.SetFloat("EoscarKritChance", player.eosCarKritChance);

            PlayerPrefs.SetFloat("DionysuscarDodge", player.dionysusCarDodge);
            PlayerPrefs.SetFloat("TaiowacarDodge", player.taiowaCarDodge);
            PlayerPrefs.SetFloat("P-RuncarDodge", player.prunCarDodge);
            PlayerPrefs.SetFloat("LyssacarDodge", player.lyssaCarDodge);
            PlayerPrefs.SetFloat("AeoluscarDodge", player.aeolusCarDodge);
            PlayerPrefs.SetFloat("HyascarDodge", player.hyasCarDodge);
            PlayerPrefs.SetFloat("HemeracarDodge", player.hemeraCarDodge);
            PlayerPrefs.SetFloat("EoscarDodge", player.eosCarDodge);

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

            PlayerPrefs.SetInt("unlockGun1000", player.unlockGun1000);
            PlayerPrefs.SetInt("unlockGun1001", player.unlockGun1001);
            PlayerPrefs.SetInt("unlockGun1002", player.unlockGun1002);
            PlayerPrefs.SetInt("unlockGun1003", player.unlockGun1003);
            PlayerPrefs.SetInt("unlockGun1004", player.unlockGun1004);
            PlayerPrefs.SetInt("unlockGun1005", player.unlockGun1005);
            PlayerPrefs.SetInt("unlockGun1006", player.unlockGun1006);
            PlayerPrefs.SetInt("unlockGun1007", player.unlockGun1007);
            PlayerPrefs.SetInt("unlockGun1008", player.unlockGun1008);
            PlayerPrefs.SetInt("unlockGun1009", player.unlockGun1009);
            PlayerPrefs.SetInt("unlockGun1010", player.unlockGun1010);
            PlayerPrefs.SetInt("unlockGun1011", player.unlockGun1011);
            PlayerPrefs.SetInt("unlockGun1012", player.unlockGun1012);
            PlayerPrefs.SetInt("unlockGun1013", player.unlockGun1013);
            PlayerPrefs.SetInt("unlockGun1014", player.unlockGun1014);
            PlayerPrefs.SetInt("unlockGun1015", player.unlockGun1015);

            PlayerPrefs.SetInt("unlockPassiveArmor", player.unlockPassiveArmor);
            PlayerPrefs.SetInt("unlockPassiveAttackSpeed", player.unlockPassiveAttackSpeed);
            PlayerPrefs.SetInt("unlockPassiveBackDamage", player.unlockPassiveBackDamage);
            PlayerPrefs.SetInt("unlockPassiveDamageUp", player.unlockPassiveDamageUp);
            PlayerPrefs.SetInt("unlockPassiveDistanceDamage", player.unlockPassiveDistanceDamage);
            PlayerPrefs.SetInt("unlockPassiveDodge", player.unlockPassiveDodge);
            PlayerPrefs.SetInt("unlockPassiveEffectsDuration", player.unlockPassiveEffectsDuration);
            PlayerPrefs.SetInt("unlockPassiveHeadshot", player.unlockPassiveHeadshot);
            PlayerPrefs.SetInt("unlockPassiveHealthRecovery", player.unlockPassiveHealthRecovery);
            PlayerPrefs.SetInt("unlockPassiveKritChanceUp", player.unlockPassiveKritChanceUp);
            PlayerPrefs.SetInt("unlockPassiveKritDamageUp", player.unlockPassiveKritDamageUp);
            PlayerPrefs.SetInt("unlockPassiveLucky", player.unlockPassiveLucky);
            PlayerPrefs.SetInt("unlockPassiveMassEnemyDamage", player.unlockPassiveMassEnemyDamage);
            PlayerPrefs.SetInt("unlockPassiveMaxHpUp", player.unlockPassiveMaxHpUp);
            PlayerPrefs.SetInt("unlockPassiveRage", player.unlockPassiveRage);
            PlayerPrefs.SetInt("unlockPassiveVampirizm", player.unlockPassiveVampirizm);

            PlayerPrefs.SetString("unlockNewItems", player.unlockNewItem);

            PlayerPrefs.SetString("tutorialComplite", player.tutorialGameComplite);
            PlayerPrefs.SetString("tutorialHubComplite", player.tutorialHubComplite);
            PlayerPrefs.SetString("tutorialLoc1Complite", player.tutorialLoc1Complite);
            PlayerPrefs.SetString("tutorialCards", player.tutorialCards);

            PlayerPrefs.SetInt("maxLocation", player.maxLocation);
            #endregion
        }
        else
        {
            if (!PlayerPrefs.HasKey("playerMoney"))
            {
                PlayerPrefs.SetInt("maxLocation", 1);

                PlayerPrefs.SetInt("soundSettings", 1);
                PlayerPrefs.SetInt("musicSettings", 1);

                PlayerPrefs.SetInt("playerMoney", 1000);
                PlayerPrefs.SetInt("playerHard", 20);
                PlayerPrefs.SetInt("playerFuelCurrent", 20);
                PlayerPrefs.SetInt("playerFuelMax", 20);
                PlayerPrefs.SetInt("playerLevel", 1);

                PlayerPrefs.SetInt("unlockGun1000", 1);
                PlayerPrefs.SetInt("unlockGun1001", 1);
                PlayerPrefs.SetInt("unlockGun1002", 1);
                PlayerPrefs.SetInt("unlockGun1003", 0);
                PlayerPrefs.SetInt("unlockGun1004", 1);
                PlayerPrefs.SetInt("unlockGun1005", 0);
                PlayerPrefs.SetInt("unlockGun1006", 0);
                PlayerPrefs.SetInt("unlockGun1007", 0);
                PlayerPrefs.SetInt("unlockGun1008", 0);
                PlayerPrefs.SetInt("unlockGun1009", 1);
                PlayerPrefs.SetInt("unlockGun1010", 1);
                PlayerPrefs.SetInt("unlockGun1011", 0);
                PlayerPrefs.SetInt("unlockGun1012", 0);
                PlayerPrefs.SetInt("unlockGun1013", 0);
                PlayerPrefs.SetInt("unlockGun1014", 1);
                PlayerPrefs.SetInt("unlockGun1015", 0);

                PlayerPrefs.SetInt("unlockPassiveArmor", 0);
                PlayerPrefs.SetInt("unlockPassiveAttackSpeed", 1);
                PlayerPrefs.SetInt("unlockPassiveBackDamage", 0);
                PlayerPrefs.SetInt("unlockPassiveDamageUp", 1);
                PlayerPrefs.SetInt("unlockPassiveDistanceDamage", 1);
                PlayerPrefs.SetInt("unlockPassiveDodge", 0);
                PlayerPrefs.SetInt("unlockPassiveEffectsDuration", 0);
                PlayerPrefs.SetInt("unlockPassiveHeadshot", 1);
                PlayerPrefs.SetInt("unlockPassiveHealthRecovery", 1);
                PlayerPrefs.SetInt("unlockPassiveKritChanceUp", 1);
                PlayerPrefs.SetInt("unlockPassiveKritDamageUp", 1);
                PlayerPrefs.SetInt("unlockPassiveLucky", 0);
                PlayerPrefs.SetInt("unlockPassiveMassEnemyDamage", 0);
                PlayerPrefs.SetInt("unlockPassiveMaxHpUp", 1);
                PlayerPrefs.SetInt("unlockPassiveRage", 0);
                PlayerPrefs.SetInt("unlockPassiveVampirizm", 1);

                PlayerPrefs.SetString("unlockNewItems", "false");

                PlayerPrefs.SetString("tutorialComplite", "false");
                PlayerPrefs.SetString("tutorialHubComplite", "false");
                PlayerPrefs.SetString("tutorialLoc1Complite", "false");
                PlayerPrefs.SetString("tutorialCards", "false");
            }
        }

        Debug.Log("Data Load");
        InitScene.initCount++;
    }

    private void OnApplicationPause()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Application.loadedLevelName != "InitScene")
        {
            SaveData();
        }          
#endif
    }

    private void OnApplicationQuit()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Application.loadedLevelName != "InitScene")
        {
            SaveData();
        }          
#endif
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
    public int maxLocation;

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

    public float dionysusCarDamage;
    public float taiowaCarDamage;
    public float prunCarDamage;
    public float lyssaCarDamage;
    public float aeolusCarDamage;
    public float hyasCarDamage;
    public float hemeraCarDamage;
    public float eosCarDamage;

    public float dionysusCarHealth;
    public float taiowaCarHealth;
    public float prunCarHealth;
    public float lyssaCarHealth;
    public float aeolusCarHealth;
    public float hyasCarHealth;
    public float hemeraCarHealth;
    public float eosCarHealth;

    public float dionysusCarKritChance;
    public float taiowaCarKritChance;
    public float prunCarKritChance;
    public float lyssaCarKritChance;
    public float aeolusCarKritChance;
    public float hyasCarKritChance;
    public float hemeraCarKritChance;
    public float eosCarKritChance;

    public float dionysusCarDodge;
    public float taiowaCarDodge;
    public float prunCarDodge;
    public float lyssaCarDodge;
    public float aeolusCarDodge;
    public float hyasCarDodge;
    public float hemeraCarDodge;
    public float eosCarDodge;

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

    //Unlock Items
    public int unlockGun1000;
    public int unlockGun1001;
    public int unlockGun1002;
    public int unlockGun1003;
    public int unlockGun1004;
    public int unlockGun1005;
    public int unlockGun1006;
    public int unlockGun1007;
    public int unlockGun1008;
    public int unlockGun1009;
    public int unlockGun1010;
    public int unlockGun1011;
    public int unlockGun1012;
    public int unlockGun1013;
    public int unlockGun1014;
    public int unlockGun1015;

    public int unlockPassiveArmor;
    public int unlockPassiveAttackSpeed;
    public int unlockPassiveBackDamage;
    public int unlockPassiveDamageUp;
    public int unlockPassiveDistanceDamage;
    public int unlockPassiveDodge;
    public int unlockPassiveEffectsDuration;
    public int unlockPassiveHeadshot;
    public int unlockPassiveHealthRecovery;
    public int unlockPassiveKritChanceUp;
    public int unlockPassiveKritDamageUp;
    public int unlockPassiveLucky;
    public int unlockPassiveMassEnemyDamage;
    public int unlockPassiveMaxHpUp;
    public int unlockPassiveRage;
    public int unlockPassiveVampirizm;

    public string unlockNewItem;

    public string tutorialGameComplite;
    public string tutorialHubComplite;
    public string tutorialLoc1Complite;
    public string tutorialCards;
}
