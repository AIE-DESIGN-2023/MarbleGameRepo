using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] GameObject VC1;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActive)
        {
            
            TiltBoard(GetNearestSide());

        }

        WS = Input.GetAxis("WS");
        AD = Input.GetAxis("AD");
    }

    private void TiltBoard(GameObject nearestSide)
    {
        float cameraX = VC1.transform.position.x;
        float cameraZ = VC1.transform.position.z;

        float marbleX = marble.transform.position.x;
        float marbleZ = marble.transform.position.z;

        float diffX = cameraX - marbleX;
        float diffZ = cameraZ - marbleZ;

        float diffXAbs = Mathf.Abs(Mathf.Abs(VC1.transform.position.x) - Mathf.Abs(marble.transform.position.x));
        float diffZAbs = Mathf.Abs(Mathf.Abs(VC1.transform.position.z) - Mathf.Abs(marble.transform.position.z));

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

    private GameObject GetNearestSide()
    {
        float distance = 0;
        float smallestDistance = 100f;
        GameObject output = null;

        foreach(GameObject side in sides)
        {
            distance = Vector3.Distance(side.transform.position, VC1.transform.position);
            if(distance < smallestDistance)
            {
                smallestDistance = distance;
                output = side;
            }
        }

        //Debug.Log(output);
        return output;
    }
}
