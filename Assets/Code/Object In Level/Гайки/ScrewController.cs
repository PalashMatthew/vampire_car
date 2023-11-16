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
        if (WaveController.isWaveEnd)
            Destroy(gameObject);

        _destroyDistance = GameObject.Find("Player").GetComponent<PlayerStats>().screwPickUpDistance;

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

        if (transform.position.z < -20)
        {
            Destroy(gameObject);
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
        float _screwCount;

        if (GameObject.Find("Player").GetComponent<PlayerPassiveController>().isScrewValueUp)
        {
            _screwCount = screwCount * GameObject.Find("Player").GetComponent<PlayerStats>().screwValueUp;
            GlobalStats.screwCount += _screwCount;
        } 
        else
        {
            GlobalStats.screwCount += screwCount;
        }
            
        GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().UpdateScrewText();
        Destroy(gameObject);
    }
}
