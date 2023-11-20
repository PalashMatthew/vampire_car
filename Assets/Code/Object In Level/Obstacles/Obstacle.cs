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
}
