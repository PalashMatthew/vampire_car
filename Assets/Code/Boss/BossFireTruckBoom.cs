using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckBoom : MonoBehaviour
{
    [HideInInspector]
    public BossFireTruckController _brain;

    public void Initialize()
    {
        StartCoroutine(OffCollider());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_brain.damage);
            Destroy(gameObject);
        }
    }

    IEnumerator OffCollider()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
