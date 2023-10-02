using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float min_z;
    Vector3 smoothedPosition;
    Vector3 desiredPosition;

    public bool player_active;

    public bool follow;

    public float speed_back;

    public float maxXBound;


    void Awake()
    {
        target = GameObject.Find("Player").transform;

        desiredPosition = target.position + offset;

        transform.position = desiredPosition;

        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        player_active = true;
        //follow = true;
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float coeff = Screen.width / (maxXBound * 2);

            Vector3 camPos;
            camPos = Input.mousePosition;

            float x = camPos.x / coeff - maxXBound;

            if (x < minCameraX) x = minCameraX;
            if (x > maxCameraX) x = maxCameraX;

            transform.DOMoveX(x, 0.1f);

            //transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Moved)
        //    {
        //        desiredPosition = target.position + offset;
        //        ////smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        //        //smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //        //smoothedPosition = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        //        //transform.position = desiredPosition;

        //        if (desiredPosition.x < minCameraX)
        //        {
        //            desiredPosition = new Vector3(minCameraX, desiredPosition.y, desiredPosition.z);
        //        }

        //        if (desiredPosition.x > maxCameraX)
        //        {
        //            desiredPosition = new Vector3(maxCameraX, desiredPosition.y, desiredPosition.z);
        //        }

        //        transform.DOMoveX(desiredPosition.x, 0.1f);
        //    }
        //}

        //if (follow)
        //{
        //    desiredPosition = target.position + offset;
        //    //smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        //    smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //    smoothedPosition = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        //    transform.position = smoothedPosition;
        //}

        //Bounds();
    }

    public void BackSpeed()
    {
        StartCoroutine(ReturnTimeScale());
    }

    IEnumerator ReturnTimeScale()
    {
        while (smoothSpeed < 18)
        {
            smoothSpeed += Time.unscaledDeltaTime * speed_back;

            if (smoothSpeed > 18)
            {
                smoothSpeed = 18;
            }
            yield return null;
        }
    }

    public void FastFollow()
    {
        desiredPosition = target.position + offset;

        transform.position = desiredPosition;

        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
        follow = true;
    }

    public void EndLevel()
    {
        transform.GetComponent<Camera>().DOFieldOfView(10, 0.75f).SetEase(Ease.OutBack);
        offset = new Vector3(offset.x, offset.y + 0.7f, offset.z);
    }

    public float minCameraX;
    public float maxCameraX;

    void Bounds()
    {
        if (transform.position.x < minCameraX)
        {
            transform.position = new Vector3(minCameraX, transform.position.y, transform.position.z);
        }

        if (transform.position.x > maxCameraX)
        {
            transform.position = new Vector3(maxCameraX, transform.position.y, transform.position.z);
        }
    }
}
