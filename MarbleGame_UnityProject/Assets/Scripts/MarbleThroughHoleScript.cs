using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class MarbleThroughHoleScript : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerController.holeCams[0].GetComponent<CinemachineVirtualCamera>().Priority = 0;
        playerController.holeCams[1].GetComponent<CinemachineVirtualCamera>().Priority = 5;
    }


    private void OnTriggerExit(Collider other)
    {
        playerController.holeCams[1].GetComponent<CinemachineVirtualCamera>().Priority = 0;
        playerController.holeCams[2].GetComponent<CinemachineVirtualCamera>().Priority = 5;

        playerController.gameController.GetComponent<GameController>().hasBeatenLevel = true;
    }
}
