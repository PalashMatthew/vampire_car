using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PartnerGun : MonoBehaviour
{
    public GameObject bulletObj;
    public GameObject muzzleObj;
    public Transform bulletSpawnPoint;

    Gun _gunController;

    public GameObject mesh;

    void Initialize()
    {
        _gunController = GetComponent<Gun>();
    }

    private void Start()
    {
        Initialize();

        StartCoroutine(Shot());
    }

    private void Update()
    {
        transform.eulerAngles = Vector3.zero;

        mesh.transform.DOMove(transform.position, 0.2f);
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
