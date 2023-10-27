using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerMesh : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerMovement playerMovement;

    public string carName;

    public GameObject mesh;
    public MeshRenderer meshRenderer;


    public void CarChoise()
    {
        mesh.SetActive(true);

        playerController.meshRenderer.Clear();
        playerController.meshRenderer.Add(meshRenderer);

        playerMovement.mesh = gameObject;
    }
}
