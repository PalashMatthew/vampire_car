using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGun : MonoBehaviour
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
        GameObject _gm = Instantiate(objBullet, bulletPos.position, transform.rotation);
        _gm.GetComponent<GrenadeGunObj>().endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 30f);
        _gm.GetComponent<GrenadeGunObj>()._gunController = _gunController;

        if (_gunController.projectileValue == 2)
        {
            GameObject _gm2 = Instantiate(objBullet, bulletPos.position, transform.rotation);
            _gm2.GetComponent<GrenadeGunObj>().endPos = new Vector3(transform.position.x + 7, transform.position.y, transform.position.z + 24f);
            _gm2.GetComponent<GrenadeGunObj>()._gunController = _gunController;
        }

        if (_gunController.projectileValue == 3)
        {
            GameObject _gm2 = Instantiate(objBullet, bulletPos.position, transform.rotation);
            _gm2.GetComponent<GrenadeGunObj>().endPos = new Vector3(transform.position.x + 7, transform.position.y, transform.position.z + 24f);
            _gm2.GetComponent<GrenadeGunObj>()._gunController = _gunController;

            GameObject _gm3 = Instantiate(objBullet, bulletPos.position, transform.rotation);
            _gm3.GetComponent<GrenadeGunObj>().endPos = new Vector3(transform.position.x - 7, transform.position.y, transform.position.z + 24f);
            _gm3.GetComponent<GrenadeGunObj>()._gunController = _gunController;
        }

        StartCoroutine(Attack());
    }
}
