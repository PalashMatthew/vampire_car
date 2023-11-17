using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    EnemyController enemyController;

    //Fire
    public ParticleSystem vfxFire;
    bool isFire;

    //Fire
    public ParticleSystem vfxPoison;
    bool isPoison;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();

        isFire = false;
        isPoison = false;
    }

    #region Fire Effect
    public void FireEffect(float damage)
    {
        if (!isFire)
        {
            vfxFire.Play();
            StartCoroutine(Fire(damage / 100 * 30, 3));
            isFire = true;
        }        
    }

    IEnumerator Fire(float damage, int effectCount)
    {
        yield return new WaitForSeconds(1);

        if (effectCount > 0)
        {
            enemyController.Hit(damage, false);

            effectCount--;
            StartCoroutine(Fire(damage, effectCount));
        }        
        else
        {
            isFire = false;
            vfxFire.Stop();
        }
    }
    #endregion

    #region Poison Effect
    public void PoisonEffect(float damage)
    {
        if (!isPoison)
        {
            vfxPoison.Play();
            StartCoroutine(Poison(damage, 3));
            isPoison = true;
        }
    }

    IEnumerator Poison(float damage, int effectCount)
    {
        damage = damage - (damage / 100 * 25);

        yield return new WaitForSeconds(1);

        if (effectCount > 0)
        {
            enemyController.Hit(damage, false);

            effectCount--;
            StartCoroutine(Poison(damage, effectCount));
        }
        else
        {
            isPoison = false;
            vfxPoison.Stop();
        }
    }
    #endregion
}
