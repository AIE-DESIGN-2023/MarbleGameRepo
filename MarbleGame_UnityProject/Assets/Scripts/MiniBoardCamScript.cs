using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoardCamScript : MonoBehaviour
{

    [SerializeField] PlayerController Pcontroller;
    [SerializeField] CinemachineVirtualCamera CMVirtualCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CinemachineOrbitalTransposer CMOrbitalTransposer = CMVirtualCam.GetComponent<CinemachineOrbitalTransposer>();
        float activeCamValue = Pcontroller.activeCamValue;
        CMOrbitalTransposer.m_XAxis.Value = activeCamValue;
    }
}
