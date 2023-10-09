using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckController : MonoBehaviour
{
    public int currentPhase = 1;

    public enum BossActions
    {
        FirstAppearance,
        FindPlayer,
        Pause,
        FireAttack1
    }
    public BossActions action;

    EnemyController _enemyController;
        
    public float moveSpeedStart;
    public float moveSpeedFindPlayer;

    bool _isFirstAppearance = true;
    GameObject _player;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (action == BossActions.FirstAppearance)
        {
            if (transform.position.z > 47)
                BaseMovement();
            else
            {
                action = BossActions.FindPlayer;
                StartCoroutine(FindPlayerTimer());
            }
        }

        if (action == BossActions.FindPlayer)
        {
            FindPlayer();
        }
    }

    //Следит за игроком по X пока не поймает его
    void FindPlayer()
    {
        transform.DOMoveX(_player.transform.position.x, moveSpeedFindPlayer);
    }

    IEnumerator FindPlayerTimer()
    {
        yield return new WaitForSeconds(2);
        action = BossActions.Pause;
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(2);
        action = BossActions.FireAttack1;
    }

    void BaseMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeedStart);
    }
}
