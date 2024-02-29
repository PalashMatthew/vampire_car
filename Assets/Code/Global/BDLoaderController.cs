using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;
using System.Globalization;
using System.Numerics;

public class BDLoaderController : MonoBehaviour
{
    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {       
        if (!PlayerPrefs.HasKey("activeCarID"))
        {
            //PlayerPrefs.SetString("activeCarID", "Dionysus");
            //PlayerPrefs.SetString("selectedCarID", "Dionysus");

            //PlayerPrefs.SetInt("DionysuscarLevel", 1);
            //PlayerPrefs.SetInt("TaiowacarLevel", 1);
            //PlayerPrefs.SetInt("P-RuncarLevel", 1);
            //PlayerPrefs.SetInt("LyssacarLevel", 1);
            //PlayerPrefs.SetInt("AeoluscarLevel", 1);
            //PlayerPrefs.SetInt("HyascarLevel", 1);
            //PlayerPrefs.SetInt("HemeracarLevel", 1);
            //PlayerPrefs.SetInt("EoscarLevel", 1);

            //PlayerPrefs.SetInt("DionysuscarPurchased", 1);
            //PlayerPrefs.SetInt("TaiowacarPurchased", 2);
            //PlayerPrefs.SetInt("P-RuncarPurchased", 2);
            //PlayerPrefs.SetInt("LyssacarPurchased", 2);
            //PlayerPrefs.SetInt("AeoluscarPurchased", 2);
            //PlayerPrefs.SetInt("HyascarPurchased", 2);
            //PlayerPrefs.SetInt("HemeracarPurchased", 2);
            //PlayerPrefs.SetInt("EoscarPurchased", 2);
        }
    }

