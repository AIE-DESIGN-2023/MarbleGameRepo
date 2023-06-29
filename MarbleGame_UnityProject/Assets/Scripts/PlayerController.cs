using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("GameObjects")]
    [Space]

    [SerializeField] GameObject board;
    [SerializeField] GameObject ball;

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
            //x positive
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("X POSITIVE");
                board.transform.Rotate(TILTSPEED * Time.deltaTime, 0, 0);
/*
                //clamp x Rotation
                if (board.transform.rotation.eulerAngles.x > -MAXTILT && board.transform.rotation.eulerAngles.x < MAXTILT)
                {
                
                }                */
            }

            //x negative
            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("X NEGATIVE");
                board.transform.Rotate(-TILTSPEED * Time.deltaTime, 0, 0);

                /*//clamp x rotation
                if (board.transform.rotation.eulerAngles.x > -MAXTILT && board.transform.rotation.eulerAngles.x < MAXTILT)
                {
                    
                }*/
            }

            //z positive
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("Z POSITIVE");
                board.transform.Rotate(0, 0, TILTSPEED * Time.deltaTime);

                /*//clamp z Rotation
                if (board.transform.rotation.eulerAngles.z > -MAXTILT && board.transform.rotation.eulerAngles.z < MAXTILT)
                {
                    
                }*/
            }

            //z negative
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("Z NEGATIVE");
                board.transform.Rotate(0, 0, -TILTSPEED * Time.deltaTime);
                
                /*//clamp z rotation
                if (board.transform.rotation.eulerAngles.z > -MAXTILT && board.transform.rotation.eulerAngles.z < MAXTILT)
                {
                    
                }*/
            }

            if(Input.GetKey(KeyCode.LeftArrow))
            {
                board.transform.Rotate(0, TILTSPEED * Time.deltaTime, 0);
            }


            if (Input.GetKey(KeyCode.RightArrow))
            {
                board.transform.Rotate(0, TILTSPEED * Time.deltaTime, 0);
            }
        }
    }
}
