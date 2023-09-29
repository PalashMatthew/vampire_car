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

        foreach (GameObject gm in bulletObj)
        {
            gm.SetActive(true);
            gm.GetComponent<LightningBullet>()._gunController = _gunController;
        }

        yield return new WaitForSeconds(_gunController.timeOfAction);

        foreach (GameObject gm in bulletObj)
        {
            gm.SetActive(false);
        }
        StartCoroutine(Shot());
    }
}
