using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle1Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player_bullet")
        {
            other.gameObject.transform.localEulerAngles = new Vector3(other.gameObject.transform.localEulerAngles.x, 
                                                                      other.gameObject.transform.localEulerAngles.y - 180,
                                                                      other.gameObject.transform.localEulerAngles.z);

            other.gameObject.GetComponent<DefaultGunBullet>().isPlayerAttack = true;
        }
    }
}
