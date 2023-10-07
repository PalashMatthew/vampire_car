using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    public GameObject mesh;

    public float rotateSpeed;

    bool isMoveForward;

    private void Start()
    {        
        StartCoroutine(ChangeDirection());
    }

    private void Update()
    {
        Movement();

        mesh.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }

    void Movement()
    {
        if (isMoveForward)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _gunController.bulletMoveSpeed);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, _gunController.bulletMoveSpeed * Time.deltaTime);
        }
    }

    IEnumerator ChangeDirection()
    {
        isMoveForward = true;
        yield return new WaitForSeconds(_gunController.timeOfAction / 2);
        isMoveForward = false;
        yield return new WaitForSeconds(_gunController.timeOfAction / 2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Hit(_gunController.CalculateDamage());
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
        }
    }
}
