using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] GameObject marble;
    [SerializeField] string marbleTag;
    [SerializeField] float bounceSpeed;
    [SerializeField] Vector3 bounceDirection;


    private void Update()
    {
        if (marble == null)
        {
            marble = GameObject.FindGameObjectWithTag(marbleTag);
        }

        bounceDirection = marble.transform.position - transform.position;
        bounceDirection *= bounceSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision BOuncer / Marble");

        Debug.Log("Bounce Direction: " + bounceDirection);

        /*        bounceVector = new Vector3(bounceDirection.x * bounceSpeed, bounceDirection.y * bounceSpeed, bounceDirection.z * bounceSpeed);
                Debug.Log("Bounce Vector: " + bounceVector);*/

        marble.GetComponent<Rigidbody>().AddForce(bounceDirection, ForceMode.Impulse);

        //animation
        if(!GetComponent<Animator>().GetBool("Bounce Bool"))
        {
            GetComponent<Animator>().SetBool("Bounce Bool", true);
            StartCoroutine("BouncerCountdown");
        }       
        
    }

    
    IEnumerator BouncerCountdown()
    {
        yield return new WaitForSeconds(0.15f);
        GetComponent<Animator>().SetBool("Bounce Bool", false);
    }
}
