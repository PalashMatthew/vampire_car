using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrow : MonoBehaviour
{
    [HideInInspector]
    public Gun _gunController;

    public GameObject bulletObj;

    private PlayerStats _playerStats;
    private PlayerPassiveController _playerPassiveController;

    private float _damage;
    private float punchingCount;


    private void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _playerPassiveController = GameObject.Find("Player").GetComponent<PlayerPassiveController>();

        _damage = _gunController.damage;

        punchingCount = _playerStats.punchingCount;

        StartCoroutine(Timer());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _gunController.bulletMoveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            _gunController.DamageEnemy(other.gameObject, gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "boss")
        {
            _gunController.DamageBoss(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
            Destroy(gameObject);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.6f);
        Attack();
    }

    void Attack()
    {
        GameObject _bulletObj1 = Instantiate(bulletObj, transform.position, Quaternion.identity);
        _bulletObj1.GetComponent<BowBullet>()._gunController = _gunController;        
        _bulletObj1.transform.eulerAngles = new Vector3(0, -45f, 90);

        GameObject _bulletObj2 = Instantiate(bulletObj, transform.position, Quaternion.identity);
        _bulletObj2.GetComponent<BowBullet>()._gunController = _gunController;
        _bulletObj2.transform.eulerAngles = new Vector3(0, 45f, 90);

        if (_gunController.projectileValue == 3)
        {
            GameObject _bulletObj3 = Instantiate(bulletObj, transform.position, Quaternion.identity);
            _bulletObj3.GetComponent<BowBullet>()._gunController = _gunController;
            _bulletObj3.transform.eulerAngles = new Vector3(0, 0, 90);
        }

        if (_gunController.projectileValue == 4)
        {
            GameObject _bulletObj3 = Instantiate(bulletObj, transform.position, Quaternion.identity);
            _bulletObj3.GetComponent<BowBullet>()._gunController = _gunController;
            _bulletObj3.transform.eulerAngles = new Vector3(0, 0, 90);

            GameObject _bulletObj4 = Instantiate(bulletObj, transform.position, Quaternion.identity);
            _bulletObj4.GetComponent<BowBullet>()._gunController = _gunController;
            _bulletObj4.transform.eulerAngles = new Vector3(0, -90f, 90);
        }

        if (_gunController.projectileValue == 5)
        {
            GameObject _bulletObj3 = Instantiate(bulletObj, transform.position, Quaternion.identity);
            _bulletObj3.GetComponent<BowBullet>()._gunController = _gunController;
            _bulletObj3.transform.eulerAngles = new Vector3(0, 0, 90);

            GameObject _bulletObj4 = Instantiate(bulletObj, transform.position, Quaternion.identity);
            _bulletObj4.GetComponent<BowBullet>()._gunController = _gunController;
            _bulletObj4.transform.eulerAngles = new Vector3(0, -90f, 90);

            GameObject _bulletObj5 = Instantiate(bulletObj, transform.position, Quaternion.identity);
            _bulletObj5.GetComponent<BowBullet>()._gunController = _gunController;
            _bulletObj5.transform.eulerAngles = new Vector3(0, 90f, 90);
        }

        Destroy(gameObject);
    }
}
