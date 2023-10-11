using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    [SerializeField] public List<ObstacleWaveController> obstaclesInWave;
    public List<GameObject> obstaclesObj;

    public float obstaclesMoveSpeed;

    public float spawnTime;
    public bool isSpawn;

    public float minXSpawn;
    public float maxXSpawn;

    public bool isBossFight;

    WaveController _waveController;

    private void Start()
    {        
        StartCoroutine(ObstaclesGen());
    }

    IEnumerator ObstaclesGen()
    {
        yield return new WaitForSeconds(spawnTime);

        if (isSpawn)
        {
            GameObject inst = Instantiate(obstaclesObj[Random.RandomRange(0, obstaclesObj.Count)], new Vector3(Random.Range(minXSpawn, maxXSpawn), 0, 75f), transform.rotation);

            inst.GetComponent<Obstacle>().moveSpeed = obstaclesMoveSpeed;
        }

        if (!isBossFight)
            StartCoroutine(ObstaclesGen());
    }

    public void NewWave()
    {
        _waveController = GameObject.Find("GameplayController").GetComponent<WaveController>();
        isSpawn = obstaclesInWave[_waveController.currentWave - 1].isSpawn;
        spawnTime = obstaclesInWave[_waveController.currentWave - 1].spawnTime;
    }
}

[System.Serializable]
public class ObstacleWaveController
{
    public bool isSpawn;
    public float spawnTime;
}
