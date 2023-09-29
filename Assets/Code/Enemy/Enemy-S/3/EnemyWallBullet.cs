using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallBullet : MonoBehaviour
{
    private float _hp = 3;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player_bullet")
        {
            _hp -= 1;
            Destroy(other.gameObject);

            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
