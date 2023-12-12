using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Truck : MonoBehaviour
{
    public bool isTruckUpDown;

    public float moveSpeed;

    public float damage;
    [HideInInspector] public EnemyController _controller;

    public bool isMoveRight;


    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (!isTruckUpDown)
        {
            if (isMoveRight)
            {
                transform.Translate(Vector3.right * 3.5f * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * 3.5f * Time.deltaTime);
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(damage);
            _controller.BackDamage(damage);
        }
    }
}
