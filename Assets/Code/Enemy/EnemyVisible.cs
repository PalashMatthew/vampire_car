using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisible : MonoBehaviour
{
    EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponentInParent<EnemyController>();
    }

    private void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            _enemyController.isVisible = true;
        }
        else
        {
            if (_enemyController.isVisible)
            {
                _enemyController.isVisible = false;
                _enemyController.DestroyAfk();
            }
            
        }
    }
}
