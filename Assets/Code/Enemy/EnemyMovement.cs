using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyController _enemyController;
    
    public bool isLazer;
    public float moveSpeed;

    [Header("Local Move")]
    public bool localMove;  //Будет ли статичный враг двигаться вправо влево после того как выедет на экран?
    private bool _isStartLocalMove;
    public float localMoveSpeed;
    private Vector3 localMoveDirection;
    public float maxX, minX;

    [Header("NavMesh")]
    public LayerMask layer;
    RaycastHit hitForward, hitForward2, hitForward3;
    RaycastHit hitRight;
    RaycastHit hitLeft;
    RaycastHit hitForwardLeft;
    RaycastHit hitForwardRight;
    Ray rayForward, rayForward2, rayForward3;
    Ray rayRight;
    Ray rayLeft;
    Ray rayForwardLeft;
    Ray rayForwardRight;
    public Transform posRayForwardRight;
    public Transform posRayForwardLeft;

    public float _saveMoveSpeed;
    public bool _isRotate;


    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();        
    }

    private void Update()
    {
        moveSpeed = _enemyController.moveSpeed;

        if (_enemyController.carType == EnemyController.CarType.Movement)
        {
            BaseMovement();
        }

        if (_enemyController.carType == EnemyController.CarType.Static)
        {
            if (transform.position.z > 45)
                BaseMovement();
            else if (!_enemyController.isCarStop)
                _enemyController.isCarStop = true;

            if (_enemyController.isCarStop && localMove)
            {
                StartCoroutine(LocalMoveEnum());
                localMove = false;
            }

            if (_enemyController.isCarStop && isLazer)
            {
                gameObject.GetComponent<EnemyLazer>().StartCoroutine(gameObject.GetComponent<EnemyLazer>().AttackEnum());
                isLazer = false;
            }

            if (_isStartLocalMove)
            {
                LocalMove();
            }
        }

        //NavMesh();
    }

    void BaseMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    public IEnumerator LocalMoveEnum()
    {
        _isStartLocalMove = true;
        //transform.DOMoveX(transform.position.x + 4, 2);
        localMoveDirection = Vector3.right;

        yield return new WaitForSeconds(2);

        //transform.DOMoveX(transform.position.x - 4, 2);
        localMoveDirection = Vector3.left;

        yield return new WaitForSeconds(2);

        _isStartLocalMove = false;
        StartCoroutine(LocalMoveEnum());
    }

    void LocalMove()
    {
        transform.Translate(localMoveDirection * Time.deltaTime * localMoveSpeed);

        if (transform.position.x > maxX) transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        if (transform.position.x < minX) transform.position = new Vector3(minX, transform.position.y, transform.position.z);
    }

    void NavMesh()
    {
        #region Forward
        rayForward = new Ray(new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z - 2.25f), -Vector3.forward);

        Physics.Raycast(rayForward, out hitForward, 7, layer);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z - 2.25f), -Vector3.forward * 7, Color.red);
        #endregion

        #region Forward2
        rayForward2 = new Ray(new Vector3(transform.position.x - 1.25f, transform.position.y + 1.2f, transform.position.z - 2.25f), -Vector3.forward);

        Physics.Raycast(rayForward2, out hitForward2, 7, layer);
        Debug.DrawRay(new Vector3(transform.position.x - 1.25f, transform.position.y + 1.2f, transform.position.z - 2.25f), -Vector3.forward * 7, Color.red);
        #endregion

        #region Forward3
        rayForward3 = new Ray(new Vector3(transform.position.x + 1.25f, transform.position.y + 1.2f, transform.position.z - 2.25f), -Vector3.forward);

        Physics.Raycast(rayForward3, out hitForward3, 7, layer);
        Debug.DrawRay(new Vector3(transform.position.x + 1.25f, transform.position.y + 1.2f, transform.position.z - 2.25f), -Vector3.forward * 7, Color.red);
        #endregion

        //#region Right
        //rayRight = new Ray(new Vector3(transform.position.x - 1.5f, transform.position.y + 1.2f, transform.position.z), Vector3.left);

        //Physics.Raycast(rayRight, out hitRight, 7, layer);
        //Debug.DrawRay(new Vector3(transform.position.x - 1.5f, transform.position.y + 1.2f, transform.position.z), Vector3.left * 7, Color.red);
        //#endregion

        //#region Left
        //rayLeft = new Ray(new Vector3(transform.position.x + 1.25f, transform.position.y + 1.2f, transform.position.z), Vector3.right);

        //Physics.Raycast(rayLeft, out hitLeft, 7, layer);
        //Debug.DrawRay(new Vector3(transform.position.x + 1.25f, transform.position.y + 1.2f, transform.position.z), Vector3.right * 7, Color.red);
        //#endregion

        //#region ForwardLeft
        //rayForwardLeft = new Ray(posRayForwardLeft.position, posRayForwardLeft.forward);

        //Physics.Raycast(rayForwardLeft, out hitForwardLeft, 7, layer);
        //Debug.DrawRay(posRayForwardLeft.position, posRayForwardLeft.forward * 7, Color.red);
        //#endregion

        //#region ForwardRight
        //rayForwardRight = new Ray(posRayForwardRight.position, posRayForwardRight.forward);

        //Physics.Raycast(rayForwardRight, out hitForwardRight, 7, layer);
        //Debug.DrawRay(posRayForwardRight.position, posRayForwardRight.forward * 7, Color.red);
        //#endregion

        if (hitForward.collider != null || hitForward2.collider != null || hitForward3.collider != null && !_isRotate)
        {
            if (hitForward.collider != null)
            {
                if (hitForward.collider.gameObject.tag == "enemy")
                {
                    _enemyController.moveSpeed = hitForward.collider.gameObject.GetComponent<EnemyController>().moveSpeed;
                }
            } else if (hitForward2.collider != null)
            {
                if (hitForward2.collider.gameObject.tag == "enemy")
                {
                    _enemyController.moveSpeed = hitForward2.collider.gameObject.GetComponent<EnemyController>().moveSpeed;
                }
            }
            else if (hitForward3.collider != null)
            {
                if (hitForward3.collider.gameObject.tag == "enemy")
                {
                    _enemyController.moveSpeed = hitForward3.collider.gameObject.GetComponent<EnemyController>().moveSpeed;
                }
            } else
            {
                _enemyController.moveSpeed = 1;
            }
            
            _isRotate = true;
        }

        if (hitForward.collider == null && hitForward2.collider == null && hitForward3.collider == null && _isRotate)
        {
            _enemyController.moveSpeed = _saveMoveSpeed;
            moveSpeed = _saveMoveSpeed;
            _isRotate = false;
        }

        //if (hitForward.collider != null && !_isRotate)
        //{
        //    if (hitRight.collider == null)
        //    {
        //        transform.DORotate(new Vector3(0, 215f, 0), 0.3f);
        //        _isRotate = true;
        //        StartCoroutine(back(2));
        //        return;
        //    }

        //    if (hitLeft.collider == null)
        //    {
        //        transform.DORotate(new Vector3(0, 145f, 0), 0.3f);
        //        _isRotate = true;
        //        StartCoroutine(back(1));
        //        return;
        //    }
        //} else if (hitForward2.collider != null && !_isRotate)
        //{
        //    if (hitLeft.collider == null && hitForwardLeft.collider == null)
        //    {
        //        transform.DORotate(new Vector3(0, 145f, 0), 0.3f);
        //        _isRotate = true;
        //        StartCoroutine(back(1));
        //        return;
        //    }
        //} else if (hitForward3.collider != null && !_isRotate)
        //{
        //    if (hitRight.collider == null && hitForwardRight.collider == null)
        //    {
        //        transform.DORotate(new Vector3(0, 215f, 0), 0.3f);
        //        _isRotate = true;
        //        StartCoroutine(back(2));
        //        return;
        //    }
        //} else
        //{
        //    moveSpeed = 0;
        //}
    }

    IEnumerator back(int dir)
    {
        yield return new WaitForSeconds(0.1f);
        
        //Left
        if (dir == 1)
        {
            if (hitLeft.collider != null || hitForwardLeft.collider != null)
            {
                transform.DORotate(new Vector3(0, 180f, 0), 0.3f);
                _isRotate = false;
                yield break;
            }

            if (hitForward2.collider != null || hitForward3.collider != null)
            {
                StartCoroutine(back(dir));
            }
            else
            {
                //yield return new WaitForSeconds(0.2f);
                transform.DORotate(new Vector3(0, 180f, 0), 0.3f);
                _isRotate = false;
            }
        } else
        {
            if (hitRight.collider != null || hitForwardRight.collider != null)
            {
                transform.DORotate(new Vector3(0, 180f, 0), 0.3f);
                _isRotate = false;
                yield break;
            }

            if (hitForward2.collider != null || hitForward3.collider != null)
            {
                StartCoroutine(back(dir));
            }
            else
            {
                //yield return new WaitForSeconds(0.2f);
                transform.DORotate(new Vector3(0, 180f, 0), 0.3f);
                _isRotate = false;
            }
        }
        

        
    }
}
