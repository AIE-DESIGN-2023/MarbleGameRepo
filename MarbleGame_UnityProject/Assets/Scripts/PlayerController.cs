using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] GameObject[] cameras;
    [SerializeField] GameObject activeCam;

    [SerializeField] bool canChangeCamera;
    [SerializeField] float canChangeCameraTimer;
    [SerializeField] float canChangeCameraTimeLimit;

    [SerializeField] int cameraPriority1 = 1;
    [SerializeField] int cameraPriority0 = 0;
    [SerializeField] int activeCamIndex;
    [SerializeField] float activeCamValue; 

    [Space]
    [Header("GameObjects")]
    [Space]

    [SerializeField] GameObject board;
    [SerializeField] GameObject marble;
    [SerializeField] GameObject[] sides;

    [Space]
    [Header("Variables")]
    [Space]

    [SerializeField] bool playerActive;

    [Range(-1f, 1f)]
    [SerializeField] float WS;

    [Range(-1f, 1f)]
    [SerializeField] float AD;

    [SerializeField] float MAXTILT;
    [SerializeField] float TILTSPEED;



    // Start is called before the first frame update
    void Start()
    {
        cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().Priority = 1;
        activeCam = cameras[activeCamIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActive)
        {
            TiltBoard();
            CameraControl();
        }

        WS = Input.GetAxis("WS");
        AD = Input.GetAxis("AD");
        Debug.Log(Input.mouseScrollDelta.y);
    }

    private void CameraControl()
    {
        //get active cam value
        activeCamValue = cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value;

        //set all values to be equal
        for(int i = 0; i < cameras.Length; i++)
        {
            if (i != activeCamIndex)
            {
                cameras[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value = activeCamValue;
            }
        }
        
        //scroll up
        if(Input.mouseScrollDelta.y < 0 && canChangeCamera && activeCamIndex < cameras.Length-1)
        {
            //change priority
            cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().Priority = cameraPriority0;
            cameras[activeCamIndex+1].GetComponent<CinemachineVirtualCamera>().Priority = cameraPriority1;

            //increase camera index
            activeCamIndex++;

            //set can change camera to false
            canChangeCamera = false;
            canChangeCameraTimer = canChangeCameraTimeLimit;
        }

        //scroll down
        if (Input.mouseScrollDelta.y > 0 && canChangeCamera && activeCamIndex > 0)
        {
            //change priority
            cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().Priority = cameraPriority0;
            cameras[activeCamIndex - 1].GetComponent<CinemachineVirtualCamera>().Priority = cameraPriority1;

            //increase camera index
            activeCamIndex--;

            //set can change camera to false
            canChangeCamera = false;
            canChangeCameraTimer = canChangeCameraTimeLimit;
        }

        //timer
        if(!canChangeCamera)
        {
            canChangeCameraTimer -= Time.deltaTime;

            if(canChangeCameraTimer < 0)
            {
                canChangeCamera = true;
            }
        }

    }

    private void TiltBoard()
    {
        float cameraX = activeCam.transform.position.x;
        float cameraZ = activeCam.transform.position.z;

        float marbleX = marble.transform.position.x;
        float marbleZ = marble.transform.position.z;

        float diffX = cameraX - marbleX;
        float diffZ = cameraZ - marbleZ;

        float diffXAbs = Mathf.Abs(Mathf.Abs(activeCam.transform.position.x) - Mathf.Abs(marble.transform.position.x));
        float diffZAbs = Mathf.Abs(Mathf.Abs(activeCam.transform.position.z) - Mathf.Abs(marble.transform.position.z));

        //side 1
        if (diffZAbs >= diffXAbs && diffZ > 0)
        {
            Debug.Log("Side 1");
            board.transform.Rotate(WS * TILTSPEED * Time.deltaTime, 0, 0);
            board.transform.Rotate(0, 0, AD * TILTSPEED * Time.deltaTime);
        }

        //side 2
        if (diffZAbs <= diffXAbs && diffX < 0)
        {
            Debug.Log("Side 2");
            board.transform.Rotate(0, 0, WS * TILTSPEED * Time.deltaTime);
            board.transform.Rotate(-AD * TILTSPEED * Time.deltaTime, 0, 0);            
        }


        //side 3
        if (diffZAbs >= diffXAbs && diffZ < 0)
        {
            Debug.Log("Side 3");
            board.transform.Rotate(-WS * TILTSPEED * Time.deltaTime, 0, 0);
            board.transform.Rotate(0, 0, -AD * TILTSPEED * Time.deltaTime);
        }

        //side 4
        if (diffZAbs <= diffXAbs && diffX > 0)
        {
            Debug.Log("Side 4");
            board.transform.Rotate(0, 0, -WS * TILTSPEED * Time.deltaTime);
            board.transform.Rotate(AD * TILTSPEED * Time.deltaTime, 0, 0);
        }

    }
}
