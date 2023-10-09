using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckFireball : MonoBehaviour
{
    public float moveSpeed;

    public BossFireTruckController _brain;


    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_brain.damage);
            Destroy(gameObject);
        }
    }
}
