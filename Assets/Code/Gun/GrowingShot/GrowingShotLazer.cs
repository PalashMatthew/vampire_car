using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingShotLazer : MonoBehaviour
{
    public Gun _gunController;
    public GrowingShotGun _growingShotGun;

    CapsuleCollider _capsuleCollider;

    public ParticleSystem vfxShot;
    public ParticleSystem vfxPrewarm;

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(_gunController.shotSpeed - 0.5f);

        vfxPrewarm.Play();

        yield return new WaitForSeconds(0.5f);

        vfxShot.Play();
        _capsuleCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);

        _capsuleCollider.enabled = false;

        _growingShotGun.DamageEnemy();

        yield return new WaitForSeconds(0.2f);

        vfxShot.Stop();

        StartCoroutine(Attack());
    }
}
