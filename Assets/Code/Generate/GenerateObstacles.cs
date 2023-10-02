using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    public List<GameObject> obstaclesObj;

    public float obstaclesMoveSpeed;

    public float spawnTime;

    public float minXSpawn;
    public float maxXSpawn;

    private void Start()
    {
        StartCoroutine(ObstaclesGen());
    }

    IEnumerator ObstaclesGen()
    {
        yield return new WaitForSeconds(spawnTime);

        GameObject inst = Instantiate(obstaclesObj[Random.RandomRange(0, obstaclesObj.Count)], new Vector3(Random.Range(minXSpawn, maxXSpawn), 0, 75f), transform.rotation);

        inst.GetComponent<InstObjectMove>().moveSpeed = obstaclesMoveSpeed;

        StartCoroutine(ObstaclesGen());
    }
}
