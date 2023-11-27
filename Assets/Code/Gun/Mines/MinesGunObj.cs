using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MinesGunObj : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    public GameObject boomObj;
    public GameObject objBomb;

    public ParticleSystem partVfx;
    public GameObject mesh;

    SphereCollider _sphereCollider;

    public Vector3 movePos;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();

        StartCoroutine(StartAnim());
        _sphereCollider.enabled = false;

        StartCoroutine(DronExit());
    }

    IEnumerator StartAnim()
    {
        transform.DOMove(movePos, 0.5f).SetEase(Ease.OutCubic);

        mesh.transform.localScale = new Vector3(3.3f, 3.3f, 3.3f);

        mesh.transform.DOScale(6f, 1);
        mesh.transform.DOMoveY(3.8f, 1f).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(1);

        //partVfx.gameObject.transform.localScale = new Vector3(0, 0, 0);
        //partVfx.gameObject.transform.DOScale(0.6228768f, 0.3f);

        //partVfx.Play();
        _sphereCollider.enabled = true;
    }

    IEnumerator DronExit()
    {
        yield return new WaitForSeconds(_gunController.timeOfAction);

        transform.DOMove(new Vector3(25f, 0f, 65f), 3f).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            StopAllCoroutines();
            StartCoroutine(Attack());
            _sphereCollider.enabled = false;
        }
    }

    IEnumerator Attack()
    {
        objBomb.transform.DOMoveY(-1, 0.3f).SetEase(Ease.Linear);
        objBomb.transform.parent = null;

        yield return new WaitForSeconds(0.3f);

        

        yield return new WaitForSeconds(0.1f);

        GameObject _boomObj = Instantiate(boomObj, objBomb.transform.position, Quaternion.identity);
        _boomObj.GetComponent<BoomObj>()._gunController = _gunController;
        _boomObj.transform.localScale = new Vector3(_boomObj.transform.localScale.x * _gunController.areaValue, _boomObj.transform.localScale.y * _gunController.areaValue, _boomObj.transform.localScale.z * _gunController.areaValue);
        _boomObj.GetComponent<BoomObj>().Initialize();

        Destroy(objBomb);

        //partVfx.gameObject.transform.DOScale(0, 0.3f);

        transform.DOMove(new Vector3(25f, 0f, 65f), 3f).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
}
