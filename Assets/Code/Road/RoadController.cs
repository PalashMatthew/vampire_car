using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    //public float moveSpeed;

    //MeshRenderer meshRenderer;

    //private void Start()
    //{
    //    meshRenderer = GetComponent<MeshRenderer>();
    //}

    //private void Update()
    //{
    //    meshRenderer.material.mainTextureOffset = new Vector2(0, meshRenderer.material.mainTextureOffset.y + moveSpeed * Time.deltaTime);
    //}

    private void Update()
    {
        if (transform.position.z <= -114.5f)
        {
            Instantiate(gameObject, new Vector3(transform.position.x, 0, 242.5f), transform.rotation);
            Destroy(gameObject);
        }
    }
}
