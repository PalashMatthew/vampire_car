using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Detail", menuName = "DetailCard")]
public class DetailCard : ScriptableObject
{
    public enum ItemType
    {
        Gun,
        Engine,
        Brakes,
        FuelSystem,
        Suspension,
        Transmission
    }
    public ItemType itemType;
}
