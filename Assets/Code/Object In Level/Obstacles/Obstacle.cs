using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float damage;
    public float hp;

    public bool isDestroyedObj;

    public MeshRenderer meshRenderer;

    #region Hit
    public void Hit(float _damage)
    {
        if (isDestroyedObj)
        {
            hp -= _damage;
            GetComponentInChildren<EnemyUI>().ViewDamage((int)_damage);
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

    void Dead()
    {

    }
}