    public void LoadCarInfo(string _json)
    {
        var _file = JSON.Parse(_json);        

        for (int a = 1; a < _file["values"].Count; a++)
        {
            List<float> _carInfo = new List<float>();
            List<string> _carNameInfo = new List<string>();
            List<string> _carUpgradeNameInfo = new List<string>();

            var itemo = JSON.Parse(_file["values"][a].ToString());

            string s = itemo.ToString();

            char[] _info = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                _info[i] = s[i];
            }

            string s1 = "";

            int cellNum = 1;

            for (int i = 0; i < _info.Length; i++)
            {
                s1 += _info[i];

                if (_info[i] == '"' && _info[i + 1] != ',' && _info[i + 1] != ']')
                {
                    int cellStart = i + 1;
                    int cellEnd = 0;

                    for (int r = cellStart; r < _info.Length; r++)
                    {
                        if (_info[r] == '"')
                        {
                            cellEnd = r;
                            goto l1;
                        }
                    }
                    l1:

                    string s2 = "";

                    for (int u = cellStart; u < cellEnd; u++)
                    {
                        s2 += _info[u];
                    }

                    

                    if (i == 1)
                    {
                        _carNameInfo.Add(s2);
                    }
                    else if (cellNum == 14 || cellNum == 16 || cellNum == 18 || cellNum == 20)
                    {
                        _carUpgradeNameInfo.Add(s2);
                    }
                    else
                    {
                        _carInfo.Add(float.Parse(s2, CultureInfo.InvariantCulture.NumberFormat));
                    }

                    cellNum++;
                }
            }

            for (int i = 0; i < _carInfo.Count; i++)
            {
                string _variableName = "";

                if (i == 0)
                {
                    _variableName = _carNameInfo[0] + "carDamage";
                }

                if (i == 1)
                {
                    _variableName = _carNameInfo[0] + "carHealth";
                }

                if (i == 2)
                {
                    _variableName = _carNameInfo[0] + "carKritChance";
                }

                if (i == 3)
                {
                    _variableName = _carNameInfo[0] + "carDodge";
                }

                if (i == 4)
                {
                    _variableName = _carNameInfo[0] + "carDamageMax";
                }

                if (i == 5)
                {
                    _variableName = _carNameInfo[0] + "carHealthMax";
                }

                if (i == 6)
                {
                    _variableName = _carNameInfo[0] + "carKritChanceMax";
                }

                if (i == 7)
                {
                    _variableName = _carNameInfo[0] + "carDodgeMax";
                }

                if (i == 8)
                {
                    _variableName = _carNameInfo[0] + "carDamageStepUp";
                }

                if (i == 9)
                {
                    _variableName = _carNameInfo[0] + "carHealthStepUp";
                }

                if (i == 10)
                {
                    _variableName = _carNameInfo[0] + "carKritChanceStepUp";
                }

                if (i == 11)
                {
                    _variableName = _carNameInfo[0] + "carDodgeStepUp";
                }

                if (i == 12)
                {
                    PlayerPrefs.SetString(_carNameInfo[0] + "carUpgrade10lvlId", _carUpgradeNameInfo[0]);
                    _variableName = _carNameInfo[0] + "carUpgrade10lvl";
                }

                if (i == 13)
                {
                    PlayerPrefs.SetString(_carNameInfo[0] + "carUpgrade20lvlId", _carUpgradeNameInfo[1]);
                    _variableName = _carNameInfo[0] + "carUpgrade20lvl";
                }

                if (i == 14)
                {
                    PlayerPrefs.SetString(_carNameInfo[0] + "carUpgrade30lvlId", _carUpgradeNameInfo[2]);
                    _variableName = _carNameInfo[0] + "carUpgrade30lvl";
                }

                if (i == 15)
                {
                    PlayerPrefs.SetString(_carNameInfo[0] + "carUpgrade40lvlId", _carUpgradeNameInfo[3]);
                    _variableName = _carNameInfo[0] + "carUpgrade40lvl";
                }

                //Debug.Log(_variableName + _carInfo[i]);
                PlayerPrefs.SetFloat(_variableName, _carInfo[i]);
            }
        }
     }

    public void LoadGunUpgradeInfo(string _json)
    {
        var _file = JSON.Parse(_json);

        for (int a = 2; a < _file["values"].Count; a++)
        {
            List<int> _gunUpgradeInfo = new List<int>();
            List<string> _gunNameInfo = new List<string>();

            var itemo = JSON.Parse(_file["values"][a].ToString());

            string s = itemo.ToString();

            char[] _info = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                _info[i] = s[i];
            }

            string s1 = "";

            for (int i = 0; i < _info.Length; i++)
            {
                s1 += _info[i];               

                if (_info[i] == '"' && _info[i + 1] != ',' && _info[i + 1] != ']')
                {
                    int cellStart = i + 1;
                    int cellEnd = 0;

                    for (int r = cellStart; r < _info.Length; r++)
                    {
                        if (_info[r] == '"')
                        {
                            cellEnd = r;
                            goto l1;
                        }
                    }
                    l1:

                    string s2 = "";

                    for (int u = cellStart; u < cellEnd; u++)
                    {
                        s2 += _info[u];
                    }

                    if (s2 == "")
                    {
                        s2 = "0";
                    }                    

                    if (i == 1)
                    {
                        _gunNameInfo.Add(s2);
                    } 
                    else
                    {
                        _gunUpgradeInfo.Add(Int32.Parse(s2));
                    }                                    
                }
            }

            for (int i = 0; i < _gunUpgradeInfo.Count; i++)
            {
                string _variableName = "";

                if (i == 0)
                {
                    _variableName = _gunNameInfo[0] + "lv1_1_common";
                }

                if (i == 1)
                {
                    _variableName = _gunNameInfo[0] + "lv1_1_rare";
                }

                if (i == 2)
                {
                    _variableName = _gunNameInfo[0] + "lv1_1_legendary";
                }

                if (i == 3)
                {
                    _variableName = _gunNameInfo[0] + "lv1_2_common";
                }

                if (i == 4)
                {
                    _variableName = _gunNameInfo[0] + "lv1_2_rare";
                }

                if (i == 5)
                {
                    _variableName = _gunNameInfo[0] + "lv1_2_legendary";
                }

                if (i == 6)
                {
                    _variableName = _gunNameInfo[0] + "lv2_1_common";
                }

                if (i == 7)
                {
                    _variableName = _gunNameInfo[0] + "lv2_1_rare";
                }

                if (i == 8)
                {
                    _variableName = _gunNameInfo[0] + "lv2_1_legendary";
                }

                if (i == 9)
                {
                    _variableName = _gunNameInfo[0] + "lv2_2_common";
                }

                if (i == 10)
                {
                    _variableName = _gunNameInfo[0] + "lv2_2_rare";
                }

                if (i == 11)
                {
                    _variableName = _gunNameInfo[0] + "lv2_2_legendary";
                }

                if (i == 12)
                {
                    _variableName = _gunNameInfo[0] + "lv3_1_common";
                }

                if (i == 13)
                {
                    _variableName = _gunNameInfo[0] + "lv3_1_rare";
                }

                if (i == 14)
                {
                    _variableName = _gunNameInfo[0] + "lv3_1_legendary";
                }

                if (i == 15)
                {
                    _variableName = _gunNameInfo[0] + "lv3_2_common";
                }

                if (i == 16)
                {
                    _variableName = _gunNameInfo[0] + "lv3_2_rare";
                }

                if (i == 17)
                {
                    _variableName = _gunNameInfo[0] + "lv3_2_legendary";
                }

                PlayerPrefs.SetFloat(_variableName, _gunUpgradeInfo[i]);
            }
        }
    }

    public void LoadPassiveUpgradeInfo(string _json)
    {
        var _file = JSON.Parse(_json);

        for (int a = 1; a < _file["values"].Count; a++)
        {
            List<float> _passiveUpgradeInfo = new List<float>();
            List<string> _passiveNameInfo = new List<string>();

            var itemo = JSON.Parse(_file["values"][a].ToString());

            string s = itemo.ToString();

            char[] _info = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                _info[i] = s[i];
            }

            string s1 = "";

            for (int i = 0; i < _info.Length; i++)
            {
                s1 += _info[i];

                if (_info[i] == '"' && _info[i + 1] != ',' && _info[i + 1] != ']')
                {
                    int cellStart = i + 1;
                    int cellEnd = 0;

                    for (int r = cellStart; r < _info.Length; r++)
                    {
                        if (_info[r] == '"')
                        {
                            cellEnd = r;
                            goto l1;
                        }
                    }
                    l1:

                    string s2 = "";

                    for (int u = cellStart; u < cellEnd; u++)
                    {
                        s2 += _info[u];
                    }

                    if (i == 1)
                    {
                        _passiveNameInfo.Add(s2);
                    } 
                    else
                    {
                        _passiveUpgradeInfo.Add(float.Parse(s2, CultureInfo.InvariantCulture.NumberFormat));
                    }                    
                }
            }

            for (int i = 0; i < _passiveUpgradeInfo.Count; i++)
            {
                string _variableName = "";

                if (i == 0)
                {
                    _variableName = _passiveNameInfo[0] + "passiveUpgradeCommon";
                }

                if (i == 1)
                {
                    _variableName = _passiveNameInfo[0] + "passiveUpgradeRare";
                }

                if (i == 2)
                {
                    _variableName = _passiveNameInfo[0] + "passiveUpgradeLegendary";
                }

                //Debug.Log(_variableName + " = " + _passiveUpgradeInfo[i]);

                PlayerPrefs.SetFloat(_variableName, _passiveUpgradeInfo[i]);
            }
        }
    }

    public void LoadGunBaseSettingsInfo(string _json)
    {
        var _file = JSON.Parse(_json);

        for (int a = 1; a < _file["values"].Count; a++)
        {
            List<float> _gunSettingsInfo = new List<float>();
            List<string> _gunNameInfo = new List<string>();

            var itemo = JSON.Parse(_file["values"][a].ToString());

            string s = itemo.ToString();

            char[] _info = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                _info[i] = s[i];
            }

            string s1 = "";

            for (int i = 0; i < _info.Length; i++)
            {
                s1 += _info[i];

                if (_info[i] == '"' && _info[i + 1] != ',' && _info[i + 1] != ']')
                {
                    int cellStart = i + 1;
                    int cellEnd = 0;

                    for (int r = cellStart; r < _info.Length; r++)
                    {
                        if (_info[r] == '"')
                        {
                            cellEnd = r;
                            goto l1;
                        }
                    }
                    l1:

                    string s2 = "";

                    for (int u = cellStart; u < cellEnd; u++)
                    {
                        s2 += _info[u];
                    }

                    if (i == 1)
                    {
                        _gunNameInfo.Add(s2);
                    }
                    else
                    {
                        _gunSettingsInfo.Add(float.Parse(s2, CultureInfo.InvariantCulture.NumberFormat));
                    }
                }
            }

            for (int i = 0; i < _gunSettingsInfo.Count; i++)
            {
                string _variableName = "";

                if (i == 0)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsDamage";
                }

                if (i == 1)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsShotSpeed";
                }

                if (i == 2)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsBulletMoveSpeed";
                }

                if (i == 3)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsTimeOfAction";
                }

                if (i == 4)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsFreezeTime";
                }

                if (i == 5)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsProjectile";
                }

                if (i == 6)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsArea";
                }

                if (i == 7)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsRotateSpeed";
                }

                if (i == 8)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsRicochet";
                }

                if (i == 9)
                {
                    _variableName = _gunNameInfo[0] + "gunBaseSettingsMultiplyDamage";
                }

                //Debug.Log(_variableName + " = " + _passiveUpgradeInfo[i]);

                PlayerPrefs.SetFloat(_variableName, _gunSettingsInfo[i]);
            }
        }
    }

    public void LoadTalentsInfo(string _json)
    {
        var _file = JSON.Parse(_json);

        for (int a = 1; a < _file["values"].Count; a++)
        {
            List<int> _talentsValue = new List<int>();
            string _talentsName = "";

            var itemo = JSON.Parse(_file["values"][a].ToString());

            string s = itemo.ToString();

            char[] _info = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                _info[i] = s[i];
            }

            string s1 = "";

            for (int i = 0; i < _info.Length; i++)
            {
                s1 += _info[i];

                if (_info[i] == '"' && _info[i + 1] != ',' && _info[i + 1] != ']')
                {
                    int cellStart = i + 1;
                    int cellEnd = 0;

                    for (int r = cellStart; r < _info.Length; r++)
                    {
                        if (_info[r] == '"')
                        {
                            cellEnd = r;
                            goto l1;
                        }
                    }
                    l1:

                    string s2 = "";

                    for (int u = cellStart; u < cellEnd; u++)
                    {
                        s2 += _info[u];
                    }

                    if (s2 == "")
                    {
                        s2 = "0";
                    }

                    if (i == 1)
                    {
                        _talentsName = s2;
                    } 
                    else
                    {
                        _talentsValue.Add(Int32.Parse(s2));
                    }
                }
            }

            PlayerPrefs.SetString("talent" + a + "name", _talentsName);

            for (int i = 0; i < _talentsValue.Count; i++)
            {
                if (i == 0)
                {
                    PlayerPrefs.SetInt("talent" + a + "price", _talentsValue[0]);
                }

                if (i == 1)
                {
                    PlayerPrefs.SetInt("talent" + a + "value", _talentsValue[1]);
                }
            }
        }
    }

    public void LoadDropSystemInfo(string _json)
    {
        var _file = JSON.Parse(_json);

        for (int a = 1; a < _file["values"].Count; a++)
        {
            List<float> _dropSystemInfo = new List<float>();

            var itemo = JSON.Parse(_file["values"][a].ToString());

            string s = itemo.ToString();

            char[] _info = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                _info[i] = s[i];
            }

            string s1 = "";

            for (int i = 0; i < _info.Length; i++)
            {
                s1 += _info[i];

                if (_info[i] == '"' && _info[i + 1] != ',' && _info[i + 1] != ']')
                {
                    int cellStart = i + 1;
                    int cellEnd = 0;

                    for (int r = cellStart; r < _info.Length; r++)
                    {
                        if (_info[r] == '"')
                        {
                            cellEnd = r;
                            goto l1;
                        }
                    }
                    l1:

                    string s2 = "";

                    for (int u = cellStart; u < cellEnd; u++)
                    {
                        s2 += _info[u];
                    }

                    _dropSystemInfo.Add(float.Parse(s2, CultureInfo.InvariantCulture.NumberFormat));
                }
            }

            for (int i = 0; i < _dropSystemInfo.Count; i++)
            {
                if (i == 0)
                {
                    PlayerPrefs.SetFloat(a + "dropSystemMoneyMin", _dropSystemInfo[i]);
                }

                if (i == 1)
                {
                    PlayerPrefs.SetFloat(a + "dropSystemMoneyMax", _dropSystemInfo[i]);
                }

                if (i == 2)
                {
                    PlayerPrefs.SetFloat(a + "dropSystemExpMin", _dropSystemInfo[i]);
                }

                if (i == 3)
                {
                    PlayerPrefs.SetFloat(a + "dropSystemExpMax", _dropSystemInfo[i]);
                }

                if (i == 4)
                {
                    PlayerPrefs.SetFloat(a + "dropSystemDrawingMin", _dropSystemInfo[i]);
                }

                if (i == 5)
                {
                    PlayerPrefs.SetFloat(a + "dropSystemDrawingMax", _dropSystemInfo[i]);
                }

                if (i == 6)
                {
                    PlayerPrefs.SetFloat(a + "dropSystemTitanMin", _dropSystemInfo[i]);
                }

                if (i == 7)
                {
                    PlayerPrefs.SetFloat(a + "dropSystemTitanMax", _dropSystemInfo[i]);
                }
            }
        }
    }

    public void LoadLocalization(string _json)
    {
        var _file = JSON.Parse(_json);

        for (int a = 1; a < _file["values"].Count; a++)
        {
            List<string> _locInfo = new List<string>();

            var itemo = JSON.Parse(_file["values"][a].ToString());

            string s = itemo.ToString();

            char[] _info = new char[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                _info[i] = s[i];
            }

            string s1 = "";

            for (int i = 0; i < _info.Length; i++)
            {
                s1 += _info[i];

                if (_info[i] == '"' && _info[i + 1] != ',' && _info[i + 1] != ']')
                {
                    int cellStart = i + 1;
                    int cellEnd = 0;

                    for (int r = cellStart; r < _info.Length; r++)
                    {
                        if (_info[r] == '"')
                        {
                            cellEnd = r;
                            goto l1;
                        }
                    }
                    l1:

                    string s2 = "";

                    for (int u = cellStart; u < cellEnd; u++)
                    {
                        s2 += _info[u];
                    }

                    _locInfo.Add(s2);
                }
            }

            PlayerPrefs.SetString("ru" + _locInfo[0], _locInfo[1]);
            PlayerPrefs.SetString("en" + _locInfo[0], _locInfo[2]);
        }

        if (!PlayerPrefs.HasKey("activeLang"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                PlayerPrefs.SetString("activeLang", "ru");
            }
            else
            {
                PlayerPrefs.SetString("activeLang", "en");
            }
        }
    }
}
