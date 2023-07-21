using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ASSIGN THESE VARAIBLES IN EDITOR")]
    [SerializeField] public GameObject[] cameras;
    [SerializeField] public GameObject[] holeCams;
    [SerializeField] public GameObject board;
    [SerializeField] public GameObject spawnPos;
    [Space]
    [SerializeField] GameObject gameControllerPrefab;
    [SerializeField] GameObject marblePrefab;
<<<<<<< HEAD
=======
    [Space]
    [SerializeField] public int sceneIndex;
>>>>>>> origin/Ru2

    [Space]
    [Space]
    [Space]
    [Header("VARIABLES THAT ARE FOUND OR INSTANTIATED")]
    [SerializeField] public GameObject marble;
    [SerializeField] public GameObject gameController;

    [Space]
    [Space]
    [Header("Other Variables")]
    [Space]
    [Header("Camera")]
    [SerializeField] public GameObject activeCam;
    [SerializeField] int activeCamIndex;
    [Space]
    [Header("Camera ~ Lock")]
    [SerializeField] bool cameraLock;
    [SerializeField] float cameraSpeedMax = 300;
    [SerializeField] float cameraSpeedMin = 0;
    [Space]
    [Header("Camera ~ Scroll")]
    [SerializeField] bool canChangeCamera;
    [SerializeField] float canChangeCameraTimer;
    [SerializeField] float canChangeCameraTimeLimit;
    [SerializeField] int cameraPriority1 = 1;
    [SerializeField] int cameraPriority0 = 0;
    [Space]
    [Header("Camera XValue")]
    [SerializeField] float activeCamValue;
    [Space]
    [Header("Camera YValue")]
    [Range(-1, 1)]
    [SerializeField] float y_tracker;
    [SerializeField] float y_mouseAxis;
    [Space]
    [SerializeField] float VC1_ydefault;
    [SerializeField] float VC1_ydiff;
    [SerializeField] float VC2_ydefault;
    [SerializeField] float VC2_ydiff;
    [SerializeField] float VC3_ydefault;
    [SerializeField] float VC3_ydiff;

    [Space]
    [Header("Player Input Variables")]
    [Space]
    [SerializeField] public bool playerActive;
    [Space]
    [SerializeField] float WS;
    [SerializeField] float AD;
    [Space]
    [SerializeField] float TILTSPEED;
    [Space]
    [SerializeField] public Vector3 currentRot;
    [SerializeField] public Vector3 targetRot;
    [SerializeField] float moveTimer;
    [SerializeField] float MOVETIMEAMOUNT;




    // Start is called before the first frame update
    void Start()
    {
        //FIND MARBLE AND GAME CONTROLLER IF ALREADY IN LEVEL
        gameController = GameObject.FindGameObjectWithTag("GameController");
        marble = GameObject.FindGameObjectWithTag("Marble");

        //INSTANTIATE IF NOT FOUND
        if(gameController == null)
        {
            gameController = Instantiate(gameControllerPrefab);

        }

        //
        if(marble == null)
        {
            marble = Instantiate(marblePrefab, spawnPos.transform.position, Quaternion.identity, gameObject.transform);
        }


        //Camera setup 
        foreach (GameObject holeCam in holeCams)
        {
            if(holeCam != null)
                holeCam.GetComponent<CinemachineVirtualCamera>().LookAt = marble.transform;
        }
        foreach (GameObject camera in cameras)
        {
            camera.GetComponent<CinemachineVirtualCamera>().Follow = marble.transform;
            camera.GetComponent<CinemachineVirtualCamera>().LookAt = marble.transform;
        }

        cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().Priority = 1;
        activeCam = cameras[activeCamIndex];

        //boardSetup
        currentRot = board.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerActive)
        {
            TiltBoard();
            CameraControl();
            InputManager();
        }

        
    }

    private void InputManager()
    {
        WS = Input.GetAxis("WS");
        AD = Input.GetAxis("AD");
    }

    

    private void CameraControl()
    {
        CameraLock();
        foreach(GameObject holeCam in holeCams)
        {
            if(holeCam != null)
            holeCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value = activeCamValue;
        }
        

        if (!cameraLock)
        {
            ///GET VARIABLES
            //get active cam value
            activeCamValue = cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value;

            //get y value
            y_mouseAxis = Input.GetAxis("Mouse Y");
            y_tracker += y_mouseAxis;
            y_tracker = Mathf.Clamp(y_tracker, -1f, 1f);

            ///EQUALISE VALUES
            //set all values to be equal
            for (int i = 0; i < cameras.Length; i++)
            {
                if (i != activeCamIndex)
                {
                    cameras[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.Value = activeCamValue;
                }
            }

            //set y values
            //vc1
            cameras[0].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_FollowOffset.y = (y_tracker * VC1_ydiff) / 2 + VC1_ydefault;
            cameras[1].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_FollowOffset.y = (y_tracker * VC2_ydiff) / 2 + VC2_ydefault;
            cameras[2].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_FollowOffset.y = (y_tracker * VC3_ydiff) / 2 + VC3_ydefault;

            

            //SCROLLING
            //scroll up
            if (Input.mouseScrollDelta.y < 0 && canChangeCamera && activeCamIndex < cameras.Length - 1)
            {
                //change priority
                cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().Priority = cameraPriority0;
                cameras[activeCamIndex + 1].GetComponent<CinemachineVirtualCamera>().Priority = cameraPriority1;

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
            if (!canChangeCamera)
            {
                canChangeCameraTimer -= Time.deltaTime;

                if (canChangeCameraTimer < 0)
                {
                    canChangeCamera = true;
                }
            }
        }
    }

    private void CameraLock()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(cameraLock)
            {
                cameraLock = false;
                //set camera speeds;
                for (int i = 0; i < cameras.Length; i++)
                {
                    if (i != activeCamIndex)
                    {
                        cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed = cameraSpeedMax;
                    }
                }

                foreach(GameObject holeCam in holeCams)
                {
                    holeCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed = cameraSpeedMax;
                }
                

            }
            else
            {
                cameraLock = true;
                //set camera speeds;
                for (int i = 0; i < cameras.Length; i++)
                {
                    if (i != activeCamIndex)
                    {
                        cameras[activeCamIndex].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed = cameraSpeedMin;
                    }
                }

                foreach(GameObject holeCam in holeCams)
                {
                    holeCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XAxis.m_MaxSpeed = cameraSpeedMin;
                }
                
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

        if (currentRot != targetRot)
        {
            moveTimer = 0;
        }
        moveTimer += Time.deltaTime;


        //USE ACTIVE CAM ORBITAL TRACKER VALUE
        /* M is marble
         * 
         * H  A  B 
         * G  M  C
         * F  E  D
         * 
         */
        //A TESTED
        if (activeCamValue > 337.5 || activeCamValue < 22.5)
        {
            //Debug.Log("Side A");
            targetRot = new Vector3(currentRot.x - WS * TILTSPEED * Time.deltaTime, currentRot.y, currentRot.z - AD * TILTSPEED * Time.deltaTime);
        }

        //B
        else if(activeCamValue > 22.5 && activeCamValue < 67.5)
        {
            //Debug.Log("Side B");
            targetRot = new Vector3(currentRot.x - WS * TILTSPEED * Time.deltaTime - AD * TILTSPEED * Time.deltaTime, currentRot.y, currentRot.z + WS * TILTSPEED * Time.deltaTime - AD * TILTSPEED * Time.deltaTime);
        }

        //C TESTED
        else if (activeCamValue >= 67.5 && activeCamValue < 112.5)
        {
            //Debug.Log("Side C");
            targetRot = new Vector3(currentRot.x - AD * TILTSPEED * Time.deltaTime, currentRot.y, currentRot.z + WS * TILTSPEED * Time.deltaTime);
        }

        //D TESTED
        else if (activeCamValue >= 112.5 && activeCamValue < 157.5)
        {
            //Debug.Log("Side D");
            targetRot = new Vector3(currentRot.x + WS * TILTSPEED * Time.deltaTime - AD * TILTSPEED * Time.deltaTime, currentRot.y, currentRot.z + WS * TILTSPEED * Time.deltaTime + AD * TILTSPEED * Time.deltaTime);
        }

        //E TESTED
        else if (activeCamValue >= 157.5 && activeCamValue < 202.5)
        {
            //Debug.Log("Side E");
            targetRot = new Vector3(currentRot.x + WS * TILTSPEED * Time.deltaTime, currentRot.y, currentRot.z + AD * TILTSPEED * Time.deltaTime);
        }

        //F TESTED
        else if (activeCamValue >= 202.5 && activeCamValue < 247.5)
        {
            //Debug.Log("Side F");
            targetRot = new Vector3(currentRot.x + WS * TILTSPEED * Time.deltaTime + AD * TILTSPEED * Time.deltaTime, currentRot.y, currentRot.z - WS * TILTSPEED * Time.deltaTime + AD * TILTSPEED * Time.deltaTime);
        }

        //G TESTED
        else if (activeCamValue >= 247.5 && activeCamValue < 292.5)
        {
            //Debug.Log("Side G");
            targetRot = new Vector3(currentRot.x + AD * TILTSPEED * Time.deltaTime, currentRot.y, currentRot.z - WS * TILTSPEED * Time.deltaTime);
        }

        //H TESTED
        else if (activeCamValue >= 292.5 && activeCamValue < 337.5)
        {
            //Debug.Log("Side H");
            targetRot = new Vector3(currentRot.x - WS * TILTSPEED * Time.deltaTime + AD * TILTSPEED * Time.deltaTime, currentRot.y, currentRot.z - WS * TILTSPEED * Time.deltaTime - AD * TILTSPEED * Time.deltaTime);
        }
        
        currentRot = Vector3.Slerp(currentRot, targetRot, moveTimer / MOVETIMEAMOUNT);
        board.transform.rotation = Quaternion.Euler(currentRot.x, currentRot.y, currentRot.z); 
    }
}


/*//side 1
        if (diffZAbs >= diffXAbs && diffZ > 0)
        {
            //Debug.Log("Side 1");
            board.transform.Rotate(WS * TILTSPEED * Time.deltaTime, 0, 0);
            board.transform.Rotate(0, 0, AD * TILTSPEED * Time.deltaTime);
        }

        //side 2
        if (diffZAbs <= diffXAbs && diffX < 0)
        {
            //Debug.Log("Side 2");
            board.transform.Rotate(0, 0, WS * TILTSPEED * Time.deltaTime);
            board.transform.Rotate(-AD * TILTSPEED * Time.deltaTime, 0, 0);            
        }


        //side 3
        if (diffZAbs >= diffXAbs && diffZ < 0)
        {
            //Debug.Log("Side 3");
            board.transform.Rotate(-WS * TILTSPEED * Time.deltaTime, 0, 0);
            board.transform.Rotate(0, 0, -AD * TILTSPEED * Time.deltaTime);
        }

        //side 4
        if (diffZAbs <= diffXAbs && diffX > 0)
        {
            //Debug.Log("Side 4");
            board.transform.Rotate(0, 0, -WS * TILTSPEED * Time.deltaTime);
            board.transform.Rotate(AD * TILTSPEED * Time.deltaTime, 0, 0);
        }*/
