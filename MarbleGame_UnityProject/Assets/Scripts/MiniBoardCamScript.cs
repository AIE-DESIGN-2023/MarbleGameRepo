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
        CMVirtualCam.GetComponent<CinemachineOrbitalTransposer>().m_XAxis.Value = Pcontroller.activeCamValue;
    }
}
