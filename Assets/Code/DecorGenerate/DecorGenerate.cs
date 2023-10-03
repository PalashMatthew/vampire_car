using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorGenerate : MonoBehaviour
{
    [Header("High Decor")]    
    public List<GameObject> highDecor;
    public float minHighDecorTime;
    public float maxHighDecorTime;
    public float highDecorYPos;
    public float highDecorZPos;
    public float minHighDecorXPos;
    public float maxHighDecorXPos;

    [Header("Soil")]
    public GameObject soilObj;
    public List<Material> soilMaterials;
    public float soilTimeSpawn;
    public float soilYPos;
    public float soilZPos;
    public float minSoilXPos, maxSoilXPos;
    public float minSoilScale, maxSoilScale;

    [Header("Place Decor")]
    public List<GameObject> decorObj;
    public float decorMoveSpeed;

    public float spawnTime;

    public float minXSpawn;
    public float maxXSpawn;

    private void Start()
    {
        FirstSpawn();

        StartCoroutine(DecorGen());
        StartCoroutine(HighDecorGen());
        //StartCoroutine(SoilGen());
    }

    IEnumerator DecorGen()
    {
        yield return new WaitForSeconds(spawnTime);

        GameObject inst = Instantiate(decorObj[Random.Range(0, decorObj.Count)], new Vector3(Random.Range(minXSpawn, maxXSpawn), 0, 75f), transform.rotation);

        inst.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;

        StartCoroutine(DecorGen());
    }

    IEnumerator HighDecorGen()
    {
        yield return new WaitForSeconds(Random.Range(minHighDecorTime, maxHighDecorTime));

        Vector3 _pos1 = new Vector3(Random.Range(minHighDecorXPos, maxHighDecorXPos), highDecorYPos, highDecorZPos);

        GameObject inst1 = Instantiate(highDecor[Random.Range(0, highDecor.Count)], _pos1, transform.rotation);
        inst1.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;

        Vector3 _pos2 = new Vector3(Random.Range(-minHighDecorXPos, -maxHighDecorXPos), highDecorYPos, highDecorZPos);

        GameObject inst2 = Instantiate(highDecor[Random.Range(0, highDecor.Count)], _pos2, transform.rotation);
        inst2.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;

        StartCoroutine(HighDecorGen());
    }

    IEnumerator SoilGen()
    {
        GameObject inst = Instantiate(soilObj, new Vector3(Random.Range(minSoilXPos, maxSoilXPos), soilYPos, soilZPos), transform.rotation);

        float _scale = Random.Range(minSoilScale, maxSoilScale);
        inst.transform.localScale = new Vector3(_scale, _scale, _scale);
        inst.GetComponent<MeshRenderer>().material = soilMaterials[Random.Range(0, soilMaterials.Count)];

        inst.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;

        yield return new WaitForSeconds(soilTimeSpawn);

        StartCoroutine(SoilGen());
    }

    void FirstSpawn()
    {
        #region High Decor
        for (int i = 0; i < 17; i++)
        {
            int dir = Random.Range(1, 3);

            if (dir == 1)
            {
                Vector3 _pos = new Vector3(Random.Range(minHighDecorXPos, maxHighDecorXPos), highDecorYPos, Random.Range(-9, highDecorZPos));

                GameObject inst = Instantiate(highDecor[Random.Range(0, highDecor.Count)], _pos, transform.rotation);
                inst.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;
            }

            if (dir == 2)
            {
                Vector3 _pos = new Vector3(Random.Range(-minHighDecorXPos, -maxHighDecorXPos), highDecorYPos, Random.Range(-9, highDecorZPos));

                GameObject inst = Instantiate(highDecor[Random.Range(0, highDecor.Count)], _pos, transform.rotation);
                inst.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;
            }
        }
        #endregion

        #region Soil
        //for (int i = 0; i < 4; i++)
        //{
        //    GameObject inst = Instantiate(soilObj, new Vector3(Random.Range(minSoilXPos, maxSoilXPos), soilYPos, Random.Range(-9, soilZPos)), transform.rotation);

        //    float _scale = Random.Range(minSoilScale, maxSoilScale);
        //    inst.transform.localScale = new Vector3(_scale, _scale, _scale);
        //    inst.GetComponent<MeshRenderer>().material = soilMaterials[Random.Range(0, soilMaterials.Count)];

        //    inst.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;
        //}
        #endregion

        #region Decor
        for (int i = 0; i < 10; i++)
        {
            GameObject inst = Instantiate(decorObj[Random.Range(0, decorObj.Count)], new Vector3(Random.Range(minXSpawn, maxXSpawn), 0, Random.Range(-15, 75f)), transform.rotation);

            inst.GetComponent<InstObjectMove>().moveSpeed = decorMoveSpeed;
        }
        #endregion
    }
}
