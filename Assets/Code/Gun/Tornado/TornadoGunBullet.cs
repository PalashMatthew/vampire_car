using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoGunBullet : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    public bool isPlayerAttack = false;
    public float rotateSpeed;
    public List<GameObject> objProjectiles;
    public GameObject projectilesParent;

    private void Start()
    {
        if (_gunController.projectileValue == 1)
        {
            objProjectiles[0].SetActive(true);
            objProjectiles[1].SetActive(false);
            objProjectiles[2].SetActive(false);
            objProjectiles[3].SetActive(false);
        }

        if (_gunController.projectileValue == 2)
        {
            objProjectiles[0].SetActive(true);
            objProjectiles[1].SetActive(true);
            objProjectiles[2].SetActive(false);
            objProjectiles[3].SetActive(false);
        }

        if (_gunController.projectileValue == 3)
        {
            objProjectiles[0].SetActive(true);
            objProjectiles[1].SetActive(true);
            objProjectiles[2].SetActive(true);
            objProjectiles[3].SetActive(false);
        }

        if (_gunController.projectileValue == 4)
        {
            objProjectiles[0].SetActive(true);
            objProjectiles[1].SetActive(true);
            objProjectiles[2].SetActive(true);
            objProjectiles[3].SetActive(true);
        }

        foreach (GameObject obj in objProjectiles)
        {
            obj.GetComponent<TornadoGunBulletObj>()._gunController = _gunController;
        }
    }


    private void Update()
    {
        transform.Translate(transform.forward * _gunController.bulletMoveSpeed * Time.deltaTime);

        //foreach (GameObject obj in objProjectiles)
        //{
        //    obj.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        //}        

        projectilesParent.transform.Rotate(Vector3.up * Time.deltaTime * _gunController.rotateSpeed);

        if (transform.position.z > 80)
            Destroy(gameObject);
    }
}
