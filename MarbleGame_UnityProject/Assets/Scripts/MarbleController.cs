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
    [SerializeField] float rollingAudioDecreaseSpeed;
    [SerializeField] AudioClip rollingWood;
    [SerializeField] bool isGrounded = false;
    
    [Space]
    [Header("Impacts")]
    [Space]
    [SerializeField] float softThreshold;
    [SerializeField] float hardThreshold;
    [SerializeField] bool hasGroundImpactPlayed = false;
    [SerializeField] float groundImpactTimer;
    [SerializeField] float groundImpactWaitTime;
    [Space]
    [SerializeField] AudioClip tempClip;
    [Space]
    [SerializeField] List<AudioClip> wallWoodImpacts;
    [SerializeField] List<AudioClip> wallGlassImpacts;


    [Space]
    [Header("Tags")]
    [SerializeField] string WALLWOODTAG;
    [SerializeField] string WALLGLASSTAG;
    [SerializeField] string FLOORWOODTAG;

    [Space]
    [Header("Marble Variables")]
    [SerializeField] float marbleMagnitude;
    [SerializeField] float maxMagnitude;
    [SerializeField] float offsetMagnitude;
    [Space]
    [SerializeField] float pitchOffset;
    [SerializeField] float pitchVariation;
    [SerializeField] float pitchMaxVariation;
    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        rollingSource.clip = rollingWood;
    }

    //update
    private void Update()
    {
        VolumneCalculator();
        GroundedController();
    }


    private void GroundedController()
    {
        if (isGrounded)
        {
            if (!rollingSource.isPlaying)
            {
                rollingSource.Play();
            }

            //apply marble magnitude
            rollingSource.volume = marbleMagnitude - offsetMagnitude;
            //rollingSource.pitch = pitchVariation+pitchOffset;

            //floor impact
            if (!hasGroundImpactPlayed)
            {
                hasGroundImpactPlayed = true;

                impactSource.clip = wallWoodImpacts[Random.Range(0, wallWoodImpacts.Count)];

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

        if (groundImpactTimer < 0 && !isGrounded)
        {
            hasGroundImpactPlayed = false;
        }
    }

    //on collision enter
    private void OnCollisionEnter(Collision collision)
    {
        //wood wall
        if(collision.gameObject.tag == WALLWOODTAG)
        {
            Debug.Log("WALL WOOD TAG COLLISION ENTER");
            impactSource.clip = wallWoodImpacts[Random.Range(0, wallWoodImpacts.Count)];
            PlayImpact();
        }


        //glass wall
        if (collision.gameObject.tag == WALLGLASSTAG)
        {
            impactSource.clip = wallGlassImpacts[Random.Range(0, wallGlassImpacts.Count)];
            PlayImpact();
        }

        //floor wood
        if (collision.gameObject.tag == FLOORWOODTAG)
        {
            Debug.Log("FLOOR WOOD TAG ENTER");
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == FLOORWOODTAG)
        {
            Debug.Log("FLOOR WOOD TAG Stay");
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
