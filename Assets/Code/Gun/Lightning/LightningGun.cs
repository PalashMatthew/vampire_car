using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningGun : MonoBehaviour
{
    public List<GameObject> bulletObj;

    Gun _gunController;


    void Initialize()
    {
        _gunController = GetComponent<Gun>();

        foreach (GameObject gm in bulletObj)
        {
            gm.SetActive(false);
        }        
    }

    private void Start()
    {
        Initialize();

        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(_gunController.shotSpeed);

        for (int i = 1; i <= _gunController.projectileValue; i++)
        {
            bulletObj[i-1].SetActive(true);
            bulletObj[i - 1].GetComponent<LightningBullet>()._gunController = _gunController;
        }

        //foreach (GameObject gm in bulletObj)
        //{
        //    gm.SetActive(true);
        //    gm.GetComponent<LightningBullet>()._gunController = _gunController;
        //}

        yield return new WaitForSeconds(_gunController.timeOfAction);

        foreach (GameObject gm in bulletObj)
        {
            gm.SetActive(false);
        }
        StartCoroutine(Shot());
    }
}
