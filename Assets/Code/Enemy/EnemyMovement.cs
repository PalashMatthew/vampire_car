using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyController _enemyController;
    
    public bool isLazer;
    public float moveSpeed;

    private bool _isMoveAccess = true;

    [Header("Local Move")]
    public bool localMove;  //Будет ли статичный враг двигаться вправо влево после того как выедет на экран?
    public bool _isStartLocalMove;
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

    private float _zPosStop;

    private bool _isJumping = false;


    private void Start()
    {
        _isMoveAccess = true;
        _enemyController = GetComponent<EnemyController>();

        if (_enemyController.carType == EnemyController.CarType.Static)
        {
            _zPosStop = Random.Range(39f, 56f);
        }
    }

    private void Update()
    {
        moveSpeed = _enemyController.moveSpeed;

        if (_isMoveAccess)
        {
            if (_enemyController.carType == EnemyController.CarType.Movement)
            {
                BaseMovement();
            }

            if (_enemyController.carType == EnemyController.CarType.Static)
            {
                if (transform.position.z > _zPosStop)
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
        }

        NavMesh();
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

        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            localMoveDirection = Vector3.right;
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            localMoveDirection = Vector3.left;
        }
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

        if (hitForward.collider != null || hitForward2.collider != null || hitForward3.collider != null)
        {
            if (hitForward.collider != null)
            {
                if (hitForward.collider.gameObject.tag == "enemy")
                {
                    hitForward.collider.gameObject.GetComponent<EnemyController>().moveSpeed = _enemyController.moveSpeed;
                }

                if (hitForward.collider.gameObject.tag == "obstacle" && hitForward.collider.gameObject.layer == 8)
                {
                    hitForward.collider.gameObject.GetComponent<Obstacle>().moveSpeed = _enemyController.moveSpeed;
                }
            } 
            else if (hitForward2.collider != null)
            {
                if (hitForward2.collider.gameObject.tag == "enemy")
                {
                    hitForward2.collider.gameObject.GetComponent<EnemyController>().moveSpeed = _enemyController.moveSpeed;
                }

                if (hitForward2.collider.gameObject.tag == "obstacle" && hitForward2.collider.gameObject.layer == 8)
                {
                    hitForward2.collider.gameObject.GetComponent<Obstacle>().moveSpeed = _enemyController.moveSpeed;
                }
            }
            else if (hitForward3.collider != null)
            {
                if (hitForward3.collider.gameObject.tag == "enemy")
                {
                    hitForward3.collider.gameObject.GetComponent<EnemyController>().moveSpeed = _enemyController.moveSpeed;
                }

                if (hitForward3.collider.gameObject.tag == "obstacle" && hitForward3.collider.gameObject.layer == 8)
                {
                    hitForward3.collider.gameObject.GetComponent<Obstacle>().moveSpeed = _enemyController.moveSpeed;
                }
            } 
        }
    }

    IEnumerator JumpAnim()
    {
        transform.DOMoveZ(transform.position.z - 7.5f, 0.3f).SetEase(Ease.Linear);
        transform.DORotate(new Vector3(-26f, 180, 0), 0.1f).SetEase(Ease.Linear);
        transform.DOMoveY(2.88f, 0.3f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.1f);
        transform.DORotate(new Vector3(0f, 180, 0), 0.1f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.2f);
        transform.DOMoveZ(transform.position.z - 2, 0.2f).SetEase(Ease.Linear);        

        yield return new WaitForSeconds(0.1f);

        transform.DORotate(new Vector3(26f, 180, 0), 0.1f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.1f);

        transform.DOMoveZ(transform.position.z - 2, 0.4f).SetEase(Ease.Linear);
        transform.DOMoveY(0f, 0.3f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.3f);

        transform.DORotate(new Vector3(0f, 180, 0), 0.3f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.3f);
        _isMoveAccess = true;
        _isJumping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enemyController.carType == EnemyController.CarType.Movement)
        {
            if (other.tag == "jump" && !_isJumping)
            {
                _isMoveAccess = false;
                StartCoroutine(JumpAnim());
            }
        }
    }

    public IEnumerator MoveInside()
    {
        transform.DOMoveY(46, 2);
        yield return new WaitForSeconds(2);
        _enemyController.Dead();
    }
}
