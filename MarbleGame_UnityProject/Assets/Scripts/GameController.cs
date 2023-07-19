using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Space]
    [Header("Gameobjects")]
    [Space]
    [SerializeField] PlayerController playerController;

    [Space]
    [Header("Level Variables")]
    [Space]
    [SerializeField] public bool hasBeatenLevel;
    [SerializeField] public bool reachedBeatenLevelHitbox;
    [SerializeField] public int sceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (reachedBeatenLevelHitbox)
        {
            
            StartCoroutine("LevelTransition");
        }
    }

    public IEnumerator LevelTransition()
    {
        //remove player control
        playerController.playerActive = false;

        //reset hole cams
        foreach (GameObject holeCam in playerController.holeCams)
        {
            holeCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }

        //freeze marble position
        playerController.marble.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        //load new level
        SceneManager.LoadScene(sceneIndex++);

        //move marble to new prespawn location


        //reset marble spin and rotation


        //unfreeze marble


        yield return new WaitForSeconds(1f);
    }
}
