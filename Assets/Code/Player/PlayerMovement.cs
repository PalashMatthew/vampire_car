using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    public float moveXSpeed;
    public float moveZSpeed;

    public float screenProcentToRotate;  //Процент экрана, в котором начинается поворот
    public float playerRotateCoeffPlusWidth;

    public float cameraRotateSpeedCoeff;

    public float coeffX;
    public float coeffZ;    

    [Header("Bounds")]
    public float boundX;    
    public float boundZMin;
    public float boundZMax;

    public float boundXMin;
    public float boundXMax;

    [Header("Animations")]
    public GameObject mesh;
    public float playerAnimRotateSpeed;
    private float playerStartMoveX;
    public List<GameObject> wheelsObj;
    public float wheelRotateSpeed;
    public GameObject wheelRightObj, wheelLeftObj;

    [Header("Objects")]
    public Camera cam;
    public GameObject playerCenter;
    public GameObject playerGlobalCenter;
    public GameObject cameraCenter;
    public GameObject cameraCenter2;

    Vector3 mouseWorldPoint;
    Vector3 mouseScreenPoint;

    public FloatingJoystick joy;

    float startMousePosX = 0;
    float startMousePosZ = 0;
    float startPlayerX = 0;
    float startPlayerZ = 0;

    public LayerMask layer;

    public float rotateSpeed;

    PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!_playerController.isDead)
        {
            MouseInputSettings();
            PlayerAnimation();
            //CameraMove();
            Move();
        }
    }

    void MouseInputSettings()
    {
        mouseWorldPoint = Input.mousePosition;
        mouseWorldPoint.z = 10.0f;
        mouseWorldPoint = cam.ScreenToWorldPoint(mouseWorldPoint);

        mouseScreenPoint = Input.mousePosition;
        mouseScreenPoint.z = 10.0f;
    }

    void PlayerAnimation()
    {
        foreach (GameObject gm in wheelsObj)
        {
            gm.transform.Rotate(new Vector3(wheelRotateSpeed * Time.deltaTime, 0, 0));
        }
    }

    void CameraMove()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 playerScreenPos;
            playerScreenPos = transform.position;
            playerScreenPos = cam.WorldToScreenPoint(playerScreenPos);

            if (playerScreenPos.x < (float)Screen.width / 100 * screenProcentToRotate)
            {
                Camera.main.GetComponent<CameraController>().follow = true;

                //float _needPocent = (float)Screen.width / 100 * screenProcentToRotate;
                //cameraRotateSpeed = (_needPocent - playerScreenPos.x) / 10 * cameraRotateSpeedCoeff * Time.deltaTime;

                //cameraCenter.transform.position = new Vector3(cameraCenter.transform.position.x - cameraRotateSpeed * Time.deltaTime,
                //                                              cameraCenter.transform.position.y,
                //                                              cameraCenter.transform.position.z);
                //boundXMin -= cameraRotateSpeed * Time.deltaTime;
                //boundXMax -= cameraRotateSpeed * Time.deltaTime;
                //camMoveLeft = true;
                //camMoveRight = false;
            }
            else if (playerScreenPos.x > (float)Screen.width / 100 * (100 - screenProcentToRotate))
            {
                Camera.main.GetComponent<CameraController>().follow = true;
                //float _needPocent = (float)Screen.width / 100 * (100 - screenProcentToRotate);
                //cameraRotateSpeed = (playerScreenPos.x - _needPocent) / 10 * cameraRotateSpeedCoeff * Time.deltaTime;

                //cameraCenter.transform.position = new Vector3(cameraCenter.transform.position.x + cameraRotateSpeed * Time.deltaTime,
                //                                              cameraCenter.transform.position.y,
                //                                              cameraCenter.transform.position.z);
                //boundXMin += cameraRotateSpeed * Time.deltaTime;
                //boundXMax += cameraRotateSpeed * Time.deltaTime;
                //camMoveLeft = false;
                //camMoveRight = true;
            }
            else
            {
                Camera.main.GetComponent<CameraController>().follow = false;
                //camMoveLeft = false;
                //camMoveRight = false;
            }
        }
    }

    void Move()
    {
        RaycastHit _hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Vector3 newPos = new Vector3(0, 0, 0);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, layer))
        {
            newPos = new Vector3(_hit.point.x, _hit.point.y, _hit.point.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            float coeff = Screen.width / (boundX * 2);

            Vector3 camPos;
            camPos = Input.mousePosition;

            float x = camPos.x / coeff - boundX;

            startMousePosX = x;
            startMousePosZ = newPos.z;

            startPlayerX = transform.position.x;
            startPlayerZ = transform.position.z;
        }

        if (Input.GetMouseButton(0))
        {
            float coeff = Screen.width / (boundX * 2);

            Vector3 camPos;
            camPos = Input.mousePosition;

            float x = camPos.x / coeff - boundX;

            //if (x < boundXMin) x = boundXMin;
            //if (x > boundXMax) x = boundXMax;

            float currentX = startPlayerX + (x - startMousePosX);
            float currentZ = startPlayerZ + (newPos.z - startMousePosZ) * coeffZ;

            #region Bounds
            if (currentX < boundXMin) currentX = boundXMin;
            if (currentZ < boundZMin) currentZ = boundZMin;
            if (currentX > boundXMax) currentX = boundXMax;
            if (currentZ > boundZMax) currentZ = boundZMax;
            #endregion

            #region Animations
            //if (currentX < transform.position.x - 0.1f || camMoveLeft)
            //{
            //    mesh.transform.DOLocalRotate(new Vector3(0, -20, 0), playerAnimRotateSpeed);
            //    wheelLeftObj.transform.DOLocalRotate(new Vector3(0, -25, 0), 0);
            //    wheelRightObj.transform.DOLocalRotate(new Vector3(0, -25, 0), 0);
            //    //transform.eulerAngles = new Vector3(0, -10f, 0);
            //}
            //else if (currentX > transform.position.x + 0.1f || camMoveRight)
            //{
            //    //transform.eulerAngles = new Vector3(0, 10f, 0);
            //    mesh.transform.DOLocalRotate(new Vector3(0, 20, 0), playerAnimRotateSpeed);
            //    wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 25, 0), 0);
            //    wheelRightObj.transform.DOLocalRotate(new Vector3(0, 25, 0), 0);
            //}
            //else
            //{
            //    //transform.eulerAngles = new Vector3(0, 0f, 0);
            //    mesh.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            //    wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            //    wheelRightObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            //}
            #endregion


            //transform.position = new Vector3(currentX, 0, currentZ);

            

            transform.DOMove(new Vector3(currentX, 0, currentZ), 0.1f);

            //transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            wheelRightObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
        }
    }
}
