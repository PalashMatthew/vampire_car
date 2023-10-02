using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    public int currentLevel;

    public List<int> screwCountFromNewLevel = new List<int>();

    private int _globalScrewCount;
    public int screwCountInThisLevel;

    private void Update()
    {
        if (screwCountInThisLevel >= screwCountFromNewLevel[currentLevel - 1])
        {
            currentLevel++;
            screwCountInThisLevel = 0;
        }
    }
}
