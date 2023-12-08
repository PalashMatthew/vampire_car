using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKitController : MonoBehaviour
{
    public float value;

    public float moveSpeed;
    public float rotateSpeed;

    public GameObject icon;


    private void Start()
    {
        StartCoroutine(Dead());
    }

    private void Update()
    {
        transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);

        icon.transform.Rotate(new Vector3(0, rotateSpeed, 0));
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(20);

        PlayerStats _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        string _hp;

        if (_playerStats.currentHp < _playerStats.maxHp)
        {
            _hp = "NotFullHP";
        }
        else
        {
            _hp = "FullHP";
        }
        
        GameObject.Find("Firebase").GetComponent<FirebaseSetup>().Event_FirstAidKitDestroy(_hp);

        Destroy(gameObject);
    }
}
