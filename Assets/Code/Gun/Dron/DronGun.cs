using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronGun : MonoBehaviour
{
    Gun _gunController;

    public List<GameObject> projectileObj;

    void Initialize()
    {
        _gunController = GetComponent<Gun>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        foreach (GameObject obj in projectileObj)
        {
            obj.transform.Rotate(Vector3.up * Time.deltaTime * _gunController.bulletMoveSpeed);
        }
    }
}
