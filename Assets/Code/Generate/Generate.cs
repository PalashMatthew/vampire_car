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

        float _rand = Random.Range(minXSpawn, maxXSpawn);
        float _x = _rand / step;
        _x = (int)_x;
        _x *= step;

        GameObject inst = Instantiate(waveController.ChoiseEnemy(), new Vector3(_x, 0, 85f), transform.rotation);
        _gameplayController.activeEnemy.Add(inst);

        inst.transform.eulerAngles = new Vector3(0, 180, 0);

        StartCoroutine(EnemyGen());
    }

    public void SpawnPattern(GameObject _pattern, float _xSpawn)
    {
        Instantiate(_pattern, new Vector3(Random.Range(-_xSpawn, _xSpawn), 0, 90), transform.rotation);
    }
}
