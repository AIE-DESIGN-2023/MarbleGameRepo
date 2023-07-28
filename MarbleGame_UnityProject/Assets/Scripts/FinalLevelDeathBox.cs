using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalLevelDeathBox : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] string marbleTag;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject virtualCamera;
    [SerializeField] public Animator UIAnimator;

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
            StartCoroutine("EndCredits");
            //playerController.gameController.GetComponent<GameController>().reachedBeatenLevelHitbox = true;
        }
    }

    IEnumerator EndCredits()
    {
        canvas.SetActive(false);
        virtualCamera.GetComponent<CinemachineVirtualCamera>().LookAt = null;
        virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
        UIAnimator.SetTrigger("CreditsTrigger");
        yield return null;
    }


    IEnumerator RestartLevel()
    {
        playerController.gameController.GetComponent<GameController>().UIAnimator.SetTrigger("Death Exit");
        playerController.playerActive = false;

        yield return new WaitForSeconds(0.55f);
        //reset marble position
        playerController.marble.transform.position = playerController.spawnPos.transform.position;
        playerController.marble.GetComponent<Rigidbody>().Sleep();
        playerController.marble.GetComponent<Rigidbody>().WakeUp();

        //reset board rotation
        playerController.currentRot = Vector3.zero;
        playerController.targetRot = Vector3.zero;
        playerController.board.transform.rotation = Quaternion.identity;
        

        yield return new WaitForSeconds(0.25f);

        playerController.gameController.GetComponent<GameController>().UIAnimator.SetTrigger("Death Enter");

        yield return new WaitForSeconds(0.1f);

        playerController.playerActive = true;
    }
}
