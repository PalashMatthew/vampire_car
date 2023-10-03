using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObstacleController : MonoBehaviour
{
    public bool isMineActivate;

    public Material mat1, mat2;
    public GameObject mineZone;

    public GameObject _mineCollider;

    public GameObject fxBoom;


    private void Start()
    {
        _mineCollider.SetActive(false);

        StartCoroutine(MineActivate());
    }

    public IEnumerator MineActivate()
    {
        mineZone.GetComponent<MeshRenderer>().material = mat1;
        yield return new WaitForSeconds(0.25f);
        mineZone.GetComponent<MeshRenderer>().material = mat2;
        yield return new WaitForSeconds(0.25f);
        mineZone.GetComponent<MeshRenderer>().material = mat1;
        yield return new WaitForSeconds(0.25f);
        mineZone.GetComponent<MeshRenderer>().material = mat2;
        yield return new WaitForSeconds(0.25f);
        mineZone.GetComponent<MeshRenderer>().material = mat1;
        yield return new WaitForSeconds(0.25f);
        mineZone.GetComponent<MeshRenderer>().material = mat2;
        yield return new WaitForSeconds(0.25f);
        mineZone.GetComponent<MeshRenderer>().material = mat1;
        yield return new WaitForSeconds(0.25f);
        mineZone.GetComponent<MeshRenderer>().material = mat2;
        yield return new WaitForSeconds(0.25f);
        //BOOM
        _mineCollider.SetActive(true);
    }

    public void Boom()
    {
        fxBoom.SetActive(true);
        mineZone.SetActive(false);
        _mineCollider.SetActive(false);
        Destroy(gameObject, 3);
    }
}
