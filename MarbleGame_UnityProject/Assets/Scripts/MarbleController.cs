using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    [Space]
    [Header("Audio Components")]
    [Space]
    [SerializeField] AudioListener audioListener;
    [SerializeField] AudioSource rollingSource;
    [SerializeField] AudioSource impactSource;

    [Space]
    [Header("Clips")]
    [Space]
    [Space]
    [Header("Rolling")]
    [SerializeField] AudioClip rollingWood;
    [Space]
    [Header("Impacts")]
    [SerializeField] AudioClip tempClip;
    [SerializeField] List<AudioClip> wallWoodImpacts;
    [SerializeField] List<AudioClip> wallGlassImpacts;

    [Space]
    [Header("Tags")]
    [SerializeField] string WALLWOODTAG;
    [SerializeField] string WALLGLASSTAG;
    [SerializeField] string FLOORWOODTAG;
    [SerializeField] string FLOORGLASSTAG;



    //impact sounds
    private void OnCollisionEnter(Collision collision)
    {
        //wood wall
        if(collision.gameObject.tag == WALLWOODTAG)
        {
            Debug.Log("wood impact");
        }


        //glass wall
        if (collision.gameObject.tag == WALLWOODTAG)
        {

        }

    }
}
