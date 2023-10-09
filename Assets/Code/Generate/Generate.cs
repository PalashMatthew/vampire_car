using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public float spawnTime;
    public float moveSpeedCoeff;

    public float step;

    private GameObject _player;

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

    private void Start()
    {
        _player = GameObject.Find("Player");
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
    }

    public void StartSpawn()
    {
        if (isSpawnAccess)
        {
            StopAllCoroutines();
            StartCoroutine(EnemyGen());
        }
    }

    IEnumerator EnemyGen()
    {
        yield return new WaitForSeconds(spawnTime);

        float _randX = Random.Range(minXSpawn, maxXSpawn);
        float _x = _randX / step;
        _x = (int)_x;
        _x *= step;

        float _randZ = Random.Range(minZSpawn, maxZSpawn);

        GameObject inst = Instantiate(waveController.ChoiseEnemy(), new Vector3(_x, 0, _randZ), transform.rotation);
        _gameplayController.activeEnemy.Add(inst);

        inst.transform.eulerAngles = new Vector3(0, 180, 0);

        inst.GetComponent<EnemyController>().moveSpeedMin *= moveSpeedCoeff;
        inst.GetComponent<EnemyController>().moveSpeedMax *= moveSpeedCoeff;
        inst.GetComponent<EnemyController>().Initialize();

        StartCoroutine(EnemyGen());
    }

    public void SpawnPattern(GameObject _pattern, float _xSpawn)
    {
        float _randZ = Random.Range(minZSpawnPattern, maxZSpawnPattern);
        Instantiate(_pattern, new Vector3(Random.Range(-_xSpawn, _xSpawn), 0, _randZ), transform.rotation);
    }

    public void BossFight()
    {
        StopAllCoroutines();
        StartCoroutine(BossFightEnum());
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

            inst.transform.eulerAngles = new Vector3(0, 180, 0);

            GameObject.Find("GameplayUI").GetComponent<GameplayUIController>()._isBossFight = true;
        }
    }
}

[System.Serializable]
public class EnemyCoeff
{
    public float damageCoeff = 1;
    public float healthCoeff = 1;
}
