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
    [Space]
    [SerializeField] public GameObject transistionUI;
    [SerializeField] public float transitionX_Current;
    [SerializeField] public float transitionX_Enter;
    [SerializeField] public float transitionX_Middle;
    [SerializeField] public float transitionX_Exit;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController)
        {
            playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
        }


        if (reachedBeatenLevelHitbox)
        {
            reachedBeatenLevelHitbox = false;
            StartCoroutine("LevelTransition");
        }
    }

    public IEnumerator LevelTransition()
    {
        //remove player control
        playerController.playerActive = false;




        //load new level
        SceneManager.LoadScene(sceneIndex++);

        //move marble to new prespawn location


        //reset marble spin and rotation


        //unfreeze marble


        yield return new WaitForSeconds(1f);
    }
}
