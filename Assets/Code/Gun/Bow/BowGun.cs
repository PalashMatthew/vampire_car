using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowGun : MonoBehaviour
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
        _bullet.GetComponent<BowArrow>()._gunController = _gunController;
        _bullet.transform.eulerAngles = new Vector3(0, 0, 90);

        StartCoroutine(Attack());
    }
}
