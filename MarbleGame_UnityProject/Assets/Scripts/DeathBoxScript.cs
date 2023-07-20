using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBoxScript : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] string marbleTag;

    private void OnTriggerEnter(Collider other)
    {
        if (!playerController.gameController.GetComponent<GameController>().hasBeatenLevel)
        {
            if (other.tag == marbleTag)
            {
                Debug.Log("Respawn ball");
                Respawn();

            }
        }
        else
        {
            playerController.gameController.GetComponent<GameController>().reachedBeatenLevelHitbox = true;
        }
    }

    private void Respawn()
    {
        //reset marble position
        playerController.marble.transform.position = playerController.spawnPos.transform.position;
        playerController.marble.GetComponent<Rigidbody>().Sleep();
        playerController.marble.GetComponent<Rigidbody>().WakeUp();

        //reset board rotation
        playerController.board.transform.rotation = Quaternion.identity;
        playerController.currentRot = new Vector3(0f, 0f, 0f);
        playerController.targetRot = new Vector3(0f, 0f, 0f);
    }
}
