using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesGun : MonoBehaviour
{
    Gun _gunController;
    GameplayController _gameplayController;

    public GameObject objBullet;

    public Transform bulletPos;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _gunController = GetComponent<Gun>();
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(_gunController.shotSpeed);
        
        GameObject _bullet = Instantiate(objBullet, bulletPos.position, transform.rotation);
        _bullet.GetComponent<MinesGunObj>()._gunController = _gunController;
        _bullet.GetComponent<MinesGunObj>().movePos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);

        if (_gunController.projectileValue == 2)
        {
            GameObject _bullet2 = Instantiate(objBullet, bulletPos.position, transform.rotation);
            _bullet2.GetComponent<MinesGunObj>()._gunController = _gunController;
            _bullet2.GetComponent<MinesGunObj>().movePos = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z + 7);
        }

        if (_gunController.projectileValue == 3)
        {
            GameObject _bullet2 = Instantiate(objBullet, bulletPos.position, transform.rotation);
            _bullet2.GetComponent<MinesGunObj>()._gunController = _gunController;
            _bullet2.GetComponent<MinesGunObj>().movePos = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z + 7);

            GameObject _bullet3 = Instantiate(objBullet, bulletPos.position, transform.rotation);
            _bullet3.GetComponent<MinesGunObj>()._gunController = _gunController;
            _bullet3.GetComponent<MinesGunObj>().movePos = new Vector3(transform.position.x - 4, transform.position.y, transform.position.z + 7);
        }

        StartCoroutine(Attack());
    }
}
