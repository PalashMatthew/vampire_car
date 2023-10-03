using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleObstacleController : MonoBehaviour
{
    Obstacle _controller;

    private void Start()
    {
        _controller = GetComponent<Obstacle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            other.gameObject.GetComponent<PlayerMovement>().PuddleActivate();
        }
    }
}
