using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    public float rotateSpeed;

    bool isMoveForward;

    private void Start()
    {        
        StartCoroutine(ChangeDirection());
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isMoveForward)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _gunController.bulletMoveSpeed);
        } 
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, _gunController.bulletMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= 5)
                Destroy(gameObject);
        }
    }

    IEnumerator ChangeDirection()
    {
        isMoveForward = true;
        yield return new WaitForSeconds(_gunController.timeOfAction / 2);
        isMoveForward = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            _gunController.DamageEnemy(other.gameObject, gameObject);
            //other.gameObject.GetComponent<EnemyController>().Hit(_gunController.CalculateDamage());
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
        }

        if (other.tag == "boss")
        {
            //_gunController.DamageBoss(other.gameObject);
            _gunController.DamageBoss(other.gameObject, gameObject);
        }
    }
}
