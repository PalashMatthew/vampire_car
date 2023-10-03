using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacleController : MonoBehaviour
{
    Obstacle _controller;

    private void Start()
    {
        _controller = GetComponentInParent<Obstacle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_controller.damage);
        }
    }
}
