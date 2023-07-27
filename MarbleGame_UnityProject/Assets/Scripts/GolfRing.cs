using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfRing : MonoBehaviour
{

    public GameObject particleEffect;

    

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Marble")
        {
            particleEffect.SetActive(true);
        }
    }
}
