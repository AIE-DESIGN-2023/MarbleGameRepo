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
        //bounceDirection *= bounceSpeed;

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision BOuncer / Marble");

        Debug.Log("Bounce Direction: " + bounceDirection);


/*        bounceVector = new Vector3(bounceDirection.x * bounceSpeed, bounceDirection.y * bounceSpeed, bounceDirection.z * bounceSpeed);
        Debug.Log("Bounce Vector: " + bounceVector);*/

        marble.GetComponent<Rigidbody>().AddForce(bounceDirection, ForceMode.Impulse);
    }
}
