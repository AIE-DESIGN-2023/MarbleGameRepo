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
    [Space]
    [SerializeField] List<AudioClip> wallWoodImpactsSoft;
    [SerializeField] List<AudioClip> wallWoodImpactsMedium;
    [SerializeField] List<AudioClip> wallWoodImpactsHard;
    [SerializeField] List<AudioClip> wallGlassImpactsSoft;
    [SerializeField] List<AudioClip> wallGlassImpactsMedium;
    [SerializeField] List<AudioClip> wallGlassImpactsHard;


    [Space]
    [Header("Tags")]
    [SerializeField] string WALLWOODTAG;
    [SerializeField] string WALLGLASSTAG;
    [SerializeField] string FLOORWOODTAG;
    [SerializeField] string FLOORGLASSTAG;

    [Space]
    [Header("Marble Variables")]
    [SerializeField] float marbleMagnitude;
    [SerializeField] float maxMagnitude;

    //update
    private void Update()
    {
        //get marble magnitude
        marbleMagnitude = Mathf.Abs(GetComponent<Rigidbody>().velocity.x) + Mathf.Abs(GetComponent<Rigidbody>().velocity.y) + Mathf.Abs(GetComponent<Rigidbody>().velocity.z);
        
        //clamp marble magnitude
        marbleMagnitude = Mathf.Clamp(marbleMagnitude, 0, maxMagnitude);

        //divide marble magnitude by max
        marbleMagnitude = marbleMagnitude / maxMagnitude;

        //apply marble magnitude
        rollingSource.volume = marbleMagnitude;
    }

    //impact sounds
    private void OnCollisionEnter(Collision collision)
    {
        //wood wall
        if(collision.gameObject.tag == WALLWOODTAG)
        {
            //
        }


        //glass wall
        if (collision.gameObject.tag == WALLWOODTAG)
        {

        }

    }
}
