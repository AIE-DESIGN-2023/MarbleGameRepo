using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfSceneCameraChange : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        foreach(GameObject cam in playerController.cameras)
        {
            cam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }

        playerController.activeCam = playerController.cameras[1];
        playerController.activeCamIndex = 1;
    }
}
