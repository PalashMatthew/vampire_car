using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PartnerGun : MonoBehaviour
{
    public List<GameObject> partnerObj;

    Gun _gunController;

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
        if (_gunController.projectileValue == 1)
        {
            partnerObj[0].SetActive(true);
            partnerObj[1].SetActive(false);
            partnerObj[2].SetActive(false);
            partnerObj[3].SetActive(false);
        }

        if (_gunController.projectileValue == 2)
        {
            partnerObj[0].SetActive(true);
            partnerObj[1].SetActive(true);
            partnerObj[2].SetActive(false);
            partnerObj[3].SetActive(false);
        }

        if (_gunController.projectileValue == 3)
        {
            partnerObj[0].SetActive(true);
            partnerObj[1].SetActive(true);
            partnerObj[2].SetActive(true);
            partnerObj[3].SetActive(false);
        }

        if (_gunController.projectileValue == 4)
        {
            partnerObj[0].SetActive(true);
            partnerObj[1].SetActive(true);
            partnerObj[2].SetActive(true);
            partnerObj[3].SetActive(true);
        }
    }
}
