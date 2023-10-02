using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorGenerate : MonoBehaviour
{
    public List<GameObject> decorObj;

    public float decorMoveSpeed;

    public float spawnTime;

    public float minXSpawn;
    public float maxXSpawn;

    private void Start()
    {
        StartCoroutine(DecorGen());
    }

    IEnumerator DecorGen()
    {
        yield return new WaitForSeconds(spawnTime);

        GameObject inst = Instantiate(decorObj[Random.RandomRange(0, decorObj.Count)], new Vector3(Random.Range(minXSpawn, maxXSpawn), 0, 75f), transform.rotation);

        //float xPos = Random.Range(playerGlobalCenter.transform.eulerAngles.z - 25, playerGlobalCenter.transform.eulerAngles.z + 25);
        //float zPos = Random.Range(0, 360);

        //inst.transform.eulerAngles = new Vector3(xPos, 0, 0);
        inst.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;

        StartCoroutine(DecorGen());
    }
}
