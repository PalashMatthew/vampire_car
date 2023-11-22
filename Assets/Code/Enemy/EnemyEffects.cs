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
            float value = 3 + (3 / 100 * GameObject.Find("Player").GetComponent<PlayerStats>().effectDuration);
            StartCoroutine(Fire(damage / 100 * 30, value));
            isFire = true;
        }        
    }

    IEnumerator Fire(float damage, float effectCount)
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
            float value = 3 + (3 / 100 * GameObject.Find("Player").GetComponent<PlayerStats>().effectDuration);
            StartCoroutine(Poison(damage, value));
            isPoison = true;
        }
    }

    IEnumerator Poison(float damage, float effectCount)
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
