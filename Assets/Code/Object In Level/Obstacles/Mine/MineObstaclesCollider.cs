using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObstaclesCollider : MonoBehaviour
{
    Obstacle _controller;
    MineObstacleController _obstacleController;

    private void Start()
    {
        _controller = GetComponentInParent<Obstacle>();
        _obstacleController = GetComponentInParent<MineObstacleController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_controller.damage);
            _obstacleController.Boom();
        }
    }
}
