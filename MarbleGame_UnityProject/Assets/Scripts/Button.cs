using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public GameObject gate;
    public Material buttonActiveColour;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Marble")
        {
            GetComponent<Renderer>().material = buttonActiveColour;
            gate.gameObject.SetActive(false);
        }
    }
}
