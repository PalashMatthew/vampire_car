using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGunObj : MonoBehaviour
{
    public Gun _gunController;

    public Vector3 endPos;
    public GameObject grenadeObj;
    public GameObject boomObj;

    bool _isMove;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private void Update()
    {
        if (_isMove)
        {
            grenadeObj.transform.Rotate(Vector3.up * Time.deltaTime * 500);
            grenadeObj.transform.Rotate(Vector3.right * Time.deltaTime * 800);
        }
    }

    IEnumerator Move()
    {
        _isMove = true;
        transform.DOMoveZ(endPos.z, 1.4f);
        transform.DOMoveX(endPos.x, 1.4f);
        grenadeObj.transform.DOMoveY(transform.position.y + 5.5f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        grenadeObj.transform.DOMoveY(0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        _isMove = false;

        yield return new WaitForSeconds(0.3f);

        GameObject _boomObj = Instantiate(boomObj, transform.position, Quaternion.identity);
        _boomObj.GetComponent<BoomObj>()._gunController = _gunController;
        _boomObj.transform.localScale = new Vector3(_boomObj.transform.localScale.x * _gunController.areaValue, _boomObj.transform.localScale.y * _gunController.areaValue, _boomObj.transform.localScale.z * _gunController.areaValue);
        _boomObj.GetComponent<BoomObj>().Initialize();

        Destroy(gameObject);
    }
}
