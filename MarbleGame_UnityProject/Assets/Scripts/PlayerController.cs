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
    [SerializeField] GameObject ball;
    [SerializeField] GameObject[] sides;

    [Space]
    [Header("Variables")]
    [Space]

    [SerializeField] bool playerActive;
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
    }

    private void TiltBoard(GameObject nearestSide)
    {
        if(nearestSide.tag == "Side (1)")
        {
            //x positive
            if (Input.GetKey(KeyCode.W))
            {
                board.transform.Rotate(-TILTSPEED * Time.deltaTime, 0, 0);
            }
            //x negative
            if (Input.GetKey(KeyCode.S))
            {
                board.transform.Rotate(TILTSPEED * Time.deltaTime, 0, 0);
            }
            //z positive
            if (Input.GetKey(KeyCode.A))
            {
                board.transform.Rotate(0, 0, -TILTSPEED * Time.deltaTime);
            }
            //z negative
            if (Input.GetKey(KeyCode.D))
            {
                board.transform.Rotate(0, 0, TILTSPEED * Time.deltaTime);
            }
        }

        //side 2
        if (nearestSide.tag == "Side (2)")
        {
            //x positive
            if (Input.GetKey(KeyCode.W))
            {
                board.transform.Rotate(0, 0, -TILTSPEED * Time.deltaTime);
            }
            //x negative
            if (Input.GetKey(KeyCode.S))
            {
                board.transform.Rotate(0, 0, TILTSPEED * Time.deltaTime);
            }
            //z positive
            if (Input.GetKey(KeyCode.A))
            {
                board.transform.Rotate(TILTSPEED * Time.deltaTime, 0, 0);
            }
            //z negative
            if (Input.GetKey(KeyCode.D))
            {
                board.transform.Rotate(-TILTSPEED * Time.deltaTime, 0, 0);
            }
        }


        //side 3
        if (nearestSide.tag == "Side (3)")
        {
            //x positive
            if (Input.GetKey(KeyCode.W))
            {
                board.transform.Rotate(TILTSPEED * Time.deltaTime, 0, 0);
            }
            //x negative
            if (Input.GetKey(KeyCode.S))
            {
                board.transform.Rotate(-TILTSPEED * Time.deltaTime, 0, 0);
            }
            //z positive
            if (Input.GetKey(KeyCode.A))
            {
                board.transform.Rotate(0, 0, TILTSPEED * Time.deltaTime);
            }
            //z negative
            if (Input.GetKey(KeyCode.D))
            {
                board.transform.Rotate(0, 0, -TILTSPEED * Time.deltaTime);
            }
        }

        //side 4
        if (nearestSide.tag == "Side (4)")
        {
            //x positive
            if (Input.GetKey(KeyCode.W))
            {
                board.transform.Rotate(0, 0, TILTSPEED * Time.deltaTime);
            }
            //x negative
            if (Input.GetKey(KeyCode.S))
            {
                board.transform.Rotate(0, 0, -TILTSPEED * Time.deltaTime);
            }
            //z positive
            if (Input.GetKey(KeyCode.A))
            {
                board.transform.Rotate(-TILTSPEED * Time.deltaTime, 0, 0);
            }
            //z negative
            if (Input.GetKey(KeyCode.D))
            {
                board.transform.Rotate(TILTSPEED * Time.deltaTime, 0, 0);
            }
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

        Debug.Log(output);
        return output;
    }
}
