using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankBullet2 : MonoBehaviour
{
    [HideInInspector] public EnemyController _controller;
    public float damage;

    public GameObject bulletObj;
    public Vector3 target;


    private void Update()
    {
        transform.Translate(Vector3.forward * 30 * Time.deltaTime);

        if (transform.position.z < -20f)
            Destroy(gameObject);

        if (WaveController.isWaveEnd)
            Destroy(gameObject);

        if (Vector3.Distance(transform.position, target) <= 1)
        {
            GameObject gm1 = Instantiate(bulletObj, transform.position, transform.rotation);
            GameObject gm2 = Instantiate(bulletObj, transform.position, transform.rotation);
            GameObject gm3 = Instantiate(bulletObj, transform.position, transform.rotation);
            GameObject gm4 = Instantiate(bulletObj, transform.position, transform.rotation);

            gm1.transform.eulerAngles = new Vector3(0, 0, 0);
            gm2.transform.eulerAngles = new Vector3(0, 90, 0);
            gm3.transform.eulerAngles = new Vector3(0, 180, 0);
            gm4.transform.eulerAngles = new Vector3(0, -90, 0);

            gm1.GetComponent<BossTankBullet1>().damage = 10;
            gm1.GetComponent<BossTankBullet1>()._controller = _controller;

            gm2.GetComponent<BossTankBullet1>().damage = 10;
            gm2.GetComponent<BossTankBullet1>()._controller = _controller;

            gm3.GetComponent<BossTankBullet1>().damage = 10;
            gm3.GetComponent<BossTankBullet1>()._controller = _controller;

            gm4.GetComponent<BossTankBullet1>().damage = 10;
            gm4.GetComponent<BossTankBullet1>()._controller = _controller;

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.gameObject.GetComponent<PlayerController>().Hit(damage);
            _controller.BackDamage(damage);

            Destroy(gameObject);
        }

        if (other.tag == "obstacle")
        {
            Destroy(gameObject);
        }
    }
}
