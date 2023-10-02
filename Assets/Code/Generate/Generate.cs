using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public float spawnTime;

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

    private void Start()
    {
        _player = GameObject.Find("Player");
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();

        if (isSpawnAccess)
            StartCoroutine(EnemyGen());
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

        StartCoroutine(EnemyGen());
    }

    public void SpawnPattern(GameObject _pattern, float _xSpawn)
    {
        float _randZ = Random.Range(minZSpawnPattern, maxZSpawnPattern);
        Instantiate(_pattern, new Vector3(Random.Range(-_xSpawn, _xSpawn), 0, _randZ), transform.rotation);
    }
}
