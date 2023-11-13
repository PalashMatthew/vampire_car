using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCharacteristics : MonoBehaviour
{
    public List<string> carName;

    public List<DetailCard> engineCards;

    private void Start()
    {
        GlobalCarCharacteristics();
    }

    public void GlobalCarCharacteristics()
    {
        PlayerPrefs.SetFloat("carGlobalCoeffdamage", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffhealth", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffshotSpeed", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffkritChance", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffkritDamage", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffvampirizm", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffbackDamage", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffdistanceDamage", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffdodge", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffarmor", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffmassEnemyDamage", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffheadshot", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffscrewValueUp", 0);
        PlayerPrefs.SetFloat("carGlobalCoefflucky", 0);
        PlayerPrefs.SetFloat("carGlobalCoeffeffectsDuration", 0);

        foreach (string _carName in carName)
        {
            int carLevel = PlayerPrefs.GetInt(_carName + "carLevel");

            for (int i = 0; i < 4; i++)
            {
                int upgNum = 0;

                if (i == 0)
                    upgNum = 10;
                if (i == 1)
                    upgNum = 20;
                if (i == 2)
                    upgNum = 30;
                if (i == 3)
                    upgNum = 40;

                if (carLevel >= upgNum)
                {
                    PlayerPrefs.SetFloat("carGlobalCoeff" + PlayerPrefs.GetString(_carName + "carUpgrade" + upgNum + "lvlId"), 
                                         PlayerPrefs.GetFloat("carGlobalCoeff" + PlayerPrefs.GetString(_carName + "carUpgrade" + upgNum + "lvlId")) 
                                         + PlayerPrefs.GetFloat(_carName + "carUpgrade" + upgNum + "lvl"));                    
                }                
            }            
        }
    }

    public void ItemCharacteristics()
    {
        
    }
}
