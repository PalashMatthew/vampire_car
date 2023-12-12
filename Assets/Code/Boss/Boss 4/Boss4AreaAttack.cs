using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Boss4AreaAttack : MonoBehaviour
{
    public ParticleSystem vfxArea;
    public ParticleSystem vfxMeteor;

    public float timeDestroy;

    [HideInInspector] public EnemyController _controller;

    public float damage;


    private void Start()
    {
        vfxArea.Play();
        vfxMeteor.Play();

        Destroy(gameObject, timeDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(damage);
            _controller.BackDamage(damage);
        }
    }
}
