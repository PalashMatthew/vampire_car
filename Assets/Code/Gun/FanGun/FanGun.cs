using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanGun : MonoBehaviour
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

        if (_gunController.projectileValue == 2)
        {
            StartCoroutine(AnotherShot());
        }

        if (_gunController.projectileValue == 3)
        {
            StartCoroutine(AnotherShot());
            StartCoroutine(TripleShot());
        }

        if (_gunController.projectileValue == 4)
        {
            StartCoroutine(AnotherShot());
            StartCoroutine(TripleShot());
            StartCoroutine(FourShot());
        }

        GameObject _bullet1 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet1.transform.eulerAngles = new Vector3(0, 20, 0);
        _bullet1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        yield return new WaitForSeconds(0.1f);

        GameObject _bullet2 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet2.transform.eulerAngles = new Vector3(0, 0, 0);
        _bullet2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        yield return new WaitForSeconds(0.1f);

        GameObject _bullet3 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet3.transform.eulerAngles = new Vector3(0, -20, 0);
        _bullet3.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        StartCoroutine(Attack());
    }

    IEnumerator AnotherShot()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject _bullet1 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet1.transform.eulerAngles = new Vector3(0, 30, 0);
        _bullet1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        yield return new WaitForSeconds(0.1f);

        GameObject _bullet2 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet2.transform.eulerAngles = new Vector3(0, 0, 0);
        _bullet2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        yield return new WaitForSeconds(0.1f);

        GameObject _bullet3 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet3.transform.eulerAngles = new Vector3(0, -30, 0);
        _bullet3.GetComponent<DefaultGunBullet>()._gunController = _gunController;
    }

    IEnumerator TripleShot()
    {
        yield return new WaitForSeconds(0.2f);

        GameObject _bullet1 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet1.transform.eulerAngles = new Vector3(0, 30, 0);
        _bullet1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        yield return new WaitForSeconds(0.1f);

        GameObject _bullet2 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet2.transform.eulerAngles = new Vector3(0, 0, 0);
        _bullet2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        yield return new WaitForSeconds(0.1f);

        GameObject _bullet3 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet3.transform.eulerAngles = new Vector3(0, -30, 0);
        _bullet3.GetComponent<DefaultGunBullet>()._gunController = _gunController;
    }

    IEnumerator FourShot()
    {
        yield return new WaitForSeconds(0.3f);

        GameObject _bullet1 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet1.transform.eulerAngles = new Vector3(0, 30, 0);
        _bullet1.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        yield return new WaitForSeconds(0.1f);

        GameObject _bullet2 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet2.transform.eulerAngles = new Vector3(0, 0, 0);
        _bullet2.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        yield return new WaitForSeconds(0.1f);

        GameObject _bullet3 = Instantiate(objBullet, bulletPos.position, transform.rotation);

        _bullet3.transform.eulerAngles = new Vector3(0, -30, 0);
        _bullet3.GetComponent<DefaultGunBullet>()._gunController = _gunController;
    }
}
