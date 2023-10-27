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

    float startMousePosX = 0;
    float startPlayerX = 0;

    GameplayController _gameplayController;

    void Awake()
    {
        target = GameObject.Find("Player").transform;

        desiredPosition = target.position + offset;

        transform.position = desiredPosition;

        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        player_active = true;
        follow = true;

        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
    }

    public void Update()
    {
        if (follow && !GameplayController.isPause)
        {
            if (_gameplayController.inputSettings == GameplayController.InputSettings.RelativeToTheFinger)
            {
                RelativeToTheFinger();
            }

            if (_gameplayController.inputSettings == GameplayController.InputSettings.FingerTracking)
            {
                FingerTracking();
            }

            if (_gameplayController.inputSettings == GameplayController.InputSettings.Joy)
            {
                transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            }
        }
    }

    void RelativeToTheFinger()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                float coeff = Screen.width / (maxXBound * 2);

                Vector3 camPos;
                camPos = touch.position;

                float x = camPos.x / coeff - maxXBound;

                startMousePosX = x;

                startPlayerX = transform.position.x;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                float coeff = Screen.width / (maxXBound * 2);

                Vector3 camPos;
                camPos = touch.position;

                float x = camPos.x / coeff - maxXBound;

                float currentX = startPlayerX + (x - startMousePosX);

                if (currentX < minCameraX) currentX = minCameraX;
                if (currentX > maxCameraX) currentX = maxCameraX;

                transform.DOMoveX(currentX, 0.2f).SetUpdate(true);
            }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            float coeff = Screen.width / (maxXBound * 2);

            Vector3 camPos;
            camPos = Input.mousePosition;

            float x = camPos.x / coeff - maxXBound;

            startMousePosX = x;

            startPlayerX = transform.position.x;
        }

        if (Input.GetMouseButton(0))
        {
            float coeff = Screen.width / (maxXBound * 2);

            Vector3 camPos;
            camPos = Input.mousePosition;

            float x = camPos.x / coeff - maxXBound;

            float currentX = startPlayerX + (x - startMousePosX);

            if (currentX < minCameraX) currentX = minCameraX;
            if (currentX > maxCameraX) currentX = maxCameraX;

            transform.DOMoveX(currentX, 0.2f).SetUpdate(true);
        }
#endif
    }

    void FingerTracking()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButton(0))
        {
            float coeff = Screen.width / (maxXBound * 2);

            Vector3 camPos;
            camPos = Input.mousePosition;

            float x = camPos.x / coeff - maxXBound;

            float currentX = x;

            if (currentX < minCameraX) currentX = minCameraX;
            if (currentX > maxCameraX) currentX = maxCameraX;

            transform.DOMoveX(currentX, 0.2f).SetUpdate(true);
        }
    }

    public void UpdateCoord()
    {
        float coeff = Screen.width / (maxXBound * 2);

        Vector3 camPos;
        camPos = Input.mousePosition;

        float x = camPos.x / coeff - maxXBound;

        startMousePosX = x;

        startPlayerX = transform.position.x;
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
