using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineObstacleController : MonoBehaviour
{
    public float damage;
    public float hp;

    public bool isMineActivate;

    public Material mat1, mat2;
    public GameObject mineZone;

    SphereCollider _mineCollider;


    private void Start()
    {
        _mineCollider = GetComponent<SphereCollider>();
        _mineCollider.enabled = false;
    }

    public IEnumerator MineActivate()
    {
        mineZone.GetComponent<MeshRenderer>().material = mat1;
        yield return new WaitForSeconds(0.5f);
        mineZone.GetComponent<MeshRenderer>().material = mat2;
        yield return new WaitForSeconds(0.5f);
        mineZone.GetComponent<MeshRenderer>().material = mat1;
        yield return new WaitForSeconds(0.5f);
        mineZone.GetComponent<MeshRenderer>().material = mat2;
        yield return new WaitForSeconds(0.5f);
        //BOOM
        _mineCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
