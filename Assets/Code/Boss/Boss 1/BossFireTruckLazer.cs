using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckLazer : MonoBehaviour
{
    bool _isPause;

    public BossFireTruckController _bossController;

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "player" && !_isPause)
        {
            other.gameObject.GetComponent<PlayerController>().Hit(_bossController.damage);
            _isPause = true;
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(0.3f);
        _isPause = false;
    }
}