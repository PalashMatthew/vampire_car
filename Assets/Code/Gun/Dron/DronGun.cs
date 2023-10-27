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
            obj.transform.Rotate(Vector3.up * Time.deltaTime * _gunController.rotateSpeed);
        }

        if (_gunController.projectileValue == 1)
        {
            projectileObj[0].SetActive(true);
            projectileObj[1].SetActive(false);
            projectileObj[2].SetActive(false);
            projectileObj[3].SetActive(false);
        }

        if (_gunController.projectileValue == 2)
        {
            projectileObj[0].SetActive(true);
            projectileObj[1].SetActive(true);
            projectileObj[2].SetActive(false);
            projectileObj[3].SetActive(false);
        }

        if (_gunController.projectileValue == 3)
        {
            projectileObj[0].SetActive(true);
            projectileObj[1].SetActive(true);
            projectileObj[2].SetActive(true);
            projectileObj[3].SetActive(false);
        }

        if (_gunController.projectileValue == 4)
        {
            projectileObj[0].SetActive(true);
            projectileObj[1].SetActive(true);
            projectileObj[2].SetActive(true);
            projectileObj[3].SetActive(true);
        }
    }
}
