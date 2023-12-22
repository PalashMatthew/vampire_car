using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadPit : MonoBehaviour
{
    public Image imgPit;
    public GameObject rotateObj;
    public float moveSpeed;

    private void Start()
    {
        rotateObj.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        //imgPit.GetComponent<RectTransform>().DORotate(new Vector3(0, 0, Random.Range(0, 360)), 0);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        imgPit.DOFade(0, 3.5f);
        Destroy(gameObject, 3.5f);
    }

    private void Update()
    {
        transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);
    }
}
