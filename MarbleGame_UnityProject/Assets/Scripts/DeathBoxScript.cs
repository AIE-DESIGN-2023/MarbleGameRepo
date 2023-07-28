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
        playerController.gameController.GetComponent<GameController>().UIAnimator.SetTrigger("Death Exit");
        playerController.playerActive = false;

        yield return new WaitForSeconds(0.55f);
        //reset board rotation
        playerController.currentRot = Vector3.zero;
        playerController.targetRot = Vector3.zero;
        playerController.board.transform.rotation = Quaternion.identity;

        yield return new WaitForSeconds(0.15f);

        //reset marble position
        playerController.marble.transform.position = playerController.spawnPos.transform.position;
        playerController.marble.GetComponent<Rigidbody>().Sleep();
        playerController.marble.GetComponent<Rigidbody>().WakeUp();

        yield return new WaitForSeconds(0.75f);

        playerController.gameController.GetComponent<GameController>().UIAnimator.SetTrigger("Death Enter");

        yield return new WaitForSeconds(0.25f);

        playerController.playerActive = true;
    }
}
