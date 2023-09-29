using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReversal : MonoBehaviour
{
    public enum ReverseVersion
    {
        ReverseZ,
        ReverseLeftRight
    }

    public ReverseVersion reverseVersion;
    public float reverseSpeed;


    EnemyController _enemyController;

    GameObject _player;

    bool _isRevers = false;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
        _player = GameObject.Find("Player");

        if (reverseVersion == ReverseVersion.ReverseLeftRight)
        {
            StartCoroutine(ReverseLeftRight());
        }
    }

    private void Update()
    {
        if (reverseVersion == ReverseVersion.ReverseZ)
        {
            if (!_isRevers)
            {
                if (transform.position.z < _player.transform.position.z && _enemyController.isVisible)
                {
                    _isRevers = true;
                    StartCoroutine(Revers());
                }
            }
        }
    }

    IEnumerator Revers()
    {
        yield return new WaitForSeconds(0.15f);

        transform.DOLookAt(_player.transform.position, 0.3f);

        yield return new WaitForSeconds(0.3f);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    IEnumerator ReverseLeftRight()
    {
        yield return new WaitForSeconds(reverseSpeed);
        transform.DORotate(new Vector3(0, 200, 0), 0.3f);

        yield return new WaitForSeconds(reverseSpeed);
        transform.DORotate(new Vector3(0, 160, 0), 0.3f);

        StartCoroutine(ReverseLeftRight());
    }
}
