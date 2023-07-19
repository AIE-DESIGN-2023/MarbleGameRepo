using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MarbleInHoleScript : MonoBehaviour
{
    [SerializeField] string marbleTag;
    [SerializeField] GameObject VC4;
    [SerializeField] int priorityOn;
    [SerializeField] int priorityOff;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == marbleTag)
        {
            VC4.GetComponent<CinemachineVirtualCamera>().Priority = priorityOn;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Marble")
        {
            VC4.GetComponent<CinemachineVirtualCamera>().Priority = priorityOff;
        }
    }
}
