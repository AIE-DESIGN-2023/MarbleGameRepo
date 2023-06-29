using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxScript : MonoBehaviour
{
    [SerializeField] GameController gameController;

    [SerializeField] GameObject marble;
    [SerializeField] GameObject board;

    [SerializeField] string marbleTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == marbleTag)
        {
            //reset marble position
            marble.transform.position = gameController.spawnPosition.transform.position;
            marble.GetComponent<Rigidbody>().velocity = Vector3.zero;

            //reset board rotation
            board.transform.rotation = Quaternion.identity;
        }
    }
}
