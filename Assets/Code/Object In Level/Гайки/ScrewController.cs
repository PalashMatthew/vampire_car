using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrewController : MonoBehaviour
{
    public float screwCount;
    public float moveSpeed;
    public float collectSpeed;
    public float collectSpeedCoeff;

    public float _collectionDistance;
    public float _destroyDistance;

    private Transform _playerPos;

    private bool _isFind = false;
    private bool _isCollect = false;

    private void Start()
    {
        _playerPos = GameObject.Find("Player").transform;

        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void Update()
    {
        if (Vector3.Distance(_playerPos.position, transform.position) <= _collectionDistance)
        {
            _isFind = true;
            
        } 
        else if (!_isFind)
        {
            Movement();
        }

        if (_isFind)
        {
            MoveToPlayer();

            if (Vector3.Distance(_playerPos.position, transform.position) <= _destroyDistance && !_isCollect)
            {
                _isCollect = true;
                DestroyAnim();
            }
        }
    }

    void Movement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerPos.position, collectSpeed * Time.deltaTime);
        collectSpeed += collectSpeedCoeff;
    }

    void DestroyAnim()
    {
        GlobalStats.screwCount += screwCount;
        GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().UpdateScrewText();
        Destroy(gameObject);
    }
}
