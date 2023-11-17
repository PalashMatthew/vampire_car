using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Set", menuName = "SetCard")]
public class SetCard : ScriptableObject
{
    [Header("Base")]
    public string setID;

    public string bonusDescription1;
    public string bonusDescription2;
    public string bonusDescription3;

    public float value1;
    public float value2;
    public float value3;
}
