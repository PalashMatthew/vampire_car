using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    public int currentLevel;

    public List<int> enemyCountFromNewLevel = new List<int>();

    private int _globalScrewCount;
    public int enemyCountInThisLevel;

    private void Update()
    {
        if (enemyCountInThisLevel >= enemyCountFromNewLevel[currentLevel - 1])
        {
            GameObject.Find("Player").GetComponent<PlayerController>().LevelUp();
            currentLevel++;
            enemyCountInThisLevel = 0;
        }
    }
}
