using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerGunObj : MonoBehaviour
{
    public GameObject bulletObj;
    public GameObject muzzleObj;
    public Transform bulletSpawnPoint;

    Gun _gunController;

    void Initialize()
    {
        _gunController = GetComponentInParent<Gun>();
    }

    void OnEnable()
    {
        Initialize();
        StopAllCoroutines();
        StartCoroutine(Shot());
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.zero;

        //transform.DOMove(transform.position, 0.2f);
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(_gunController.shotSpeed);

        GameObject _inst = Instantiate(bulletObj, bulletSpawnPoint.position, transform.rotation);
        _inst.GetComponent<DefaultGunBullet>()._gunController = _gunController;

        GameObject _instMuzzle = Instantiate(muzzleObj, bulletSpawnPoint.position, transform.rotation);
        _instMuzzle.transform.parent = bulletSpawnPoint;
        Destroy(_instMuzzle, 2);

        StartCoroutine(Shot());
    }
}
