using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireTruckMeshMove : MonoBehaviour
{
    GameObject _player;
    public BossFireTruckController _controller;
    public int meshNum;

    public float moveSpeed;
    public GameObject boomObj;

    public float timer;

    public bool isMove;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (isMove)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            transform.LookAt(_player.transform.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            if (Vector3.Distance(transform.position, _player.transform.position) < 5f)
            {
                GameObject _inst = Instantiate(boomObj, transform.position, transform.rotation);
                _inst.GetComponent<BossFireTruckBoom>()._brain = _controller;
                switch (meshNum)
                {
                    case 1:
                        _controller.mesh1Destroy = true;
                        break;

                    case 2:
                        _controller.mesh2Destroy = true;
                        break;

                    case 3:
                        _controller.mesh3Destroy = true;
                        break;

                    case 4:
                        _controller.mesh4Destroy = true;
                        break;

                    case 5:
                        _controller.mesh5Destroy = true;
                        break;

                    case 6:
                        _controller.mesh6Destroy = true;
                        break;
                }

                Destroy(gameObject);
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                GameObject _inst = Instantiate(boomObj, transform.position, transform.rotation);
                _inst.GetComponent<BossFireTruckBoom>()._brain = _controller;
                switch (meshNum)
                {
                    case 1:
                        _controller.mesh1Destroy = true;
                        break;

                    case 2:
                        _controller.mesh2Destroy = true;
                        break;

                    case 3:
                        _controller.mesh3Destroy = true;
                        break;

                    case 4:
                        _controller.mesh4Destroy = true;
                        break;

                    case 5:
                        _controller.mesh5Destroy = true;
                        break;

                    case 6:
                        _controller.mesh6Destroy = true;
                        break;
                }

                Destroy(gameObject);
            }
        }
    }
}
