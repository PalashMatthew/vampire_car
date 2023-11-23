using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Base")]
    public float damage;
    public float hp;

    public bool isDestroyedObj;

    public MeshRenderer meshRenderer;

    [Header("Movement")]
    public bool isMove;
    public float moveSpeed;

    public GameObject screwObj;
    public GameObject fxExplosion;

    public bool isSpeedUp;
    public float newSpeed;

    bool isVisible;



    private void Update()
    {
        if (isMove)
        {
            Move();
        }

        if (transform.position.z < -20)
        {
            Destroy(gameObject);
        }

        if (isSpeedUp)
        {
            if (moveSpeed < newSpeed)
            {
                moveSpeed += Time.deltaTime * 2;
            }

            if (moveSpeed > newSpeed)
            {
                moveSpeed = newSpeed;
                isSpeedUp = false;
            }
        }

        CheckVisible();
    }

    #region Hit
    public void Hit(float _damage)
    {
        if (isDestroyedObj)
        {
            hp -= _damage;
            StartCoroutine(HitAnim());

            if (hp <= 0)
            {
                Dead();
            }
        }
    }

    IEnumerator HitAnim()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.DisableKeyword("_EMISSION");
    }
    #endregion

    public void Dead()
    {
        GameObject.Find("Generate Controller").GetComponent<GenerateObstacles>().instObstacles.Remove(gameObject);

        //Instantiate(screwObj, transform.position, transform.rotation);

        GameObject _fx = Instantiate(fxExplosion, transform.position, transform.rotation);
        Destroy(_fx, 3);

        Destroy(gameObject);
    }

    private void Move()
    {
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isDestroyedObj)
    //    {
    //        if (other.gameObject.tag == "enemy" && isVisible)
    //        {
    //            Dead();
    //        }
    //    }        
    //}

    void CheckVisible()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            isVisible = true;
        }
        else
        {
            if (isVisible)
            {
                isVisible = false;
            }
        }
    }
}
