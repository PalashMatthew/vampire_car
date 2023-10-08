using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilObj : MonoBehaviour
{
    public Gun _gunController;
    public List<ParticleSystem> vfx;

    public void Initialize()
    {
        StartCoroutine(EndAttack());

        foreach(ParticleSystem _part  in vfx)
        {
            _part.Stop();

            var main = _part.main;
            main.duration = _gunController.timeOfAction - 0.6f;
            _part.Play();
        }
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(_gunController.timeOfAction);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            //other.gameObject.GetComponent<EnemyController>().Hit(_gunController.CalculateDamage());
            _gunController.DamageEnemy(other.gameObject);
        }

        if (other.tag == "obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().Hit(_gunController.CalculateDamage());
        }
    }
}
