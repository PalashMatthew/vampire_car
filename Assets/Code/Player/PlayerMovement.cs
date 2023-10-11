using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    public bool isMoveAccess;

    public float moveSpeed;
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

    Vector3 newPos;

    RaycastHit _hit;
    Ray ray;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        isMoveAccess = true;
    }

    private void Update()
    {
        if (!_playerController.isDead && isMoveAccess && !GameplayController.isPause)
        {
            MouseInputSettings();
            PlayerAnimation();
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
        if (Input.GetMouseButtonUp(0))
        {
            mesh.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            wheelRightObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
        }
    }

    void Move()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            ray = cam.ScreenPointToRay(touch.position);

            newPos = new Vector3(0, 0, 0);

            if (Physics.Raycast(ray, out _hit, Mathf.Infinity, layer))
            {
                newPos = new Vector3(_hit.point.x, _hit.point.y, _hit.point.z);
            }

            if (touch.phase == TouchPhase.Began)
            {
                float coeff = Screen.width / (boundX * 2);

                Vector3 camPos;
                camPos = touch.position;

                float x = camPos.x / coeff - boundX;

                startMousePosX = x;
                startMousePosZ = newPos.z;

                startPlayerX = transform.position.x;
                startPlayerZ = transform.position.z;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                float coeff = Screen.width / (boundX * 2);

                Vector3 camPos;
                camPos = touch.position;

                float x = camPos.x / coeff - boundX;

                float currentX = startPlayerX + (x - startMousePosX);
                float currentZ = startPlayerZ + (newPos.z - startMousePosZ) * coeffZ;

                #region Bounds
                if (currentX < boundXMin) currentX = boundXMin;
                if (currentZ < boundZMin) currentZ = boundZMin;
                if (currentX > boundXMax) currentX = boundXMax;
                if (currentZ > boundZMax) currentZ = boundZMax;
                #endregion

                #region Animations
                if (currentX < transform.position.x - 0.1f)
                {
                    mesh.transform.DOLocalRotate(new Vector3(0, -20, 0), playerAnimRotateSpeed);
                    wheelLeftObj.transform.DOLocalRotate(new Vector3(0, -25, 0), 0);
                    wheelRightObj.transform.DOLocalRotate(new Vector3(0, -25, 0), 0);
                    //transform.eulerAngles = new Vector3(0, -10f, 0);
                }
                else if (currentX > transform.position.x + 0.1f)
                {
                    //transform.eulerAngles = new Vector3(0, 10f, 0);
                    mesh.transform.DOLocalRotate(new Vector3(0, 20, 0), playerAnimRotateSpeed);
                    wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 25, 0), 0);
                    wheelRightObj.transform.DOLocalRotate(new Vector3(0, 25, 0), 0);
                }
                else
                {
                    //transform.eulerAngles = new Vector3(0, 0f, 0);
                    mesh.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
                    wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
                    wheelRightObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
                }
                #endregion

                transform.DOMove(new Vector3(currentX, 0, currentZ), moveSpeed).SetUpdate(true);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
                wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
                wheelRightObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            }
        }
#endif
#if UNITY_EDITOR
        ray = cam.ScreenPointToRay(Input.mousePosition);

        newPos = new Vector3(0, 0, 0);

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

            float currentX = startPlayerX + (x - startMousePosX);
            float currentZ = startPlayerZ + (newPos.z - startMousePosZ) * coeffZ;

            #region Bounds
            if (currentX < boundXMin) currentX = boundXMin;
            if (currentZ < boundZMin) currentZ = boundZMin;
            if (currentX > boundXMax) currentX = boundXMax;
            if (currentZ > boundZMax) currentZ = boundZMax;
            #endregion

            #region Animations
            if (currentX < transform.position.x - 0.1f)
            {
                mesh.transform.DOLocalRotate(new Vector3(0, -20, 0), playerAnimRotateSpeed);
                wheelLeftObj.transform.DOLocalRotate(new Vector3(0, -25, 0), 0);
                wheelRightObj.transform.DOLocalRotate(new Vector3(0, -25, 0), 0);
                //transform.eulerAngles = new Vector3(0, -10f, 0);
            }
            else if (currentX > transform.position.x + 0.1f)
            {
                //transform.eulerAngles = new Vector3(0, 10f, 0);
                mesh.transform.DOLocalRotate(new Vector3(0, 20, 0), playerAnimRotateSpeed);
                wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 25, 0), 0);
                wheelRightObj.transform.DOLocalRotate(new Vector3(0, 25, 0), 0);
            }
            else
            {
                //transform.eulerAngles = new Vector3(0, 0f, 0);
                mesh.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
                wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
                wheelRightObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            }
            #endregion

            transform.DOMove(new Vector3(currentX, 0, currentZ), moveSpeed).SetUpdate(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            wheelLeftObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
            wheelRightObj.transform.DOLocalRotate(new Vector3(0, 0, 0), playerAnimRotateSpeed);
        }
#endif
    }

    //Лужа
    #region Puddle 
    public void PuddleActivate()  //Включаем когда наехали на лужу
    {
        StartCoroutine(Puddle());
        StartCoroutine(PuddleAnimation());
    }

    IEnumerator Puddle()
    {
        isMoveAccess = false;
        cam.GetComponent<CameraController>().follow = false;
        yield return new WaitForSeconds(1);

        RaycastHit _hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        newPos = new Vector3(0, 0, 0);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, layer))
        {
            newPos = new Vector3(_hit.point.x, _hit.point.y, _hit.point.z);
        }

        float coeff = Screen.width / (boundX * 2);

        Vector3 camPos;
        camPos = Input.mousePosition;

        float x = camPos.x / coeff - boundX;

        startMousePosX = x;
        startMousePosZ = newPos.z;

        startPlayerX = transform.position.x;
        startPlayerZ = transform.position.z;

        isMoveAccess = true;
        cam.GetComponent<CameraController>().UpdateCoord();
        cam.GetComponent<CameraController>().follow = true;
    }

    IEnumerator PuddleAnimation()
    {
        transform.DORotate(new Vector3(0, -20, 0), 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.DORotate(new Vector3(0, 20, 0), 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.DORotate(new Vector3(0, -20, 0), 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.DORotate(new Vector3(0, 20, 0), 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        yield return new WaitForSeconds(0.2f);
    }
    #endregion
}
