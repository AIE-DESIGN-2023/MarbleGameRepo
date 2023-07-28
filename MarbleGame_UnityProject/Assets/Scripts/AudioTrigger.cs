using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public Collider Collider;


    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Marble")
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
