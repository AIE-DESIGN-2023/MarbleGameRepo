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
                StartCoroutine("RestartLevel");
            }
        }
        else
        {
            playerController.gameController.GetComponent<GameController>().reachedBeatenLevelHitbox = true;
        }
    }


    IEnumerator RestartLevel()
    {
        playerController.gameController.GetComponent<GameController>().UIAnimator.SetTrigger("Transition Exit");

        yield return new WaitForSeconds(3f);
        //reset marble position
        playerController.marble.transform.position = playerController.spawnPos.transform.position;
        playerController.marble.GetComponent<Rigidbody>().Sleep();
        playerController.marble.GetComponent<Rigidbody>().WakeUp();

        //reset board rotation
        playerController.board.transform.rotation = Quaternion.identity;
        playerController.currentRot = Vector3.zero;
        playerController.targetRot = Vector3.zero;


        playerController.gameController.GetComponent<GameController>().UIAnimator.SetTrigger("Transition Enter");
    }
}
