using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public float spawnTime;
    public float moveSpeedCoeff;

    public float step;

    private GameObject _player;

    GenerateObstacles _obstacles;
    GameplayController _gameplayController;
    public WaveController waveController;

    public float minXSpawn;
    public float maxXSpawn;
    public float minZSpawn;
    public float maxZSpawn;

    public float minZSpawnPattern;
    public float maxZSpawnPattern;

    public bool isSpawnAccess;

    public GameObject bossObj;

    [SerializeField] public List<EnemyCoeff> enemyCoeffList;

    GameObject pattern;
    float xSpawn;

    private bool _isSpawnPattern;

    public GameObject objFirstAidKit;

    public int dronCountInScreen = 0;


    public void StartSpawn()
    {
        _isSpawnPattern = false;
        dronCountInScreen = 0;

        _player = GameObject.Find("Player");
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
        _obstacles = GetComponent<GenerateObstacles>();

        if (isSpawnAccess)
        {
            StopAllCoroutines();
            StartCoroutine(EnemyGen());
        }
    }

    IEnumerator EnemyGen()
    {
        if (!_isSpawnPattern)
        {
            List<float> _spawnPosMass = new List<float>();

            for (int i = 0; i < waveController.waveList[waveController.currentWave - 1].enemySpawnCount; i++)
            {
                l1:
                float _randX = Random.Range(minXSpawn, maxXSpawn);
                float _x = _randX / step;
                _x = (int)_x;
                _x *= step;

                if (!_spawnPosMass.Contains(_x))
                {
                    _spawnPosMass.Add(_x);
                } 
                else
                {
                    goto l1;
                }                               

                float _randZ = Random.Range(minZSpawn, maxZSpawn);

                spawn:
                GameObject _en = waveController.ChoiseEnemy();

                if (_en.GetComponent<EnemyController>().carType == EnemyController.CarType.Static)
                {
                    if (dronCountInScreen >= waveController.waveList[waveController.currentWave - 1].maxDronCount)
                    {
                        goto spawn;
                    } 
                    else
                    {
                        dronCountInScreen++;
                    }
                }

                GameObject inst = Instantiate(waveController.ChoiseEnemy(), new Vector3(_x, 0, _randZ), transform.rotation);
                _gameplayController.activeEnemy.Add(inst);

                inst.transform.eulerAngles = new Vector3(0, 180, 0);

                //inst.GetComponent<EnemyController>().moveSpeedMin *= moveSpeedCoeff;
                //inst.GetComponent<EnemyController>().moveSpeedMax *= moveSpeedCoeff;
                inst.GetComponent<EnemyController>().Initialize();
            }
        }
        else
        {
            float _randZ = Random.Range(minZSpawnPattern, maxZSpawnPattern);
            Instantiate(pattern, new Vector3(Random.Range(-xSpawn, xSpawn), 1, _randZ), transform.rotation);

            _isSpawnPattern = false;
        }

        yield return new WaitForSeconds(spawnTime);        

        StartCoroutine(EnemyGen());
    }

    public void FirstAidKit(float value)
    {
        float _randX = Random.Range(minXSpawn, maxXSpawn);
        GameObject _inst = Instantiate(objFirstAidKit, new Vector3(_randX, 1, minZSpawn), transform.rotation);
        _inst.GetComponent<FirstAidKitController>().value = value;
    }

    public void SpawnPattern(GameObject _pattern, float _xSpawn)
    {
        pattern = _pattern;
        xSpawn = _xSpawn;

        _isSpawnPattern = true;
    }

    public void BossFight()
    {
        StopAllCoroutines();
        StartCoroutine(BossFightEnum());
        _obstacles.isBossFight = true;
    }

    IEnumerator BossFightEnum()
    {
        yield return new WaitForSeconds(1);

        GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().isShowPanelWave = false;

        if (_gameplayController.activeEnemy.Count > 0)
        {
            StartCoroutine(BossFightEnum());
        } 
        else
        {
            GameObject inst = Instantiate(bossObj, new Vector3(0, 0, 90f), transform.rotation);
            _gameplayController.activeEnemy.Add(inst);
            inst.name = "BOSS";            

            inst.transform.eulerAngles = new Vector3(0, 180, 0);

            GameObject.Find("GameplayUI").GetComponent<GameplayUIController>()._isBossFight = true;
            gameObject.GetComponent<GenerateObstacles>().isBossFight = true;
        }
    }
}

[System.Serializable]
public class EnemyCoeff
{
    public float damageCoeff = 1;
    public float healthCoeff = 1;
}
