using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [SerializeField] bool isGrounded = false;
    [SerializeField] bool hasGroundImpactPlayed = false;
    [SerializeField] float groundImpactTimer;
    [SerializeField] float groundImpactWaitTime;
    [SerializeField] float rollingAudioDecreaseSpeed;
    [SerializeField] AudioClip rollingWood;
    [Space]
    [Header("Impacts")]
    [Space]
    [SerializeField] float softThreshold;
    [SerializeField] float hardThreshold;
    [Space]
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
    [SerializeField] float pitchOffset;
    [SerializeField] float pitchVariation;
    [SerializeField] float pitchMaxVariation;

    private void Start()
    {
        rollingSource.clip = rollingWood;
    }

    //update
    private void Update()
    {
        VolumneCalculator();

        if (isGrounded)
        {
            //apply marble magnitude
            rollingSource.volume = marbleMagnitude;
            rollingSource.pitch = pitchVariation+pitchOffset;

            //floor impact
            if(!hasGroundImpactPlayed)
            {
                hasGroundImpactPlayed = true;

                GetWoodImpactClip();

                //set volume
                impactSource.volume = 1;
                //play clip
                impactSource.Play();
            }
        }
        else
        {
            rollingSource.volume -= Time.deltaTime * rollingAudioDecreaseSpeed;
            groundImpactTimer -= Time.deltaTime;
        }

        if(groundImpactTimer < 0 && !isGrounded)
        {
            hasGroundImpactPlayed = false;
        }
    }

    private void GetWoodImpactClip()
    {
        if (marbleMagnitude > hardThreshold)
        {
            Debug.Log("PLAY HARD WOOD WALL SOUND");
            impactSource.clip = wallWoodImpactsHard[Random.Range(0, wallWoodImpactsHard.Count)];
        }
        else if (marbleMagnitude > softThreshold && marbleMagnitude < hardThreshold)
        {
            Debug.Log("PLAY MEDIUM WOOD WALL SOUND");
            impactSource.clip = wallWoodImpactsMedium[Random.Range(0, wallWoodImpactsMedium.Count)];
        }
        else if (marbleMagnitude < softThreshold)
        {
            Debug.Log("Play soft sound");
            impactSource.clip = wallWoodImpactsSoft[Random.Range(0, wallWoodImpactsSoft.Count)];
        }
    }

    //on collision enter
    private void OnCollisionEnter(Collision collision)
    {
        //wood wall
        if(collision.gameObject.tag == WALLWOODTAG)
        {
            Debug.Log("WALL WOOD TAG COLLISION ENTER");
            GetWoodImpactClip();
            PlayImpact();
        }


        //glass wall
        if (collision.gameObject.tag == WALLWOODTAG)
        {

        }

        //floor wood
        if (collision.gameObject.tag == FLOORWOODTAG)
        {
            Debug.Log("FLOOR WOOD TAG ENTER");
            isGrounded = true;
        }
    }

    private void PlayImpact()
    {
        //set volume
        impactSource.volume = marbleMagnitude;
        //play clip
        impactSource.Play();
    }

    private void VolumneCalculator()
    {
        //get marble magnitude
        marbleMagnitude = Mathf.Abs(GetComponent<Rigidbody>().velocity.x) + Mathf.Abs(GetComponent<Rigidbody>().velocity.y) + Mathf.Abs(GetComponent<Rigidbody>().velocity.z);

        //clamp marble magnitude
        marbleMagnitude = Mathf.Clamp(marbleMagnitude, 0, maxMagnitude);

        //divide marble magnitude by max
        marbleMagnitude = marbleMagnitude / maxMagnitude;

        //calculate pitch variation
        pitchVariation = (marbleMagnitude - 0.5f);
    }

    private void OnCollisionExit(Collision collision)
    {
        //wood floor
        if (collision.gameObject.tag == FLOORWOODTAG)
        {
            Debug.Log("FLOOR WOOD TAG EXIT");
            isGrounded = false;
            groundImpactTimer = groundImpactWaitTime;
        }
    }
}
